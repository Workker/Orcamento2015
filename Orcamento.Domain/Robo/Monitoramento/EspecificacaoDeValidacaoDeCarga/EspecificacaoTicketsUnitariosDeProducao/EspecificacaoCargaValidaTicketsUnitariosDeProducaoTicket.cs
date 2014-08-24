using Orcamento.Domain.Entities.Monitoramento;
using Orcamento.Domain.Robo.Monitoramento.EstrategiasDeCargas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Orcamento.Domain.Robo.Monitoramento.EspecificacaoDeValidacaoDeCarga.EspecificacaoTicketsUnitariosDeProducao
{
    public class EspecificacaoCargaValidaTicketsUnitariosDeProducaoTicket : EspecificacaoCargaValidaTicketsUnitariosDeProducao
    {
        public EspecificacaoCargaValidaTicketsUnitariosDeProducaoTicket(TicketDeProducaoExcel ticketDeProducaoExcel, List<TicketDeProducao> ticketsDeProducao)
            : base(ticketDeProducaoExcel, ticketsDeProducao)
        {

        }

        public override bool IsSatisfiedBy(Carga candidate)
        {
            base.IsSatisfiedBy(candidate);

            var satisfeito = TicketsDeProducao.Any(t => t.Setor.NomeSetor == TicketDeProducaoExcel.setor && t.SubSetor.NomeSetor == TicketDeProducaoExcel.subSetor);

            if (!satisfeito)
            {
                //Passar setores e subSertoresa

                candidate.AdicionarDetalhe("Parcela não encontrada",
                                           string.Format("Não foi possível encontrar a Parcela: {0} do subsetor : {1}",
                                           TicketDeProducaoExcel.mes,
                                           TicketDeProducaoExcel.subSetor),
                                           TicketDeProducaoExcel.Linha, TipoDetalheEnum.erroDeValidacao);
            }

            return satisfeito;
        }
    }
}
