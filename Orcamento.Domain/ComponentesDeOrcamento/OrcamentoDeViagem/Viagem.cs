using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace Orcamento.Domain
{
    public class Viagem : IAggregateRoot<int>
    {
        public virtual int Id
        {
            get;
            set;
        }
        public virtual IList<Ticket> Tickets { get; set; }
        public virtual Cidade Cidade { get; set; }

        public Viagem(Cidade cidade,List<Ticket> tickets) 
        {
            Contract.Requires(cidade != null,"Cidade deve ser informada.");
            Contract.Requires(tickets != null, "Os tickets devem ser informados");
            Contract.Requires(tickets.All(t => t.Cidade == cidade), "Os tickets devem ser da mesma cidade que a viagem");
            Contract.Requires(!tickets.Exists(t => t.TipoTicket.Id == 1), "Não deve ter ticket do tipo diaria na viagem, a viagem deve ser desatrelada da diaria");

            this.Cidade = cidade;
            this.Tickets = new List<Ticket>();
            InformarTickets(tickets);
        }

        private void InformarTickets(List<Ticket> tickets)
        {
            foreach (var item in tickets)
            {
                InformarTicket(item);
            }
        }

        public Viagem() 
        { }

        public virtual void InformarTicket(Ticket ticket)
        {
            Contract.Requires(ticket != null,"Ticket informado está nulo.");
            Contract.Requires(ticket.Cidade != null,"Cidade do Ticket não informada.");
            Contract.Requires(ticket.Cidade == this.Cidade,"Cidade do ticket deve ser igual a cidade da viagem.");
            Contract.Requires(ticket.TipoTicket != null, "Tipo do ticket não informado.");

            this.Tickets.Add(ticket);
        }
    }
}
