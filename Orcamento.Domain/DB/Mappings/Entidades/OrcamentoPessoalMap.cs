using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;

namespace Orcamento.Domain.DB.Mappings.Entidades
{
    public class OrcamentoPessoalMap : SubclassMap<OrcamentoPessoal>
    {
        public OrcamentoPessoalMap()
        {
            DiscriminatorValue("4");
        }
    }
}
