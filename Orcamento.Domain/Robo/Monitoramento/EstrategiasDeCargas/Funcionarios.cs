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

            LerExcel(carga, funcionarios);

            var setores = new Setores();
            var centrosDeCusto = new CentrosDeCusto();
            var departamentos = new List<Departamento>();
            var centros = new List<CentroDeCusto>();
            var centrosNaoEncontrados = new List<string>();

            foreach (var funcionarioExcel in funcionarios)
            {
                if (!departamentos.Any(d => d.Nome == funcionarioExcel.Departamento))
                {
                    var setorCarga = setores.ObterPor(funcionarioExcel.Departamento);

                    if (setorCarga == null)
                        carga.AdicionarDetalhe("Hospital/Setor nao encontrado", "Hospital/Setor: " + funcionarioExcel.Departamento + " inexistente.", funcionarioExcel.Linha,TipoDetalheEnum.erro);
                    else
                        departamentos.Add(setorCarga);
                }

                if (!centros.Any(d => d.CodigoDoCentroDeCusto == funcionarioExcel.CodigoCentroDeCusto))
                {
                    var centroCarga = centrosDeCusto.ObterPor(funcionarioExcel.CodigoCentroDeCusto);

                    if (centroCarga == null)
                        carga.AdicionarDetalhe("Centro de custo nao encontrado", "centro de custo: " + funcionarioExcel.CodigoCentroDeCusto + " inexistente.", funcionarioExcel.Linha, TipoDetalheEnum.erro);
                    else
                        centros.Add(centroCarga);

                }

            }

            foreach (var funcionarioExcel in funcionarios)
            {
                var setor = departamentos.FirstOrDefault(d => d.Nome == funcionarioExcel.Departamento);

                var centro = centros.FirstOrDefault(d => d.CodigoDoCentroDeCusto == funcionarioExcel.CodigoCentroDeCusto);

                if (centro == null)
                {
                    carga.AdicionarDetalhe("Centro de custo nulo", "Centro de custo codigo: " + funcionarioExcel.CodigoCentroDeCusto + " inexistente.", funcionarioExcel.Linha, TipoDetalheEnum.erro);
                    continue;
                }
                
                if (centro == null)
                {
                    carga.AdicionarDetalhe("Centro de custo nulo", "Centro de custo codigo: " + funcionarioExcel.CodigoCentroDeCusto + " inexistente.", funcionarioExcel.Linha, TipoDetalheEnum.erro);
                    continue;
                }
                var funcionario = new Funcionario(setor);

                if (funcionarioExcel.Ano == default(int))
                    carga.AdicionarDetalhe("Ano não preenchido", "Ano do funcionário não preenchido", funcionarioExcel.Linha, TipoDetalheEnum.erro);
                else
                    funcionario.AnoAdmissao = funcionarioExcel.Ano;

                if (string.IsNullOrEmpty(funcionarioExcel.Funcao))
                    carga.AdicionarDetalhe("Função não preenchida", "Função do funcionário não preenchida", funcionarioExcel.Linha, TipoDetalheEnum.erro);
                else
                    funcionario.Cargo = funcionarioExcel.Funcao;

                if (funcionarioExcel.Mes == default(int))
                    carga.AdicionarDetalhe("Mês não preenchido", "Mês do funcionário não preenchido", funcionarioExcel.Linha, TipoDetalheEnum.erro);
                else
                    funcionario.DataAdmissao = funcionarioExcel.Mes;

                if (string.IsNullOrEmpty(funcionarioExcel.NumeroMatricula))
                    carga.AdicionarDetalhe("Número de matrícula não preenchido", "Número de matrícula não preenchido", funcionarioExcel.Linha, TipoDetalheEnum.erro);
                else
                    funcionario.Matricula = funcionarioExcel.NumeroMatricula;

                if (string.IsNullOrEmpty(funcionarioExcel.Nome))
                    carga.AdicionarDetalhe("Nome não Preenchido", "Nome não Preenchido", funcionarioExcel.Linha, TipoDetalheEnum.erro);
                else
                    funcionario.Nome = funcionarioExcel.Nome;

                if (funcionarioExcel.Salario == default(double))
                    carga.AdicionarDetalhe("Salário não preenchido", "Salário não preenchido", funcionarioExcel.Linha, TipoDetalheEnum.erro);
                else
                    funcionario.Salario = funcionarioExcel.Salario;

                if (funcionarioExcel.NumeroVaga == default(int))
                    carga.AdicionarDetalhe("Número de vaga não preenchido", "Número de vaga não preenchido", funcionarioExcel.Linha, TipoDetalheEnum.erro);
                else
                    funcionario.NumeroDeVaga = funcionarioExcel.NumeroVaga;

                centro.Adicionar(funcionario);
            }

            if(carga.Ok())
              centrosDeCusto.SalvarLista(centros);
        }

        private void LerExcel(Carga carga, List<FuncionarioExcel> funcionarios)
        {
            string _conectionstring;
            _conectionstring = @"Provider=Microsoft.ACE.OLEDB.12.0;";
            _conectionstring += String.Format("Data Source={0};", carga.Diretorio);
            _conectionstring += "Extended Properties='Excel 8.0;HDR=NO;'";

            var cn = new OleDbConnection(_conectionstring);
            var cmd = new OleDbCommand("Select * from [carga$]", cn);
            cn.Open();
            var reader = cmd.ExecuteReader();


            int i = 0;

            while (reader.Read())
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
                i++;
            }
        }
    }
}
