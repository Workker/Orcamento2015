using Orcamento.Domain.ComponentesDeOrcamento.OrcamentoPessoal;
using Orcamento.Domain.DB.Repositorio;
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

        public void Processar(Carga carga)
        {
            var funcionarios = new List<FuncionarioExcel>();
            var setores = new Setores();
            var centrosDeCusto = new CentrosDeCusto();
            var departamentos = new List<Departamento>();
            var centros = new List<CentroDeCusto>();
            var centrosNaoEncontrados = new List<string>();

            LerExcel(carga, funcionarios);

            if (funcionarios.Count == 0)
            {
                carga.AdicionarDetalhe("Nenhum registro foi obtido", "Nenhum registro foi obtido por favor verifique o excel.", 0, TipoDetalheEnum.erroLeituraExcel);
                return;
            }
            ProcessarSetorECentroDeCusto(carga, funcionarios, departamentos, setores, centros, centrosDeCusto);

            ProcessaFuncionario(carga, funcionarios, departamentos, centros);

            try
            {
                if (carga.Ok())
                    centrosDeCusto.SalvarLista(centros);
            }
            catch (Exception)
            {
                carga.AdicionarDetalhe("Erro ao Salvar Funcionarios", "Ocorreu um erro ao tentar salvar os funcionarios.", 0, TipoDetalheEnum.erroDeProcesso);
            }

        }

        private void ProcessaFuncionario(Carga carga, List<FuncionarioExcel> funcionarios, List<Departamento> departamentos, List<CentroDeCusto> centros)
        {
            try
            {
                foreach (var funcionarioExcel in funcionarios)
                {
                    var setor = departamentos.FirstOrDefault(d => d.Nome == funcionarioExcel.Departamento);

                    var centro = centros.FirstOrDefault(d => d.CodigoDoCentroDeCusto == funcionarioExcel.CodigoCentroDeCusto);

                    if (ValidarEstrutura(carga, setor, funcionarioExcel, centro)) continue;

                    var funcionario = new Funcionario(setor);

                    ValidarFuncionario(carga, funcionarioExcel, funcionario);

                    centro.Adicionar(funcionario);
                }
            }
            catch (Exception)
            {
                carga.AdicionarDetalhe("Erro ao processar Funcionario", "Ocorreu um erro ao tentar processar os funcionarios", 0, TipoDetalheEnum.erroDeProcesso);
            }
        }

        private void ProcessarSetorECentroDeCusto(Carga carga, List<FuncionarioExcel> funcionarios, List<Departamento> departamentos, Setores setores,
                                                         List<CentroDeCusto> centros, CentrosDeCusto centrosDeCusto)
        {
            try
            {
                foreach (var funcionarioExcel in funcionarios)
                {
                    if (!departamentos.Any(d => d.Nome == funcionarioExcel.Departamento))
                    {
                        AdicionarSetor(carga, setores, funcionarioExcel, departamentos);
                    }

                    if (!centros.Any(d => d.CodigoDoCentroDeCusto == funcionarioExcel.CodigoCentroDeCusto))
                    {
                        AdicionarCentroDeCusto(carga, centrosDeCusto, funcionarioExcel, centros);
                    }
                }
            }
            catch (Exception)
            {
                carga.AdicionarDetalhe("Erro processamento", "Ocorreu um erro ao tentar processar os setores e os centros de custo da carga.", 0, TipoDetalheEnum.erroDeProcesso);
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

        private void AdicionarSetor(Carga carga, Setores setores, FuncionarioExcel funcionarioExcel, List<Departamento> departamentos)
        {
            var setorCarga = setores.ObterPor(funcionarioExcel.Departamento);

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
            catch (Exception)
            {
                carga.AdicionarDetalhe("Erro ao processar Funcionario", "Ocorreu um erro ao processar o Funcionario Matricula: " + funcionario.Matricula, funcionarioExcel.Linha, TipoDetalheEnum.erroDeProcesso);
            }
        }

        private bool ValidarEstrutura(Carga carga, Departamento setor, FuncionarioExcel funcionarioExcel,
                                             CentroDeCusto centro)
        {
            if (setor == null)
            {
                carga.AdicionarDetalhe("Setor inexistente", "Setor: " + funcionarioExcel.Departamento + " inexistente.",
                                       funcionarioExcel.Linha, TipoDetalheEnum.erroDeProcesso);
            }

            if (centro == null)
            {
                carga.AdicionarDetalhe("Centro de custo nulo",
                                       "Centro de custo codigo: " + funcionarioExcel.CodigoCentroDeCusto + " inexistente.",
                                       funcionarioExcel.Linha, TipoDetalheEnum.erroDeProcesso);
            }

            return DiferentesDeNulo(setor, centro);
        }

        private static bool DiferentesDeNulo(Departamento setor, CentroDeCusto centro)
        {
            return setor != null && centro != null;
        }

        private void LerExcel(Carga carga, List<FuncionarioExcel> funcionarios)
        {

            var reader = InicializarCarga(carga);

            if (reader == null)
                carga.AdicionarDetalhe("Nao foi possivel Ler o excel", "Nao foi possivel Ler o excel por favor verifique o layout.", 0, TipoDetalheEnum.erroLeituraExcel);
            else
                LerExcel(carga, funcionarios, reader);
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
                        funcionarioExcel.CodigoCentroDeCusto = (string)reader[2];
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
                catch (Exception)
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
