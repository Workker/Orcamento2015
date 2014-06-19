using FluentNHibernate.Mapping;
using Orcamento.Domain.Gerenciamento;

namespace Orcamento.Domain.DB.Mappings.Entidades
{
    public sealed class GrupoDeContaMap : ClassMap<GrupoDeConta>
    {
        public GrupoDeContaMap()
        {
            Id(a => a.Id);
            Map(a => a.Nome);
            HasManyToMany(a => a.Contas).Cascade.AllDeleteOrphan();
        }
    }
}
