using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Orcamento.Domain.ComponentesDeOrcamento.OrcamentoPessoal;
using Orcamento.Domain.DB.Mappings;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using System.Data.OleDb;
using Orcamento.Domain;
using Orcamento.Domain.DB.Repositorio;
using Orcamento.Domain.Gerenciamento;
using Orcamento.Domain.ComponentesDeOrcamento;
using Orcamento.Domain.ComponentesDeOrcamento.OrcamentoDeProducao;
using Orcamento.Domain.Servico.Pessoal;
using Orcamento.Domain.Servico.Hospitalar;

namespace Orcamento.Test.InfraStructure
{
    public class RegistroExcel
    {
        public string NomeHospital { get; set; }
        public string CodigoCentroDeCusto { get; set; }
        public string DescricaoCentroDeCusto { get; set; }
        public string CodigoConta { get; set; }
        public string DescricaoConta { get; set; }
        public string GrupoResumoNome { get; set; }
        public string Mes { get; set; }
        public long Valor { get; set; }
    }

    public class UsuarioExcel
    {
        public string Usuario { get; set; }
        public string Login { get; set; }
        public string Hospital { get; set; }
        public string CodigoCentroDeCusto { get; set; }
    }

    public class UsuarioCoorporativoExcel
    {
        public string Usuario { get; set; }
        public string Login { get; set; }
        public string NomeSetor { get; set; }
        public string CodCentro { get; set; }
        public string Descricao { get; set; }

    }

    public class CentroDeCustoContaExcel
    {
        public string Diretoria { get; set; }
        public string CodCentro { get; set; }
        public string DescricaoCentro { get; set; }
        public string DescricaoConta { get; set; }
        public string CodConta { get; set; }
        public string GrupoConta { get; set; }
    }

    public class FuncionarioExcel
    {
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

    [TestFixture]
    public class Iniciar_Com_Tudo_Que_O_Sistema_Precisa_Test
    {
        public Setor Setor { get; set; }
        public List<Conta> Contas { get; set; }
        public List<Cidade> Cidades { get; set; }
        public List<TipoTicket> Tipos { get; set; }
        public List<Ticket> TicketsViagem { get; set; }
        public List<Ticket> TicketsDiaria { get; set; }

        [Test]
        public void a_Criar_Banco_De_Dados_Por_Modelo()
        {
            try
            {
                Fluently.Configure().Database(MsSqlConfiguration.MsSql2005.ConnectionString(c => c
               .FromAppSetting("Conexao")
                ).ShowSql()).Mappings(m => m.FluentMappings.AddFromAssemblyOf<OrcamentoHospitalarMap>()).Mappings(m => m.MergeMappings())
                .ExposeConfiguration(BuildSchema).BuildSessionFactory();
            }
            catch (Exception)
            {
                throw;
            }
        }

        [Test]
        public void b_InserirTiposDeUsuario()
        {
            TipoUsuario tipoUsuarioAdministrador = new TipoUsuario { Nome = "Administrador" };
            TipoUsuario tipoUsuarioHospital = new TipoUsuario { Nome = "Hospital" };
            TipoUsuario tipoUsuarioCorporativo = new TipoUsuario { Nome = "Corporativo" };

            TipoUsuarios tipoUsuarios = new TipoUsuarios();

            tipoUsuarios.Salvar(tipoUsuarioAdministrador);
            tipoUsuarios.Salvar(tipoUsuarioHospital);
            tipoUsuarios.Salvar(tipoUsuarioCorporativo);
        }

        [Test]
        public void c_integrar_dados_do_excel_test()
        {
            var tiposConta = new TiposConta();

            var tipoContaOutras = new TipoConta { Nome = "Outras" };

            tiposConta.Adicionar(tipoContaOutras);

            string _conectionstring;
            _conectionstring = @"Provider=Microsoft.Jet.OLEDB.4.0;";
            _conectionstring += String.Format("Data Source={0};", "D:\\Hospital2.xls");
            _conectionstring += "Extended Properties='Excel 8.0;HDR=NO;'";

            OleDbConnection cn = new OleDbConnection(_conectionstring);
            OleDbCommand cmd = new OleDbCommand("Select * from [HOSPITAL x CENTRO CUSTO MOD$]", cn);
            cn.Open();
            OleDbDataReader reader = cmd.ExecuteReader();

            List<RegistroExcel> documento = new List<RegistroExcel>();

            List<Conta> listaContas = new List<Conta>();
            List<CentroDeCusto> listaCentrosDeCusto = new List<CentroDeCusto>();

            Hospitais repositorioDeHospitais = new Hospitais();
            CentrosDeCusto repositorioDeCusto = new CentrosDeCusto();
            Contas repositorioContas = new Contas();

            int i = 0;

            while (reader.Read())
            {
                if (i == 0)
                {
                    i++;
                    continue;
                }
                if (reader[0] == DBNull.Value)
                    break;

                RegistroExcel registroExcel = new RegistroExcel()
                {
                    NomeHospital = (string)reader[0],
                    CodigoCentroDeCusto = (string)reader[1],
                    DescricaoCentroDeCusto = (string)reader[2]
                };

                if (reader[3] != DBNull.Value)
                {
                    registroExcel.DescricaoConta = (string)reader[3];
                    registroExcel.CodigoConta = (string)reader[4];
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

                Conta conta;
                if (!listaContas.Any(c => c.CodigoDaConta == codigoDeConta))
                {
                    conta = new Conta(descricaoDaConta, tipoContaOutras)
                    {
                        CodigoDaConta = codigoDeConta
                    };
                    repositorioContas.Salvar(conta);
                    listaContas.Add(conta);
                }
                else
                    conta = listaContas.Where(c => c.CodigoDaConta == codigoDeConta).FirstOrDefault();

            }
            GruposDeConta grupos = new GruposDeConta();
            List<GrupoDeConta> gruposDeContaLista = new List<GrupoDeConta>();
            foreach (var grupo in gruposDeConta)
            {
                GrupoDeConta grupoDeConta = new GrupoDeConta(grupo);

                var contasDoGrupo = documento.Where(x => x.GrupoResumoNome == grupo).Select(y => y.CodigoConta).Distinct();

                foreach (var codigoConta in contasDoGrupo)
                {
                    var conta = listaContas.Where(c => c.CodigoDaConta == codigoConta).FirstOrDefault();

                    grupoDeConta.Adicionar(conta);
                }

                gruposDeContaLista.Add(grupoDeConta);
                grupos.Salvar(grupoDeConta);
            }


            var codigosDecentrosDeCusto = documento.Select(x => x.CodigoCentroDeCusto).Distinct();

            foreach (var codigoDeCentro in codigosDecentrosDeCusto)
            {
                var descricaoDeCentroDeCusto = documento.Where(x => x.CodigoCentroDeCusto == codigoDeCentro).Select(y => y.DescricaoCentroDeCusto).Distinct().First();

                CentroDeCusto centroDeCusto = new CentroDeCusto(descricaoDeCentroDeCusto)
                {
                    CodigoDoCentroDeCusto = codigoDeCentro
                };

                var contas = documento.Where(x => x.CodigoCentroDeCusto == codigoDeCentro).Select(y => y.CodigoConta).Distinct();

                foreach (var conta in contas)
                {
                    centroDeCusto.AdicionarConta(listaContas.Where(x => x.CodigoDaConta == conta).SingleOrDefault());
                }

                repositorioDeCusto.Salvar(centroDeCusto);
                listaCentrosDeCusto.Add(centroDeCusto);
            }

            var hospitais = documento.Select(x => x.NomeHospital).Distinct();

            foreach (var nomeHospital in hospitais)
            {
                Hospital hospital = new Hospital();

                hospital.Nome = nomeHospital;

                var centrosDeCusto = documento.Where(x => x.NomeHospital == hospital.Nome).Select(y => y.CodigoCentroDeCusto).Distinct();

                foreach (var codigoCentroCusto in centrosDeCusto)
                {
                    hospital.AdicionarCentroDeCusto(listaCentrosDeCusto.Where(x => x.CodigoDoCentroDeCusto == codigoCentroCusto).SingleOrDefault());
                }

                repositorioDeHospitais.Salvar(hospital);
            }


        }

        [Test]
        public void d_InserirCidadesTiposTicketETicketEViagem()
        {
            Tipos<Cidade> repositorioCidades = new Tipos<Cidade>();
            Cidade saoPaulo = new Cidade("São Paulo");
            Cidade recife = new Cidade("Recife");
            Cidade brasilia = new Cidade("Brasília");

            repositorioCidades.Salvar(saoPaulo);
            repositorioCidades.Salvar(recife);
            repositorioCidades.Salvar(brasilia);

            Cidades = new List<Cidade>();
            Cidades.Add(saoPaulo);
            Cidades.Add(recife);
            Cidades.Add(brasilia);

            InserirTipoTickets();
            InserirTickets();
            InserirViagens();
            InserirDiarias();
        }

        [Test]
        public void e_InserirHospitaisComSetoresESubSetores()
        {
            Orcamentos orcamentos = new Orcamentos();

            Departamentos repositorioDepartamentos = new Departamentos();

            var departamentos = repositorioDepartamentos.Todos<Departamento>();

            //Cirurgia
            //Procedimento
            //Procedimento
            var setorHemodinamica = new SetorHospitalar("Hemodinâmica");
            var subSetorHemodinamica = new SubSetorHospital("Hemodinâmica");
            setorHemodinamica.AdicionarSetor(subSetorHemodinamica);

            var setorOncologia = new SetorHospitalar("Oncologia");
            var subSetorOncologia = new SubSetorHospital("Oncologia");
            setorOncologia.AdicionarSetor(subSetorOncologia);

            var procedimento = new ContaHospital("Procedimendo", TipoValorContaEnum.Quantidade);

            setorHemodinamica.AdicionarConta(procedimento);
            setorOncologia.AdicionarConta(procedimento);

            var centroCirurgico = new SetorHospitalar("Centro Cirúrgico");
            var centroCirurgicoSubSetor = new SubSetorHospital("Centro Cirúrgico");
            var centroCirurgicoObstetrico = new SubSetorHospital("Centro Obstétrico");
            centroCirurgico.AdicionarSetor(centroCirurgicoSubSetor);
            centroCirurgico.AdicionarSetor(centroCirurgicoObstetrico);

            //Contas ta cirurgia
            var cirurgia = new ContaHospital("Cirurgias", TipoValorContaEnum.Quantidade);
            var salas = new ContaHospital("Salas", TipoValorContaEnum.Quantidade, false, true);
            var cirurgiaPorSala = new ContaHospital("Cirurgias por Sala", TipoValorContaEnum.Quantidade, true, false);

            centroCirurgico.AdicionarConta(cirurgia);
            centroCirurgico.AdicionarConta(salas);
            centroCirurgico.AdicionarConta(cirurgiaPorSala);

            cirurgiaPorSala.AnexarConta(cirurgia);
            cirurgiaPorSala.AnexarConta(salas);

            //UTI
            var uti = new SetorHospitalar("UTI");
            var utiSemiMaternidade = new SubSetorHospital("UTI Semi Maternidade");
            var utiAdulto = new SubSetorHospital("UTI Adulto");
            var utiPediatrica = new SubSetorHospital("Uti Pediátrica");
            var utiNeoNatal = new SubSetorHospital("Uti Neo-Natal");
            var utiCoronariana = new SubSetorHospital("Uti Coronariana");
            var semiIntensiva = new SubSetorHospital("Uti Semi-Intensiva");

            //Contas da UTI
            var leito = new ContaHospital("Leito", TipoValorContaEnum.Quantidade);
            var ocupacao = new ContaHospital("Taxa de Ocupação", TipoValorContaEnum.Porcentagem);
            leito.MultiPlicaPorMes = true;
            uti.AdicionarConta(leito);
            uti.AdicionarConta(ocupacao);

            //SubSetores da UTI
            uti.AdicionarSetor(utiSemiMaternidade);
            uti.AdicionarSetor(utiAdulto);
            uti.AdicionarSetor(utiPediatrica);
            uti.AdicionarSetor(utiNeoNatal);
            uti.AdicionarSetor(utiCoronariana);
            uti.AdicionarSetor(semiIntensiva);

            //UNI
            var uni = new SetorHospitalar("UNI");
            var uniAdulto = new SubSetorHospital("Uni Adulto");
            var uniPediatrica = new SubSetorHospital("Uni Pediátrica");
            var uniMaternidade = new SubSetorHospital("Maternidade");

            uni.AdicionarSetor(uniAdulto);
            uni.AdicionarSetor(uniPediatrica);
            uni.AdicionarSetor(uniMaternidade);

            //Contas UNI
            uni.AdicionarConta(leito);
            uni.AdicionarConta(ocupacao);

            //Conta Atendimento
            var atendimento = new ContaHospital("Atendimento", TipoValorContaEnum.Quantidade);

            //Emergencia
            var emergencia = new SetorHospitalar("Emergência");
            var subEmergenciaMaternidade = new SubSetorHospital("Emergência Maternidade");
            var subEmergenciaAdulto = new SubSetorHospital("Emergência Adulto");
            var subEmergenciaPediatrica = new SubSetorHospital("Emergência Pediátrica");
            emergencia.AdicionarSetor(subEmergenciaMaternidade);
            emergencia.AdicionarSetor(subEmergenciaAdulto);
            emergencia.AdicionarSetor(subEmergenciaPediatrica);
            emergencia.AdicionarConta(atendimento);

            //Ambulatorio
            var ambulatorio = new SetorHospitalar("Ambulatório");
            var subAmbulatorio = new SubSetorHospital("Ambulatório");
            ambulatorio.AdicionarSetor(subAmbulatorio);
            ambulatorio.AdicionarConta(atendimento);

            //SADT
            var sadt = new SetorHospitalar("SADT");
            var cardiologico = new SubSetorHospital("Cardiológico");
            var resonanciaMagnetica = new SubSetorHospital("Resonância Mag");
            var ultrassonografica = new SubSetorHospital("Ultrassonografia");
            var tomografiaCompleta = new SubSetorHospital("Tomografia Comp");
            var radiologia = new SubSetorHospital("Radiologia");
            var patologiaClinica = new SubSetorHospital("Patologia Clínica");
            var outros = new SubSetorHospital("Outros");
            var exames = new ContaHospital("Exames", TipoValorContaEnum.Quantidade);

            sadt.AdicionarSetor(cardiologico);
            sadt.AdicionarSetor(resonanciaMagnetica);
            sadt.AdicionarSetor(ultrassonografica);
            sadt.AdicionarSetor(tomografiaCompleta);
            sadt.AdicionarSetor(radiologia);
            sadt.AdicionarSetor(patologiaClinica);
            sadt.AdicionarSetor(outros);
            sadt.AdicionarConta(exames);

            foreach (var departamento in departamentos)
            {
                if (departamento.GetType() == typeof(Setor))
                    continue;

                departamento.AdicionarSetor(sadt);
                departamento.AdicionarSetor(centroCirurgico);
                departamento.AdicionarSetor(uti);
                departamento.AdicionarSetor(uni);
                departamento.AdicionarSetor(emergencia);
                departamento.AdicionarSetor(ambulatorio);
                departamento.AdicionarSetor(setorHemodinamica);
                departamento.AdicionarSetor(setorOncologia);

                Hospitais repositorio = new Hospitais();
                repositorio.Salvar(departamento);
            }
        }

        [Test]
        public void f_importar_centro_de_custo_conta_coorporativo_do_excel()
        {
            string _conectionstring;
            _conectionstring = @"Provider=Microsoft.Jet.OLEDB.4.0;";
            _conectionstring += String.Format("Data Source={0};", "D:\\Coorporativo.xls");
            _conectionstring += "Extended Properties='Excel 8.0;HDR=NO;'";

            TiposConta tiposDeconta = new TiposConta();
            var tipoContaOutras = tiposDeconta.Obter<TipoConta>(1);

            OleDbConnection cn = new OleDbConnection(_conectionstring);
            OleDbCommand cmd = new OleDbCommand("Select * from [CC x Contas$]", cn);
            cn.Open();

            OleDbDataReader reader = cmd.ExecuteReader();

            List<CentroDeCustoContaExcel> centrosCustoConta = new List<CentroDeCustoContaExcel>();

            List<Conta> listaContas = new List<Conta>();
            List<CentroDeCusto> listaCentrosDeCusto = new List<CentroDeCusto>();

            CentrosDeCusto repositorioDeCusto = new CentrosDeCusto();
            Contas repositorioContas = new Contas();

            int i = 0;

            while (reader.Read())
            {
                if (i > 1)
                {
                    if (reader[0] == DBNull.Value)
                        break;

                    CentroDeCustoContaExcel centroContaExcel = new CentroDeCustoContaExcel();

                    centroContaExcel.CodCentro = (string)reader[1];
                    centroContaExcel.DescricaoCentro = (string)reader[2];
                    centroContaExcel.DescricaoConta = (string)reader[4];
                    centroContaExcel.CodConta = (string)reader[3];
                    centroContaExcel.GrupoConta = (string)reader[5];

                    centrosCustoConta.Add(centroContaExcel);
                }
                i++;
            }

            cn.Close();
            cn.Dispose();
            cmd.Dispose();

            var codigosDeConta = centrosCustoConta.Select(x => x.CodConta).Distinct();

            foreach (var codigoDeConta in codigosDeConta)
            {
                string descricaoDaConta = centrosCustoConta.Where(x => x.CodConta == codigoDeConta).Select(y => y.DescricaoConta).Distinct().FirstOrDefault();


                Conta conta = repositorioContas.ObterContaPor(codigoDeConta);

                if (!listaContas.Any(c => c.CodigoDaConta == codigoDeConta))
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
                    conta = listaContas.Where(c => c.CodigoDaConta == codigoDeConta).FirstOrDefault();
            }

            var gruposDeConta = centrosCustoConta.Select(x => x.GrupoConta).Distinct();

            GruposDeConta grupos = new GruposDeConta();
            List<GrupoDeConta> gruposDeContaLista = new List<GrupoDeConta>();
            foreach (var grupo in gruposDeConta)
            {
                var grupoContaRecuperado = grupos.ObterPor(grupo);

                if (grupoContaRecuperado == null)
                    grupoContaRecuperado = new GrupoDeConta(grupo);

                var contasDoGrupo = centrosCustoConta.Where(x => x.GrupoConta == grupo).Select(y => y.CodConta).Distinct();

                foreach (var codigoConta in contasDoGrupo)
                {
                    var conta = listaContas.Where(c => c.CodigoDaConta == codigoConta).FirstOrDefault();

                    grupoContaRecuperado.Adicionar(conta);
                }

                gruposDeContaLista.Add(grupoContaRecuperado);
                grupos.Salvar(grupoContaRecuperado);
            }


            var codigosDecentrosDeCusto = centrosCustoConta.Select(x => x.CodCentro).Distinct();

            foreach (var codigoDeCentro in codigosDecentrosDeCusto)
            {
                var descricaoDeCentroDeCusto = centrosCustoConta.Where(x => x.CodCentro == codigoDeCentro).Select(y => y.DescricaoCentro).Distinct().First();

                var centroDeCusto = repositorioDeCusto.ObterPor(codigoDeCentro);
                if (centroDeCusto == null)
                {
                    centroDeCusto = new CentroDeCusto(descricaoDeCentroDeCusto)
                    {
                        CodigoDoCentroDeCusto = codigoDeCentro
                    };
                }
                var contas = centrosCustoConta.Where(x => x.CodCentro == codigoDeCentro).Select(y => y.CodConta).Distinct();

                foreach (var conta in contas)
                {
                    centroDeCusto.AdicionarConta(listaContas.Where(x => x.CodigoDaConta == conta).SingleOrDefault());
                }

                repositorioDeCusto.Salvar(centroDeCusto);
                listaCentrosDeCusto.Add(centroDeCusto);
            }

        }

        [Test]
        public void g_importar_usuario_do_excel()
        {
            string _conectionstring;
            _conectionstring = @"Provider=Microsoft.Jet.OLEDB.4.0;";
            _conectionstring += String.Format("Data Source={0};", "D:\\Hospital2.xls");
            _conectionstring += "Extended Properties='Excel 8.0;HDR=NO;'";

            OleDbConnection cn = new OleDbConnection(_conectionstring);
            OleDbCommand cmd = new OleDbCommand("Select * from [LISTAS - OUTRAS DESPESAS$]", cn);
            cn.Open();

            OleDbDataReader reader = cmd.ExecuteReader();

            List<UsuarioExcel> usuariosExcel = new List<UsuarioExcel>();

            int i = 0;

            while (reader.Read())
            {
                if (i > 1)
                {
                    if (reader[0] == DBNull.Value)
                        break;

                    UsuarioExcel usuarioExcel = new UsuarioExcel();

                    usuarioExcel.Usuario = (string)reader[0];
                    usuarioExcel.Login = (string)reader[1];
                    usuarioExcel.Hospital = (string)reader[3];

                    usuariosExcel.Add(usuarioExcel);
                }
                i++;
            }

            Hospitais hospitais = new Hospitais();

            TipoUsuarios tipoUsuarios = new TipoUsuarios();

            var tipoUsuario = tipoUsuarios.Obter<TipoUsuario>(2);

            var repositorioUsuarios = new Usuarios();

            foreach (var usuarioExcel in usuariosExcel)
            {
                Usuario usuario = repositorioUsuarios.ObterAcesso(usuarioExcel.Login, 123456.ToString());

                if (usuario == null)
                {
                    usuario = new Usuario();

                    usuario.TipoUsuario = tipoUsuario;

                    usuario.Nome = usuarioExcel.Usuario;

                    usuario.Login = usuarioExcel.Login;

                    usuario.Senha = "123456";
                }

                Hospital hospital = hospitais.ObterPor(usuarioExcel.Hospital);

                usuario.ParticiparDe(hospital);

                repositorioUsuarios.Salvar(usuario);
            }
        }

        [Test]
        public void h_importar_usuario_coorporativo_do_excel()
        {
            string _conectionstring;
            _conectionstring = @"Provider=Microsoft.Jet.OLEDB.4.0;";
            _conectionstring += String.Format("Data Source={0};", "D:\\Coorporativo.xls");
            _conectionstring += "Extended Properties='Excel 8.0;HDR=NO;'";

            OleDbConnection cn = new OleDbConnection(_conectionstring);
            OleDbCommand cmd = new OleDbCommand("Select * from [Usuarios$]", cn);
            cn.Open();

            OleDbDataReader reader = cmd.ExecuteReader();

            List<UsuarioCoorporativoExcel> usuariosExcel = new List<UsuarioCoorporativoExcel>();

            int i = 0;

            while (reader.Read())
            {
                if (i == 2)
                {
                    if (reader[0] == DBNull.Value)
                        break;

                    UsuarioCoorporativoExcel usuarioExcel = new UsuarioCoorporativoExcel();

                    usuarioExcel.Usuario = (string)reader[0];
                    usuarioExcel.Login = (string)reader[1];
                    usuarioExcel.NomeSetor = (string)reader[3];
                    usuarioExcel.CodCentro = (string)reader[5];
                    usuarioExcel.Descricao = (string)reader[6];

                    usuariosExcel.Add(usuarioExcel);
                }
                i++;
            }

            var novo = usuariosExcel;
            Setores setores = new Setores();
            CentrosDeCusto centrosDeCusto = new CentrosDeCusto();

            TipoUsuarios tipoUsuarios = new TipoUsuarios();

            var tipoUsuario = tipoUsuarios.Obter<TipoUsuario>(3);

            var repositorioUsuarios = new Usuarios();

            foreach (var usuarioExcel in usuariosExcel)
            {
                Usuario usuario = repositorioUsuarios.ObterAcesso(usuarioExcel.Login, 123456.ToString());

                if (usuario == null)
                {
                    usuario = new Usuario();

                    usuario.TipoUsuario = tipoUsuario;

                    usuario.Nome = usuarioExcel.Usuario;

                    usuario.Login = usuarioExcel.Login;

                    usuario.Senha = "123456";
                }

                Setor setor = setores.ObterPor(usuarioExcel.NomeSetor);
                if (setor == null)
                {
                    setor = new Setor(usuarioExcel.NomeSetor);
                }

                if (usuario.Departamentos == null || !usuario.Departamentos.Any(d => d.Nome == setor.Nome))
                    usuario.ParticiparDe(setor);


                var centroDecusto = centrosDeCusto.ObterPor(usuarioExcel.CodCentro);
                if (centroDecusto == null)
                {
                    setores.Salvar(setor);
                    centroDecusto = new CentroDeCusto(usuarioExcel.Descricao) { CodigoDoCentroDeCusto = usuarioExcel.CodCentro };
                    centrosDeCusto.Salvar(centroDecusto);
                }

                if (setor.CentrosDeCusto == null || setor.CentrosDeCusto.Count == 0 || setor.CentrosDeCusto != null && setor.CentrosDeCusto.Count > 0 && !setor.CentrosDeCusto.Any(c => c.CodigoDoCentroDeCusto == centroDecusto.CodigoDoCentroDeCusto))
                    setor.AdicionarCentroDeCusto(centroDecusto);

                //ServicoSalvarDepartamento servico = new ServicoSalvarDepartamento();
                //servico.Salvar(setor);
                setores.Salvar(setor);
                repositorioUsuarios.Salvar(usuario);
            }
        }

        [Test]
        public void i_inicializar_usuario_de_teste()
        {
            TipoUsuarios tipoUsuarios = new TipoUsuarios();

            Usuario usuario = new Usuario()
            {
                Nome = "isaac",
                Login = "isaac",
                Senha = "123456",
                TipoUsuario = tipoUsuarios.Obter<TipoUsuario>(1)
            };

            Departamentos repositorioDepartamentos = new Departamentos();

            var departamentos = repositorioDepartamentos.Todos<Departamento>();

            foreach (var departamento in departamentos)
            {
                usuario.ParticiparDe(departamento);
            }

            Usuarios usuarios = new Usuarios();

            usuarios.Salvar(usuario);
        }

        [Test]
        public void j_SalvarTickets()
        {
            InserirTicketsDeProducao();
        }

        private void BuildSchema(Configuration config)
        {
            //new SchemaExport(config)
            //    .Drop(false, false);

            new SchemaExport(config)
                .Create(true, true);
        }

        public void InserirContas(Departamento setor)
        {
            var tiposConta = new TiposConta();

            var tipoContaOutros = tiposConta.Obter<TipoConta>(1);

            var maquinario = new Conta("Maquinário", tipoContaOutros);
            var cabos = new Conta("Cabos", tipoContaOutros);

            var RecursosExternos = new Conta("Recursos Externos", tipoContaOutros);
            var HorasExtras = new Conta("Horas Extras", tipoContaOutros);
            Contas = new List<Conta>();

            Contas.Add(maquinario);
            Contas.Add(cabos);
            Contas.Add(RecursosExternos);
            Contas.Add(HorasExtras);

            var contas = new Contas();
            contas.Salvar(maquinario);
            contas.Salvar(cabos);
            contas.Salvar(RecursosExternos);
            contas.Salvar(HorasExtras);
        }

        private void InserirDiarias()
        {
            Diarias diarias = new Diarias();

            foreach (var item in Cidades)
            {
                Diaria diaria = new Diaria(item, TicketsDiaria.Where(t => t.Cidade == item).ToList());
                diarias.Salvar(diaria);
            }
        }

        private void InserirViagens()
        {
            Viagens viagens = new Viagens();
            foreach (var item in Cidades)
            {
                Viagem viagem = new Viagem(item, TicketsViagem.Where(t => t.Cidade == item).ToList());
                viagens.Salvar(viagem);
            }
        }

        public void InserirTipoTickets()
        {
            Tipos<TipoTicket> repositorioTipoTicket = new Tipos<TipoTicket>();

            TipoTicket diaria = new TipoTicket("Diária");
            TipoTicket taxi = new TipoTicket("Taxi");
            TipoTicket refeicao = new TipoTicket("Refeição");
            TipoTicket passagem = new TipoTicket("Passagem");

            repositorioTipoTicket.Salvar(diaria);
            repositorioTipoTicket.Salvar(taxi);
            repositorioTipoTicket.Salvar(refeicao);
            repositorioTipoTicket.Salvar(passagem);

            Tipos = new List<TipoTicket>();
            Tipos.Add(diaria);
            Tipos.Add(taxi);
            Tipos.Add(refeicao);
            Tipos.Add(passagem);
        }

        public void InserirTickets()
        {
            TicketsDeViagens tickets = new TicketsDeViagens();
            TicketsDiaria = new List<Ticket>();
            TicketsViagem = new List<Ticket>();

            foreach (var item in Cidades)
            {
                Ticket diaria = new Ticket(Tipos[0], item);
                Ticket taxi = new Ticket(Tipos[1], item);
                Ticket refeicao = new Ticket(Tipos[2], item);
                Ticket passagem = new Ticket(Tipos[3], item);

                tickets.Salvar(diaria);
                tickets.Salvar(taxi);
                tickets.Salvar(refeicao);
                tickets.Salvar(passagem);

                TicketsDiaria.Add(diaria);
                TicketsDiaria.Add(taxi);
                TicketsDiaria.Add(taxi);
                TicketsDiaria.Add(refeicao);
                TicketsDiaria.Add(refeicao);

                TicketsViagem.Add(passagem);
                TicketsViagem.Add(passagem);
                TicketsViagem.Add(taxi);
                TicketsViagem.Add(taxi);
                TicketsViagem.Add(taxi);
                TicketsViagem.Add(taxi);
            }
        }

        public void InserirTicketsDeProducao()
        {
            Departamentos departamentos = new Departamentos();
            var hospitais = departamentos.Todos<Hospital>();

            List<TicketDeProducao> tickets = new List<TicketDeProducao>();
            List<TicketDeReceita> ticketsDeReceita = new List<TicketDeReceita>();

            foreach (var hospital in hospitais)
            {

                ticketsDeReceita.Add(new TicketDeReceita(hospital, "Glosa Interna", TipoTicketDeReceita.GlosaInterna));
                ticketsDeReceita.Add(new TicketDeReceita(hospital, "Glosa Externa", TipoTicketDeReceita.GlosaExterna));
                ticketsDeReceita.Add(new TicketDeReceita(hospital, "Reajuste de Convênios", TipoTicketDeReceita.ReajusteDeConvenios));
                ticketsDeReceita.Add(new TicketDeReceita(hospital, "Impostos", TipoTicketDeReceita.Impostos));
                ticketsDeReceita.Add(new TicketDeReceita(hospital, "Reajustes de Insumos", TipoTicketDeReceita.ReajusteDeInsumos));
                ticketsDeReceita.Add(new TicketDeReceita(hospital, "Descontos Obtidos", TipoTicketDeReceita.Descontos));

                foreach (var setor in hospital.Setores)
                {
                    foreach (var subSetor in setor.SubSetores)
                    {
                        tickets.Add(new TicketDeProducao(setor, subSetor, hospital));
                    }
                }
            }

            departamentos.SalvarLista(tickets);
            departamentos.SalvarLista(ticketsDeReceita);
        }

        [Test]
        public void l_InserirContasEGrupoDeContasNosDepartamentos()
        {
            var tiposConta = new TiposConta();
            var tipoContaBeneficios = new TipoConta { Nome = "Beneficios" };
            tiposConta.Adicionar(tipoContaBeneficios);

            Departamentos departamentos = new Departamentos();
            var listaDepartamentos = departamentos.Todos();

            var tipoContaFGTS = new TipoConta { Nome = "FGTS" };
            var tipoContaINSS = new TipoConta { Nome = "INSS" };
            var tipoContaFerias = new TipoConta { Nome = "Férias" };
            var tipoContaIndenizacao = new TipoConta { Nome = "Indenização" };
            var tipoContaDecimoTerceiro = new TipoConta { Nome = "Décimo Terceiro" };
            var tipoContaSalario = new TipoConta { Nome = "Salário" };
            var tipoContaBolsasDeEstagio = new TipoConta { Nome = "Bolsas de Estágio" };
            var tipoContaExtras = new TipoConta { Nome = "Extras" };

            tiposConta.Adicionar(tipoContaFGTS);
            tiposConta.Adicionar(tipoContaINSS);
            tiposConta.Adicionar(tipoContaFerias);
            tiposConta.Adicionar(tipoContaIndenizacao);
            tiposConta.Adicionar(tipoContaDecimoTerceiro);
            tiposConta.Adicionar(tipoContaSalario);
            tiposConta.Adicionar(tipoContaBolsasDeEstagio);
            tiposConta.Adicionar(tipoContaExtras);
            var gruposDeConta = new GruposDeConta();
            var encargosSociais = new GrupoDeConta("Encargos Sociais");
            var remuneracao = new GrupoDeConta("Remuneração");
            var beneficios = new GrupoDeConta("Benefícios");

            gruposDeConta.Salvar(beneficios);
            gruposDeConta.Salvar(remuneracao);
            gruposDeConta.Salvar(encargosSociais);

            var contaAlimentacao = new Conta("Alimentação", tipoContaBeneficios);
            contaAlimentacao.Adicionar(TipoTicketDePessoal.Alimentação);

            var contaAssistenciaMedica = new Conta("Assistência Médica", tipoContaBeneficios);
            contaAssistenciaMedica.Adicionar(TipoTicketDePessoal.AssistenciaMedica);

            var contaOutrosBeneficios = new Conta("Outros Benefícios", tipoContaBeneficios);
            contaOutrosBeneficios.Adicionar(TipoTicketDePessoal.OutrosBeneficios);

            var contaTreinamentoPessoal = new Conta("Treinamento Pessoal", tipoContaBeneficios);
            contaTreinamentoPessoal.Adicionar(TipoTicketDePessoal.TreinamentoPessoal);

            var contaValeDeTransporte = new Conta("Vale de Transporte", tipoContaBeneficios);
            contaValeDeTransporte.Adicionar(TipoTicketDePessoal.ValeDeTransporte);

            var contaOutrasDespesas = new Conta("Outras Despesas", tipoContaBeneficios);
            contaOutrasDespesas.Adicionar(TipoTicketDePessoal.OutrasDespesas);

            var contaAssistenciaOdontologica = new Conta("Assistência Odontológica", tipoContaBeneficios);
            contaAssistenciaOdontologica.Adicionar(TipoTicketDePessoal.AssistenciaOdontologica);

            beneficios.Adicionar(contaAlimentacao);
            beneficios.Adicionar(contaAssistenciaMedica);
            beneficios.Adicionar(contaAssistenciaOdontologica);
            beneficios.Adicionar(contaOutrosBeneficios);
            beneficios.Adicionar(contaTreinamentoPessoal);
            beneficios.Adicionar(contaValeDeTransporte);
            beneficios.Adicionar(contaOutrasDespesas);

            var contaFGTS = new Conta("FGTS", tipoContaFGTS);
            contaFGTS.Adicionar(TipoTicketDePessoal.FGTS);
            encargosSociais.Adicionar(contaFGTS);

            var contaINSS = new Conta("INSS", tipoContaINSS);
            contaINSS.Adicionar(TipoTicketDePessoal.INSS);
            encargosSociais.Adicionar(contaINSS);



            var contaFerias = new Conta("Férias", tipoContaFerias);
            encargosSociais.Adicionar(contaFerias);

            var contaIndenizacao = new Conta("Indenização", tipoContaIndenizacao);
            encargosSociais.Adicionar(contaIndenizacao);

            var contaDecimoTerceiro = new Conta("Décimo Terceiro", tipoContaDecimoTerceiro);
            encargosSociais.Adicionar(contaDecimoTerceiro);

            foreach (var conta in encargosSociais.Contas)
            {
                if (conta.Nome == "Indenização")
                {
                    conta.Adicionar(TipoTicketDePessoal.Indenizacao);
                }
                else
                {
                    if (conta.Nome == "INSS" || conta.Nome == "FGTS")
                        conta.Adicionar(TipoTicketDePessoal.AdicionalDeSobreaviso);

                    conta.Adicionar(TipoTicketDePessoal.AdicionalNoturno);
                    conta.Adicionar(TipoTicketDePessoal.AdicionalDeInsalubridade);
                    conta.Adicionar(TipoTicketDePessoal.AdicionaDePericulosidade);
                    conta.Adicionar(TipoTicketDePessoal.Gratificacoes);
                    conta.Adicionar(TipoTicketDePessoal.HorasExtras);
                }
            }

            remuneracao.Adicionar(new Conta("Salário", tipoContaSalario));

            var contaBolsaDeEstagio = new Conta("Bolsas Estágio", tipoContaBolsasDeEstagio);
            contaBolsaDeEstagio.Adicionar(TipoTicketDePessoal.BolsaDeEstagio);
            remuneracao.Adicionar(contaBolsaDeEstagio);


            var contaAdicionalNoturno = new Conta("Adicional Noturno", tipoContaExtras);
            contaAdicionalNoturno.Adicionar(TipoTicketDePessoal.AdicionalNoturno);
            remuneracao.Adicionar(contaAdicionalNoturno);

            var contaPericulosidade = new Conta("Periculosidade", tipoContaExtras);
            contaPericulosidade.Adicionar(TipoTicketDePessoal.AdicionaDePericulosidade);
            remuneracao.Adicionar(contaPericulosidade);

            var contaInsalubridade = new Conta("Insalubridade", tipoContaExtras);
            contaInsalubridade.Adicionar(TipoTicketDePessoal.AdicionalDeInsalubridade);
            remuneracao.Adicionar(contaInsalubridade);

            var contaHorasExtras = new Conta("Horas Extras", tipoContaExtras);
            contaHorasExtras.Adicionar(TipoTicketDePessoal.HorasExtras);
            remuneracao.Adicionar(contaHorasExtras);

            var contaGratificacoes = new Conta("Gratificações", tipoContaExtras);
            contaGratificacoes.Adicionar(TipoTicketDePessoal.Gratificacoes);
            remuneracao.Adicionar(contaGratificacoes);

            Contas contas = new Orcamento.Domain.Contas();

            contas.Salvar(contaGratificacoes);
            contas.Salvar(contaHorasExtras);
            contas.Salvar(contaInsalubridade);
            contas.Salvar(contaPericulosidade);
            contas.Salvar(contaAdicionalNoturno);
            contas.Salvar(contaBolsaDeEstagio);
            contas.Salvar(contaDecimoTerceiro);
            contas.Salvar(contaIndenizacao);
            contas.Salvar(contaFerias);
            contas.Salvar(contaINSS);
            contas.Salvar(contaFGTS);
            contas.Salvar(contaAssistenciaOdontologica);
            contas.Salvar(contaOutrasDespesas);
            contas.Salvar(contaValeDeTransporte);
            contas.Salvar(contaTreinamentoPessoal);
            contas.Salvar(contaOutrosBeneficios);
            contas.Salvar(contaAssistenciaMedica);
            contas.Salvar(contaAlimentacao);
            contas.Salvar(contaAssistenciaMedica);
            contas.Salvar(contaAssistenciaMedica);
            contas.Salvar(contaAssistenciaMedica);
            contas.Salvar(contaAssistenciaMedica);


            TicketsDeProducao tickets = new TicketsDeProducao();
            NovosOrcamentosPessoais orcamentos = new NovosOrcamentosPessoais();

            foreach (var departamento in listaDepartamentos)
            {


                var ticketDeAlimentacao = new TicketDeOrcamentoPessoal(departamento) { Ticket = TipoTicketDePessoal.Alimentação, Descricao = "Alimentação", Valor = 300 };
                var ticketDeAssistenciaMedica = new TicketDeOrcamentoPessoal(departamento) { Ticket = TipoTicketDePessoal.AssistenciaMedica, Descricao = "Assistência Médica", Valor = 300 };
                var ticketAssistencia = new TicketDeOrcamentoPessoal(departamento) { Ticket = TipoTicketDePessoal.AssistenciaOdontologica, Descricao = "Assistência Odontológica", Valor = 50 };
                var ticketDeBeneficios = new TicketDeOrcamentoPessoal(departamento) { Ticket = TipoTicketDePessoal.OutrosBeneficios, Descricao = "Outros Benefícios", Valor = 50 };
                var ticketTreinamentoPessoal = new TicketDeOrcamentoPessoal(departamento) { Ticket = TipoTicketDePessoal.TreinamentoPessoal, Descricao = "Treinamento Pessoal", Valor = 50 };
                var ticketValeTransporte = new TicketDeOrcamentoPessoal(departamento) { Ticket = TipoTicketDePessoal.ValeDeTransporte, Descricao = "Vale de Transporte", Valor = 150 };
                var ticketDeOutrasDespesas = new TicketDeOrcamentoPessoal(departamento) { Ticket = TipoTicketDePessoal.OutrasDespesas, Descricao = "Outras Despesas", Valor = 50 };

                var adicionalNoturno = new TicketDeOrcamentoPessoal(departamento) { Ticket = TipoTicketDePessoal.AdicionalNoturno, Descricao = "Adicional Noturno", Valor = 4 };
                var insalubridade = new TicketDeOrcamentoPessoal(departamento) { Ticket = TipoTicketDePessoal.AdicionalDeInsalubridade, Descricao = "Adicional de Insalubridade", Valor = 10 };
                var periculosidade = new TicketDeOrcamentoPessoal(departamento) { Ticket = TipoTicketDePessoal.AdicionaDePericulosidade, Descricao = "Adicional de Periculosidade", Valor = 1 };
                var gratificacoes = new TicketDeOrcamentoPessoal(departamento) { Ticket = TipoTicketDePessoal.Gratificacoes, Descricao = "Gratificações", Valor = 1 };
                var horasExtras = new TicketDeOrcamentoPessoal(departamento) { Ticket = TipoTicketDePessoal.HorasExtras, Descricao = "Horas Extras", Valor = 2 };
                var sobreaviso = new TicketDeOrcamentoPessoal(departamento) { Ticket = TipoTicketDePessoal.AdicionalDeSobreaviso, Descricao = "Adicional de Sobreaviso", Valor = 0 };
                var indenizacao = new TicketDeOrcamentoPessoal(departamento) { Ticket = TipoTicketDePessoal.Indenizacao, Descricao = "Indenização", Valor = 235 };
                var bolsaDeEstagio = new TicketDeOrcamentoPessoal(departamento) { Ticket = TipoTicketDePessoal.BolsaDeEstagio, Descricao = "Bolsa de Estágio", Valor = 0 };
                var fgts = new TicketDeOrcamentoPessoal(departamento) { Ticket = TipoTicketDePessoal.FGTS, Descricao = "FGTS", Valor = 8 };
                var inss = new TicketDeOrcamentoPessoal(departamento) { Ticket = TipoTicketDePessoal.INSS, Descricao = "INSS", Valor = 28 };

                foreach (var centroDeCusto in departamento.CentrosDeCusto)
                {
                    if (!centroDeCusto.GrupoDeContas.Any(g => g.Id == beneficios.Id))
                        centroDeCusto.Adicionar(beneficios);

                    if (!centroDeCusto.GrupoDeContas.Any(g => g.Id == remuneracao.Id))
                        centroDeCusto.Adicionar(remuneracao);

                    if (!centroDeCusto.GrupoDeContas.Any(g => g.Id == encargosSociais.Id))
                        centroDeCusto.Adicionar(encargosSociais);
                }

                tickets.Salvar(ticketDeAlimentacao);
                tickets.Salvar(ticketDeAssistenciaMedica);
                tickets.Salvar(ticketAssistencia);
                tickets.Salvar(ticketDeBeneficios);
                tickets.Salvar(ticketTreinamentoPessoal);
                tickets.Salvar(ticketValeTransporte);
                tickets.Salvar(ticketDeOutrasDespesas);

                tickets.Salvar(adicionalNoturno);
                tickets.Salvar(insalubridade);
                tickets.Salvar(periculosidade);
                tickets.Salvar(gratificacoes);
                tickets.Salvar(horasExtras);
                tickets.Salvar(sobreaviso);
                tickets.Salvar(indenizacao);
                tickets.Salvar(bolsaDeEstagio);
                tickets.Salvar(fgts);
                tickets.Salvar(inss);

                List<NovoOrcamentoPessoal> orcamentosPessoais = new List<NovoOrcamentoPessoal>();

                foreach (var centroDeCusto in departamento.CentrosDeCusto)
                {
                    var orcamento = new NovoOrcamentoPessoal(departamento, centroDeCusto, 2014);

                    orcamento.Adicionar(ticketDeAlimentacao);
                    orcamento.Adicionar(ticketDeAssistenciaMedica);
                    orcamento.Adicionar(ticketAssistencia);
                    orcamento.Adicionar(ticketDeBeneficios);
                    orcamento.Adicionar(ticketTreinamentoPessoal);
                    orcamento.Adicionar(ticketValeTransporte);
                    orcamento.Adicionar(ticketDeOutrasDespesas);

                    orcamento.Adicionar(adicionalNoturno);
                    orcamento.Adicionar(insalubridade);
                    orcamento.Adicionar(periculosidade);
                    orcamento.Adicionar(gratificacoes);
                    orcamento.Adicionar(horasExtras);
                    orcamento.Adicionar(sobreaviso);
                    orcamento.Adicionar(indenizacao);
                    orcamento.Adicionar(bolsaDeEstagio);
                    orcamento.Adicionar(fgts);
                    orcamento.Adicionar(inss);


                    orcamentosPessoais.Add(orcamento);
                }


                orcamentos.SalvarLista(orcamentosPessoais);
                departamentos.Salvar(departamento);
            }
        }

        [Test]
        public void m_Inserir_tickets_de_producao()
        {
            Departamentos departamentos = new Departamentos();
            var hospitais = departamentos.Todos<Hospital>();
            Insumos insumos = new Insumos();

            List<Insumo> ListaInsumos = new List<Insumo>();

            foreach (var hospital in hospitais)
            {
                var insumo = new Insumo(hospital);
                ListaInsumos.Add(insumo);
            }

            insumos.SalvarLista(ListaInsumos);

        }

        [Test]
        public void n_Inserir_acordoConvencao_no_departamento()
        {
            Departamentos departamentos = new Departamentos();
            AcordosDeConvencao acordos = new AcordosDeConvencao();
            var tdosDepartamentos = departamentos.Todos();

            List<AcordoDeConvencao> ListaAcordos = new List<AcordoDeConvencao>();
            foreach (var departamento in tdosDepartamentos)
            {
                AcordoDeConvencao acordo = new AcordoDeConvencao(departamento, 0, 0);
                ListaAcordos.Add(acordo);
            }

            acordos.SalvarLista(ListaAcordos);

        }

        private ServicoGerarOrcamentoPessoalPorCentroDeCusto _servicoGerarOrcamentoPessoalPor;
        public ServicoGerarOrcamentoPessoalPorCentroDeCusto ServicoGerarOrcamentoPessoalPor
        {
            get { return _servicoGerarOrcamentoPessoalPor ?? (_servicoGerarOrcamentoPessoalPor = new ServicoGerarOrcamentoPessoalPorCentroDeCusto()); }
            set { _servicoGerarOrcamentoPessoalPor = value; }
        }

        [Test]
        [Ignore]
        public void o_importar_funcionarios_coorporativo_do_excel()
        {
            string _conectionstring;
            _conectionstring = @"Provider=Microsoft.Jet.OLEDB.4.0;";
            _conectionstring += String.Format("Data Source={0};", "D:\\FuncionariosCoorporativo.xls");
            _conectionstring += "Extended Properties='Excel 8.0;HDR=NO;'";

            OleDbConnection cn = new OleDbConnection(_conectionstring);
            OleDbCommand cmd = new OleDbCommand("Select * from [Coorporativo$]", cn);
            cn.Open();

            OleDbDataReader reader = cmd.ExecuteReader();

            List<FuncionarioExcel> funcionarios = new List<FuncionarioExcel>();

            int i = 0;

            while (reader.Read())
            {
                if (i > 0)
                {
                    if (reader[0] == DBNull.Value)
                        break;

                    FuncionarioExcel funcionarioExcel = new FuncionarioExcel();

                    funcionarioExcel.Departamento = (string)reader[1];
                    funcionarioExcel.CodigoCentroDeCusto = (string)reader[2];
                    funcionarioExcel.NumeroMatricula = (string)reader[5];
                    funcionarioExcel.Nome = (string)reader[6];
                    funcionarioExcel.Funcao = (string)reader[7];
                    funcionarioExcel.Salario = (double)reader[8];
                    funcionarioExcel.Mes = (int)(double)reader[10];
                    funcionarioExcel.Ano = (int)(double)reader[11];

                    funcionarios.Add(funcionarioExcel);
                }
                i++;
            }

            Setores setores = new Setores();
            CentrosDeCusto centrosDeCusto = new CentrosDeCusto();
            List<Departamento> departamentos = new List<Departamento>();
            List<CentroDeCusto> centros = new List<CentroDeCusto>();

            foreach (var funcionarioExcel in funcionarios)
            {
                if (!departamentos.Any(d => d.Nome == funcionarioExcel.Departamento))
                    departamentos.Add(setores.ObterPor(funcionarioExcel.Departamento));

                if (!centros.Any(d => d.CodigoDoCentroDeCusto == funcionarioExcel.CodigoCentroDeCusto))
                    centros.Add(centrosDeCusto.ObterPor(funcionarioExcel.CodigoCentroDeCusto));


                var setor = departamentos.Where(d => d.Nome == funcionarioExcel.Departamento).FirstOrDefault();
                var centro = centros.Where(d => d.CodigoDoCentroDeCusto == funcionarioExcel.CodigoCentroDeCusto).FirstOrDefault();
                var funcionario = new Funcionario(setor)
                {
                    AnoAdmissao = funcionarioExcel.Ano,
                    Cargo = funcionarioExcel.Funcao,
                    DataAdmissao = funcionarioExcel.Mes,
                    Matricula = funcionarioExcel.NumeroMatricula,
                    Nome = funcionarioExcel.Nome,
                    Salario = funcionarioExcel.Salario
                };

                centro.Adicionar(funcionario);
            }

            centrosDeCusto.SalvarLista(centros);
        }

        [Test]
        //   [Ignore]
        public void p_importar_funcionarios_hospitalar_do_excel()
        {
            string _conectionstring;
            _conectionstring = @"Provider=Microsoft.Jet.OLEDB.4.0;";
            _conectionstring += String.Format("Data Source={0};", "D:\\Funcionarios.xls");
            _conectionstring += "Extended Properties='Excel 8.0;HDR=NO;'";

            OleDbConnection cn = new OleDbConnection(_conectionstring);
            OleDbCommand cmd = new OleDbCommand("Select * from [Hospitalar$]", cn);
            cn.Open();

            OleDbDataReader reader = cmd.ExecuteReader();

            List<FuncionarioExcel> funcionarios = new List<FuncionarioExcel>();

            int i = 0;

            while (reader.Read())
            {
                if (i > 1)
                {
                    if (reader[0] == DBNull.Value)
                        break;

                    FuncionarioExcel funcionarioExcel = new FuncionarioExcel();

                    funcionarioExcel.Departamento = (string)reader[1];
                    funcionarioExcel.CodigoCentroDeCusto = (string)reader[2];
                    funcionarioExcel.NumeroMatricula = (string)reader[5];
                    funcionarioExcel.Nome = (string)reader[6];
                    funcionarioExcel.Funcao = (string)reader[7];
                    funcionarioExcel.Salario = (double)reader[8];
                    funcionarioExcel.Mes = (int)(double)reader[10];
                    funcionarioExcel.Ano = (int)(double)reader[11];

                    funcionarios.Add(funcionarioExcel);
                }
                i++;
            }

            Departamentos setores = new Departamentos();
            CentrosDeCusto centrosDeCusto = new CentrosDeCusto();
            List<Departamento> departamentos = new List<Departamento>();
            List<CentroDeCusto> centros = new List<CentroDeCusto>();

            foreach (var funcionarioExcel in funcionarios)
            {
                if (!departamentos.Any(d => d.Nome == funcionarioExcel.Departamento))
                    departamentos.Add(setores.ObterPor(funcionarioExcel.Departamento));

                if (!centros.Any(d => d.CodigoDoCentroDeCusto == funcionarioExcel.CodigoCentroDeCusto))
                {
                    var centroDeCusto = centrosDeCusto.ObterPor(funcionarioExcel.CodigoCentroDeCusto);
                    if (centroDeCusto != null)
                        centros.Add(centroDeCusto);
                }



                var setor = departamentos.Where(d => d.Nome == funcionarioExcel.Departamento).FirstOrDefault();

                var centro = centros.FirstOrDefault(d => d.CodigoDoCentroDeCusto == funcionarioExcel.CodigoCentroDeCusto);

                if (centro == null)
                    continue;
                var funcionario = new Funcionario(setor)
                {
                    AnoAdmissao = funcionarioExcel.Ano,
                    Cargo = funcionarioExcel.Funcao,
                    DataAdmissao = funcionarioExcel.Mes,
                    Matricula = funcionarioExcel.NumeroMatricula,
                    Nome = funcionarioExcel.Nome,
                    Salario = funcionarioExcel.Salario
                };
                if (centro != null)
                    centro.Adicionar(funcionario);
            }


            centrosDeCusto.SalvarLista(centros);
        }

        [Test]
        //[Ignore]
        public void q_Inserir_DREDe2012()
        {
            Departamentos departamentos = new Departamentos();
            var hospitais = departamentos.Todos<Hospital>();

            List<DRETotal> dreTotal = new List<DRETotal>();
            foreach (var hospital in hospitais)
            {
                DRETotal dre = new DRETotal(hospital);

                dreTotal.Add(dre);
            }

            DRES dres = new DRES();
            dres.SalvarLista(dreTotal);
        }

        [Test]
        // [Ignore]
        public void r_insetir_setor_bercario()
        {
            ContasHospitalares contas = new ContasHospitalares();
            var leito = contas.ObterContaPor("Leito");
            var taxa = contas.ObterContaPor("Taxa de Ocupação");

            var departamentos = new Departamentos();
            var todos = departamentos.Todos<Hospital>();

            var bercario = new SetorHospitalar("Berçário");
            var bercarioAltoRisco = new SubSetorHospital("Berçário Alto Risco");
            var bercarioSemiIntensiva = new SubSetorHospital("Berçário Semi-intensiva");

            bercario.AdicionarSetor(bercarioAltoRisco);
            bercario.AdicionarSetor(bercarioSemiIntensiva);
            bercario.AdicionarConta(leito);
            bercario.AdicionarConta(taxa);

            Insumos insumos = new Insumos();

            foreach (var departamento in todos)
            {
                departamento.AdicionarSetor(bercario);
                departamentos.Salvar(departamento);

                var insumo = insumos.ObterInsumo(departamento);
                insumo.CriarCustoUnitarioHospitalarPor(bercario);
                insumos.Salvar(insumo);

                foreach (var subsetor in bercario.SubSetores)
                {
                    var ticket = new TicketDeProducao(bercario, subsetor, departamento);

                    insumos.Salvar(ticket);
                }
            }

        }

        [Test]
        [Ignore]
        public void InserirTIcketDePEssoalDepartamento()
        {
            Departamentos departamentos = new Departamentos();
            var departamneto = departamentos.Obter(298);

            ServicoSalvarDepartamento servico = new ServicoSalvarDepartamento();

            servico.Salvar(departamneto);
        }

        [Test]
        [Ignore]
        public void s_inserir_novo_centro_de_custo_hospitalar()
        {
            var tiposConta = new TiposConta();

            var tipoContaOutras = new TipoConta { Nome = "Outras" };

            tiposConta.Adicionar(tipoContaOutras);

            string _conectionstring;
            _conectionstring = @"Provider=Microsoft.Jet.OLEDB.4.0;";
            _conectionstring += String.Format("Data Source={0};", "D:\\CentroDeCustoHospitalarOutro.xls");
            _conectionstring += "Extended Properties='Excel 8.0;HDR=NO;'";

            OleDbConnection cn = new OleDbConnection(_conectionstring);
            OleDbCommand cmd = new OleDbCommand("Select * from [centro$]", cn);
            cn.Open();
            OleDbDataReader reader = cmd.ExecuteReader();

            List<RegistroExcel> documento = new List<RegistroExcel>();

            List<Conta> listaContas = new List<Conta>();
            List<CentroDeCusto> listaCentrosDeCusto = new List<CentroDeCusto>();

            Hospitais repositorioDeHospitais = new Hospitais();
            CentrosDeCusto repositorioDeCusto = new CentrosDeCusto();
            Contas repositorioContas = new Contas();

            int i = 0;

            while (reader.Read())
            {
                if (i < 2)
                {
                    i++;
                    continue;
                }
                if (reader[0] == DBNull.Value)
                    break;


                RegistroExcel registroExcel = new RegistroExcel()
                {
                    NomeHospital = (string)reader[0],
                    CodigoCentroDeCusto = (string)reader[1],
                    DescricaoCentroDeCusto = (string)reader[2]
                };

                if (reader[3] != DBNull.Value)
                {
                    registroExcel.DescricaoConta = (string)reader[3];
                    registroExcel.CodigoConta = (string)reader[4];
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

                if (conta == null)
                    Assert.Fail();

                listaContas.Add(conta);
            }

            var codigosDecentrosDeCusto = documento.Select(x => x.CodigoCentroDeCusto).Distinct();

            foreach (var codigoDeCentro in codigosDecentrosDeCusto)
            {
                var descricaoDeCentroDeCusto = documento.Where(x => x.CodigoCentroDeCusto == codigoDeCentro).Select(y => y.DescricaoCentroDeCusto).Distinct().First();

                CentroDeCusto centroDeCusto = new CentroDeCusto(descricaoDeCentroDeCusto)
                {
                    CodigoDoCentroDeCusto = codigoDeCentro
                };

                var contas = documento.Where(x => x.CodigoCentroDeCusto == codigoDeCentro).Select(y => y.CodigoConta).Distinct();

                foreach (var conta in contas)
                {
                    centroDeCusto.AdicionarConta(listaContas.Where(x => x.CodigoDaConta == conta).SingleOrDefault());
                }

                repositorioDeCusto.Salvar(centroDeCusto);
                listaCentrosDeCusto.Add(centroDeCusto);
            }

            var hospitais = documento.Select(x => x.NomeHospital).Distinct();

            foreach (var nomeHospital in hospitais)
            {
                Hospital hospital = repositorioDeHospitais.ObterPor(nomeHospital);

                if (hospital == null)
                    Assert.Fail();

                var centrosDeCusto = documento.Where(x => x.NomeHospital == hospital.Nome).Select(y => y.CodigoCentroDeCusto).Distinct();

                foreach (var codigoCentroCusto in centrosDeCusto)
                {
                    hospital.AdicionarCentroDeCusto(listaCentrosDeCusto.Where(x => x.CodigoDoCentroDeCusto == codigoCentroCusto).SingleOrDefault());
                }

                repositorioDeHospitais.Salvar(hospital);
            }

        }

        [Test]
        // [Ignore]
        public void t_amarrar_Pessoal_ao_centro_de_custo()
        {
            var departamentos = new Departamentos();
            var todos = departamentos.Todos<Hospital>();

            NovosOrcamentosPessoais orcamentos = new NovosOrcamentosPessoais();
            GruposDeConta grupos = new GruposDeConta();

            var encargosSociais = grupos.ObterPor("Encargos Sociais");
            var remuneracao = grupos.ObterPor("Remuneração");
            var beneficios = grupos.ObterPor("Benefícios");

            List<NovoOrcamentoPessoal> orcamentosPessoais = new List<NovoOrcamentoPessoal>();
            TicketsDeOrcamentoPessoal tickets = new TicketsDeOrcamentoPessoal();


            foreach (var departamento in todos)
            {

                foreach (var centroDeCusto in departamento.CentrosDeCusto)
                {
                    if (!centroDeCusto.GrupoDeContas.Any(g => g.Id == beneficios.Id))
                        centroDeCusto.Adicionar(beneficios);

                    if (!centroDeCusto.GrupoDeContas.Any(g => g.Id == remuneracao.Id))
                        centroDeCusto.Adicionar(remuneracao);

                    if (!centroDeCusto.GrupoDeContas.Any(g => g.Id == encargosSociais.Id))
                        centroDeCusto.Adicionar(encargosSociais);

                    var ticketsDepartamento = tickets.Todos(departamento);

                    var orcamento = new NovoOrcamentoPessoal(departamento, centroDeCusto, 2014);

                    foreach (var ticket in ticketsDepartamento)
                    {
                        orcamento.Adicionar(ticket);
                    }


                    orcamentosPessoais.Add(orcamento);
                }

            }

            orcamentos.SalvarLista(orcamentosPessoais);


        }

        [Test]
        [Ignore]
        public void z_deletar_Orcamentos_coorporativo()
        {
            Orcamentos orcamentos = new Orcamentos();
            Departamentos departamentos = new Departamentos();
            Usuarios usuarios = new Usuarios();

            var todosSetores = departamentos.Todos<Setor>();
            List<Orcamento.Domain.Orcamento> todosCoorporativos = new List<Orcamento.Domain.Orcamento>();
            foreach (var setor in todosSetores)
            {
                var todos = orcamentos.TodosPor(setor);

                if (todos != null && todos.Count > 0)
                    todosCoorporativos.AddRange(todos);
            }

            orcamentos.Deletar(todosCoorporativos);


        }

        [Test]
        [Ignore]
        public void z_deletar_Orcamento_pessoal_Coorporativo()
        {
            NovosOrcamentosPessoais orcamentos = new NovosOrcamentosPessoais();

            var todos = orcamentos.TodosCoorporativo();
            orcamentos.Deletar(todos);


        }

        [Test]
        [Ignore]
        public void Deletar_todos_Ticket_de_pessoal()
        {
            TicketsDeOrcamentoPessoal tickets = new TicketsDeOrcamentoPessoal();
            Departamentos departamentos = new Departamentos();
            var todosSetores = departamentos.Todos<Setor>();

            List<TicketDeOrcamentoPessoal> todosTickets = new List<TicketDeOrcamentoPessoal>();
            foreach (var setor in todosSetores)
            {
                var ticketsRecuperados = tickets.Todos(setor);
                if (ticketsRecuperados != null && ticketsRecuperados.Count > 0)
                    todosTickets.AddRange(ticketsRecuperados);
            }

            tickets.Deletar(todosTickets);
        }

        [Test]
        [Ignore]
        public void Deletar_DRE_TOtal()
        {
            DRES dres = new DRES();
            var todos = dres.Todos<DRETotal>();

            var filtrados = todos.Where(t => t.Departamento.Tipo == TipoDepartamento.setor).ToList();

            dres.Deletar(filtrados);

        }

        [Test]
        [Ignore]
        public void z_deletar_usuarios_coorporativo()
        {
            Usuarios usuarios = new Usuarios();
            TipoUsuarios tipoUsuarios = new TipoUsuarios();

            var tipoUsuario = tipoUsuarios.Obter<TipoUsuario>(3);

            var todos = usuarios.TodosPor(tipoUsuario);

            usuarios.Deletar(todos);
        }

        [Test]
        [Ignore]
        public void Deletar_Departamento_centro_deCusto_coorporativo()
        {
            Departamentos departamentos = new Departamentos();
            var setores = departamentos.Todos<Setor>().ToList();
            List<CentroDeCusto> centros = new List<CentroDeCusto>();
            CentrosDeCusto repositorioCentro = new CentrosDeCusto();
            Funcionarios repositoriofuncionarios = new Funcionarios();

            //foreach (var setor in setores)
            //{
            //    var centrosRecuperados = setor.CentrosDeCusto;
            //    if (centrosRecuperados != null && centrosRecuperados.Count > 1)
            //        centros.AddRange(centrosRecuperados);
            //}



            departamentos.Deletar(setores);

            // repositorioCentro.Deletar(centros);

        }

        [Test]
        [Ignore]
        public void z_atribuirDepartamentos_para_Adm()
        {
            TipoUsuarios tipoUsuarios = new TipoUsuarios();


            Departamentos repositorioDepartamentos = new Departamentos();

            Usuarios usuarios = new Usuarios();
            var todos = usuarios.TodosPor(tipoUsuarios.Obter<TipoUsuario>(1));

            var departamentos = repositorioDepartamentos.Todos<Setor>();

            foreach (var usuario in todos)
            {
                foreach (var departamento in departamentos)
                {
                    usuario.ParticiparDe(departamento);
                }

                usuarios.Salvar(usuario);
            }


        }

        [Test]
        [Ignore]
        public void inserirTcketsParaOCrianca()
        {
            ServicoSalvarDepartamento servico = new ServicoSalvarDepartamento();

            Departamentos departamentos = new Departamentos();

            var departamento = departamentos.Obter(24);

            servico.InserirTicketsDePessoal(departamento);
        }

        [Test]
      //  [Ignore]
        public void InserirBercarioParaOrcamentosAntigos()
        {
            Setores setores = new Setores();


            var bercario = setores.Obter<SetorHospitalar>(9);

            var subAltoRisco = setores.Obter<SubSetorHospital>(25);
            var subSemiIntencivo = setores.Obter<SubSetorHospital>(26);

            var leito = setores.Obter<ContaHospital>(5);
            var ocupacao = setores.Obter<ContaHospital>(6);

            var orcamentos = new Orcamentos();

            var orcamentoHospitalar = orcamentos.Obter<OrcamentoHospitalar>(1);


            orcamentoHospitalar.CriarServico(leito, subAltoRisco, bercario);
            orcamentoHospitalar.CriarServico(ocupacao, subAltoRisco, bercario);
            orcamentoHospitalar.CriarServico(leito, subSemiIntencivo, bercario);
            orcamentoHospitalar.CriarServico(ocupacao, subSemiIntencivo, bercario);

            orcamentoHospitalar.CriarFatorReceita(subSemiIntencivo, bercario);
            orcamentoHospitalar.CriarFatorReceita(subAltoRisco, bercario);

            orcamentos.Salvar(orcamentoHospitalar);

        }

        [Test]
        [Ignore]
        public void s_inserir_novo_centro_de_custo_com_usuarios_coorporativo()
        {
            var tiposConta = new TiposConta();

            var tipoContaOutras = new TipoConta { Nome = "Outras" };

            tiposConta.Adicionar(tipoContaOutras);

            string _conectionstring;
            _conectionstring = @"Provider=Microsoft.Jet.OLEDB.4.0;";
            _conectionstring += String.Format("Data Source={0};", "D:\\UsuarioCentroCustoCoorporativo.xls");
            _conectionstring += "Extended Properties='Excel 8.0;HDR=NO;'";

            OleDbConnection cn = new OleDbConnection(_conectionstring);
            OleDbCommand cmd = new OleDbCommand("Select * from [Centros$]", cn);
            cn.Open();
            OleDbDataReader reader = cmd.ExecuteReader();

            List<RegistroExcel> documento = new List<RegistroExcel>();

            List<Conta> listaContas = new List<Conta>();
            List<CentroDeCusto> listaCentrosDeCusto = new List<CentroDeCusto>();

            Departamentos repositorioDepartamento = new Departamentos();
            CentrosDeCusto repositorioDeCusto = new CentrosDeCusto();
            Contas repositorioContas = new Contas();

            int i = 0;

            while (reader.Read())
            {
                if (i < 2)
                {
                    i++;
                    continue;
                }
                if (reader[0] == DBNull.Value)
                    break;


                RegistroExcel registroExcel = new RegistroExcel()
                {
                    NomeHospital = (string)reader[0],
                    CodigoCentroDeCusto = (string)reader[1],
                    DescricaoCentroDeCusto = (string)reader[2]
                };

                if (reader[3] != DBNull.Value)
                {
                    registroExcel.DescricaoConta = (string)reader[4];
                    registroExcel.CodigoConta = (string)reader[3];
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

                if (conta == null)
                    Assert.Fail();

                listaContas.Add(conta);
            }

            var codigosDecentrosDeCusto = documento.Select(x => x.CodigoCentroDeCusto).Distinct();

            foreach (var codigoDeCentro in codigosDecentrosDeCusto)
            {
                var descricaoDeCentroDeCusto = documento.Where(x => x.CodigoCentroDeCusto == codigoDeCentro).Select(y => y.DescricaoCentroDeCusto).Distinct().First();

                var centro = repositorioDeCusto.ObterPor(codigoDeCentro);

                if (centro != null)
                    Assert.Fail();

                CentroDeCusto centroDeCusto = new CentroDeCusto(descricaoDeCentroDeCusto)
                {
                    CodigoDoCentroDeCusto = codigoDeCentro
                };

                var contas = documento.Where(x => x.CodigoCentroDeCusto == codigoDeCentro).Select(y => y.CodigoConta).Distinct();

                foreach (var conta in contas)
                {
                    centroDeCusto.AdicionarConta(listaContas.Where(x => x.CodigoDaConta == conta).SingleOrDefault());
                }

                repositorioDeCusto.Salvar(centroDeCusto);
                listaCentrosDeCusto.Add(centroDeCusto);
            }

            var hospitais = documento.Select(x => x.NomeHospital).Distinct();

            foreach (var nomeHospital in hospitais)
            {
                Departamento departamento = repositorioDepartamento.ObterPor(nomeHospital);

                if (departamento == null)
                    Assert.Fail();

                var centrosDeCusto = documento.Where(x => x.NomeHospital == departamento.Nome).Select(y => y.CodigoCentroDeCusto).Distinct();

                foreach (var codigoCentroCusto in centrosDeCusto)
                {
                    departamento.AdicionarCentroDeCusto(listaCentrosDeCusto.Where(x => x.CodigoDoCentroDeCusto == codigoCentroCusto).SingleOrDefault());
                }

                repositorioDepartamento.Salvar(departamento);
            }

        }

        [Test]
        [Ignore]
        public void DeletarCentrosDeCusto()
        {
            string _conectionstring;
            _conectionstring = @"Provider=Microsoft.Jet.OLEDB.4.0;";
            _conectionstring += String.Format("Data Source={0};", "D:\\Exclussao.xls");
            _conectionstring += "Extended Properties='Excel 8.0;HDR=NO;'";

            OleDbConnection cn = new OleDbConnection(_conectionstring);
            OleDbCommand cmd = new OleDbCommand("Select * from [exclusao$]", cn);
            cn.Open();

            OleDbDataReader reader = cmd.ExecuteReader();

            List<UsuarioExcel> usuariosExcel = new List<UsuarioExcel>();

            int i = 0;

            while (reader.Read())
            {
                if (i > 1)
                {
                    if (reader[5] == DBNull.Value)
                        break;

                    UsuarioExcel usuarioExcel = new UsuarioExcel();

                    if (reader[0] != DBNull.Value)
                        usuarioExcel.Usuario = (string)reader[0];
                    if (reader[1] != DBNull.Value)
                        usuarioExcel.Login = (string)reader[1];
                    if (reader[3] != DBNull.Value)
                        usuarioExcel.Hospital = (string)reader[3];


                    usuarioExcel.CodigoCentroDeCusto = (string)reader[5];

                    usuariosExcel.Add(usuarioExcel);
                }
                i++;
            }

            cn.Close();
            cn.Dispose();
            cmd.Dispose();

            var departamentos = usuariosExcel.Select(x => x.Hospital).Distinct();
            Departamentos repositorioDepartamento = new Departamentos();
            List<Departamento> listaDepartamentos = new List<Departamento>();

            foreach (var nome in departamentos.Where(d => d != null))
            {


                var departamento = repositorioDepartamento.ObterPor(nome);

                if (departamento == null)
                    Assert.Fail("Departamento não econtrado");

                listaDepartamentos.Add(departamento);

                //TicketsDeOrcamentoPessoal tickets = new TicketsDeOrcamentoPessoal();
                //var todosTickets = tickets.Todos(departamento);

                //TicketsDeProducao ticketis = new TicketsDeProducao();
                //var ticketsDeProducao = ticketis.Todos(departamento);

                //ticketis.Deletar(ticketsDeProducao);

                //var ticketsDeReceita = new TicketsDeReceita();
                //var listaTIcketsDeReceita = ticketsDeReceita.Todos(departamento);

                //ticketsDeReceita.Deletar(listaTIcketsDeReceita);

                //tickets.Deletar(todosTickets);


                Usuarios usuarios = new Usuarios();
                var ListaUsuarios = usuarios.Todos<Usuario>();

                foreach (var usuario in ListaUsuarios)
                {
                    if (usuario.Departamentos != null && usuario.Departamentos.Count > 0 && usuario.Departamentos.Any(d => d.Id == departamento.Id))
                    {
                        usuario.Departamentos.Remove(departamento);
                        usuarios.Salvar(usuario);
                    }

                }

                //Orcamentos orcamentos = new Orcamentos();
                //var listaOrcamentos = orcamentos.TodosPor(departamento);

                //orcamentos.Deletar(listaOrcamentos);

                //departamento.CentrosDeCusto = null;

                //repositorioDepartamento.Salvar(departamento);

                //DRES dres = new DRES();
                //var dre = dres.Obter(departamento);
                //dres.Deletar(dre);

                //AcordosDeConvencao acordos = new AcordosDeConvencao();
                //var acordoDeConvencao = acordos.ObterAcordoDeConvencao(departamento);
                //acordos.Deletar(acordoDeConvencao);

                //Insumos insumos = new Insumos();
                //var insumo = insumos.ObterInsumo(departamento);
                //insumos.Deletar(insumo);

                //NovosOrcamentosPessoais novosOrcamentos = new NovosOrcamentosPessoais();
                //var orcamentosPessoal = novosOrcamentos.Todos(departamento.Id);
                //novosOrcamentos.Deletar(orcamentosPessoal.ToList());

            }


            //var codigosDecentrosDeCusto = usuariosExcel.Select(x => x.CodigoCentroDeCusto).Distinct();
            //CentrosDeCusto centros = new CentrosDeCusto();
            //List<CentroDeCusto> centrosDeCusto = new List<CentroDeCusto>();
            //List<NovoOrcamentoPessoal> ListaNovosOrcamentos = new List<NovoOrcamentoPessoal>();
            //foreach (var codigoDeCentro in codigosDecentrosDeCusto)
            //{

            //    var centroDeCusto = centros.ObterPor(codigoDeCentro);
            //    if (centroDeCusto == null)
            //        Assert.Fail("Centro de Custo não encontrado");

            //    if (centroDeCusto.Funcionarios != null && centroDeCusto.Funcionarios.Count > 0 && centroDeCusto.Funcionarios.All(d => listaDepartamentos.Any(di => di.Id == d.Departamento.Id)))
            //    {
            //        centroDeCusto.Funcionarios = null;
            //        centros.Salvar(centroDeCusto);
            //    }

            //    if (centroDeCusto.Funcionarios != null && centroDeCusto.Funcionarios.Count > 0)
            //        Assert.Fail("Existe Funcionarios neste centro");

            //    centrosDeCusto.Add(centroDeCusto);

            //    NovosOrcamentosPessoais novosOrcamentos = new NovosOrcamentosPessoais();
            //    var orcamentosPessoal = novosOrcamentos.TodosPor(centroDeCusto.Id);

            //    ListaNovosOrcamentos.AddRange(orcamentosPessoal);


            //    //         centros.Deletar(centroDeCusto);

            //}

            //centros.Deletar(centrosDeCusto);
            repositorioDepartamento.Deletar(listaDepartamentos);

        }

        [Test]
        [Ignore]
        public void TesteDepartamento()
        {
            Departamentos departamentos = new Departamentos();
            var departamento = departamentos.Obter(300);

            ServicoSalvarDepartamento servico = new ServicoSalvarDepartamento();
            servico.Salvar(departamento);
        }
    }
}
