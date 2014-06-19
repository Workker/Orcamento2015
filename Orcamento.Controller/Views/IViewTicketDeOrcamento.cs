using System.Collections.Generic;
using Orcamento.Domain;

namespace Orcamento.Controller.Views
{
    public interface IViewTicketDeOrcamento
    {
        void Carregar(IList<TicketDeOrcamentoPessoal> ticketsDeOrcamentoPessoal);
    }
}
