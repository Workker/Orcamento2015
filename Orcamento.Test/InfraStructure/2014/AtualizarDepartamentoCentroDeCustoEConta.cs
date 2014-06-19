using NUnit.Framework;
using Orcamento.Domain;
using Orcamento.Domain.ComponentesDeOrcamento.OrcamentoPessoal;
using Orcamento.Domain.DB.Repositorio;
using Orcamento.Domain.Gerenciamento;
using Orcamento.Domain.Servico.Hospitalar;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace Orcamento.Test.InfraStructure._2014
{
    [TestFixture]
    public class AtualizarDepartamentoCentroDeCustoEConta
    {
        [Test]
        public void atualizar_estrutura_coorporativa()
        {
            var tiposConta = new TiposConta();
            var tiposDeconta = new TiposConta();
            var tipoContaOutras = tiposDeconta.Obter<TipoConta>(1);

            string _conectionstring = @"Provider=Microsoft.Jet.OLEDB.4.0;";
            _conectionstring += String.Format("Data Source={0};", "D:\\CargaOrcamento2014\\CcXcontaCoorporativoNew.xls");
            _conectionstring += "Extended Properties='Excel 8.0;HDR=YES;IMEX=1;'";

            var cn = new OleDbConnection(_conectionstring);
            var cmd = new OleDbCommand("Select * from [ccxconta$]", cn);
            cn.Open();
            var reader = cmd.ExecuteReader();

            var documento = new List<RegistroExcel>();

            var listaContas = new List<Conta>();
            var listaCentrosDeCusto = new List<CentroDeCusto>();

            var repositorioDeHospitais = new Setores();
            var repositorioDeCusto = new CentrosDeCusto();
            var repositorioContas = new Contas();

            int i = 0;

            while (reader.Read())
            {
                if (i == 0)
                {
                    i++;
                    continue;
                }
                if (reader[0] == null || reader[0] == DBNull.Value || string.IsNullOrEmpty(reader[0].ToString()))
                    break;

                var registroExcel = new RegistroExcel()
                {
                    NomeHospital = (string)reader[6],
                    CodigoCentroDeCusto = Convert.ToString(reader[1]),
                    DescricaoCentroDeCusto = (string)reader[2]
                };

                if (reader[5] != DBNull.Value)
                {
                    registroExcel.DescricaoConta = (string)reader[4];
                    registroExcel.CodigoConta = Convert.ToString(reader[3]);
                    registroExcel.GrupoResumoNome = (string)reader[5];
                }

                documento.Add(registroExcel);
            }

            cn.Close();
            cn.Dispose();
            cmd.Dispose();

            var gruposDeConta = documento.Select(x => x.GrupoResumoNome).Distinct();


            var codigosDeConta = documento.Select(x => x.CodigoConta).Distinct();

            foreach (var codigoDeConta in codigosDeConta)
            {
                string descricaoDaConta = documento.Where(x => x.CodigoConta == codigoDeConta).Select(y => y.DescricaoConta).Distinct().First();

                var conta = repositorioContas.ObterContaPor(codigoDeConta);

                if (listaContas.All(c => c.CodigoDaConta != codigoDeConta))
                {
                    if (conta == null)
                    {
                        conta = new Conta(descricaoDaConta, tipoContaOutras)
                        {
                            CodigoDaConta = codigoDeConta
                        };
                        repositorioContas.Salvar(conta);
                    }

                    listaContas.Add(conta);
                }
                else
                    conta = listaContas.FirstOrDefault(c => c.CodigoDaConta == codigoDeConta);

            }
            var grupos = new GruposDeConta();
            var gruposDeContaLista = new List<GrupoDeConta>();
            foreach (var grupo in gruposDeConta)
            {
                var grupoDeConta = grupos.ObterPor(grupo);

                if (grupoDeConta == null)
                    grupoDeConta = new GrupoDeConta(grupo);

                var contasDoGrupo = documento.Where(x => x.GrupoResumoNome == grupo).Select(y => y.CodigoConta).Distinct();

                foreach (var codigoConta in contasDoGrupo)
                {
                    var conta = listaContas.FirstOrDefault(c => c.CodigoDaConta == codigoConta);

                    if (grupoDeConta.Contas == null)
                        grupoDeConta.Contas = new List<Conta>();

                    if (grupoDeConta.Contas.All(c => c.CodigoDaConta != codigoConta))
                        grupoDeConta.Adicionar(conta);
                }

                gruposDeContaLista.Add(grupoDeConta);
                grupos.Salvar(grupoDeConta);
            }


            var codigosDecentrosDeCusto = documento.Select(x => x.CodigoCentroDeCusto).Distinct();

            foreach (var codigoDeCentro in codigosDecentrosDeCusto)
            {
                var descricaoDeCentroDeCusto = documento.Where(x => x.CodigoCentroDeCusto == codigoDeCentro).Select(y => y.DescricaoCentroDeCusto).Distinct().First();

                var centroDeCusto = repositorioDeCusto.ObterPor(codigoDeCentro);
                if (centroDeCusto == null)
                {
                    centroDeCusto = new CentroDeCusto(descricaoDeCentroDeCusto)
                    {
                        CodigoDoCentroDeCusto = codigoDeCentro
                    };
                }

                var contas = documento.Where(x => x.CodigoCentroDeCusto == codigoDeCentro).Select(y => y.CodigoConta).Distinct();

                if (centroDeCusto.Contas == null)
                    centroDeCusto.Contas = new List<Conta>();

                foreach (var conta in contas)
                {
                    if (centroDeCusto.Contas.All(c => c.CodigoDaConta != conta))
                        centroDeCusto.AdicionarConta(listaContas.SingleOrDefault(x => x.CodigoDaConta == conta));
                }

                repositorioDeCusto.Salvar(centroDeCusto);
                listaCentrosDeCusto.Add(centroDeCusto);
            }

            var hospitais = documento.Select(x => x.NomeHospital).Distinct();

            foreach (var nomeHospital in hospitais)
            {
                var hospital = repositorioDeHospitais.ObterPor(nomeHospital) ??
                                    new Setor(nomeHospital);

                var centrosDeCusto = documento.Where(x => x.NomeHospital == hospital.Nome).Select(y => y.CodigoCentroDeCusto).Distinct();

                if (hospital.CentrosDeCusto == null)
                    hospital.CentrosDeCusto = new List<CentroDeCusto>();

                foreach (var codigoCentroCusto in centrosDeCusto)
                {
                    if (hospital.CentrosDeCusto.All(c => c.CodigoDoCentroDeCusto != codigoCentroCusto))
                        hospital.AdicionarCentroDeCusto(listaCentrosDeCusto.SingleOrDefault(x => x.CodigoDoCentroDeCusto == codigoCentroCusto));
                }
                ServicoSalvarDepartamento servico = new ServicoSalvarDepartamento();
                servico.Salvar(hospital);
                //repositorioDeHospitais.Salvar(hospital);
            }

        }

        [Test]
        public void atualizar_estrutura_hospitalar()
        {

            var tiposConta = new TiposConta();
            var tiposDeconta = new TiposConta();
            var tipoContaOutras = tiposDeconta.Obter<TipoConta>(1);

            string _conectionstring = @"Provider=Microsoft.Jet.OLEDB.4.0;";
            _conectionstring += String.Format("Data Source={0};", "D:\\CargaOrcamento2014\\DepartamentoCentroDeCustoContaHospitalarNew.xls");
            _conectionstring += "Extended Properties='Excel 8.0;HDR=YES;IMEX=1;'";

            var cn = new OleDbConnection(_conectionstring);
            var cmd = new OleDbCommand("Select * from [hospital$]", cn);
            cn.Open();
            var reader = cmd.ExecuteReader();

            var documento = new List<RegistroExcel>();

            var listaContas = new List<Conta>();
            var listaCentrosDeCusto = new List<CentroDeCusto>();

            var repositorioDeHospitais = new Hospitais();
            var repositorioDeCusto = new CentrosDeCusto();
            var repositorioContas = new Contas();

            int i = 0;

            while (reader.Read())
            {
                if (i == 0)
                {
                    i++;
                    continue;
                }
                if (reader[0] == null || reader[0] == DBNull.Value || string.IsNullOrEmpty(reader[0].ToString()))
                    break;

                var registroExcel = new RegistroExcel()
                {
                    NomeHospital = (string)reader[5],
                    CodigoCentroDeCusto = Convert.ToString(reader[0]),
                    DescricaoCentroDeCusto = (string)reader[1]
                };

                if (reader[5] != DBNull.Value)
                {
                    registroExcel.DescricaoConta = (string)reader[3];
                    registroExcel.CodigoConta = Convert.ToString(reader[2]);
                    registroExcel.GrupoResumoNome = (string)reader[4];
                }

                documento.Add(registroExcel);
            }

            cn.Close();
            cn.Dispose();
            cmd.Dispose();

            var gruposDeConta = documento.Select(x => x.GrupoResumoNome).Distinct();


            var codigosDeConta = documento.Select(x => x.CodigoConta).Distinct();

            foreach (var codigoDeConta in codigosDeConta)
            {
                string descricaoDaConta = documento.Where(x => x.CodigoConta == codigoDeConta).Select(y => y.DescricaoConta).Distinct().First();

                var conta = repositorioContas.ObterContaPor(codigoDeConta);

                if (listaContas.All(c => c.CodigoDaConta != codigoDeConta))
                {
                    if (conta == null)
                    {
                        conta = new Conta(descricaoDaConta, tipoContaOutras)
                        {
                            CodigoDaConta = codigoDeConta
                        };
                        repositorioContas.Salvar(conta);
                    }

                    listaContas.Add(conta);
                }
                else
                    conta = listaContas.FirstOrDefault(c => c.CodigoDaConta == codigoDeConta);

            }
            var grupos = new GruposDeConta();
            var gruposDeContaLista = new List<GrupoDeConta>();
            foreach (var grupo in gruposDeConta)
            {
                var grupoDeConta = grupos.ObterPor(grupo);

                if (grupoDeConta == null)
                    grupoDeConta = new GrupoDeConta(grupo);

                var contasDoGrupo = documento.Where(x => x.GrupoResumoNome == grupo).Select(y => y.CodigoConta).Distinct();

                foreach (var codigoConta in contasDoGrupo)
                {
                    var conta = listaContas.FirstOrDefault(c => c.CodigoDaConta == codigoConta);

                    if (grupoDeConta.Contas == null)
                        grupoDeConta.Contas = new List<Conta>();

                    if (grupoDeConta.Contas.All(c => c.CodigoDaConta != codigoConta))
                        grupoDeConta.Adicionar(conta);
                }

                gruposDeContaLista.Add(grupoDeConta);
                grupos.Salvar(grupoDeConta);
            }


            var codigosDecentrosDeCusto = documento.Select(x => x.CodigoCentroDeCusto).Distinct();

            foreach (var codigoDeCentro in codigosDecentrosDeCusto)
            {
                var descricaoDeCentroDeCusto = documento.Where(x => x.CodigoCentroDeCusto == codigoDeCentro).Select(y => y.DescricaoCentroDeCusto).Distinct().First();

                var centroDeCusto = repositorioDeCusto.ObterPor(codigoDeCentro);
                if (centroDeCusto == null)
                {
                    centroDeCusto = new CentroDeCusto(descricaoDeCentroDeCusto)
                    {
                        CodigoDoCentroDeCusto = codigoDeCentro
                    };
                }

                var contas = documento.Where(x => x.CodigoCentroDeCusto == codigoDeCentro).Select(y => y.CodigoConta).Distinct();

                if (centroDeCusto.Contas == null)
                    centroDeCusto.Contas = new List<Conta>();

                foreach (var conta in contas)
                {
                    if (centroDeCusto.Contas.All(c => c.CodigoDaConta != conta))
                        centroDeCusto.AdicionarConta(listaContas.SingleOrDefault(x => x.CodigoDaConta == conta));
                }

                repositorioDeCusto.Salvar(centroDeCusto);
                listaCentrosDeCusto.Add(centroDeCusto);
            }

            var hospitais = documento.Select(x => x.NomeHospital).Distinct();

            foreach (var nomeHospital in hospitais)
            {
                var hospital = repositorioDeHospitais.ObterPor(nomeHospital) ??
                                    new Hospital { Nome = nomeHospital };

                var centrosDeCusto = documento.Where(x => x.NomeHospital == hospital.Nome).Select(y => y.CodigoCentroDeCusto).Distinct();

                if (hospital.CentrosDeCusto == null)
                    hospital.CentrosDeCusto = new List<CentroDeCusto>();

                foreach (var codigoCentroCusto in centrosDeCusto)
                {
                    if (hospital.CentrosDeCusto.All(c => c.CodigoDoCentroDeCusto != codigoCentroCusto))
                        hospital.AdicionarCentroDeCusto(listaCentrosDeCusto.SingleOrDefault(x => x.CodigoDoCentroDeCusto == codigoCentroCusto));
                }

                repositorioDeHospitais.Salvar(hospital);
            }
        }

        [Test]
        public void atualizar_estrutura_hospitalar_departamentoExistentes()
        {

            var tiposConta = new TiposConta();
            var tiposDeconta = new TiposConta();
            var tipoContaOutras = tiposDeconta.Obter<TipoConta>(1);

            string _conectionstring = @"Provider=Microsoft.Jet.OLEDB.4.0;";
            _conectionstring += String.Format("Data Source={0};", "D:\\CargaOrcamento2014\\DepartamentoCentroDeCustoContaHospitalarNew.xls");
            _conectionstring += "Extended Properties='Excel 8.0;HDR=YES;IMEX=1;'";

            var cn = new OleDbConnection(_conectionstring);
            var cmd = new OleDbCommand("Select * from [c$]", cn);
            cn.Open();
            var reader = cmd.ExecuteReader();

            var documento = new List<RegistroExcel>();

            var listaContas = new List<Conta>();
            var listaCentrosDeCusto = new List<CentroDeCusto>();

            var repositorioDeHospitais = new Hospitais();
            var repositorioDeCusto = new CentrosDeCusto();
            var repositorioContas = new Contas();

            int i = 0;

            while (reader.Read())
            {
                if (i == 0)
                {
                    i++;
                    continue;
                }
                if (reader[0] == null || reader[0] == DBNull.Value || string.IsNullOrEmpty(reader[0].ToString()))
                    break;

                var registroExcel = new RegistroExcel()
                {
                    NomeHospital = (string)reader[5],
                    CodigoCentroDeCusto = Convert.ToString(reader[0]),
                    DescricaoCentroDeCusto = (string)reader[1]
                };

                if (reader[5] != DBNull.Value)
                {
                    registroExcel.DescricaoConta = (string)reader[3];
                    registroExcel.CodigoConta = Convert.ToString(reader[2]);
                    registroExcel.GrupoResumoNome = (string)reader[4];
                }

                documento.Add(registroExcel);
            }

            cn.Close();
            cn.Dispose();
            cmd.Dispose();

            var gruposDeConta = documento.Select(x => x.GrupoResumoNome).Distinct();


            var codigosDeConta = documento.Select(x => x.CodigoConta).Distinct();

            foreach (var codigoDeConta in codigosDeConta)
            {
                string descricaoDaConta = documento.Where(x => x.CodigoConta == codigoDeConta).Select(y => y.DescricaoConta).Distinct().First();

                var conta = repositorioContas.ObterContaPor(codigoDeConta);

                if (listaContas.All(c => c.CodigoDaConta != codigoDeConta))
                {
                    if (conta == null)
                    {
                        throw new Exception();

                        conta = new Conta(descricaoDaConta, tipoContaOutras)
                        {
                            CodigoDaConta = codigoDeConta
                        };
                        repositorioContas.Salvar(conta);
                    }

                    listaContas.Add(conta);
                }
                else
                    conta = listaContas.FirstOrDefault(c => c.CodigoDaConta == codigoDeConta);

            }
            var grupos = new GruposDeConta();
            var gruposDeContaLista = new List<GrupoDeConta>();
            foreach (var grupo in gruposDeConta)
            {
                var grupoDeConta = grupos.ObterPor(grupo);

                if (grupoDeConta == null)
                    throw new Exception();

                var contasDoGrupo = documento.Where(x => x.GrupoResumoNome == grupo).Select(y => y.CodigoConta).Distinct();

                foreach (var codigoConta in contasDoGrupo)
                {
                    var conta = listaContas.FirstOrDefault(c => c.CodigoDaConta == codigoConta);

                    if (grupoDeConta.Contas == null)
                        grupoDeConta.Contas = new List<Conta>();

                    if (grupoDeConta.Contas.All(c => c.CodigoDaConta != codigoConta))
                        grupoDeConta.Adicionar(conta);
                }

                gruposDeContaLista.Add(grupoDeConta);
                grupos.Salvar(grupoDeConta);
            }


            var codigosDecentrosDeCusto = documento.Select(x => x.CodigoCentroDeCusto).Distinct();

            foreach (var codigoDeCentro in codigosDecentrosDeCusto)
            {
                var descricaoDeCentroDeCusto = documento.Where(x => x.CodigoCentroDeCusto == codigoDeCentro).Select(y => y.DescricaoCentroDeCusto).Distinct().First();

                var centroDeCusto = repositorioDeCusto.ObterPor(codigoDeCentro);
                if (centroDeCusto == null)
                {
                        throw new Exception();

                    centroDeCusto = new CentroDeCusto(descricaoDeCentroDeCusto)
                    {
                        CodigoDoCentroDeCusto = codigoDeCentro
                    };
                }

                var contas = documento.Where(x => x.CodigoCentroDeCusto == codigoDeCentro).Select(y => y.CodigoConta).Distinct();

                if (centroDeCusto.Contas == null)
                    centroDeCusto.Contas = new List<Conta>();

                foreach (var conta in contas)
                {
                    if (centroDeCusto.Contas.All(c => c.CodigoDaConta != conta))
                        centroDeCusto.AdicionarConta(listaContas.SingleOrDefault(x => x.CodigoDaConta == conta));
                }

                repositorioDeCusto.Salvar(centroDeCusto);
                listaCentrosDeCusto.Add(centroDeCusto);
            }

            var hospitais = documento.Select(x => x.NomeHospital).Distinct();

            foreach (var nomeHospital in hospitais)
            {
                var hospital = repositorioDeHospitais.ObterPor(nomeHospital);
                if(hospital == null)
                    throw new Exception();

                var centrosDeCusto = documento.Where(x => x.NomeHospital == hospital.Nome).Select(y => y.CodigoCentroDeCusto).Distinct();

                if (hospital.CentrosDeCusto == null)
                    hospital.CentrosDeCusto = new List<CentroDeCusto>();

                foreach (var codigoCentroCusto in centrosDeCusto)
                {
                    if (hospital.CentrosDeCusto.All(c => c.CodigoDoCentroDeCusto != codigoCentroCusto))
                        hospital.AdicionarCentroDeCusto(listaCentrosDeCusto.SingleOrDefault(x => x.CodigoDoCentroDeCusto == codigoCentroCusto));
                }

                repositorioDeHospitais.Salvar(hospital);
            }
        }

    }
}
