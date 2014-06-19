using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Orcamento.Domain.ComponentesDeOrcamento.OrcamentoPessoal;
using FluentNHibernate.Mapping;

namespace Orcamento.Domain.DB.Mappings.Entidades
{
    public class ControleDeCentroDeCustoMap : ClassMap<ControleDeCentroDeCusto>
    {
        public ControleDeCentroDeCustoMap()
        {
            Id(c => c.Id);
            References(c => c.Departamento);
            References(c => c.CentroDeCusto);
            Map(c => c.Salvo);
        }
    }
}
