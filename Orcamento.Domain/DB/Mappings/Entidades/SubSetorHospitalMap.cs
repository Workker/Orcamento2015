using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;

namespace Orcamento.Domain.DB.Mappings
{
    public class SubSetorHospitalMap : ClassMap<SubSetorHospital>
    {
        public SubSetorHospitalMap() 
        {
            Id(s => s.Id);
            Map(s => s.NomeSetor);
        }
    }
}
