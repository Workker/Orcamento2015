using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;

namespace Orcamento.Domain.DB.Mappings
{
    public class ViagemCidadeMap : SubclassMap<ViagemCidade>
    {
        public ViagemCidadeMap() 
        {
            DiscriminatorValue("2");
            References(v => v.Viagem);
        }
    }
}
