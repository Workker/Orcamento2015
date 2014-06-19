using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Orcamento.Domain;

namespace Orcamento.Domain.DB.Mappings.Entidades
{
    public class TicketParcelaMap : ClassMap<TicketParcela>
    {
        public TicketParcelaMap() 
        {
            Id(t => t.Id);
            Map(t => t.Mes);
            Map(t => t.Valor);
            Map(t => t.Negativo);
        }
    }
}
