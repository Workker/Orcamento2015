using FluentNHibernate.Mapping;
using Orcamento.Domain.Gerenciamento;

namespace Orcamento.Domain.DB.Mappings.Entidades
{
    public sealed class TipoContaMap : ClassMap<TipoConta>
    {
        public TipoContaMap()
        {
            Id(x => x.Id);
            Map(x => x.Nome);
        }
    }
}
