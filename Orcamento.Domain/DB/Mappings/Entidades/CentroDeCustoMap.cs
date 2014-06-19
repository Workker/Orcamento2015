
using FluentNHibernate.Mapping;
using Orcamento.Domain.Gerenciamento;

namespace Orcamento.Domain.DB.Mappings.Entidades
{
    public sealed class CentroDeCustoMap : ClassMap<CentroDeCusto>
    {
        public CentroDeCustoMap() 
        {
            Id(c => c.Id);
            Map(c => c.Nome);
            Map(c => c.CodigoDoCentroDeCusto);
            HasManyToMany(c => c.Contas);
            HasManyToMany(c => c.GrupoDeContas);
            HasMany(c => c.Funcionarios).Cascade.All();
        }
    }
}
