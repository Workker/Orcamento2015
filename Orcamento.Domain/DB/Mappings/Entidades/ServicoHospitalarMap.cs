using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;

namespace Orcamento.Domain.DB.Mappings
{
    public class ServicoHospitalarMap : ClassMap<ServicoHospitalar>
    {
        public ServicoHospitalarMap() 
        {
            Id(s => s.Id);
            References(s => s.Conta);
            References(s => s.SubSetor);
            References(s => s.Setor);
            HasMany(s => s.Valores).Cascade.All();
        }
    }
}
