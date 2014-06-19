using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;

namespace Orcamento.Domain.DB.Mappings
{
    public class DiariaMap : ClassMap<Diaria>
    {
        public DiariaMap()
        {
            Id(i => i.Id);
            References(d => d.Cidade);
            HasManyToMany(i => i.Tickets).Cascade.All();
        }
    }
}
