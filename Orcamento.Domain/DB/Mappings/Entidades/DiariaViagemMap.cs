using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;

namespace Orcamento.Domain.DB.Mappings
{
    public class DiariaViagemMap : SubclassMap<DiariaViagem>
    {
        public DiariaViagemMap()
        {
            DiscriminatorValue("1");
            References(v => v.Diaria);
        }
    }
}
