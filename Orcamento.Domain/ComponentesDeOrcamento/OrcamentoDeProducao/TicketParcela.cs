using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Orcamento.Domain
{
    [Serializable]
    public class TicketParcela : IAggregateRoot<int>
    {
        public virtual int Id
        {
            get;
            set;
        }

        public virtual bool Negativo { get; set; }

        public virtual MesEnum Mes{get;set;}

        public virtual double Valor { get; set; }

        public TicketParcela(MesEnum mesEnum)
        {
            this.Mes = mesEnum;
        }

        protected TicketParcela() { }
    }
}
