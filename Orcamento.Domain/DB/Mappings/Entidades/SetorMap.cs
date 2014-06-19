using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Orcamento.Domain;

namespace Orcamento.Domain.DB.Mappings
{
    public class SetorMap : SubclassMap<Setor>
    {
        public SetorMap() 
        {
            DiscriminatorValue("2");
        }
    }
}
