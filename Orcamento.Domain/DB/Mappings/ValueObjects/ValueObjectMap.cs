using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;

namespace Orcamento.Domain.DB.Mappings
{
    public abstract class ValueObjectMap<T> : ClassMap<T> where T : ValueObject
    {
        public ValueObjectMap()
        {
            Id(x => x.Id);
            Map(x => x.Descricao);
        }
    }
}
