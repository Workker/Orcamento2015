using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;

namespace Orcamento.Domain.DB.Mappings
{
    public class ContaHospitalMap : ClassMap<ContaHospital>
    {
        public ContaHospitalMap() 
        {
            Id(c => c.Id);
            Map(c => c.Nome);
            Map(c => c.TipoValorContaEnum);
            Map(c => c.ContabilizaProducao);
            Map(c => c.MultiPlicaPorMes);
            Map(c => c.Calculado);
            HasMany(c => c.ContasAnexadas);
        }
    }
}
