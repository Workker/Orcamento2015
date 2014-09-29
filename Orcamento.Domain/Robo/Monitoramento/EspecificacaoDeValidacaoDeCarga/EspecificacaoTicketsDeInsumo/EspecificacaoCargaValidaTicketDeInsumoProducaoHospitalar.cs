using System.Collections.Generic;
using System.Linq;
using Orcamento.Domain.Entities.Monitoramento;
using Orcamento.Domain.Robo.Monitoramento.EstrategiasDeCargas;

namespace Orcamento.Domain.Robo.Monitoramento.EspecificacaoDeValidacaoDeCarga
{
    public class EspecificacaoCargaValidaTicketDeInsumoProducaoHospitalar :
        EspecificacaoCargaValidaTicketsDeInsumoGeral
    {
        public EspecificacaoCargaValidaTicketDeInsumoProducaoHospitalar(TicketDeInsumoExcel ticketDeInsumoExcel,
                                                                       List<ProducaoHospitalar> producoesHospitalares)
            : base(ticketDeInsumoExcel, producoesHospitalares)
        {
        }

        public override bool IsSatisfiedBy(Carga candidate)
        {
            base.IsSatisfiedBy(candidate);

            bool satisfeito = (TicketDeInsumoExcel.mes >= 1 && TicketDeInsumoExcel.mes <= 12) &&
            ProducoesHospitalares != null && ProducoesHospitalares.Count > 0 &&
            ProducoesHospitalares.Any(t => TicketDeInsumoExcel.mes == (int)t.Mes);

            //if (ProducoesHospitalares.Count(t => TicketDeInsumoExcel.mes == (int)t.Mes) == 1)
            //    candidate.AdicionarDetalhe("Mais de uma parcela",
            //                               string.Format("Existe mais de uma Parcela: {0} do subsetor : {1}",
            //                                             TicketDeInsumoExcel.mes,
            //                                             TicketDeInsumoExcel.subSetor),
            //                               TicketDeInsumoExcel.Linha, TipoDetalheEnum.erroDeValidacao);

            if (!satisfeito)
                candidate.AdicionarDetalhe("Parcela não encontrada",
                                           string.Format("Não foi possível encontrar a Parcela: {0} do subsetor : {1}",
                                                         TicketDeInsumoExcel.mes,
                                                         TicketDeInsumoExcel.subSetor),
                                           TicketDeInsumoExcel.Linha, TipoDetalheEnum.erroDeValidacao);

            return satisfeito;
        }
    }
}