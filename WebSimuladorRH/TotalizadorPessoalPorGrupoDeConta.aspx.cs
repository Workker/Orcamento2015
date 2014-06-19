using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Orcamento.Domain;
using Orcamento.Domain.DB.Repositorio;
using Orcamento.Domain.Gerenciamento;

namespace WebSimuladorRH
{
    public partial class TotalizadorPessoalPorGrupoDeConta : BasePage
    {
        public Departamento SetorSelecionado
        {
            get
            {
                return (Departamento)Session["SetorSelecionado"];
            }
            set
            {
                Session["SetorSelecionado"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            VerificaSeExisteSessaoDeUsuario();
            if (!Page.IsPostBack)
            {
                VerificaSeExisteSessaoDeUsuario();
                Departamentos setores = new Departamentos();
                var setorSelecionado = setores.Obter((int)Session["DepartamentoLogadoId"]);

                dropCentroCusto.DataSource = setorSelecionado.CentrosDeCusto.OrderBy(x  => x.Nome);
                dropCentroCusto.DataTextField = "Nome";
                dropCentroCusto.DataValueField = "Id";
                dropCentroCusto.DataBind();

                dropCentroCusto.Items.Insert(0, new ListItem("Todos", "0"));

                PopularRepeater();
            }
        }

        private void PopularRepeater()
        {
          //  var despesasPessoais = new DespesasPessoais();
            //rptContas.DataSource = despesasPessoais.Listar();
            //rptContas.DataBind();
        }

        //protected void dropCentroCusto_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (dropCentroCusto.SelectedValue == "0")
        //        PopularRepeater();
        //    else
        //    {

        //        var despesasPessoais = new DespesasPessoais();
        //        rptContas.DataSource = despesasPessoais.Listar(dropCentroCusto.SelectedValue);
        //        rptContas.DataBind();
        //    }
        //}
    }
}