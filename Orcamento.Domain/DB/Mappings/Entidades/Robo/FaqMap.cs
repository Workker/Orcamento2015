using FluentNHibernate.Mapping;
using Orcamento.Domain.Robo.Faq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Orcamento.Domain.DB.Mappings.Entidades.Robo
{
    public class FaqMap : ClassMap<Faq>
    {
        public  FaqMap()
        {
            Id(d => d.Id).GeneratedBy.Identity();
            References(f => f.TipoFaq);
            Map(f => f.Nome);
            HasMany(f => f.Perguntas).Cascade.All();
        }
    }
}
