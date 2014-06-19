using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using Orcamento.Controller;
using Orcamento.Domain.ComponentesDeOrcamento.OrcamentoPessoal.Despesas;
using Orcamento.Domain.DB.Repositorio;
using Orcamento.Domain;
using Orcamento.Domain.Gerenciamento;

namespace WebSimuladorRH.Pessoal
{
    public partial class OrcamentoPessoal : BasePage
    {
        #region Atributos

        private OrcamentoPessoalController controller;

        #endregion

        #region Propriedades

        public Departamento Departamento
        {
            get
            {
                return (Departamento)Session["DepartamentoLogado"];
            }
            set
            {
                Session["DepartamentoLogado"] = value;
            }
        }

        public Orcamento.Domain.Orcamento Orcamento
        {
            get
            {
                return (Orcamento.Domain.Orcamento)Session["OrcamentoCriado"];
            }
            set
            {
                Session["OrcamentoCriado"] = value;
            }
        }

        public List<DespesaPessoal> DespesasPessoaisInformadas
        {
            get
            {
                return (List<DespesaPessoal>)Session["DespesasPessoaisInformadas"];
            }
            set
            {
                Session["DespesasPessoaisInformadas"] = value;
            }
        }

        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            controller = new OrcamentoPessoalController();

            Departamento = controller.ObterDepartamentoPor((int)Session["DepartamentoLogadoId"]);

            if (Page.IsPostBack) return;

            PreencherDropCentroDeCusto();
        }

        protected void SelecionandoOCentroDeCusto(object sender, EventArgs e)
        {
            if (dropCentroDeCusto.SelectedValue != "0")
            {
                divHeadCount.Visible = true;

                var centroDeCustoId = int.Parse(dropCentroDeCusto.SelectedValue);

                var centroDeCusto = controller.ObterCentroDeCustoPor(centroDeCustoId: centroDeCustoId);

                HeadCount.Text = centroDeCusto.Funcionarios.Where(f => f.Departamento.Id == Departamento.Id).Count().ToString();

                PreencherOrcamentos();
            }
        }

        #endregion

        #region Métodos

        public void PreencherDropCentroDeCusto()
        {
            var centrosDeCusto = controller.ObterCentrosDeCustoPor(Departamento);

            dropCentroDeCusto.DataValueField = "Id";
            dropCentroDeCusto.DataTextField = "Nome";
            dropCentroDeCusto.DataSource = centrosDeCusto;
            dropCentroDeCusto.DataBind();

            dropCentroDeCusto.Items.Insert(0, new ListItem("Selecione", "0"));
        }

        private void PreencherOrcamentos()
        {
            var centroDeCustoId = int.Parse(dropCentroDeCusto.SelectedValue);

            rptDespesasPessoais.DataSource = controller.ObterTotalizadorDePessoalPor(centroDeCustoId,Departamento);
            rptDespesasPessoais.DataBind();
        }

        #endregion
    }
}