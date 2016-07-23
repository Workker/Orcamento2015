
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Orcamento.Domain.ComponentesDeOrcamento.OrcamentoPessoal;
using Orcamento.Domain.DB.Repositorio;
using Orcamento.Domain.Gerenciamento;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace Orcamento.TestMethod.InfraStructure._2014
{
    [TestClass]
    public class AtualizarFuncionariosTestMethod
    {

        [TestMethod]
        public void atualizar_quadro_de_funcionarios_atualizar_salario_hospitalares()
        {
            string _conectionstring;
            _conectionstring = @"Provider=Microsoft.Jet.OLEDB.4.0;";
            _conectionstring += String.Format("Data Source={0};", "D:\\cargaorcamento2014\\FuncionariosHospitalAdicionalSalarial.xls");
            _conectionstring += "Extended Properties='Excel 8.0;HDR=NO;'";

            var cn = new OleDbConnection(_conectionstring);
            var cmd = new OleDbCommand("Select * from [carga$]", cn);
            cn.Open();

            var reader = cmd.ExecuteReader();

            var funcionarios = new List<FuncionarioExcel>();

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

                    //if(reader[1].ToString() == "CAXIAS")
                    //    continue;

                    funcionarioExcel.Departamento = (string)reader[1];
                    funcionarioExcel.CodigoCentroDeCusto = (string)reader[2];
                    funcionarioExcel.NumeroMatricula = Convert.ToInt32(reader[5]).ToString();
                    funcionarioExcel.Nome = (string)reader[6];
                    funcionarioExcel.Funcao = (string)reader[7];
                    funcionarioExcel.Salario = (double)reader[8];
                    funcionarioExcel.Mes = (int)(double)reader[10];
                    funcionarioExcel.Ano = (int)(double)reader[11];
                    funcionarioExcel.NumeroVaga = int.Parse(reader[13].ToString());

                    funcionarios.Add(funcionarioExcel);
                }
                i++;
            }

            var setores = new Hospitais();
            var centrosDeCusto = new CentrosDeCusto();
            var departamentos = new List<Departamento>();
            var centros = new List<CentroDeCusto>();
            var centrosNaoEncontrados = new List<string>();
            foreach (var funcionarioExcel in funcionarios)
            {
                if (!departamentos.Where(d => d != null).Any(d => d.Nome == funcionarioExcel.Departamento))
                    departamentos.Add(setores.ObterPor(funcionarioExcel.Departamento));

                if (!centros.Any(d => d.CodigoDoCentroDeCusto == funcionarioExcel.CodigoCentroDeCusto))
                {
                    var centroR = centrosDeCusto.ObterPor(funcionarioExcel.CodigoCentroDeCusto);

                    if (centroR == null)
                        centrosNaoEncontrados.Add(funcionarioExcel.CodigoCentroDeCusto);
                    else
                    {
                        centros.Add(centroR);
                    }

                }

            }
            List<string> centrosNaoEncontradosNew = new List<string>();
            foreach (var funcionarioExcel in funcionarios)
            {

                var setor = departamentos.Where(d => d != null).FirstOrDefault(d => d.Nome == funcionarioExcel.Departamento);
                var centro =
             centros.FirstOrDefault(d => d.CodigoDoCentroDeCusto == funcionarioExcel.CodigoCentroDeCusto);
                if (centro == null)
                {
                    centrosNaoEncontradosNew.Add(funcionarioExcel.CodigoCentroDeCusto);
                    continue;
                }

                var funcionario = centro.Funcionarios.Where(f=> f.Departamento.Nome == funcionarioExcel.Departamento).Single(f => f.Matricula == funcionarioExcel.NumeroMatricula);
                funcionario.Salario = funcionarioExcel.Salario;
            }
            var TestMethode = centrosNaoEncontradosNew.Distinct();
            centrosDeCusto.SalvarLista(centros);
        }

        [TestMethod]
        public void atualizar_quadro_de_funcionarios_hospitalares()
        {
            string _conectionstring;
            _conectionstring = @"Provider=Microsoft.Jet.OLEDB.4.0;";
            _conectionstring += String.Format("Data Source={0};", "D:\\cargaorcamento2014\\FuncionariosHospitalarVagasEmAberto.xls");
            _conectionstring += "Extended Properties='Excel 8.0;HDR=NO;'";

            var cn = new OleDbConnection(_conectionstring);
            var cmd = new OleDbCommand("Select * from [c$]", cn);
            cn.Open();

            var reader = cmd.ExecuteReader();

            var funcionarios = new List<FuncionarioExcel>();

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

                    //if(reader[1].ToString() == "CAXIAS")
                    //    continue;

                    funcionarioExcel.Departamento = (string)reader[1];
                    funcionarioExcel.CodigoCentroDeCusto = (string)reader[2];
                    funcionarioExcel.NumeroMatricula = (string)reader[5].ToString();
                    funcionarioExcel.Nome = (string)reader[6];
                    funcionarioExcel.Funcao = (string)reader[7];
                    funcionarioExcel.Salario = (double)reader[8];
                    funcionarioExcel.Mes = (int)(double)reader[10];
                    funcionarioExcel.Ano = (int)(double)reader[11];
                    funcionarioExcel.NumeroVaga = int.Parse(reader[13].ToString());

                    funcionarios.Add(funcionarioExcel);
                }
                i++;
            }

            var setores = new Hospitais();
            var centrosDeCusto = new CentrosDeCusto();
            var departamentos = new List<Departamento>();
            var centros = new List<CentroDeCusto>();
            var centrosNaoEncontrados = new List<string>();
            foreach (var funcionarioExcel in funcionarios)
            {
                if (!departamentos.Any(d => d.Nome == funcionarioExcel.Departamento))
                    departamentos.Add(setores.ObterPor(funcionarioExcel.Departamento));

                if (!centros.Any(d => d.CodigoDoCentroDeCusto == funcionarioExcel.CodigoCentroDeCusto))
                {
                    var centroR = centrosDeCusto.ObterPor(funcionarioExcel.CodigoCentroDeCusto);

                    if (centroR == null)
                        centrosNaoEncontrados.Add(funcionarioExcel.CodigoCentroDeCusto);
                    else
                    {
                        centros.Add(centroR);
                    }

                }

            }
            List<string> centrosNaoEncontradosNew = new List<string>();
            foreach (var funcionarioExcel in funcionarios)
            {

                var setor = departamentos.FirstOrDefault(d => d.Nome == funcionarioExcel.Departamento);
                var centro =
             centros.FirstOrDefault(d => d.CodigoDoCentroDeCusto == funcionarioExcel.CodigoCentroDeCusto);
                if (centro == null)
                {
                    centrosNaoEncontradosNew.Add(funcionarioExcel.CodigoCentroDeCusto);
                    continue;
                }
                var funcionario = new Funcionario(setor)
                {
                    AnoAdmissao = funcionarioExcel.Ano,
                    Cargo = funcionarioExcel.Funcao,
                    DataAdmissao = funcionarioExcel.Mes,
                    Matricula = funcionarioExcel.NumeroMatricula,
                    Nome = funcionarioExcel.Nome,
                    Salario = funcionarioExcel.Salario,
                    NumeroDeVaga = funcionarioExcel.NumeroVaga
                };
                centro.Adicionar(funcionario);
            }
            var TestMethode = centrosNaoEncontradosNew.Distinct();
            centrosDeCusto.SalvarLista(centros);
        }

        [TestMethod]
        public void atualizar_quadro_de_funcionarios_Coorporativo()
        {
            string _conectionstring;
            _conectionstring = @"Provider=Microsoft.Jet.OLEDB.4.0;";
            _conectionstring += String.Format("Data Source={0};", "D:\\cargaorcamento2014\\FuncionariosCoorporativoComplementar.xls");
            _conectionstring += "Extended Properties='Excel 8.0;HDR=NO;'";

            var cn = new OleDbConnection(_conectionstring);
            var cmd = new OleDbCommand("Select * from [carga$]", cn);
            cn.Open();

            var reader = cmd.ExecuteReader();

            var funcionarios = new List<FuncionarioExcel>();

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

                    //if(reader[1].ToString() == "CAXIAS")
                    //    continue;

                    funcionarioExcel.Departamento = (string)reader[1];
                    funcionarioExcel.CodigoCentroDeCusto = (string)reader[2];
                    funcionarioExcel.NumeroMatricula = Convert.ToInt32(reader[5]).ToString();
                    funcionarioExcel.Nome = (string)reader[6];
                    funcionarioExcel.Funcao = (string)reader[7];
                    funcionarioExcel.Salario = (double)reader[8];
                    funcionarioExcel.Mes = (int)(double)reader[10];
                    funcionarioExcel.Ano = (int)(double)reader[11];
                    funcionarioExcel.NumeroVaga = int.Parse(reader[13].ToString());

                    funcionarios.Add(funcionarioExcel);
                }
                i++;
            }

            var setores = new Setores();
            var centrosDeCusto = new CentrosDeCusto();
            var departamentos = new List<Departamento>();
            var centros = new List<CentroDeCusto>();
            var centrosNaoEncontrados = new List<string>();
            foreach (var funcionarioExcel in funcionarios)
            {
                if (!departamentos.Any(d => d.Nome == funcionarioExcel.Departamento))
                {
                    var setor = setores.ObterPor(funcionarioExcel.Departamento);
                        if(setor ==  null)
                            throw  new Exception();
                    departamentos.Add(setor);
                }

                if (!centros.Any(d => d.CodigoDoCentroDeCusto == funcionarioExcel.CodigoCentroDeCusto))
                {
                    var centroR = centrosDeCusto.ObterPor(funcionarioExcel.CodigoCentroDeCusto);

                    if (centroR == null)
                        centrosNaoEncontrados.Add(funcionarioExcel.CodigoCentroDeCusto);
                    else
                    {
                        centros.Add(centroR);
                    }

                }

            }
            List<string> centrosNaoEncontradosNew = new List<string>();
            foreach (var funcionarioExcel in funcionarios)
            {

                var setor = departamentos.FirstOrDefault(d => d.Nome == funcionarioExcel.Departamento);
                var centro =
             centros.FirstOrDefault(d => d.CodigoDoCentroDeCusto == funcionarioExcel.CodigoCentroDeCusto);
                if (centro == null)
                {
                    centrosNaoEncontradosNew.Add(funcionarioExcel.CodigoCentroDeCusto);
                    continue;
                }
                var funcionario = new Funcionario(setor)
                {
                    AnoAdmissao = funcionarioExcel.Ano,
                    Cargo = funcionarioExcel.Funcao,
                    DataAdmissao = funcionarioExcel.Mes,
                    Matricula = funcionarioExcel.NumeroMatricula,
                    Nome = funcionarioExcel.Nome,
                    Salario = funcionarioExcel.Salario,
                    NumeroDeVaga = funcionarioExcel.NumeroVaga
                };
                centro.Adicionar(funcionario);
            }
            var TestMethode = centrosNaoEncontradosNew.Distinct();
            centrosDeCusto.SalvarLista(centros);
        }

        [TestMethod]
        public  void AssociarUsuarioToDepartamentoCoorporativos()
        {
            var usuario = new Usuarios().ObterUsuarioPorId(51);
            var departamentos = new Setores().Todos();
            foreach (var setor in departamentos)
            {
                usuario.ParticiparDe(setor);
            }

            new Usuarios().Salvar(usuario);
        }

    }
}
