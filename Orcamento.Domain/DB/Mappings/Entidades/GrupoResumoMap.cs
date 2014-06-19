using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Orcamento.Domain.Gerenciamento;
using FluentNHibernate.Mapping;

namespace Orcamento.Domain.DB.Mappings.Entidades
{
    public class GrupoResumoMap : ClassMap<GrupoResumo>
    {
        public GrupoResumoMap() 
        {
            Id(g => g.Id);
            Map(g => g.Nome);
            HasMany(g => g.Contas);
        }
    }
}
