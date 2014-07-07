using Orcamento.Domain.DB.Repositorio;
using Orcamento.Domain.Entities.Monitoramento;
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
    }

    public class ProcessaTicketsDeProducao : IProcessaCarga
    {
        private Carga carga;
        private List<TicketDeProducao> Tickets { get; set; }

        public void Processar(Carga carga)
        {
            this.carga = carga;
            var ticketsDeProducaoExcel = new List<TicketDeProducaoExcel>();
            var tickets = new TicketsDeProducao();

            LerExcel(ticketsDeProducaoExcel, carga);

            if (ticketsDeProducaoExcel.Count == 0)
            {
                carga.AdicionarDetalhe("Nenhum registro foi obtido", "Nenhum registro foi obtido por favor verifique o excel.", 0, TipoDetalheEnum.erroLeituraExcel);
                return;
            }

            ProcessarTicketsDeProducao(ticketsDeProducaoExcel);

            try
            {
                if (carga.Ok())
                    tickets.SalvarLista(Tickets);
            }
            catch (Exception)
            {
                carga.AdicionarDetalhe("Erro ao Salvar Tickets de producao", "Ocorreu um erro ao tentar salvar os tickets de producao.", 0, TipoDetalheEnum.erroDeProcesso);
            }

        }

        private void ProcessarTicketsDeProducao(List<TicketDeProducaoExcel> ticketsDeProducaoExcel)
        {
            var departamentosExcel = ticketsDeProducaoExcel.Select(x => x.Departamento).Distinct();
            var repositorioDepartamentos = new Hospitais();
            var hospitais = repositorioDepartamentos.Todos();

            Tickets = new List<TicketDeProducao>();

            foreach (var hospital in hospitais)
            {
                TicketsDeProducao ticketsDeProducao = new TicketsDeProducao();
                var ticketsDeHospital = ticketsDeProducao.Todos(hospital).ToList();

                ProcessaTicket(ticketsDeProducaoExcel, hospital, ticketsDeHospital);

                if (ticketsDeHospital != null && ticketsDeHospital.Count > 0)
                    Tickets.AddRange(ticketsDeHospital);
            }

        }

        private void ProcessaTicket(List<TicketDeProducaoExcel> ticketsDeProducaoExcel, Hospital hospital, List<TicketDeProducao> ticketsDeHospital)
        {
            try
            {
                var tickets = new TicketsDeProducao();

                var registros = ticketsDeProducaoExcel.Where(d => d.Departamento == hospital.Nome).ToList();


                foreach (var registro in registros)
                {
                    if (ticketsDeHospital == null || ticketsDeHospital.Count == 0 || ticketsDeHospital.Count(t => t.Setor.NomeSetor == registro.setor && t.SubSetor.NomeSetor == registro.subSetor) == 0)
                    {
                        carga.AdicionarDetalhe("Nao existem tickets de producao", "Nao existem tickets de producao para estes dados informados no hospital: " + hospital.Nome, registro.Linha, TipoDetalheEnum.erroDeProcesso);
                        continue;
                    }

                    var parcelas = ObterParcelas(ticketsDeHospital, registro);

                    if (parcelas == null)
                        continue;

                    InformarValor(parcelas, registro);
                }
            }
            catch (Exception)
            {
                this.carga.AdicionarDetalhe("Nao foi possivel processar Hospital", "Nao foi possivel processar tickets do Hospital: " + hospital.Nome, 0, TipoDetalheEnum.erroDeProcesso);

            }
        }

        private void InformarValor(IList<TicketParcela> parcelas, TicketDeProducaoExcel registro)
        {
            parcelas.Single(p => p.Mes == (MesEnum)registro.mes).Valor = registro.valor;
        }

        private IList<TicketParcela> ObterParcelas(List<TicketDeProducao> ticketsDeHospital, TicketDeProducaoExcel registro)
        {
            try
            {
                var parcelas = ticketsDeHospital.Single(
                t => t.Setor.NomeSetor == registro.setor && t.SubSetor.NomeSetor == registro.subSetor).Parcelas;

                return parcelas;
            }
            catch (Exception)
            {
                carga.AdicionarDetalhe("Erro ao processar", "Ocorreu um erro ao tentar obter as parcelas dos tickets de Receita no hospital: " + registro.Departamento + " podem ter mais de um conjunto de parcelas associadas com o setor e subsetor informado", registro.Linha, TipoDetalheEnum.erroDeProcesso);
                return null;
            }

        }

        private OleDbDataReader InicializarCarga(Carga carga)
        {
            try
            {
                string _conectionstring;
                _conectionstring = @"Provider=Microsoft.ACE.OLEDB.12.0;";
                _conectionstring += String.Format("Data Source={0};", carga.Diretorio);
                _conectionstring += "Extended Properties='Excel 8.0;HDR=NO;'";

                var cn = new OleDbConnection(_conectionstring);
                var cmd = new OleDbCommand("Select * from [carga$]", cn);
                cn.Open();

                var reader = cmd.ExecuteReader();
                return reader;
            }
            catch (Exception)
            {
                carga.AdicionarDetalhe("Erro na leitura", "Nao foi possivel ler o excel, por favor verifque se o layout esta correto (colunas, valores, nome da aba(carga) )", 0,
                                          TipoDetalheEnum.erroLeituraExcel);
                return null;
            }
        }
        private void LerExcel(List<TicketDeProducaoExcel> ticketsDeProducaoExcel, Carga carga)
        {
            var reader = InicializarCarga(carga);

            if (reader == null)
                carga.AdicionarDetalhe("Nao foi possivel Ler o excel", "Nao foi possivel Ler o excel por favor verifique o layout.", 0, TipoDetalheEnum.erroLeituraExcel);
            else
                LerExcel(ticketsDeProducaoExcel, carga, reader);

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


                        var ticketDeProducaoExcel = new TicketDeProducaoExcel
                                                        {
                                                            Departamento = reader[0].ToString(),
                                                            setor = reader[1].ToString(),
                                                            subSetor = reader[2].ToString(),
                                                            mes = Convert.ToInt32(reader[4]),
                                                            valor = Convert.ToInt32(reader[5])
                                                        };

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
    }
}
