using FluentNHibernate.Mapping;

namespace Orcamento.Domain.DB.Mappings.Entidades
{
    public class TicketDeOrcamentoPessoalMap:ClassMap<TicketDeOrcamentoPessoal>
    {
        public TicketDeOrcamentoPessoalMap()
        {
            Id(x => x.Id);
            Map(x => x.Descricao);
            Map(x => x.Valor);
            Map(x => x.Ticket);
            References(x => x.Departamento);
        }
    }
}
