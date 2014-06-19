using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Orcamento.Domain.Gerenciamento;

namespace Orcamento.Domain.DB.Mappings.Entidades
{
    public class ContaTicketMap : ClassMap<ContaTicket>
    {
        public ContaTicketMap()
        {
            Id(c => c.Id);
            Map(c => c.Ticket);
        }
    }
}
