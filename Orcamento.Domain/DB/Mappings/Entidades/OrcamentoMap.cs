using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;

namespace Orcamento.Domain.DB.Mappings
{
    public class OrcamentoMap : ClassMap<Orcamento>
    {
        public OrcamentoMap() 
        {
            Id(o => o.Id);
            References(o => o.Setor);
            References(o => o.CentroDeCusto);
            Map(o => o.Ano);
            Map(o => o.VersaoFinal);
            Map(o => o.NomeOrcamento);
            DiscriminateSubClassesOnColumn("TipoOrcamento");
        }
    }
}
