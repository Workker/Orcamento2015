using Orcamento.Domain.Entities.Monitoramento;
using Orcamento.Domain.Gerenciamento;
using Orcamento.Domain.Robo.Monitoramento.EspecificacaoDeValidacaoDeCarga.EspecificacaoEstruturaOrcamentaria;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace Orcamento.Domain.Robo.Monitoramento.EstrategiasDeCargas
{
    public class EstruturaOrcamentaria : ProcessaCarga
    {
        private MotorDeValidacaoDeEstruturaOrcamentaria motor;
        private List<EstruturaOrcamentariaExcel> estruturaOrcamentariaExcel;

        // TODO: implementar um serviço de dominio para criar as entidades
        public override void Processar(Carga carga, bool salvar = false)
        {
            try
            {
                this.carga = carga;
                estruturaOrcamentariaExcel = new List<EstruturaOrcamentariaExcel>();

                LerExcel();

                if (NenhumRegistroEncontrado(carga)) return;

                ValidarCarga();

                if (CargaContemErros()) return;

                // TODO: implementar
                ProcessarEstruturaOrcamentaria();

                if (CargaContemErros()) return;

                // TODO: implementar
                SalvarAlteracoes(salvar);
            }
            catch (Exception ex)
            {
                carga.AdicionarDetalhe("Erro ao processar Estrutura Orçamentária", "Ocorreu um erro ao tentar processar a estrutura orçamentária.", 0, TipoDetalheEnum.erroDeProcesso, ex.Message);
            }
        }

        private void ValidarCarga()
        {
            motor = new MotorDeValidacaoDeEstruturaOrcamentaria(carga, estruturaOrcamentariaExcel);
            motor.Validar();
        }

        private bool NenhumRegistroEncontrado(Carga carga)
        {
            if (estruturaOrcamentariaExcel.Count == 0)
            {
                carga.AdicionarDetalhe("Nenhum registro foi obtido", "Nenhum registro foi obtido por favor verifique o excel.",
                                       0, TipoDetalheEnum.erroLeituraExcel);
                return true;
            }
            return false;
        }

        private void ProcessarEstruturaOrcamentaria()
        {

        }

        private void LerExcel()
        {
            try
            {
                processo = new Processo();
                var reader = processo.InicializarCarga(carga);

                if (reader == null)
                    carga.AdicionarDetalhe("Nao foi possivel Ler o excel", "Nao foi possivel Ler o excel por favor verifique o layout.", 0, TipoDetalheEnum.erroLeituraExcel);
                else
                    LerExcel(estruturaOrcamentariaExcel, carga, reader);
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

        private void LerExcel(List<EstruturaOrcamentariaExcel> estruturaOrcamentariaExcel, Carga carga,
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

                        var itemEstruturaOrcamentariaExcel = new EstruturaOrcamentariaExcel();
                        itemEstruturaOrcamentariaExcel.Departamento = reader[0].ToString();
                        itemEstruturaOrcamentariaExcel.TipoDepartamento = (TipoDepartamento)int.Parse(reader[1].ToString());
                        itemEstruturaOrcamentariaExcel.TipoAlteracaoDepartamento = (TipoAlteracao)int.Parse(reader[2].ToString());
                        itemEstruturaOrcamentariaExcel.NomeDaConta = reader[3].ToString();
                        itemEstruturaOrcamentariaExcel.CodigoDaConta = reader[4].ToString();
                        itemEstruturaOrcamentariaExcel.TipoAlteracaoConta = (TipoAlteracao)int.Parse(reader[5].ToString());
                        itemEstruturaOrcamentariaExcel.NomeCentroDeCusto = reader[6].ToString();
                        itemEstruturaOrcamentariaExcel.CodigoCentroDeCusto = reader[7].ToString();
                        itemEstruturaOrcamentariaExcel.TipoAlteracaoCentroDeCusto = (TipoAlteracao)int.Parse(reader[8].ToString());
                        itemEstruturaOrcamentariaExcel.NomeDoGrupoDeConta = reader[9].ToString();
                        itemEstruturaOrcamentariaExcel.TipoAlteracaoGrupoDeConta = (TipoAlteracao)int.Parse(reader[10].ToString());
                        itemEstruturaOrcamentariaExcel.Linha = i + 1;

                        estruturaOrcamentariaExcel.Add(itemEstruturaOrcamentariaExcel);
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
            //TicketsDeProducao repositorio = new TicketsDeProducao();
            //repositorio.SalvarLista(tickets);

            //carga.AdicionarDetalhe("Carga realizada com sucesso.",
            //                         "Carga de tickets unitários de produção nome: " + carga.NomeArquivo + " .", 0, TipoDetalheEnum.sucesso);
        }
    }

    public class EstruturaOrcamentariaExcel
    {
        public string Departamento { get; set; }
        public TipoDepartamento TipoDepartamento { get; set; }
        public TipoAlteracao TipoAlteracaoDepartamento { get; set; }
        public Departamento DepartamentoEntidade { get; set; }
        public string NomeDaConta { get; set; }
        public string CodigoDaConta { get; set; }
        public TipoAlteracao TipoAlteracaoConta { get; set; }
        public string NomeCentroDeCusto { get; set; }
        public string CodigoCentroDeCusto { get; set; }
        public TipoAlteracao TipoAlteracaoCentroDeCusto { get; set; }
        public string NomeDoGrupoDeConta { get; set; }
        public TipoAlteracao TipoAlteracaoGrupoDeConta { get; set; }
        public int Linha { get; set; }
    }

    public enum TipoAlteracao
    {
        Inclusao = 1,
        Alteracao = 2
    }
}
