using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;

namespace Orcamento.Domain.DB.Mappings
{
    public class OrcamentoOperacionalVersaoMap : SubclassMap<OrcamentoOperacionalVersao>
    {
        public OrcamentoOperacionalVersaoMap()
        {
            DiscriminatorValue("2");
            HasMany(d => d.DespesasOperacionais).Cascade.All();
        }
    }
}
