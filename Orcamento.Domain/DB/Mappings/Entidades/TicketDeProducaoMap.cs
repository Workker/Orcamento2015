using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;

namespace Orcamento.Domain.DB.Mappings.Entidades
{
    public class TicketDeProducaoMap : ClassMap<TicketDeProducao>
    {
        public TicketDeProducaoMap()
        {
            Id(t => t.Id);
            References(t => t.Setor);
            References(t => t.SubSetor);
            References(t => t.Hospital);
            HasMany(t => t.Parcelas).Cascade.All();
        }
    }
}
