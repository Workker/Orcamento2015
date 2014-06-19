using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;

namespace Orcamento.Domain.DB.Mappings
{
    public class IDespesaDeViagemMap : ClassMap<DespesaDeViagem>
    {
        public IDespesaDeViagemMap()
        {
            Id(d => d.Id);
            Map(d => d.Mes);
            Map(d => d.Quantidade);
            DiscriminateSubClassesOnColumn("TipoDespesa");
        }
    }
}
