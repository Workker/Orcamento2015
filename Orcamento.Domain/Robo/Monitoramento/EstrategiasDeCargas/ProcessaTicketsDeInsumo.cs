using Orcamento.Domain.ComponentesDeOrcamento.OrcamentoDeProducao;
using Orcamento.Domain.DB.Repositorio;
using Orcamento.Domain.Gerenciamento;
using Orcamento.Domain.Robo.Monitoramento;
using Orcamento.Domain.Robo.Monitoramento.EspecificacaoDeValidacaoDeCarga.EspecificacaoTicketsDeInsumo;
using Orcamento.Domain.Robo.Monitoramento.EstruturaOrcamentaria;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace Orcamento.Domain.Entities.Monitoramento
{

    public class TicketDeInsumoExcel
    {
        public string Departamento { get; set; }
        public string setor { get; set; }
        public string subSetor { get; set; }
        public int mes { get; set; }
        public int valor { get; set; }
        public int Linha { get; set; }

        public Departamento DepartamentoEntidade { get; set; }
        public SetorHospitalar SetorHospitalar { get; set; }
        public SubSetorHospital SubSetorHospital { get; set; }
        public Insumo Insumo { get; set; }
        public CustoUnitario CustoUnitario { get; set; }
        public ProducaoHospitalar ProducaoHospitalar { get; set; }

    }

    public class ProcessaTicketsDeInsumo : ProcessaCarga
    {

        private List<CustoUnitario> custosUnitarios;
        private Domain.Robo.Monitoramento.Processo processo;
        private List<TicketDeInsumoExcel> ticketsDeInsumoExcel;
        private MotorDeValidacaoTicketDeInsumo motor;

        public override void Processar(Carga carga, bool salvar = false)
        {
            try
            {
                this.carga = carga;
                ticketsDeInsumoExcel = new List<TicketDeInsumoExcel>();

                LerExcel(ticketsDeInsumoExcel, carga);

                if (NenhumRegistroEncontrado(carga))
                    return;

                ValidarCarga();

                if (CargaContemErros())
                    return;

                SalvarAlteracoes(salvar);
            }
            catch (Exception ex)
            {
                carga.AdicionarDetalhe("Erro ao processar Tickets de insumo", "Ocorreu um erro ao tentar processar os tickets de insumo.", 0, TipoDetalheEnum.erroDeProcesso, ex.Message);
            }
        }

        private void ValidarCarga()
        {
            motor = new MotorDeValidacaoTicketDeInsumo(carga, ticketsDeInsumoExcel);
            motor.Validar();
        }

        private bool NenhumRegistroEncontrado(Carga carga)
        {
            if (ticketsDeInsumoExcel.Count == 0)
            {
                carga.AdicionarDetalhe("Nenhum registro foi obtido", "Nenhum registro foi obtido por favor verifique o excel.",
                                       0, TipoDetalheEnum.erroLeituraExcel);
                return true;
            }
            return false;
        }

        private void ProcessarCustoUnitario()
        {
            var departamentosExcel = ticketsDeInsumoExcel.Select(x => x.Departamento).Distinct();
            custosUnitarios = new List<CustoUnitario>();

            foreach (var departamento in departamentosExcel)
            {
                var Insumo =
                    motor.Insumos.FirstOrDefault(h => h.Departamento.Nome == departamento);

                ProcessaTicket(Insumo.Departamento, Insumo.CustosUnitarios.ToList());

                if (Insumo.CustosUnitarios != null && Insumo.CustosUnitarios.Count > 0)
                    custosUnitarios.AddRange(Insumo.CustosUnitarios);
            }
        }

        private void ProcessaTicket(Departamento hospital, List<CustoUnitario> custosUnitarios)
        {
            try
            {
                var registros = ticketsDeInsumoExcel.Where(d => d.Departamento == hospital.Nome).ToList();

                foreach (var registro in registros)
                {
                    InformarValor(registro, custosUnitarios);
                }
            }
            catch (Exception)
            {
                this.carga.AdicionarDetalhe("Nao foi possivel processar Hospital", "Nao foi possivel processar tickets do Hospital: " + hospital.Nome, 0, TipoDetalheEnum.erroDeProcesso);

            }
        }

        private void InformarValor(TicketDeInsumoExcel registro, List<CustoUnitario> custosUnitarios)
        {
            try
            {
                var ticket = custosUnitarios.FirstOrDefault(
                    t =>
                    t.Setor.NomeSetor == registro.setor &&
                    t.SubSetor.NomeSetor == registro.subSetor);

                var parcela = ticket.Valores.FirstOrDefault(t => registro.mes == (int)t.Mes);

                parcela.Valor = registro.valor;
            }
            catch (Exception)
            {
                this.carga.AdicionarDetalhe("Nao foi possivel processar Hospital", "Parcela do mes : " + registro.mes + " nao encontrada.", registro.Linha, TipoDetalheEnum.erroDeProcesso);
            }

        }



        private void LerExcel(List<TicketDeInsumoExcel> ticketsDeInsumoExcel, Carga carga)
        {
            try
            {
                processo = new Domain.Robo.Monitoramento.Processo();
                var reader = processo.InicializarCarga(carga);

                if (reader == null)
                    carga.AdicionarDetalhe("Nao foi possivel Ler o excel", "Nao foi possivel Ler o excel por favor verifique o layout.", 0, TipoDetalheEnum.erroLeituraExcel);
                else
                    LerExcel(ticketsDeInsumoExcel, carga, reader);
            }
            catch (Exception ex)
            {
                carga.AdicionarDetalhe("Nao foi possivel Ler o excel", "Nao foi possivel Ler o excel por favor verifique o layout.", 0, TipoDetalheEnum.erroLeituraExcel, ex.Message);
            }
            finally
            {
                processo.FinalizarCarga();
            }
        }

        private void LerExcel(List<TicketDeInsumoExcel> ticketsDeProducaoExcel, Carga carga,
                                     OleDbDataReader reader)
        {
            int i = 0;
            while (reader.Read())
            {
                try
                {
                    if (i > 0)
                    {
                        if (reader[0] == DBNull.Value || string.IsNullOrEmpty(reader[0].ToString()))
                            break;


                        var ticketDeInsumo = new TicketDeInsumoExcel();
                        ticketDeInsumo.Departamento = reader[0].ToString();
                        ticketDeInsumo.setor = reader[1].ToString();
                        ticketDeInsumo.subSetor = reader[2].ToString();
                        ticketDeInsumo.mes = Convert.ToInt32(reader[3]);
                        ticketDeInsumo.valor = Convert.ToInt32(reader[4]);


                        ticketDeInsumo.Linha = i + 1;

                        ticketsDeProducaoExcel.Add(ticketDeInsumo);
                    }
                }
                catch (Exception ex)
                {
                    carga.AdicionarDetalhe("Erro na linha", "Ocorreu um erro ao tentar ler a linha do excel", i + 1,
                                           TipoDetalheEnum.erroLeituraExcel);
                }
                finally
                {
                    i++;
                }
            }
        }

        internal override void SalvarDados()
        {
            ProcessarCustoUnitario();
            CustosUnitarios repositorio = new CustosUnitarios();
            repositorio.SalvarLista(custosUnitarios);

            carga.AdicionarDetalhe("Carga realizada com sucesso.",
                                     "Carga de tickets de insumo nome: " + carga.NomeArquivo + " .", 0, TipoDetalheEnum.sucesso);
        }
    }

}
