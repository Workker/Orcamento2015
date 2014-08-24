using Orcamento.Domain.Entities.Monitoramento;
using Orcamento.Domain.Gerenciamento;
using Orcamento.Domain.Robo.Monitoramento.EstrategiasDeCargas;

namespace Orcamento.Domain.Robo.Monitoramento.EspecificacaoDeValidacaoDeCarga.EspecificacaoTicketsUnitariosDeProducao
{
    public class EspecificacaoCargaValidaTicketDeProducaoDepartamento : EspecificacaoCargaValidaTicketsUnitariosDeProducao
    {
        public EspecificacaoCargaValidaTicketDeProducaoDepartamento(TicketDeProducaoExcel ticketDeProducaoExcel,
                                                                    Departamento departamento)
            : base(ticketDeProducaoExcel, departamento)
        {
        }

        public override bool IsSatisfiedBy(Carga candidate)
        {
            base.IsSatisfiedBy(candidate);

            var satisfeito = Departamento != null;

            if (!satisfeito)
                candidate.AdicionarDetalhe("Hospital não encontrado",
                                       "Hospital: " + TicketDeProducaoExcel.Departamento + " inexistente.",
                                       TicketDeProducaoExcel.Linha, TipoDetalheEnum.erroDeValidacao);

            return satisfeito;
        }
    }
}