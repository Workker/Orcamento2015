using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Orcamento.Domain;
using Orcamento.Domain.DB.Repositorio;

namespace WebSimuladorRH
{
    public partial class Tickets : BasePage
    {
        private Orcamento.Domain.DB.Repositorio.TicketsDeViagens repositorio;
        public Orcamento.Domain.DB.Repositorio.TicketsDeViagens RepositorioTickets
        {
            get { return repositorio ?? (repositorio = new Orcamento.Domain.DB.Repositorio.TicketsDeViagens()); }
            set { repositorio = value; }
        }

        public List<Ticket> TicketsSource
        {
            get
            {
                return (List<Ticket>)Session["TicketsSource"];
            }
            set
            {
                Session["TicketsSource"] = value;
                PreencherTickets();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            VerificaSeExisteSessaoDeUsuario();

            VerificarSeUsuarioCoorporativo();

            if (!Page.IsPostBack)
            {
                TicketsSource = RepositorioTickets.Todos<Ticket>().ToList();
            }
        }

        private void PreencherTickets()
        {
            rptTickets.DataSource = TicketsSource.OrderBy(x => x.NomeCidade).ThenBy(y => y.TipoTicket.Descricao);
            rptTickets.DataBind();
        }

        protected void Unnamed1_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (RepeaterItem ticket in rptTickets.Items)
                {
                    var txtValor = (TextBox)ticket.FindControl("txtValor");
                    var id = (HiddenField)ticket.FindControl("Id");

                    if (!string.IsNullOrEmpty(txtValor.Text))
                    {
                        var firstOrDefault = TicketsSource.FirstOrDefault(t => t.Id == Convert.ToInt32(id.Value));
                        if (firstOrDefault != null)
                            firstOrDefault.Valor =
                                Convert.ToInt32(txtValor.Text.Replace("(", "").Replace(")", "").Replace(",00","").Replace(".",""));
                    }
                }

                foreach (var item in TicketsSource)
                {
                    RepositorioTickets.Salvar(item);
                }
            }
            catch (Exception ex)
            {
                Notificacao = ex.Message;
            }
            EnviarMensagem();
        }

        protected void IrParaPainelOrcamento_Clik(object sender, EventArgs e)
        {
            IrParaPainelOrcamento();
        }

        public void IrParaPainelOrcamento()
        {
            Response.Redirect("/PainelOrcamento.aspx");
        }
    }
}