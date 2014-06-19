using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Orcamento.Domain.ComponentesDeOrcamento.OrcamentoDeViagem;

namespace Orcamento.Domain.DB.Mappings
{
    public class OrcamentoDeViagemMap  : SubclassMap<OrcamentoDeViagem>
    {
        public OrcamentoDeViagemMap()
        {
            DiscriminatorValue("1");
            HasMany(o => o.Despesas).Cascade.All();
        }
    }
}
