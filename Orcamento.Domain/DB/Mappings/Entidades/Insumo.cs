using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Orcamento.Domain.ComponentesDeOrcamento.OrcamentoDeProducao;
using FluentNHibernate.Mapping;

namespace Orcamento.Domain.DB.Mappings.Entidades
{
    public class InsumoMap : ClassMap<Insumo>
    {
        InsumoMap()
        {
            Id(i => i.Id);
            References(i => i.Departamento);
            HasMany(d => d.CustosUnitarios).Cascade.All();
        }
    }
}
