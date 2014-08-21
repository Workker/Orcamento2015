using Orcamento.Domain.ComponentesDeOrcamento.OrcamentoPessoal;
using Orcamento.Domain.DB.Repositorio;
using Orcamento.Domain.DB.Repositorio.Robo;
using Orcamento.Domain.Gerenciamento;
using Orcamento.Domain.Robo.Monitoramento;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;


namespace Orcamento.Domain.Entities.Monitoramento
{
    public class Funcionarios : IProcessaCarga
    {
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
        }

        private Processo Processo { get; set; }

        public void Processar(Carga carga, bool salvar = false)
        {
            try
            {
                var funcionarios = new List<FuncionarioExcel>();
                var setores = new Departamentos();
                var centrosDeCusto = new CentrosDeCusto();
                var departamentos = new List<Departamento>();
                var centros = new List<CentroDeCusto>();
                var centrosNaoEncontrados = new List<string>();
                Cargas cargas = new Cargas();

                LerExcel(carga, funcionarios);

                if (NenhunFuncionarioEncontrado(carga, funcionarios)) return;

                ProcessarDepartamentoECentroDeCusto(carga, funcionarios, departamentos, setores, centros, centrosDeCusto);
                if(CargaContemErros(carga)) return;

                ProcessaFuncionario(carga, funcionarios, departamentos, centros);
                if (CargaContemErros(carga)) return;

                ProcessarMudancas(carga, salvar, centrosDeCusto, centros, cargas);
            }
            catch (Exception ex)
            {
                carga.AdicionarDetalhe("Erro ao Salvar funcionarios", "Ocorreu um erro ao tentar salvar os funcionarios.", 0, TipoDetalheEnum.erroDeProcesso, ex.Message);
            }
        }

        private static bool CargaContemErros(Carga carga)
        {
            return !carga.Ok();
        }

        private static bool NenhunFuncionarioEncontrado(Carga carga, List<FuncionarioExcel> funcionarios)
        {
            if (funcionarios.Count == 0)
            {
                carga.AdicionarDetalhe("Nenhum registro foi obtido", "Nenhum registro foi obtido por favor verifique o excel.",
                                       0, TipoDetalheEnum.erroLeituraExcel);
                return true;
            }
            return false;
        }

        private void ProcessarMudancas(Carga carga, bool salvar, CentrosDeCusto centrosDeCusto, List<CentroDeCusto> centros, Cargas cargas)
        {
            if (carga.Ok() && salvar)
            {
                SalvarCentrosDecusto(carga, centrosDeCusto, centros, cargas);
            }
        }

        private void SalvarCentrosDecusto(Carga carga, CentrosDeCusto centrosDeCusto, List<CentroDeCusto> centros, Cargas cargas)
        {
            try
            {
                centrosDeCusto.SalvarLista(centros);
                carga.AdicionarDetalhe("Carga realizada com sucesso.",
                                       "Carga de funcionarios nome : " + carga.NomeArquivo + " .", 0, TipoDetalheEnum.sucesso);
            }
            catch (Exception ex)
            {
                carga.AdicionarDetalhe("Erro ao Tentar alterar os centros de custo", "Ocorreu um erro ao tentar salvar os funcionarios, veja a excecão.", 0, TipoDetalheEnum.erroDeProcesso, ex.Message);
            }
        }

        private void ProcessaFuncionario(Carga carga, List<FuncionarioExcel> funcionarios, List<Departamento> departamentos, List<CentroDeCusto> centros)
        {
            try
            {
                foreach (var funcionarioExcel in funcionarios)
                {
                    var departamento = ObterDepartamentoAtreladoAoFuncionario(departamentos, funcionarioExcel);

                    var centro = ObterCentroDeCustoAtreladoAoFuncionario(centros, funcionarioExcel);

                    if (!ValidarEstrutura(carga, departamento, funcionarioExcel, centro)) continue;

                    var matriculaExiste = centro.Funcionarios.Any(f => f.Matricula == funcionarioExcel.NumeroMatricula);

                    if (ValidarMatriculaFuncionario(carga, matriculaExiste, funcionarioExcel)) continue;

                    var funcionario = CriarFuncionario(matriculaExiste, centro, funcionarioExcel, departamento);

                    ValidarFuncionario(carga, funcionarioExcel, funcionario);
                    centro.Adicionar(funcionario);
                }
            }
            catch (Exception)
            {
                carga.AdicionarDetalhe("Erro ao processar Funcionario", "Ocorreu um erro ao tentar processar os funcionarios", 0, TipoDetalheEnum.erroDeProcesso);
            }
        }

        private static CentroDeCusto ObterCentroDeCustoAtreladoAoFuncionario(List<CentroDeCusto> centros, FuncionarioExcel funcionarioExcel)
        {
            return centros.FirstOrDefault(d => d.CodigoDoCentroDeCusto == funcionarioExcel.CodigoCentroDeCusto);
        }

        private static Departamento ObterDepartamentoAtreladoAoFuncionario(List<Departamento> departamentos, FuncionarioExcel funcionarioExcel)
        {
            return departamentos.FirstOrDefault(d => d.Nome == funcionarioExcel.Departamento);
        }

        private static bool ValidarMatriculaFuncionario(Carga carga, bool matriculaExiste, FuncionarioExcel funcionarioExcel)
        {
            if (matriculaExiste && !carga.EntidadeRepetidaAltera)
            {
                carga.AdicionarDetalhe("Matricula repetida.",
                                       "Matricula:" + funcionarioExcel.NumeroMatricula + "já existe neste centro de custo.", 0,
                                       TipoDetalheEnum.erroDeProcesso, "Matricula repetida.");
                return true;
            }
            return false;
        }

        private static Funcionario CriarFuncionario(bool matriculaExiste, CentroDeCusto centro,
                                                    FuncionarioExcel funcionarioExcel, Departamento setor)
        {
            Funcionario funcionario;
            if (matriculaExiste)
                funcionario = centro.Funcionarios.FirstOrDefault(c => c.Matricula == funcionarioExcel.NumeroMatricula);
            else
                funcionario = new Funcionario(setor);
            return funcionario;
        }

        private void ProcessarDepartamentoECentroDeCusto(Carga carga, List<FuncionarioExcel> funcionarios, List<Departamento> departamentos, Departamentos repositorioDepartamentos,
                                                         List<CentroDeCusto> centros, CentrosDeCusto centrosDeCusto)
        {
            try
            {
                foreach (var funcionarioExcel in funcionarios)
                {
                    if (!departamentos.Any(d => d.Nome == funcionarioExcel.Departamento))
                    {
                        AdicionarDepartamento(carga, repositorioDepartamentos, funcionarioExcel, departamentos);
                    }

                    if (!centros.Any(d => d.CodigoDoCentroDeCusto == funcionarioExcel.CodigoCentroDeCusto))
                    {
                        AdicionarCentroDeCusto(carga, centrosDeCusto, funcionarioExcel, centros);
                    }
                }
            }
            catch (Exception ex)
            {
                carga.AdicionarDetalhe("Erro processamento", "Ocorreu um erro, ao tentar processar departamentos e centros de custo da carga.", 0, TipoDetalheEnum.erroDeProcesso, ex.Message);
            }

        }

        private void AdicionarCentroDeCusto(Carga carga, CentrosDeCusto centrosDeCusto, FuncionarioExcel funcionarioExcel,
                                                   List<CentroDeCusto> centros)
        {
            var centroCarga = centrosDeCusto.ObterPor(funcionarioExcel.CodigoCentroDeCusto);

            if (centroCarga == null)
                carga.AdicionarDetalhe("Centro de custo nao encontrado",
                                       "centro de custo: " + funcionarioExcel.CodigoCentroDeCusto + " inexistente.",
                                       funcionarioExcel.Linha, TipoDetalheEnum.erroDeProcesso);
            else
                centros.Add(centroCarga);
        }

        private void AdicionarDepartamento(Carga carga, Departamentos repoDepartamentos, FuncionarioExcel funcionarioExcel, List<Departamento> departamentos)
        {
            var setorCarga = repoDepartamentos.ObterPor(funcionarioExcel.Departamento);

            if (setorCarga == null)
                carga.AdicionarDetalhe("Hospital/Setor nao encontrado",
                                       "Hospital/Setor: " + funcionarioExcel.Departamento + " inexistente.",
                                       funcionarioExcel.Linha, TipoDetalheEnum.erroDeProcesso);
            else
                departamentos.Add(setorCarga);
        }

        private void ValidarFuncionario(Carga carga, FuncionarioExcel funcionarioExcel, Funcionario funcionario)
        {
            try
            {
                if (funcionarioExcel.Ano == default(int))
                    carga.AdicionarDetalhe("Ano não preenchido", "Ano do funcionário não preenchido", funcionarioExcel.Linha,
                                           TipoDetalheEnum.erroDeProcesso);
                else
                    funcionario.AnoAdmissao = funcionarioExcel.Ano;

                if (string.IsNullOrEmpty(funcionarioExcel.Funcao))
                    carga.AdicionarDetalhe("Função não preenchida", "Função do funcionário não preenchida", funcionarioExcel.Linha,
                                           TipoDetalheEnum.erroDeProcesso);
                else
                    funcionario.Cargo = funcionarioExcel.Funcao;

                if (funcionarioExcel.Mes == default(int))
                    carga.AdicionarDetalhe("Mês não preenchido", "Mês do funcionário não preenchido", funcionarioExcel.Linha,
                                           TipoDetalheEnum.erroDeProcesso);
                else
                    funcionario.DataAdmissao = funcionarioExcel.Mes;

                if (string.IsNullOrEmpty(funcionarioExcel.NumeroMatricula))
                    carga.AdicionarDetalhe("Número de matrícula não preenchido", "Número de matrícula não preenchido",
                                           funcionarioExcel.Linha, TipoDetalheEnum.erroDeProcesso);
                else
                    funcionario.Matricula = funcionarioExcel.NumeroMatricula;

                if (string.IsNullOrEmpty(funcionarioExcel.Nome))
                    carga.AdicionarDetalhe("Nome não Preenchido", "Nome não Preenchido", funcionarioExcel.Linha,
                                           TipoDetalheEnum.erroDeProcesso);
                else
                    funcionario.Nome = funcionarioExcel.Nome;

                if (funcionarioExcel.Salario == default(double))
                    carga.AdicionarDetalhe("Salário não preenchido", "Salário não preenchido", funcionarioExcel.Linha,
                                           TipoDetalheEnum.erroDeProcesso);
                else
                    funcionario.Salario = funcionarioExcel.Salario;

                if (funcionarioExcel.NumeroVaga == default(int))
                    carga.AdicionarDetalhe("Número de vaga não preenchido", "Número de vaga não preenchido", funcionarioExcel.Linha,
                                           TipoDetalheEnum.erroDeProcesso);
                else
                    funcionario.NumeroDeVaga = funcionarioExcel.NumeroVaga;
            }
            catch (Exception ex)
            {
                carga.AdicionarDetalhe("Erro ao processar Funcionario", "Ocorreu um erro ao processar o Funcionario Matricula: " + funcionario.Matricula, funcionarioExcel.Linha, TipoDetalheEnum.erroDeProcesso, ex.Message);
            }
        }

        private bool ValidarEstrutura(Carga carga, Departamento setor, FuncionarioExcel funcionarioExcel,
                                             CentroDeCusto centro)
        {
            ValidarDepartamento(carga, setor, funcionarioExcel);

            ValidarCentroDeCusto(carga, funcionarioExcel, centro);

            return DiferentesDeNulo(setor, centro);
        }

        private static void ValidarCentroDeCusto(Carga carga, FuncionarioExcel funcionarioExcel, CentroDeCusto centro)
        {
            if (centro == null)
            {
                carga.AdicionarDetalhe("Centro de custo nulo",
                                       "Centro de custo codigo: " + funcionarioExcel.CodigoCentroDeCusto + " inexistente.",
                                       funcionarioExcel.Linha, TipoDetalheEnum.erroDeProcesso);
            }
        }

        private static void ValidarDepartamento(Carga carga, Departamento setor, FuncionarioExcel funcionarioExcel)
        {
            if (setor == null)
            {
                carga.AdicionarDetalhe("Setor/Hospital inexistente",
                                       "Setor/Hospital: " + funcionarioExcel.Departamento + " inexistente.",
                                       funcionarioExcel.Linha, TipoDetalheEnum.erroDeProcesso);
            }
        }

        private static bool DiferentesDeNulo(Departamento setor, CentroDeCusto centro)
        {
            return setor != null && centro != null;
        }

        private void LerExcel(Carga carga, List<FuncionarioExcel> funcionarios)
        {

            Processo = new Processo();
            var reader = Processo.InicializarCarga(carga);

            if (reader == null)
                carga.AdicionarDetalhe("Nao foi possivel Ler o excel", "Nao foi possivel Ler o excel por favor verifique o layout.", 0, TipoDetalheEnum.erroLeituraExcel);
            else
                LerExcel(carga, funcionarios, reader);

            Processo.FinalizarCarga();
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

                        funcionarioExcel.Departamento = (string)reader[1];
                        funcionarioExcel.CodigoCentroDeCusto = Convert.ToString(reader[2]);
                        funcionarioExcel.NumeroMatricula = Convert.ToInt32(reader[5]).ToString();
                        funcionarioExcel.Nome = (string)reader[6];
                        funcionarioExcel.Funcao = (string)reader[7];
                        funcionarioExcel.Salario = (double)reader[8];
                        funcionarioExcel.Mes = (int)(double)reader[10];
                        funcionarioExcel.Ano = (int)(double)reader[11];
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
    }
}
