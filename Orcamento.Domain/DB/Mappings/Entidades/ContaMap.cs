using FluentNHibernate.Mapping;
using Orcamento.Domain.Gerenciamento;

namespace Orcamento.Domain.DB.Mappings.Entidades
{
    public sealed class ContaMap : ClassMap<Conta>
    {
        public ContaMap()
        {
            Id(c => c.Id);
            Map(c => c.Nome);
            Map(c => c.CodigoDaConta);
            References(c => c.TipoConta);
            HasManyToMany(x => x.TiposTickets).Cascade.All();
            
        }
    }
}
