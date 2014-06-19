using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;

namespace Orcamento.Domain.DB.Mappings
{
    public class ValorServicoHospitalarMap :ClassMap<ProducaoHospitalar>
    {
        public ValorServicoHospitalarMap() 
        {
            Id(v => v.Id);
            Map(c => c.Mes);
            Map(c => c.Valor);
        }
    }
}
