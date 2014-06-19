using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace Orcamento.Domain
{
    public class Diaria : IAggregateRoot<int>
    {
        public virtual int Id
        {
            get;
            set;
        }
        public virtual Cidade Cidade { get; set; }
        public virtual IList<Ticket> Tickets { get; set; }



        public Diaria(Cidade cidade, List<Ticket> tickets)
        {
            Contract.Requires(cidade != null, "Cidade não informada.");
            Contract.Requires(tickets != null, "Tickets não informados.");

            this.Cidade = cidade;
            this.Tickets = new List<Ticket>();
            InformarTickets(tickets);
        }

        public Diaria()
        { }

        private void InformarTickets(List<Ticket> tickets)
        {
            foreach (var item in tickets)
            {
                InformarTicket(item);
            }
        }

        public virtual void InformarTicket(Ticket ticket)
        {
            Contract.Requires(ticket != null, "Ticket informado está nulo.");
            Contract.Requires(ticket.Cidade != null, "Cidade do Ticket não informada.");
            Contract.Requires(ticket.Cidade.Equals(this.Cidade), "Cidade do ticket deve ser igual a cidade da viagem.");
            Contract.Requires(ticket.TipoTicket != null, "Tipo do ticket não informado.");

            if(this.Tickets == null)
                this.Tickets = new List<Ticket>();

            this.Tickets.Add(ticket);
        }
    }
}
