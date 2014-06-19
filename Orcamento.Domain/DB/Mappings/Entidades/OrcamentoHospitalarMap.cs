using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;

namespace Orcamento.Domain.DB.Mappings
{
    public class OrcamentoHospitalarMap : SubclassMap<OrcamentoHospitalar>
    {
        public OrcamentoHospitalarMap()
        {
            DiscriminatorValue("3");
            HasMany(d => d.Servicos).Cascade.All();
           // HasMany(d => d.CustosUnitarios).Cascade.All();
            HasMany(o => o.FatoresReceita).Cascade.All();
            Map(o => o.MemoriaDeCalculoComplexidade);
            Map(o => o.MemoriaDeCalculoUnitarios);
        }
    }
}
