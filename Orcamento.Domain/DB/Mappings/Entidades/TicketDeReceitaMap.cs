using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Orcamento.Domain.ComponentesDeOrcamento.OrcamentoDeProducao;

namespace Orcamento.Domain.DB.Mappings.Entidades
{
    public class TicketDeReceitaMap : ClassMap<TicketDeReceita>
    {
        public TicketDeReceitaMap()
        {
            Id(t => t.Id);
            References(t => t.Hospital);
            HasMany(t => t.Parcelas).Cascade.All() ;
            Map(t => t.Nome);
            Map(t => t.TipoTicket);
        }
    }
}
