using FluentNHibernate.Mapping;
using Orcamento.Domain.Robo.Faq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Orcamento.Domain.DB.Mappings.Entidades.Robo
{
    public class TipoFaqMap : ValueObjectMap<TipoFaq>
    {
        public TipoFaqMap()
        {
            Id(x => x.Id).GeneratedBy.Assigned();

        }
    }
}
