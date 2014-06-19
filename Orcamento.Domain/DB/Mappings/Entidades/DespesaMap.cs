using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;

namespace Orcamento.Domain.DB.Mappings
{
    public class DespesaMap : ClassMap<Despesa>
    {
        public DespesaMap() 
        {
            Id(d => d.Id);
            Map(d => d.Mes);
            Map(d => d.Valor);
            Map(d => d.MemoriaDeCalculo);
            References(d => d.Conta);
        }
    }
}
