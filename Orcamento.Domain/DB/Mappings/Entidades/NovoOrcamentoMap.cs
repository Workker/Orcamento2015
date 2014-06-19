using FluentNHibernate.Mapping;
using Orcamento.Domain.ComponentesDeOrcamento.OrcamentoPessoal;

namespace Orcamento.Domain.DB.Mappings.Entidades
{
    public sealed class NovoOrcamentoMap : ClassMap<NovoOrcamentoPessoal>
    {
        public NovoOrcamentoMap()
        {
            Id(x => x.Id);
            Map(x => x.Ano);
            Map(x => x.Justificativa);
            References(x => x.CentroDeCusto);
            References(x => x.Departamento);
        //    HasMany(x => x.Despesas).Cascade.All();
            HasManyToMany(x => x.Tickets).Cascade.All();
        }
    }
}
