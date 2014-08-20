using FluentNHibernate.Mapping;
using Orcamento.Domain.Robo.Monitoramento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Orcamento.Domain.DB.Mappings.Entidades.Robo
{
    public class DetalheMap : ClassMap<Detalhe>
    {
        public DetalheMap()
        {
            Id(x => x.Id).GeneratedBy.Guid();
            Map(x => x.Nome);
            Map(x => x.Descricao);
            Map(x => x.Linha);
            Map(x => x.TipoDetalhe);
            Map(x => x.Excecao);
        }
    }
}
