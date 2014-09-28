using Orcamento.Domain.DB.Repositorio;
using Orcamento.Domain.Entities.Monitoramento;
using Orcamento.Domain.Gerenciamento;
using Orcamento.Domain.Robo.Monitoramento.EspecificacaoDeValidacaoDeCarga.EspecificacaoTicketsUnitariosDeProducao;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace Orcamento.Domain.Robo.Monitoramento.EstrategiasDeCargas
{
    public class TicketDeProducaoExcel
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
        public TicketDeProducao Ticket { get; set; }
        public TicketParcela TicketParcela { get; set; }
    }

    public class ProcessaTicketsDeProducao : ProcessaCarga
    {
        private List<TicketDeProducao> tickets;
        private Processo processo;
        private List<TicketDeProducaoExcel> ticketsDeProducaoExcel;
        private MotorDeValidacaoDeTicketDeProducao motor;

        public override void Processar(Carga carga, bool salvar = false)
        {
            try
            {
                this.carga = carga;
                ticketsDeProducaoExcel = new List<TicketDeProducaoExcel>();

                LerExcel(ticketsDeProducaoExcel, carga);

                if (NenhumRegistroEncontrado(carga))
                    return;

                ValidarCarga();

                if (CargaContemErros())
                    return;

                ProcessarTicketsDeProducao();

                if (CargaContemErros())
                    return;

                SalvarAlteracoes(salvar);
            }
            catch (Exception ex)
            {
                carga.AdicionarDetalhe("Erro ao processar Tickets de produção", "Ocorreu um erro ao tentar processar os tickets de producão.", 0, TipoDetalheEnum.erroDeProcesso, ex.Message);
            }
        }

        private void ValidarCarga()
        {
            motor = new MotorDeValidacaoDeTicketDeProducao(carga, ticketsDeProducaoExcel);
            motor.Validar();
        }

        private bool NenhumRegistroEncontrado(Carga carga)
        {
            if (ticketsDeProducaoExcel.Count == 0)
            {
                carga.AdicionarDetalhe("Nenhum registro foi obtido", "Nenhum registro foi obtido por favor verifique o excel.",
                                       0, TipoDetalheEnum.erroLeituraExcel);
                return true;
            }
            return false;
        }

        private void ProcessarTicketsDeProducao()
        {
            var departamentosExcel = ticketsDeProducaoExcel.Select(x => x.Departamento).Distinct();
            tickets = new List<TicketDeProducao>();

            foreach (var hospital in motor.Departamentos)
            {
                var ticketsDeHospital = motor.Tickets.Where(h => h.Hospital.Nome == hospital.Nome).ToList();

                ProcessaTicket(hospital,  ticketsDeHospital);

                if (ticketsDeHospital != null && ticketsDeHospital.Count > 0)
                    tickets.AddRange(ticketsDeHospital);
            }
        }

        private void ProcessaTicket(Departamento hospital, List<TicketDeProducao> tickets)
        {
            try
            {
                var registros = ticketsDeProducaoExcel.Where(d => d.Departamento == hospital.Nome).ToList();

                foreach (var registro in registros)
                {
                    InformarValor(registro, tickets);
                }
            }
            catch (Exception)
            {
                this.carga.AdicionarDetalhe("Nao foi possivel processar Hospital", "Nao foi possivel processar tickets do Hospital: " + hospital.Nome, 0, TipoDetalheEnum.erroDeProcesso);

            }
        }

        private void InformarValor(TicketDeProducaoExcel registro, List<TicketDeProducao> tickets)
        {
            try
            {
                var ticket = tickets.FirstOrDefault(
                    t =>
                    t.Hospital.Nome == registro.Departamento && t.Setor.NomeSetor == registro.setor &&
                    t.SubSetor.NomeSetor == registro.subSetor);

                var parcela = ticket.Parcelas.FirstOrDefault(t => registro.mes ==(int) t.Mes);

                parcela.Valor = registro.valor;
            }
            catch (Exception)
            {
                this.carga.AdicionarDetalhe("Nao foi possivel processar Hospital", "Parcela do mes : " + registro.mes + " nao encontrada.", registro.Linha, TipoDetalheEnum.erroDeProcesso);
            }

        }



        private void LerExcel(List<TicketDeProducaoExcel> ticketsDeProducaoExcel, Carga carga)
        {
            try
            {
                processo = new Processo();
                var reader = processo.InicializarCarga(carga);

                if (reader == null)
                    carga.AdicionarDetalhe("Nao foi possivel Ler o excel", "Nao foi possivel Ler o excel por favor verifique o layout.", 0, TipoDetalheEnum.erroLeituraExcel);
                else
                    LerExcel(ticketsDeProducaoExcel, carga, reader);
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

        private void LerExcel(List<TicketDeProducaoExcel> ticketsDeProducaoExcel, Carga carga,
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


                        var ticketDeProducaoExcel = new TicketDeProducaoExcel();
                        ticketDeProducaoExcel.Departamento = reader[0].ToString();
                        ticketDeProducaoExcel.setor = reader[1].ToString();
                        ticketDeProducaoExcel.subSetor = reader[2].ToString();
                        ticketDeProducaoExcel.mes = Convert.ToInt32(reader[3]);
                        ticketDeProducaoExcel.valor = Convert.ToInt32(reader[4]);


                        ticketDeProducaoExcel.Linha = i + 1;

                        ticketsDeProducaoExcel.Add(ticketDeProducaoExcel);
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
            TicketsDeProducao repositorio = new TicketsDeProducao();
            repositorio.SalvarLista(tickets);

            carga.AdicionarDetalhe("Carga realizada com sucesso.",
                                     "Carga de tickets unitários de produção nome: " + carga.NomeArquivo + " .", 0, TipoDetalheEnum.sucesso);
        }
    }
}
