using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Orcamento.Domain.Gerenciamento;
using Orcamento.Domain.DB.Repositorio;

namespace WebSimuladorRH
{
    public partial class ControleDeCentroDeCusto : System.Web.UI.Page
    {
        public Departamento Departamento
        {
            get { return (Departamento)Session["DepartamentoProducao"]; }
            set { Session["DepartamentoProducao"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var departamentos = new Departamentos();
            ControlesCentroDeCusto controles = new ControlesCentroDeCusto();
            Departamento = departamentos.Obter((int)Session["DepartamentoLogadoId"]);


            if (!Page.IsPostBack)
            {
                PreencherControles(controles);
            }
        }

        private void PreencherControles(ControlesCentroDeCusto controles)
        {
            rptControleDeCentroDeCustos.DataSource = controles.ObterASalvarPor(Departamento);
            rptControleDeCentroDeCustos.DataBind();
        }
    }
}