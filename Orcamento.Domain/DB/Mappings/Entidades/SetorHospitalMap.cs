using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;

namespace Orcamento.Domain.DB.Mappings 
{
    public class SetorHospitalarMap : ClassMap<SetorHospitalar>
    {
        public SetorHospitalarMap() 
        {
            Id(s => s.Id);
            Map(s => s.NomeSetor);
            HasManyToMany(s => s.SubSetores).Cascade.All();
            HasManyToMany(c => c.Contas).Cascade.All();
        }
    }
}
