using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using Orcamento.Controller;
using Orcamento.Domain;
using Orcamento.Domain.DB.Repositorio;
using Orcamento.Domain.DTO;
using Orcamento.Domain.DTO.Viagem;
using Orcamento.Domain.Gerenciamento;

namespace WebSimuladorRH
{
    public partial class OrcamentoDeViagem : BasePage, IViewOrcamentoDeViagem
    {
        #region Atributos

        private string orcamento;
        private string centroDeCustoId;

        #endregion

        #region Propriedades

        public OrcamentoDeViagemController Controller
        {
            get
            {
                return
                    (OrcamentoDeViagemController)
                    (Session["OrcamentoDeViagemController"] ?? new OrcamentoDeViagemController(this));
            }

            set { Session["OrcamentoDeViagemController"] = value; }
        }

        public Departamento Departamento
        {
            get { return (Departamento)Session["SetorSelecionado"]; }
            set { Session["SetorSelecionado"] = value; }
        }

        public Orcamento.Domain.Orcamento OrcamentoViagem
        {
            get { return (Orcamento.Domain.ComponentesDeOrcamento.OrcamentoDeViagem.OrcamentoDeViagem)Session["OrcamentoDeViagemVersao"]; }
            set { Session["OrcamentoDeViagemVersao"] = value; }
        }

        public int CentroDeCustoId
        {
            get { return (int)Session["CentroDeCusto"]; }
            set { Session["CentroDeCusto"] = value; }
        }

        public List<ContaDTO> Contas
        {
            get { return (List<ContaDTO>)Session["ContasDTOViagem"]; }
            set
            {
                Session["ContasDTOViagem"] = value;
                PreencherRepeaterDespesas(value);
            }
        }

        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            btnApagarVersao.Attributes.Add("onclick", "this.disabled=true;");

            InformarDepartamentoDoUsuarioLogado();

            CarregarAPaginaPelaPrimeiraVez();
        }

        protected void SelecionandoCentroDeCusto(object sender, EventArgs e)
        {
            this.CentroDeCustoId = int.Parse(dropCentrosDeCusto.SelectedValue);
            ValidarBotaoIncluirVersao(CentroDeCustoId);
            CarregarGridsDaPaginaDeAcorcoComOCentroDeCustoSelecionado();

            HabilitarControlesDevisualizacaodaPagina();
            DesabilitarBotoesDeManipulacaoDoOrcamento();
        }

        private void ValidarBotaoIncluirVersao(int id)
        {
            divBotaoIncluirNovaVersao.Visible = CentroDeCustoId > 0;
        }

        protected void IncluindoNovaVersao(object sender, EventArgs e)
        {
            int idCentroDeCusto = int.Parse(dropCentrosDeCusto.SelectedValue);

            Controller.CriarNovoOrcamentoOperacional(Departamento, idCentroDeCusto);

            HabilitarBotoesDeManipulacaoDoOrcamento();
        }

        protected void CarregandoQuantitativosDeViagem(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var lblValorDespesa = (Label)e.Item.FindControl("valorDespesa");
                lblValorDespesa.Text = lblValorDespesa.Text;
            }
        }

        protected void CarregandoTotais(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Footer)
            {
                var despesasDeViagemDto = ObterDespesasASeremTotalizadasNoFooter();

                AtribuirACadaLiteralDoFooterOValorTotalSomado(e, despesasDeViagemDto);

                AtribuirOValorTotalDoOrcamentoNoUltimoLiteralDoFooter(e, despesasDeViagemDto);
            }
        }

        protected void CarregandoContas(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var txtValorDespesa = (TextBox)e.Item.FindControl("valorDespesa");
                txtValorDespesa.Text = txtValorDespesa.Text;
            }
        }

        protected void Salvando(object sender, EventArgs e)
        {
            try
            {
                SalvarOrcamento();
            }
            catch (Exception ex)
            {
                Notificacao = ex.Message;
            }

            if (!string.IsNullOrEmpty(Notificacao))
                EnviarMensagem();
            else
                CarregandoPaginaPor(OrcamentoViagem.Id);
        }

        protected void SelecionandoVersao(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "SelecionarVersao")
            {
                int despesaOperacionalId = int.Parse(e.CommandArgument.ToString());

                CarregandoPaginaPor(despesaOperacionalId);

                HabilitarBotoesDeManipulacaoDoOrcamento();
            }
        }

        protected void AtribuindoVersaoFinal(object sender, EventArgs e)
        {
            try
            {
                SalvarOrcamento();
                Controller.AtribuirVersaoFinal();
                CarregandoPaginaPor(OrcamentoViagem.Id);
            }
            catch (Exception ex)
            {
                Notificacao = ex.Message;
            }

            EnviarMensagem();
        }

        protected void ApagandoVersao(object sender, EventArgs e)
        {
            try
            {
                Controller.ApagarVersao();

                CarregarGridsDaPaginaDeAcorcoComOCentroDeCustoSelecionado();

                DesabilitarBotoesDeManipulacaoDoOrcamento();

                rptQuantitativosDeViagem.DataSource = null;
                rptQuantitativosDeViagem.DataBind();

                rptValoresTotais.DataSource = null;
                rptValoresTotais.DataBind();
            }
            catch (Exception ex)
            {
                Notificacao = ex.Message;
                EnviarMensagem();
            }
        }

        #endregion

        #region Métodos

        public void CarregarOrcamento()
        {
            versaoDoOrcamento.Visible = true;
            versaoDoOrcamento.Text = OrcamentoViagem.VersaoFinal ? "Versão Final" : OrcamentoViagem.NomeOrcamento;

            PreencherValoresTotais();
        }

        private void CarregarAPaginaPelaPrimeiraVez()
        {
            if (!Page.IsPostBack)
            {
                CarregarComboCentrosDeCusto();
            }
        }

        private void InformarDepartamentoDoUsuarioLogado()
        {
            var setores = new Departamentos();
            Departamento = ObterDepartamentoDoUsuarioLogado(setores);

            VerificarSeUsuarioCoorporativo();
        }

        private Departamento ObterDepartamentoDoUsuarioLogado(Departamentos setores)
        {
            return setores.Obter((int)Session["DepartamentoLogadoId"]);
        }

        private void CarregarComboCentrosDeCusto()
        {
            dropCentrosDeCusto.DataSource = Departamento.CentrosDeCusto.OrderBy(x => x.Nome);
            dropCentrosDeCusto.DataValueField = "Id";
            dropCentrosDeCusto.DataTextField = "Nome";
            dropCentrosDeCusto.DataBind();
            dropCentrosDeCusto.Items.Insert(0, new ListItem("Selecione", "0"));
        }

        public void InformarNaoExisteVersaoFinal()
        {
            Page.ClientScript.RegisterClientScriptBlock(GetType(), "Key",
                                                              "alert('Existe versão gravada porém nenhuma apontada como versão final.');",
                                                              true);
        }

        private void PreencherValoresTotais()
        {
            rptValoresTotais.DataSource = Controller.ObterDespesasTotais().OrderBy(x => x.NomeCidade).ThenBy(y => y.Despesa);
            rptValoresTotais.DataBind();
        }

        private void CarregarGridsDaPaginaDeAcorcoComOCentroDeCustoSelecionado()
        {
            Controller.CarregarPaginaDeAcordoComOCentroDeCustoSelecionado();
        }

        public void CarregarVersoesDeOrcamentos(IList<VersaoDeDespesaDTO> versoes)
        {
            if (!versoes.Any())
                versoes = null;

            rptVersoesOrcamentoDeViagens.DataSource = versoes;
            rptVersoesOrcamentoDeViagens.DataBind();
        }

        private void PreencherRepeaterDespesas(IList<ContaDTO> contas)
        {
            rptQuantitativosDeViagem.DataSource = contas.OrderBy(x => x.Conta).ThenBy(y => y.Despesa).ToList();
            rptQuantitativosDeViagem.DataBind();
        }

        private void SalvarOrcamento()
        {
            try
            {
                var contas = new List<ContaDTO>();

                foreach (RepeaterItem despesaDeViagens in rptQuantitativosDeViagem.Items)
                {
                    var despesas = (Repeater)despesaDeViagens.FindControl("rptContas");
                    var idConta = (HiddenField)despesaDeViagens.FindControl("idConta");
                    var idDespesaOperacional = (HiddenField)despesaDeViagens.FindControl("idDespesaOperacional");

                    var conta = PreencherContaDTO(idDespesaOperacional, idConta);

                    AdicionarDespesasAConta(conta, despesas);

                    contas.Add(conta);
                }

                Controller.SalvarOrcamento(contas);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void AdicionarDespesasAConta(ContaDTO conta, Repeater despesas)
        {
            foreach (RepeaterItem repeaterDespesa in despesas.Items)
            {
                var despesa = new DespesaDTO();

                var idDespesa = (HiddenField)repeaterDespesa.FindControl("idDespesa");
                var valorDespesa = (TextBox)repeaterDespesa.FindControl("valorDespesa");

                despesa.DespesaId = Convert.ToInt32(idDespesa.Value);

                if (valorDespesa.Text != string.Empty)
                    AtribuirValorFormatadoADespesa(despesa, valorDespesa);

                conta.Despesas.Add(despesa);
            }
        }

        private static ContaDTO PreencherContaDTO(HiddenField idDespesaOperacional, HiddenField idConta)
        {
            var conta = new ContaDTO();
            conta.ContaId = Convert.ToInt32(idConta.Value);
            conta.DespesaOperacionalId = Convert.ToInt32(idDespesaOperacional.Value);
            conta.Despesas = new List<DespesaDTO>();
            return conta;
        }

        private void AtribuirValorFormatadoADespesa(DespesaDTO despesa, TextBox valorDespesa)
        {
            string valorFormatado = (valorDespesa.Text).Replace("(", "");

            valorFormatado = valorFormatado.Replace(")", "");
            valorFormatado = valorFormatado.Replace(".", "");

            if (valorFormatado.Length > 3)
                despesa.Valor = Convert.ToInt32(valorFormatado.Substring(0, valorFormatado.Length - 3));
            else
                despesa.Valor = Int32.Parse(valorFormatado);
        }

        private void CarregandoPaginaPor(int despesaOperacionalId)
        {
            CarregarGridsDaPaginaDeAcorcoComOCentroDeCustoSelecionado();

            Controller.CarregarOrcamentosDeViagem(despesaOperacionalId);

            ltlVersao.Visible = true;
        }

        private void HabilitarBotoesDeManipulacaoDoOrcamento()
        {
            divBotaoSalvar.Visible = true;
            divBotaoApagandoVersao.Visible = true;
            divBotaoVersaoFinal.Visible = true;
            ltlVersao.Visible = true;
            versaoDoOrcamento.Visible = true;
        }

        private void DesabilitarBotoesDeManipulacaoDoOrcamento()
        {
            divBotaoSalvar.Visible = false;
            divBotaoApagandoVersao.Visible = false;
            divBotaoVersaoFinal.Visible = false;
            ltlVersao.Visible = false;
            versaoDoOrcamento.Visible = false;

        }

        private void HabilitarControlesDevisualizacaodaPagina()
        {
            divBotaoIncluirNovaVersao.Visible = true;
        }

        private List<DespesaDeViagemDTO> ObterDespesasASeremTotalizadasNoFooter()
        {
            var despesasDeViagemDto = new List<DespesaDeViagemDTO>();

            foreach (RepeaterItem valoresTotaisItem in rptValoresTotais.Items)
                if (valoresTotaisItem.ItemType == ListItemType.Item || valoresTotaisItem.ItemType == ListItemType.AlternatingItem)
                    AdicionarDespesasDeTodosOsMeses(despesasDeViagemDto, valoresTotaisItem);

            return despesasDeViagemDto;
        }

        private void AtribuirOValorTotalDoOrcamentoNoUltimoLiteralDoFooter(RepeaterItemEventArgs e, List<DespesaDeViagemDTO> despesasDeViagemDto)
        {
            long somaDeDespesasDeViagem = despesasDeViagemDto.Sum(x => x.ObterTotal()) == long.MinValue ? 0 : despesasDeViagemDto.Sum(x => x.ObterTotal());

            ((Literal)e.Item.FindControl("ltlTotal")).Text = AplicarMascaraDeMilhar(somaDeDespesasDeViagem);
        }

        private void AtribuirACadaLiteralDoFooterOValorTotalSomado(RepeaterItemEventArgs e, List<DespesaDeViagemDTO> despesasDeViagemDto)
        {
            for (int mes = 1; mes < 13; mes++)
            {
                long valorTotalDoMes = (despesasDeViagemDto.Sum(x => x.ObterValorTotalDoMes((MesEnum)mes))) == long.MinValue ? 0 : (despesasDeViagemDto.Sum(x => x.ObterValorTotalDoMes((MesEnum)mes)));

                ((Literal)e.Item.FindControl(string.Format("ltlTotal_{0}", mes))).Text = AplicarMascaraDeMilhar(valorTotalDoMes);
            }
        }

        private string AplicarMascaraDeMilhar(long numero)
        {
            return string.Format("({0:#,###;#,###;0})", numero);
        }

        private void AdicionarDespesasDeTodosOsMeses(List<DespesaDeViagemDTO> despesasDeViagemDto, RepeaterItem valoresTotaisItem)
        {
            var despesaDeViagemDto = new DespesaDeViagemDTO();

            var rptContas = (Repeater)valoresTotaisItem.FindControl("rptContas");

            AdicionarDespesasMesAMes(despesaDeViagemDto, rptContas);

            despesasDeViagemDto.Add(despesaDeViagemDto);
        }

        private void AdicionarDespesasMesAMes(DespesaDeViagemDTO despesaDeViagemDto, Repeater rptContas)
        {
            for (int mes = 1; mes < 13; mes++)
            {
                var literal = (Literal)rptContas.Items[mes - 1].FindControl("ltlValor");
                string valor = literal.Text.Replace("(", "").Replace(")", "").Replace(".","");

                despesaDeViagemDto.AdicionarItem((MesEnum)mes, long.Parse(valor), string.Empty);
            }
        }

        public void CarregarValidacaoDeDezVersoesDoBotaoIncluirNovaVersao()
        {
            btnIncluirNovaVersao.Attributes.Add("onclick", "alert('Não é possível criar mais de 10 versões'); return false; ");
        }

        public void RemoverValidacaoDeDezVersoesDoBotaoIncluirNovaVersao()
        {
            btnIncluirNovaVersao.Attributes.Remove("onclick");
        }

        public void ZerarViagens()
        {
            rptValoresTotais.DataSource = null;
            rptValoresTotais.DataBind();

            rptQuantitativosDeViagem.DataSource = null;
            rptQuantitativosDeViagem.DataBind();
        }

        #endregion
    }
}