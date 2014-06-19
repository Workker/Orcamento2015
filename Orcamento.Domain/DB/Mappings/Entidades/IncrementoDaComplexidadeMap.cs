using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;

namespace Orcamento.Domain.DB.Mappings
{
    public class IncrementoDaComplexidadeMap : ClassMap<IncrementoDaComplexidade>
    {
        public IncrementoDaComplexidadeMap() 
        {
            Id(i => i.Id);
            Map(i => i.Mes);
            Map(i => i.Ticket);
            Map(i => i.Complexidade);
            Map(i => i.Negativo);
        }
    }
}
