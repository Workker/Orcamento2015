using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Orcamento.Controller;
using Orcamento.Domain.Gerenciamento;
using Orcamento.Domain.DB.Repositorio;

namespace WebSimuladorRH
{
    public partial class CadastroDeCentroDeCusto : BasePage
    {
        private CentroDeCustoController controller;
        private string funcaoDaPagina;

        public CentroDeCustoController Controller
        {
            private get { return controller; }
            set { controller = value; }
        }

        public string FuncaoDaPagina
        {
            get { return funcaoDaPagina; }
            set { funcaoDaPagina = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.controller = new CentroDeCustoController();
            FuncaoDaPagina = Request.QueryString["funcao"];

            VisibilidadeDropCentroCusto();
            if (!Page.IsPostBack)
            {
                PopularRepeaterDeContas();
                PopularDropDownDeHospitais();
                PopularDropDownDeCentrosDeCusto();
            }
        }

        private void VisibilidadeDropCentroCusto()
        {
            divAlteracao.Visible = (FuncaoDaPagina == "a");
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtDescricao.Text))
                    throw new Exception("Preencha a descrição do Centro de custo.");

                if (string.IsNullOrEmpty(txtCodigo.Text))
                    throw new Exception("Preencha o codigo do Centro de custo.");

                var novaListaDeContas = new List<Conta>();

                foreach (RepeaterItem item in rptContas.Items)
                {
                    if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                    {
                        if (((CheckBox)item.FindControl("chkSelecionado")).Checked == true)
                        {
                            var conta = Controller.BuscarContaPor((int.Parse(((HiddenField)item.FindControl("hdnIdConta")).Value)));
                            novaListaDeContas.Add(conta);
                        }
                    }
                }

                if (FuncaoDaPagina == "a")
                {
                    var centroDeCusto = Controller.BuscarCentroDeCustoPor(int.Parse(ddlCentroDeCustoASerAlterado.SelectedValue));

                    //if (orcamentosOperacionais != null && orcamentosOperacionais.Count > 0)
                    //    throw new Exception("Não é possivel alterar contras do Centro de Custo:" + centroDeCusto.Nome + " pois ele está atrelado a pelomenos um Orcamento de despesa operacional.");
                    centroDeCusto.AlterarNome(txtDescricao.Text);
                    centroDeCusto.CodigoDoCentroDeCusto = txtCodigo.Text;
                    centroDeCusto.Contas = novaListaDeContas;
                    Controller.Salvar(centroDeCusto);
                }
                else
                {
                    var centroRecuperado = Controller.BuscarCentroDeCustoPor(txtCodigo.Text);

                    if (centroRecuperado != null && centroRecuperado.Id > 0)
                        throw new Exception("Este codigo de centro de custo já existe, por favor escolha outro.");

                    var centroDeCusto = new CentroDeCusto(txtDescricao.Text);

                    centroDeCusto.CodigoDoCentroDeCusto = txtCodigo.Text;

                    centroDeCusto.Contas = novaListaDeContas;
                    Controller.Salvar(centroDeCusto);
                }

                Notificacao = "Centro de custo salvo com sucesso";
            }
            catch (Exception ex)
            {
                Notificacao = ex.Message;
            }

            EnviarMensagem();
            PopularDropDownDeCentrosDeCusto();
            LimparCampos();
        }

        public void PopularRepeaterDeContas()
        {

            rptContas.DataSource = Controller.BuscarTodosAsContas().Where(y => y.TipoConta.TipoContaEnum == TipoContaEnum.Outros).OrderBy(x => x.Nome);
            rptContas.DataBind();
        }

        public void PopularRepeaterDeContasPorCentroDeCusto(int id)
        {

            rptContas.DataSource = Controller.BuscarTodasAsContasDeUmCentroDeCusto(id);
            rptContas.DataBind();
        }

        public void PopularDropDownDeHospitais()
        {

            ddlHospitais.DataSource = Controller.BuscarTodosOsHospitais();
            ddlHospitais.DataValueField = "Id";
            ddlHospitais.DataTextField = "Nome";
            ddlHospitais.DataBind();

            ddlHospitais.Items.Insert(0, new ListItem("Selecione", ""));

        }

        private void PopularDropDownDeCentrosDeCusto()
        {
            ddlCentroDeCustoASerAlterado.DataSource = Controller.BuscarTodosOsCentrosDeCusto().OrderBy(x => x.Nome);
            ddlCentroDeCustoASerAlterado.DataValueField = "Id";
            ddlCentroDeCustoASerAlterado.DataTextField = "Nome";
            ddlCentroDeCustoASerAlterado.DataBind();

            ddlCentroDeCustoASerAlterado.Items.Insert(0, new ListItem("Selecione", ""));

        }

        private void PopularDropDownDeCentrosDeCustoPor(int idDepartamento, DropDownList dropDownASerPopulada)
        {

            dropDownASerPopulada.DataSource = Controller.BuscarTodosOsCentrosDeCustoDeUmDepartamento(idDepartamento);
            dropDownASerPopulada.DataValueField = "Id";
            dropDownASerPopulada.DataTextField = "Nome";
            dropDownASerPopulada.DataBind();
        }

        protected void ddlHospitais_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopularDropDownDeCentrosDeCustoPor(int.Parse(((DropDownList)sender).SelectedValue), ddlCentrosDeCusto);
        }

        protected void btnCopiarContas_Click(object sender, EventArgs e)
        {
            hdnCodigoCentroDeCusto.Value = ddlCentrosDeCusto.SelectedValue;

            PopularRepeaterDeContas();
        }

        protected void rptContas_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            foreach (RepeaterItem item in rptContas.Items)
            {
                if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                {
                    var idConta = (HiddenField)item.FindControl("hdnIdConta");

                    IList<Conta> listaContas = new List<Conta>();

                    if (hdnCodigoCentroDeCusto.Value != string.Empty)
                        listaContas = Controller.BuscarTodasAsContasDeUmCentroDeCusto(int.Parse(hdnCodigoCentroDeCusto.Value));
                    else
                    {
                        var centroDeCusto = Controller.BuscarDepartamento(int.Parse(Session["DepartamentoLogadoId"].ToString())).CentrosDeCusto.Where(x => x.Id == int.Parse(ddlCentrosDeCusto.SelectedValue));
                    }

                    foreach (var conta in listaContas)
                    {
                        if (conta.Id == int.Parse(idConta.Value))
                        {
                            ((CheckBox)item.FindControl("chkSelecionado")).Checked = true;

                            if (FuncaoDaPagina == "a")
                                ((CheckBox)item.FindControl("chkSelecionado")).Enabled = false;
                        }
                    }
                }
            }
        }

        protected void ddlHospitalASerAlterado_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopularDropDownDeCentrosDeCustoPor(int.Parse(((DropDownList)sender).SelectedValue), ddlCentroDeCustoASerAlterado);

            ddlCentroDeCustoASerAlterado.Items.Insert(0, new ListItem("Selecione", ""));
        }

        protected void ddlCentroDeCustoASerAlterado_SelectedIndexChanged(object sender, EventArgs e)
        {

            var centroDeCusto = Controller.BuscarCentroDeCustoPor(int.Parse(ddlCentroDeCustoASerAlterado.SelectedValue));

            txtCodigo.Text = centroDeCusto.CodigoDoCentroDeCusto;
            txtDescricao.Text = centroDeCusto.Nome;
            hdnIdCentroDeCusto.Value = centroDeCusto.Id.ToString();

            hdnCodigoCentroDeCusto.Value = centroDeCusto.Id.ToString();

            PopularRepeaterDeContas();
        }

        private void LimparCampos()
        {
            txtCodigo.Text = string.Empty;
            txtDescricao.Text = string.Empty;
            hdnCodigoCentroDeCusto.Value = string.Empty;
            hdnIdCentroDeCusto.Value = string.Empty;
        }
    }
}