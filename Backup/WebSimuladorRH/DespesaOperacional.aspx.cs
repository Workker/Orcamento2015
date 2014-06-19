using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Services;
using System.Web.UI.WebControls;
using Orcamento.Controller;
using Orcamento.Controller.Views;
using Orcamento.Domain;
using Orcamento.Domain.DB.Repositorio;
using Orcamento.Domain.DTO;
using Orcamento.Domain.Gerenciamento;
using System.Threading;

namespace WebSimuladorRH
{
    public partial class DespesaOperacional : BasePage, IViewDespesaOperacional
    {
        #region Propriedades

        private DespesaOperacionalController controller;

        public Departamento SetorSelecionado
        {
            get { return (Departamento)Session["SetorSelecionado"]; }
            set { Session["SetorSelecionado"] = value; }
        }

        public int CentroDeCustoId
        {
            get { return (int)Session["CentroDeCustoIdOutrasDespesas"]; }
            set { Session["CentroDeCustoIdOutrasDespesas"] = value; }
        }

        public Orcamento.Domain.Orcamento OrcamentoOperacional
        {
            get { return (Orcamento.Domain.Orcamento)Session["OrcamentoOperacionalVersao"]; }
            set { Session["OrcamentoOperacionalVersao"] = value; }
        }

        public IList<ContaDTO> Contas
        {
            get { return (IList<ContaDTO>)Session["ContasDTO"]; }
            set { Session["ContasDTO"] = value; }
        }

        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            VerificaSeExisteSessaoDeUsuario();
            controller = new DespesaOperacionalController(this);

            if (!Page.IsPostBack)
                CarregarCentrosDeCusto();
        }

        protected void SelecionandoCentroDeCusto(object sender, EventArgs e)
        {
            CentroDeCustoId = int.Parse(dropCentroDeCusto.SelectedValue);

            PreencherVersoes();
            ZerarGrids();
            DesabilitarBotoesDeManipulacaoDoOrcamento();
            HabilitarBotaoDeInclusaoDeNovaVersao();
            ValidarBotaoIncluirVersao(CentroDeCustoId);

        }

        private void ValidarBotaoIncluirVersao(int id)
        {
            divBotaoIncluirNovaVersao.Visible = CentroDeCustoId > 0;
        }

        private void ZerarGrids()
        {
            rptDespesasOperacionais.DataSource = null;
            rptDespesasOperacionais.DataBind();

            divVersao.Visible = false;
        }

        protected void IrParaTotalizador(object sender, EventArgs e)
        {
            Response.Redirect("/Totalizador.aspx");
        }

        protected void Salvando(object sender, EventArgs e)
        {
            try
            {
                controller.Salvar();
            }
            catch (Exception ex)
            {
                Notificacao = ex.Message;
            }

            EnviarMensagem();
        }

        protected void SelecionandoVersoesDeOrcamento(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "SelecionarVersao")
            {
                int despesaOperacionalId = int.Parse(e.CommandArgument.ToString());

                controller.CarregarOrcamentoOperacional(despesaOperacionalId);
            }
            HabilitarBotoesDeManipulacaoDoOrcamento();
            HabilitarExibicaoDaVersao();
        }

        protected void AtribuindoVersaoFinalDoOrcamento(object sender, EventArgs e)
        {
            try
            {
                controller.AtribuirVersaoFinal();
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
                controller.ApagarVersao();
                ZerarCampos();
            }
            catch (Exception ex)
            {
                Notificacao = ex.Message;
            }

            ZerarCampos();
            EnviarMensagem();
        }

        private void ZerarCampos()
        {

            rptDespesasOperacionais.DataSource = null;
            rptDespesasOperacionais.DataBind();

            DesabilitarBotoesDeManipulacaoDoOrcamento();
            DesabilitarExibicaoDaVersao();
        }

        public void EsconderOBotaoApagar()
        {
            btnApagarVersao.Visible = false;
        }

        protected void TratandoOCarregamentoDasDespesasOperacionaisItemAItem(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var idDespesaContaID = (HiddenField)e.Item.FindControl("idDespesaContaID");
                var txtMemoriaDeCalculo = (TextBox)e.Item.FindControl("txtMemoriaDeCalculo");

                var lblValorDespesa = (Label)e.Item.FindControl("valorDespesa");
                lblValorDespesa.Text = "(" + lblValorDespesa.Text + ")";

                ((Button)e.Item.FindControl("lknOrcamento")).Attributes.Add("onclick",
                                                                             "Carregar('" + txtMemoriaDeCalculo.ClientID +
                                                                             "', " + idDespesaContaID.Value + ");");

                if (!string.IsNullOrEmpty(txtMemoriaDeCalculo.Text))
                    ((Image)(e.Item.FindControl("imgInformation"))).Attributes.Add("style", "display: inline;");
            }

            if (e.Item.ItemType == ListItemType.Footer)
            {
                for (int i = 1; i < 14; i++)
                {
                    if (i != 13)
                    {
                        var totalMes = (TextBox)e.Item.FindControl("totalmes" + i.ToString());
                        totalMes.Text = string.Empty;

                        totalMes.Text = "(";
                        totalMes.Text += this.OrcamentoOperacional.DespesasOperacionais.Where(d => d.Mes == (MesEnum)i).Sum(d => d.Valor).ToString("#,###,###,###0");
                        totalMes.Text += ")";
                    }
                    else
                    {
                        var totalMes = (TextBox)e.Item.FindControl("totalAnualGeral");
                        totalMes.Text = string.Empty;

                        totalMes.Text = "(";
                        totalMes.Text += this.OrcamentoOperacional.DespesasOperacionais.Sum(d => d.Valor).ToString("#,###,###,###0");
                        totalMes.Text += ")";
                    }
                }
            }
        }

        protected void CarregandoContas(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var txtValorDespesa = (TextBox)e.Item.FindControl("valorDespesa");
                txtValorDespesa.Text = "(" + string.Format("{0:#,###;#,###;0}", txtValorDespesa.Text) + ")";
            }
        }

        protected void IncluindoNovaVersao(object sender, EventArgs e)
        {
            try
            {
                EnviarMensagem();
                btnApagarVersao.Visible = true;
                int centroDeCustoId = int.Parse(dropCentroDeCusto.SelectedValue);
                controller.CriarNovoOrcamentoOperacional(this.SetorSelecionado, centroDeCustoId);
                HabilitarBotoesDeManipulacaoDoOrcamento();
                HabilitarExibicaoDaVersao();
            }
            catch (Exception ex)
            {
                Notificacao = ex.Message;
            }

            EnviarMensagem();
        }

        #endregion

        #region Métodos

        private void CarregarCentrosDeCusto()
        {
            int departamentoId = (int)Session["DepartamentoLogadoId"];

            SetorSelecionado = controller.ObterDepartamento(departamentoId);

            dropCentroDeCusto.DataSource = SetorSelecionado.CentrosDeCusto.Where(c => c.Contas.Any(co => co.TipoConta.TipoContaEnum == TipoContaEnum.Outros)).OrderBy(x => x.Nome);
            dropCentroDeCusto.DataTextField = "Nome";
            dropCentroDeCusto.DataValueField = "Id";
            dropCentroDeCusto.DataBind();

            dropCentroDeCusto.Items.Insert(0, new ListItem("Selecione", "0"));
        }

        public void InformarVersao()
        {
            ltlVersaoDoOrcamento.Text = OrcamentoOperacional.VersaoFinal ? "Versão Final" : OrcamentoOperacional.NomeOrcamento;
        }

        [WebMethod]
        public static void InformarMemoriaDeCalculo(string parametro, string texto)
        {
            try
            {
                int idDespesa = 0;
                if (Int32.TryParse(texto, out idDespesa))
                {
                    var despesas = new DespesasOperacionais();
                    Despesa despesa = despesas.Obter(idDespesa);
                    despesa.MemoriaDeCalculo = parametro;

                    despesas.Salvar(despesa);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<ContaDTO> ObterDespesasCorrentes()
        {
            var contas = new List<ContaDTO>();

            foreach (RepeaterItem despesaOperacional in rptDespesasOperacionais.Items)
            {
                var despesas = (Repeater)despesaOperacional.FindControl("rptContas");
                var idConta = (HiddenField)despesaOperacional.FindControl("idConta");
                var idDespesaOperacional = (HiddenField)despesaOperacional.FindControl("idDespesaOperacional");

                var conta = new ContaDTO();
                conta.ContaId = Convert.ToInt32(idConta.Value);
                conta.DespesaOperacionalId = Convert.ToInt32(idDespesaOperacional.Value);
                conta.Despesas = new List<DespesaDTO>();

                foreach (RepeaterItem repeaterDespesa in despesas.Items)
                {
                    var despesa = new DespesaDTO();
                    var idDespesa = (HiddenField)repeaterDespesa.FindControl("idDespesa");
                    var valorDespesa = (TextBox)repeaterDespesa.FindControl("valorDespesa");

                    despesa.DespesaId = Convert.ToInt32(idDespesa.Value);

                    if (valorDespesa.Text != string.Empty)
                    {
                        string valorFormatado = (valorDespesa.Text).Replace("(", "");
                        valorFormatado = valorFormatado.Replace(")", "");
                        valorFormatado = valorFormatado.Replace(".", "");

                        //if (valorFormatado.Length > 3)
                        //    despesa.Valor = Convert.ToInt32(valorFormatado.Substring(0, valorFormatado.Length - 3));
                        //else
                        despesa.Valor = Int32.Parse(valorFormatado);
                    }

                    conta.Despesas.Add(despesa);
                }
                contas.Add(conta);
            }
            return contas;
        }

        public void InformarNaoExisteVersaoFinal()
        {
            Page.ClientScript.RegisterClientScriptBlock(GetType(), "Key",
                                                              "alert('Existe versão gravada porém nenhuma apontada como versão final.');",
                                                              true);
        }

        public void PreencherVersoes()
        {
            var listaOrcamentos = controller.ObterOrcamentosOperacionais();

            controller.CarregarValidacoesDeControleDeVersao(listaOrcamentos);


            if (listaOrcamentos.Any())
                PreencherOrcamentos(listaOrcamentos);
            else
            {
                rptVersoesDeOrcamento.DataSource = null;
                rptVersoesDeOrcamento.DataBind();
            }
        }

        public void PreencherOrcamentos(IList<Orcamento.Domain.Orcamento> listaOrcamentos)
        {
            var objetos = new List<object>();

            foreach (Orcamento.Domain.Orcamento orcamento in listaOrcamentos)
            {
                var objeto =
                    new
                    {
                        orcamento.Id,
                        CentroDeCusto = orcamento.CentroDeCusto.Nome,
                        Versao = orcamento.VersaoFinal ? "Versão Final" : orcamento.NomeOrcamento,
                        ValorTotal = orcamento.DespesasOperacionais.Sum(d => d.Valor)
                    };
                objetos.Add(objeto);
            }

            listaOrcamentos = null;
            rptVersoesDeOrcamento.DataSource = objetos;
            rptVersoesDeOrcamento.DataBind();
        }

        public void PreencherRepeaterDespesas(IList<ContaDTO> contas)
        {
            rptDespesasOperacionais.DataSource = contas.OrderBy(x => x.Conta);
            rptDespesasOperacionais.DataBind();
        }

        public void CarregarValidacaoDeDezVersoesDoBotaoIncluirNovaVersao()
        {
            btnIncluirNovaVersao.Attributes.Add("onclick", "alert('Não é possível criar mais de 10 versões'); return false; ");
        }

        public void RemoverValidacaoDeDezVersoesDoBotaoIncluirNovaVersao()
        {
            btnIncluirNovaVersao.Attributes.Remove("onclick");
        }

        public void CarregarOsTotaisDoRodape()
        {
            var literal = (Literal)rptDespesasOperacionais.Items[12 - 1].FindControl("ltlValor");
        }

        private void HabilitarBotoesDeManipulacaoDoOrcamento()
        {
            divBotaoSalvarOrcamento.Visible = true;
            divBotaoAtribuirComoVersaoFinal.Visible = true;
            divBotaoApagarVersao.Visible = true;
        }

        private void DesabilitarBotoesDeManipulacaoDoOrcamento()
        {
            divBotaoSalvarOrcamento.Visible = false;
            divBotaoAtribuirComoVersaoFinal.Visible = false;
            divBotaoApagarVersao.Visible = false;
        }

        private void HabilitarExibicaoDaVersao()
        {
            divVersao.Visible = true;
        }

        private void DesabilitarExibicaoDaVersao()
        {
            divVersao.Visible = false;
        }

        private void HabilitarBotaoDeInclusaoDeNovaVersao()
        {
            divBotaoIncluirNovaVersao.Visible = true;
        }
        #endregion
    }
}