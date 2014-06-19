using System;
using Orcamento.Domain;
using Orcamento.Domain.DB.Repositorio;
using Orcamento.Domain.Gerenciamento;

namespace WebSimuladorRH
{
    public partial class PainelOrcamento : BasePage
    {
        #region Propriedades

        public TipoUsuarioEnum TipoUsuarioLogado
        {
            get { return (TipoUsuarioEnum)Session["TipoUsuarioId"]; }
        }

        public Departamento DepartamentoUsuario
        {
            get { return (Departamento)Session["DepartamentoUsuario"]; }
            set { Session["DepartamentoUsuario"] = value; }
        }

        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            VerificaSeExisteSessaoDeUsuario();
            InformarDepartamento();
            VerificarVisualizacaoDosBotoesPorArea();

            if (!IsPostBack)
                CarregarAPaginaPelaPrimeiraVez();

            if (Request.QueryString["alert"] == "on")
                NotificarAjudaSobreANavegacaoDoSistema();
        }

        protected void IndoParaCadastroDeDepartamento(object sender, EventArgs e) 
        {
            Response.Redirect("/CadastrarDepartamentoCorporativo.aspx");
        }


        protected void IndoParaAlteracaoDeDepartamento(object sender, EventArgs e)
        {
            Response.Redirect("/CadastrarDepartamentoCorporativo.aspx?funcao=a");
        }

        protected void IndoParaAlteracaoDeCentroDeCusto(object sender, EventArgs e) 
        {
            Response.Redirect("/CadastroDeCentroDeCusto.aspx?funcao=a");
        }

        protected void IndoParaCadastrarCentroDeCusto(object sender, EventArgs e)
        {
            Response.Redirect("/CadastroDeCentroDeCusto.aspx");
        }

        protected void IndoParaAlocacao(object sender, EventArgs e)
        {
            Response.Redirect("/AlterarLocacaoFuncionario.aspx");
        }

        protected void IndoParaOSimuladorDeDespesas(object sender, EventArgs e)
        {
            Response.Redirect("/DespesaOperacional.aspx");
        }

        protected void IndoParaOSimuladorDeViagemHospedagem(object sender, EventArgs e)
        {
            Response.Redirect("/OrcamentoDeViagem.aspx");
        }

        protected void IndoParaOSimuladorDeReceita(object sender, EventArgs e)
        {
            Response.Redirect("/OrcamentoDeProducao.aspx");
        }

        protected void IndoParaOTotalizadorPorDespesa(object sender, EventArgs e)
        {
            Response.Redirect("/TotalizadorDeDespesas.aspx");
        }

        protected void IndoParaOTotalizadorPorCentroDeCusto(object sender, EventArgs e)
        {
            Response.Redirect("/TotalizadorCentroDeCusto.aspx");
        }

        protected void IndoParaOSimuladorDePessoal(object sender, EventArgs e)
        {
            Response.Redirect("/Pessoal/OrcamentoPessoal.aspx");
        }

        protected void IndoParaOResultadoOrcado(object sender, EventArgs e)
        {
            Response.Redirect("/ResultadoOrcado.aspx");
        }

        protected void IndoParaOCadastroDeTicketDeViagem(object sender, EventArgs e)
        {
            Response.Redirect("/TicketDeViagem.aspx");
        }

        protected void IndoParaCadastroDeConta(object sender, EventArgs e)
        {
            Response.Redirect("/CadastroDeConta.aspx");
        }

        protected void IndoParaAlteracaoDeConta(object sender, EventArgs e)
        {
            Response.Redirect("/CadastroDeConta.aspx?funcao=a");
        }
        
        protected void IndoParaOSimuladorDeFuncionario(object sender, EventArgs e)
        {
            Response.Redirect("/Pessoal/Funcionario.aspx");
        }

        protected void IndoParaOTotalizadorPorGrupoDeConta(object sender, EventArgs e)
        {
            Response.Redirect("/TotalizadorPessoalPorGrupoDeConta.aspx");
        }

        protected void IndoParaOCadastroDeTicketDeProducao(object sender, EventArgs e)
        {
            Response.Redirect("/TicketDeProducao.aspx");
        }

        protected void IndoParaInsumos(object sender, EventArgs e)
        {
            Response.Redirect("/Insumo/Insumos.aspx");
        }

        protected void IndoParaControle(object sender, EventArgs e)
        {
            Response.Redirect("/ControleDeCentroDeCusto.aspx");
        }

        protected void IndoParaOCadastroDeHospital(object sender, EventArgs e)
        {
            Response.Redirect("AlteracaoDeHospital.aspx");
        }

        protected void IndoParaAlteracaoDeHospital(object sender, EventArgs e) 
        {
            Response.Redirect("AlteracaoDeHospital.aspx?funcao=a");
        }
        protected void IndoParaCadastroDeUsuario(object sender, EventArgs e) 
        {
            Response.Redirect("/cadastroUsuario.aspx");
        }

        protected void IndoParaAlteracaoDeUsuario(object sender, EventArgs e)
        {
            Response.Redirect("/cadastroUsuario.aspx?funcao=a");
        }
        protected void IndoParaOCadastroDeTicketsDePessoal(object sender, EventArgs e)
        {
            Response.Redirect("/Pessoal/CadastroDeTicketsPessoal.aspx");
        }

        protected void IndoParaODRE(object sender, EventArgs e)
        {
            Response.Redirect("/DemonstracaoDeResultado/DemonstracaoDeResultado.aspx");
        }


        protected void IndoParaODRETotal(object sender, EventArgs e)
        {
            Response.Redirect("/DemonstracaoDeResultado/DemonstracaoDeResultadoGeral.aspx");
        }

        protected void IndoParaOCadastroDeusuario(object sender, EventArgs e)
        {
            Response.Redirect("/CadastroUsuario.aspx");
        }

        #endregion

        #region Metodos

        private void NotificarAjudaSobreANavegacaoDoSistema()
        {
            Notificacao = "Ao clicar no logo da Rede D\\'or você irá ser redirecionado para a página principal.";
            EnviarMensagem();
        }

        private void InformarDepartamento()
        {
            var departamentos = new Departamentos();

            DepartamentoUsuario = departamentos.Obter((int)Session["DepartamentoLogadoId"]);
        }

        private void VerificarVisualizacaoDosBotoesPorArea()
        {
            divViagemHospedagem.Visible = (DepartamentoUsuario.GetType() == typeof(Setor));
            divReceita.Visible = DepartamentoUsuario.GetType() == typeof(Hospital);
        }

        private void CarregarAPaginaPelaPrimeiraVez()
        {
            ManipularEstilosGerais();
            ManipularVisdualizacoesGerais();
            ManipularVisualizacaoDaPaginaDeAcordoComOPerfilDoUsuarioLogado();
        }

        private void ManipularVisualizacaoDaPaginaDeAcordoComOPerfilDoUsuarioLogado()
        {
            switch (TipoUsuarioLogado)
            {
                case TipoUsuarioEnum.Hospital:
                    ManipularVisualizacaoDosBotoesParaPerfilHospital();
                    ManipularEstiloDoPerfilHospital();
                    break;
                case TipoUsuarioEnum.Corporativo:
                    ManipularVisualizacaoDosBotoesDoPerfilCorporativo();
                    ManipularEstiloDoPerfilCorporativo();
                    break;
                case TipoUsuarioEnum.Administrador:
                    ManipularEstilosDoPerfilAdministrador();
                    ManipularVisualizacaoDosBotoesDoPerfilAdministrador();
                    ExibirInsumos();
                    break;
            }

            divDRE.Visible = DepartamentoUsuario.GetType() == typeof(Hospital);
        }

        private void ManipularVisualizacaoDosBotoesDoPerfilAdministrador()
        {
            divReceita.Visible = false;
            //    divOutrasDespesas.Visible = false;
            //   divPessoal.Visible = false;
            divViagemHospedagem.Visible = false;
            // divFuncionario.Visible = false;
            //TODO: comentado até pessoal voltar
            divCadastroDeTicketsDePessoal.Visible = true;
            divDRETotal.Visible = true;
            //DivControleDeCentroDeCusto.Visible = true;
            divAlocacao.Visible = true;
            divCadstroDeHospital.Visible = true;
            divALterarHospital.Visible = true;
            divAlterarCentroDeCusto.Visible = true;
            divCadastrarCentroDeCusto.Visible = true;
            divCadastroDeConta.Visible = true;
            divAlterarConta.Visible = true;
            divAlteracaoUsuario.Visible = true;
            divCadastrarUsuario.Visible = true;
            divAlterarDepartamento.Visible = true;
            divCadastrarDepartamento.Visible = true;
            ManipularVisualizacaoDosBotoesDoPerfilAdministradorPorHospitalOuSetor();
        }

        private void ManipularVisualizacaoDosBotoesDoPerfilAdministradorPorHospitalOuSetor()
        {
            if (DepartamentoUsuario.GetType() == typeof(Setor))
            {
                divCadastroDeTicketsDeViagem.Visible = true;
                ManipularEstiloDoPerfilCorporativo();
                ManipularVisualizacaoDosBotoesDoPerfilCorporativo();
            }
            else if (DepartamentoUsuario.GetType() == typeof(Hospital))
            {
                divCadastroDeTicketDeProducao.Visible = true;
                ManipularEstiloDoPerfilHospital();
                ManipularVisualizacaoDosBotoesParaPerfilHospital();
            }
        }

        private void ManipularEstilosDoPerfilAdministrador()
        {
            btnCadastroDeTicketDeViagem.Attributes["class"] = "bold";
            btnCadastroDeTicketsDePessoal.Attributes["class"] = "bold";
            btnCadastroDeTicketDeProducao.Attributes["class"] = "bold";
            btnInsumos.Attributes["class"] = "bold";
            //btncontroleDeCentro.Attributes["class"] = "bold";
            btndreTotal.Attributes["class"] = "bold";
            btnAlocacao.Attributes["class"] = "bold";
            btnCadastroHospital.Attributes["class"] = "bold";
            btnAlterarHospital.Attributes["class"] = "bold";
            btnCadastrarCentroDeCusto.Attributes["class"] = "bold";
            btnAlterarCentroDeCusto.Attributes["class"] = "bold";
            btnAlterarConta.Attributes["class"] = "bold";
            btnCadastroDeConta.Attributes["class"] = "bold";
            btnCadastrarUsuario.Attributes["class"] = "bold";
            btnAlterarUsuario.Attributes["class"] = "bold";
            btnCadastrarDepartamento.Attributes["class"] = "bold";
            btnAlterarDepartamento.Attributes["class"] = "bold";
        }

        private void ManipularEstiloDoPerfilCorporativo()
        {
            btnViagemHospedagem.Attributes["class"] = "bold";
        }

        private void ManipularVisualizacaoDosBotoesDoPerfilCorporativo()
        {
            btnReceita.Visible = false;
            divReceita.Visible = false;
            btnViagemHospedagem.Visible = true;
            divViagemHospedagem.Visible = true;

        }

        private void ManipularEstiloDoPerfilHospital()
        {
            btnReceita.Attributes["class"] = "bold";
            btnInsumos.Attributes["class"] = "bold";
        }

        private void ManipularVisualizacaoDosBotoesParaPerfilHospital()
        {
            btnViagemHospedagem.Visible = false;
            divViagemHospedagem.Visible = false;
            btnReceita.Visible = true;
            divReceita.Visible = true;
        }

        private void ManipularVisdualizacoesGerais()
        {
            divCadastroDeTicketsDeViagem.Visible = false;
            divCadastroDeTicketsDePessoal.Visible = false;
        }

        private void ManipularEstilosGerais()
        {
            btnDRE.Attributes["class"] = "bold";
            btnOutrasDespesas.Attributes["class"] = "bold";
            btnPessoal.Attributes["class"] = "bold";
            btnResultadoOrcado.Attributes["class"] = "bold";
            btnFuncionario.Attributes["class"] = "bold";
        }

        private void ExibirInsumos()
        {
            if (DepartamentoUsuario.GetType() == typeof(Hospital))
            {
                divInsumos.Visible = true;
                btnInsumos.Attributes["class"] = "bold";
            }
        }

        #endregion


    }
}