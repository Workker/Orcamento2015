using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Orcamento.Controller;
using Orcamento.Domain.Gerenciamento;

namespace WebSimuladorRH
{
    public partial class AlteracaoDeCentroDeCusto : System.Web.UI.Page
    {
        private CentroDeCustoController controller;

        public CentroDeCustoController Controller
        {
            private get { return controller; }
            set { controller = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.controller = new CentroDeCustoController();

            if (!Page.IsPostBack)
            {
                
                PopularDropDownDeHospitais();
                ddlHospitais.SelectedValue = Session["DepartamentoLogadoId"].ToString();
                PopularDropDownDeCentrosDeCustoPor(int.Parse(ddlHospitais.SelectedValue));
                PopularRepeaterDeContas();
            }

            hdnCodigoCentroDeCusto.Value = ddlCentrosDeCusto.SelectedValue;
        }

        public void PopularDropDownDeHospitais()
        {

            ddlHospitais.DataSource = Controller.BuscarTodosOsHospitais();
            ddlHospitais.DataValueField = "Id";
            ddlHospitais.DataTextField = "Nome";
            ddlHospitais.DataBind();

            ddlHospitais.Items.Insert(0, new ListItem("Selecione", ""));
        }

        public void PopularDropDownDeCentrosDeCustoPor(int idDepartamento)
        {

            ddlCentrosDeCusto.DataSource = Controller.BuscarTodosOsCentrosDeCustoDeUmDepartamento(idDepartamento);
            ddlCentrosDeCusto.DataValueField = "CodigoDoCentroDeCusto";
            ddlCentrosDeCusto.DataTextField = "Nome";
            ddlCentrosDeCusto.DataBind();
        }

        public void PopularRepeaterDeContas()
        {

            rptContas.DataSource = Controller.BuscarTodosAsContas().Where(y => y.TipoConta.TipoContaEnum == TipoContaEnum.Outros).OrderBy(x => x.Nome);
            rptContas.DataBind();
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
                        listaContas = Controller.BuscarTodasAsContasDeUmCentroDeCusto(hdnCodigoCentroDeCusto.Value);
                    else
                    {
                        var centroDeCusto = Controller.BuscarDepartamento(int.Parse(Session["DepartamentoLogadoId"].ToString())).CentrosDeCusto.Where(x => x.Id == int.Parse(ddlCentrosDeCusto.SelectedValue));
                    }

                    foreach (var conta in listaContas)
                    {
                        if (conta.Id == int.Parse(idConta.Value))
                        {
                            ((CheckBox)item.FindControl("chkSelecionado")).Checked = true;
                        }
                    }
                }
            }
        }

        protected void ddlHospitais_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopularDropDownDeCentrosDeCustoPor(int.Parse(((DropDownList)sender).SelectedValue));
        }

        protected void btnCopiarContas_Click(object sender, EventArgs e)
        {
            hdnCodigoCentroDeCusto.Value = ddlCentrosDeCusto.SelectedValue;

            PopularRepeaterDeContas();
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            var antigaListaDeContas = Controller.BuscarTodasAsContasDeUmCentroDeCusto((ddlCentrosDeCusto.SelectedValue));
            var novaListaDeContas = new List<Conta>();

            foreach (RepeaterItem item in rptContas.Items)
            {
                if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                {
                    if (((CheckBox)item.FindControl("chkSelecionado")).Checked == true)
                    {
                        var conta = new Conta();
                        conta = Controller.BuscarContaPor(((HiddenField)item.FindControl("hdnCodigoDaConta")).Value);
                        novaListaDeContas.Add(conta);
                    }
                }
            }


            var centroDeCusto = Controller.BuscarCentroDeCustoPor(int.Parse(ddlCentrosDeCusto.SelectedValue));

            centroDeCusto.Contas = novaListaDeContas;

            Controller.Salvar(centroDeCusto);
        }
    }
}