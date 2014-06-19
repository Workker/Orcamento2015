using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;

namespace Orcamento.Domain.DB.Mappings
{
    public class ViagemMap : ClassMap<Viagem>
    {
        public ViagemMap() 
        {
            Id(v => v.Id);
            References(v => v.Cidade);
            HasManyToMany(v => v.Tickets).Cascade.All();
        }
    }
}
