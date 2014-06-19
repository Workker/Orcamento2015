using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Orcamento.Domain.ComponentesDeOrcamento.OrcamentoPessoal;
using FluentNHibernate.Mapping;

namespace Orcamento.Domain.DB.Mappings.Entidades
{
    public class AcordoDeConvencaoMap : ClassMap<AcordoDeConvencao>
    {
        public AcordoDeConvencaoMap() 
        {
            Id(i => i.Id);
            Map(i => i.MesAumento);
            Map(i => i.Porcentagem);
            References(i => i.Departamento);
        }
    }
}
