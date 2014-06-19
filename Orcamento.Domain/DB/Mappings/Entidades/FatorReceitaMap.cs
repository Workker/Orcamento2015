using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;

namespace Orcamento.Domain.DB.Mappings
{
    public class FatorReceitaMap : ClassMap<FatorReceita>
    {
        public FatorReceitaMap() 
        {
            Id(f => f.Id);
            References(f => f.Setor);
            References(f => f.SubSetor);
            HasMany(f => f.Incrementos).Cascade.All();
        }
    }
}
