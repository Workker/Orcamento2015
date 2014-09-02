using FluentNHibernate.Mapping;
using Orcamento.Domain.Robo.Monitoramento.EstruturaOrcamentaria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Orcamento.Domain.DB.Mappings.Entidades.Robo
{
    public class TipoProcessoMap : ClassMap<TipoProcesso>
    {
        public TipoProcessoMap()
        {
            Id(x => x.Id);
            Map(x => x.Tipo);
        }
    }
}
