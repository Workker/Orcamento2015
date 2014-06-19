using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Orcamento.Domain.DB.Repositorio;
using Orcamento.Domain;
using Orcamento.Domain.Gerenciamento;
using Orcamento.Domain.ComponentesDeOrcamento.OrcamentoDeProducao;

namespace WebSimuladorRH
{
    public partial class TicketDeProducao : BasePage
    {
        public IList<Orcamento.Domain.TicketDeProducao> Tickets
        {
            get { return (IList<Orcamento.Domain.TicketDeProducao>)Session["TicketDeProducao"]; }
            set { Session["TicketDeProducao"] = value; }
        }

        public IList<TicketDeReceita> TicketsDeReceita
        {
            get { return (IList<TicketDeReceita>)Session["TicketsDeReceita"]; }
            set { Session["TicketsDeReceita"] = value; }
        }

        public Departamento Departamento { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                PreencherUnitarios();
            }
        }

        private void PreencherUnitarios()
        {
            int id = int.Parse(Session["DepartamentoLogadoId"].ToString());

            Departamentos departamentos = new Departamentos();
            Departamento = departamentos.Obter(id);

            if (Departamento.GetType() != typeof(Hospital))
                Response.Redirect("/PainelOrcamento.aspx");

            TicketsDeProducao tickets = new TicketsDeProducao();
            Tickets = tickets.Todos(Departamento);

            TicketsDeReceita ticketsDeReceita = new Orcamento.Domain.DB.Repositorio.TicketsDeReceita();
            TicketsDeReceita = ticketsDeReceita.Todos(Departamento);

            RptUnitarios.DataSource = Tickets.OrderBy(x => x.Setor.NomeSetor).ThenBy(y => y.SubSetor.NomeSetor);
            RptUnitarios.DataBind();

            rptTipoDeTickets.DataSource = TicketsDeReceita.OrderBy(t => t.Nome);
            rptTipoDeTickets.DataBind();
        }

        private void PreencherValoresDeProducao()
        {
            foreach (RepeaterItem fatorReceita in RptUnitarios.Items)
            {
                var rptIncrementos = (Repeater)fatorReceita.FindControl("rptIncrementos");
                var idFator = (HiddenField)fatorReceita.FindControl("Id");


                foreach (RepeaterItem repeaterComplexidade in rptIncrementos.Items)
                {
                    var incrementoId = (HiddenField)repeaterComplexidade.FindControl("Id");
                    var txtValor = (TextBox)repeaterComplexidade.FindControl("Valor");
                    var mes = (HiddenField)repeaterComplexidade.FindControl("Mes");

                    var incremento =
                        Tickets.Where(f => f.Id == int.Parse(idFator.Value)).FirstOrDefault().
                            Parcelas.Where(i => i.Id == int.Parse(incrementoId.Value)).FirstOrDefault();
                    float ticket = 0;

                    if (float.TryParse(txtValor.Text, out ticket))
                        incremento.Valor = ticket;
                }
            }
        }

        private void PreencherValoresDeReceita()
        {
            foreach (RepeaterItem fatorReceita in rptTipoDeTickets.Items)
            {
                var rptIncrementos = (Repeater)fatorReceita.FindControl("rptIncrementos");
                var idFator = (HiddenField)fatorReceita.FindControl("Id");


                foreach (RepeaterItem repeaterComplexidade in rptIncrementos.Items)
                {
                    var incrementoId = (HiddenField)repeaterComplexidade.FindControl("Id");
                    var txtValor = (TextBox)repeaterComplexidade.FindControl("Valor");
                    var mes = (HiddenField)repeaterComplexidade.FindControl("Mes");

                    var incremento =
                        TicketsDeReceita.Where(f => f.Id == int.Parse(idFator.Value)).FirstOrDefault().
                            Parcelas.Where(i => i.Id == int.Parse(incrementoId.Value)).FirstOrDefault();
                    double ticket = 0;

                    incremento.Negativo = txtValor.Text.Substring(0, 1) == "-";
                    if (double.TryParse(txtValor.Text.Replace("%", "").Replace("-", ""), out ticket))
                        incremento.Valor = ticket;
                }
            }
        }

        protected void PreencherTicketDeReceita(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var ticketParcela = (TicketParcela)e.Item.DataItem;
                var txtValor = (TextBox)e.Item.FindControl("Valor");

                if (ticketParcela.Negativo)
                    txtValor.Text += "-";

                txtValor.Text += ticketParcela.Valor.ToString("#,###,###.#0");

                txtValor.Text += "%";
            }
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                PreencherValoresDeProducao();
                PreencherValoresDeReceita();
                SalvarUnitarios();
                SalvarTicketsDeReceita();
            }
            catch (Exception ex)
            {
                Notificacao = ex.Message;
            }
            EnviarMensagem();
        }

        private void SalvarTicketsDeReceita()
        {
            TicketsDeReceita tickets = new TicketsDeReceita();

            foreach (var ticket in TicketsDeReceita)
            {
                tickets.Salvar(ticket);
            }
        }

        private void SalvarUnitarios()
        {
            TicketsDeProducao tickets = new TicketsDeProducao();

            foreach (var ticket in Tickets)
            {
                tickets.Salvar(ticket);
            }
        }
    }
}