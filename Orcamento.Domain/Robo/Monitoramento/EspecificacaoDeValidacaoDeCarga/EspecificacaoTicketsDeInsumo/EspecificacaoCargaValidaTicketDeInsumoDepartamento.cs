using Orcamento.Domain.Entities.Monitoramento;
using Orcamento.Domain.Gerenciamento;
using Orcamento.Domain.Robo.Monitoramento.EstrategiasDeCargas;

namespace Orcamento.Domain.Robo.Monitoramento.EspecificacaoDeValidacaoDeCarga
{
    public class EspecificacaoCargaValidaTicketDeInsumoDepartamento : EspecificacaoCargaValidaTicketsDeInsumoGeral
    {
        public EspecificacaoCargaValidaTicketDeInsumoDepartamento(TicketDeInsumoExcel ticketDeInsumoExcel,
                                                                    Departamento departamento)
            : base(ticketDeInsumoExcel, departamento)
        {
        }

        public override bool IsSatisfiedBy(Carga candidate)
        {
            base.IsSatisfiedBy(candidate);

            var satisfeito = Departamento != null;

            if (!satisfeito)
                candidate.AdicionarDetalhe("Hospital não encontrado",
                                       "Hospital: " + TicketDeInsumoExcel.Departamento + " inexistente.",
                                       TicketDeInsumoExcel.Linha, TipoDetalheEnum.erroDeValidacao);

            return satisfeito;
        }
    }
}