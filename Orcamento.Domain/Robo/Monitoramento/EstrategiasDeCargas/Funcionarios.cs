using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using Orcamento.Domain.ComponentesDeOrcamento.OrcamentoPessoal;
using Orcamento.Domain.DB.Repositorio;
using Orcamento.Domain.Gerenciamento;
using Orcamento.Domain.Robo.Monitoramento;
using Orcamento.Domain.Robo.Monitoramento.EspecificacaoDeValidacaoDeCarga.EspeficicacaoFuncionarios;

namespace Orcamento.Domain.Entities.Monitoramento
{
    public class Funcionarios : ProcessaCarga
    {
        private List<FuncionarioExcel> funcionarios;
        public MotorDeValidacaoDeFuncionario motor { get; set; }

        public override void Processar(Carga carga, bool salvar = false)
        {
            try
            {
                funcionarios = new List<FuncionarioExcel>();
                this.carga = carga;

                LerExcel(carga, funcionarios);

                if (NenhunFuncionarioEncontrado())
                    return;

                ValidarCarga();

                ProcessaFuncionario();

                if (CargaContemErros()) 
                    return;

                SalvarAlteracoes(salvar);
            }
            catch (Exception ex)
            {
                carga.AdicionarDetalhe("Erro ao processar funcionários",
                                       "Ocorreu um erro ao tentar processar os funcionários.", 0,
                                       TipoDetalheEnum.erroDeProcesso, ex.Message);
            }
        }

        private void ValidarCarga()
        {
            motor = new MotorDeValidacaoDeFuncionario();
            motor.Validar(carga, funcionarios);
        }


        private bool NenhunFuncionarioEncontrado()
        {
            if (funcionarios.Count == 0)
            {
                carga.AdicionarDetalhe("Nenhum registro foi obtido",
                                       "Nenhum registro foi obtido por favor verifique o excel.",
                                       0, TipoDetalheEnum.erroLeituraExcel);
                return true;
            }
            return false;
        }

        private void SalvarCentrosDecusto()
        {
            try
            {
                var centros = new CentrosDeCusto();

                centros.SalvarLista(motor.CentrosDeCusto);
                carga.AdicionarDetalhe("Carga realizada com sucesso.",
                                       "Carga de funcionarios nome : " + carga.NomeArquivo + " .", 0,
                                       TipoDetalheEnum.sucesso);
            }
            catch (Exception ex)
            {
                carga.AdicionarDetalhe("Erro ao Tentar alterar os centros de custo",
                                       "Ocorreu um erro ao tentar salvar os funcionarios, veja a excecão.", 0,
                                       TipoDetalheEnum.erroDeProcesso, ex.Message);
            }
        }

        private void ProcessaFuncionario()
        {
            try
            {
                foreach (FuncionarioExcel funcionarioExcel in funcionarios)
                {
                    Funcionario funcionario = CriarFuncionario(funcionarioExcel);
                    funcionarioExcel.CentroDeCusto.Adicionar(funcionario);
                }
            }
            catch (Exception)
            {
                carga.AdicionarDetalhe("Erro ao processar Funcionario",
                                       "Ocorreu um erro ao tentar processar os funcionarios", 0,
                                       TipoDetalheEnum.erroDeProcesso);
            }
        }

        private Funcionario CriarFuncionario(FuncionarioExcel funcionarioExcel)
        {
            Funcionario funcionario;
            if (FuncionarioExiste(funcionarioExcel))
                funcionario =
                    funcionarioExcel.CentroDeCusto.Funcionarios.FirstOrDefault(
                        c => c.Matricula == funcionarioExcel.NumeroMatricula);
            else
                funcionario = new Funcionario(funcionarioExcel.DepartamentoEntidade);

            PreencherFuncionario(funcionario, funcionarioExcel);

            return funcionario;
        }

        private void PreencherFuncionario(Funcionario funcionario, FuncionarioExcel funcionarioExcel)
        {
            funcionario.Matricula = funcionarioExcel.NumeroMatricula;
            funcionario.Salario = funcionarioExcel.Salario;
            funcionario.Nome = funcionarioExcel.Nome;
            funcionario.DataAdmissao = funcionarioExcel.Mes;
            funcionario.Cargo = funcionarioExcel.Funcao;
            funcionario.AnoAdmissao = funcionarioExcel.Ano;
            //funcionario.NumeroDeVaga = funcionarioExcel.NumeroVaga;
        }

        private static bool FuncionarioExiste(FuncionarioExcel funcionarioExcel)
        {
            return funcionarioExcel.CentroDeCusto.Funcionarios != null &&
                   funcionarioExcel.CentroDeCusto.Funcionarios.Any(f => f.Matricula == funcionarioExcel.NumeroMatricula);
        }

        private void LerExcel(Carga carga, List<FuncionarioExcel> funcionarios)
        {
            processo = new Processo();
            OleDbDataReader reader = processo.InicializarCarga(carga);

            if (reader == null)
                carga.AdicionarDetalhe("Nao foi possivel Ler o excel",
                                       "Nao foi possivel Ler o excel por favor verifique o layout.", 0,
                                       TipoDetalheEnum.erroLeituraExcel);
            else
                LerExcel(carga, funcionarios, reader);

            processo.FinalizarCarga();
        }

        private void LerExcel(Carga carga, List<FuncionarioExcel> funcionarios, OleDbDataReader reader)
        {
            int i = 0;
            while (reader.Read())
            {
                try
                {
                    if (i > 0)
                    {
                        if (reader[0] == DBNull.Value)
                            break;

                        var funcionarioExcel = new FuncionarioExcel();

                        if (reader[5] == DBNull.Value)
                            continue;

                        funcionarioExcel.Departamento = (string) reader[1];
                        funcionarioExcel.CodigoCentroDeCusto = Convert.ToString(reader[2]);
                        funcionarioExcel.NumeroMatricula = Convert.ToInt32(reader[5]).ToString();
                        funcionarioExcel.Nome = (string) reader[6];
                        funcionarioExcel.Funcao = (string) reader[7];
                        funcionarioExcel.Salario = (double) reader[8];
                        funcionarioExcel.Mes = (int) (double) reader[10];
                        funcionarioExcel.Ano = (int) (double) reader[11];
                        funcionarioExcel.NumeroVaga = int.Parse(reader[13].ToString());
                        funcionarioExcel.Linha = i + 1;
                        funcionarios.Add(funcionarioExcel);
                    }
                }
                catch (Exception ex)
                {
                    carga.AdicionarDetalhe("Erro na linha", "Ocorreu um erro ao tentar ler a linha do excel", i + 1,
                                           TipoDetalheEnum.erroLeituraExcel, ex.Message);
                }
                finally
                {
                    i++;
                }
            }
        }

        internal override void SalvarDados()
        {
            SalvarCentrosDecusto();
        }

        #region Nested type: FuncionarioExcel

        public class FuncionarioExcel
        {
            public int Linha { get; set; }
            public string Departamento { get; set; }
            public int NumeroVaga { get; set; }
            public string CodigoCentroDeCusto { get; set; }
            public string NumeroMatricula { get; set; }
            public string Nome { get; set; }
            public string Funcao { get; set; }
            public int Mes { get; set; }
            public int Ano { get; set; }
            public double Salario { get; set; }

            public Departamento DepartamentoEntidade { get; set; }
            public CentroDeCusto CentroDeCusto { get; set; }
        }

        #endregion
    }
}