using FluentNHibernate.Mapping;
using Orcamento.Domain.Robo.Faq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Orcamento.Domain.DB.Mappings.Entidades.Robo
{
    public class PerguntaMap : ClassMap<Pergunta>
    {
        public  PerguntaMap()
        {
            Id(p => p.Id).GeneratedBy.Identity();
            Map(p => p.Nome);
            Map(p => p.Resposta);
        }
    }
}
