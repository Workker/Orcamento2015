using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Orcamento.Domain.ComponentesDeOrcamento.OrcamentoDeProducao;
using FluentNHibernate.Mapping;

namespace Orcamento.Domain.DB.Mappings.Entidades
{
    public class CustoUnitarioMap : ClassMap<CustoUnitario>
    {
        public CustoUnitarioMap()
        {
            Id(s => s.Id);
            References(s => s.SubSetor);
            References(s => s.Setor);
            HasMany(s => s.Valores).Cascade.All();
        }
    }
}
