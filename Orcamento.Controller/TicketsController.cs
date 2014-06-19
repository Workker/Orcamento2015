using Orcamento.Controller.Views;
using Orcamento.Domain;
using Orcamento.Domain.DB.Repositorio;
using Orcamento.Domain.Gerenciamento;

namespace Orcamento.Controller
{
    public class TicketsController
    {
        private IViewTicketDeOrcamento view;
        private ITicketsDeOrcamentoPessoal ticketsDeOrcamento;

        public IViewTicketDeOrcamento View
        {
            get { return view; }
            set { view = value; }
        }

        public ITicketsDeOrcamentoPessoal TicketsDeOrcamento
        {
            get { return ticketsDeOrcamento ?? (ticketsDeOrcamento = new TicketsDeOrcamentoPessoal()); }
            set { ticketsDeOrcamento = value; }
        }

        public void CarregarPagina(Departamento departamento)
        {
            View.Carregar(TicketsDeOrcamento.Todos(departamento));
        }

        public void Salvar(TicketDeOrcamentoPessoal ticket)
        {
            TicketsDeOrcamento.Salvar(ticket);
        }
    }
}
