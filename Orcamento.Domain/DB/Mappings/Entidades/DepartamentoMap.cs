using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Orcamento.Domain.Gerenciamento;

namespace Orcamento.Domain.DB.Mappings
{
    public class DepartamentoMap : ClassMap<Departamento>
    {
        public DepartamentoMap() 
        {
            Id(o => o.Id);
            DiscriminateSubClassesOnColumn("TipoOrcamento");
            Map(s => s.Nome);
            HasManyToMany(s => s.CentrosDeCusto).Cascade.All();
            //HasManyToMany(x => x.Setores).Cascade.All();
            HasManyToMany(x => x.Setores);

        }
    }
    
}
