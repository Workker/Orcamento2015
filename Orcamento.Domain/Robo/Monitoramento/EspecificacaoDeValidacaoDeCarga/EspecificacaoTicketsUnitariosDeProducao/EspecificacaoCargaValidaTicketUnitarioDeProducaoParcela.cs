using System.Collections.Generic;
using System.Linq;
using Orcamento.Domain.Entities.Monitoramento;
using Orcamento.Domain.Robo.Monitoramento.EstrategiasDeCargas;

namespace Orcamento.Domain.Robo.Monitoramento.EspecificacaoDeValidacaoDeCarga.EspecificacaoTicketsUnitariosDeProducao
{
    public class EspecificacaoCargaValidaTicketUnitarioDeProducaoParcela :
        EspecificacaoCargaValidaTicketsUnitariosDeProducao
    {
        public EspecificacaoCargaValidaTicketUnitarioDeProducaoParcela(TicketDeProducaoExcel ticketDeProducaoExcel,
                                                                       List<TicketParcela> ticketsParcela)
            : base(ticketDeProducaoExcel, ticketsParcela)
        {
        }

        public override bool IsSatisfiedBy(Carga candidate)
        {
            base.IsSatisfiedBy(candidate);

            bool satisfeito = (TicketDeProducaoExcel.mes >= 1 && TicketDeProducaoExcel.mes <= 12) &&
            TicketsParcela != null && TicketsParcela.Count > 0 &&
            TicketsParcela.Any(t => TicketDeProducaoExcel.mes == (int) t.Mes);

            //if (TicketsParcela.Count(t => TicketDeProducaoExcel.mes == (int)t.Mes) == 1)
            //    candidate.AdicionarDetalhe("Mais de uma parcela",
            //                               string.Format("Existe mais de uma Parcela: {0} do subsetor : {1}",
            //                                             TicketDeProducaoExcel.mes,
            //                                             TicketDeProducaoExcel.subSetor),
            //                               TicketDeProducaoExcel.Linha, TipoDetalheEnum.erroDeValidacao);

            if (!satisfeito)
                candidate.AdicionarDetalhe("Parcela não encontrada",
                                           string.Format("Não foi possível encontrar a Parcela: {0} do subsetor : {1}",
                                                         TicketDeProducaoExcel.mes,
                                                         TicketDeProducaoExcel.subSetor),
                                           TicketDeProducaoExcel.Linha, TipoDetalheEnum.erroDeValidacao);

            return satisfeito;
        }
    }
}