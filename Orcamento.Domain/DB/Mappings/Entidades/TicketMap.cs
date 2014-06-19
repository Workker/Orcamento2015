using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;

namespace Orcamento.Domain.DB.Mappings
{
    public  class TicketMap : ClassMap<Ticket>
    {
        public TicketMap() 
        {
            Id(t => t.Id);
            References(t => t.TipoTicket);
            References(t => t.Cidade);
            Map(t => t.Valor);
        }
    }
}
