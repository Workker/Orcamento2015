using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace Orcamento.Domain
{
    public class Ticket : IAggregateRoot<int>
    {
        public virtual TipoTicket TipoTicket { get; set; }
        public virtual long Valor { get; set; }
        public virtual Cidade Cidade { get; set; }

        public virtual string NomeCidade { get { return this.Cidade.Descricao; } }
        public virtual string Descricao { get { return this.TipoTicket.Descricao; } }

        public Ticket(TipoTicket tipoTicket, Cidade cidade)
        {
            Contract.Requires(tipoTicket != null, "Tipo do ticket não informado.");
            Contract.Requires(cidade != null, "Cidade não informada.");

            this.TipoTicket = tipoTicket;
            this.Cidade = cidade;
        }

        public Ticket() { }

        public virtual int Id
        {
            get;
            set;
        }
    }
}
