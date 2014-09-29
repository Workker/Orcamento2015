using Orcamento.Domain.Entities.Monitoramento;
using Orcamento.Domain.Robo.Monitoramento.EstrategiasDeCargas;
using System.Collections.Generic;
using System.Linq;

namespace Orcamento.Domain.Robo.Monitoramento.EspecificacaoDeValidacaoDeCarga
{
    public class EspecificacaoCargaValidaTicketUnitarioDeInsumoSubSetorHospital : EspecificacaoCargaValidaTicketsDeInsumoGeral
    {
        public EspecificacaoCargaValidaTicketUnitarioDeInsumoSubSetorHospital(TicketDeInsumoExcel ticketDeInsumoExcel,
                                                                  List<SubSetorHospital> subSetoresHospitalares)
            : base(ticketDeInsumoExcel, subSetoresHospitalares)
        {
        }

        public override bool IsSatisfiedBy(Carga candidate)
        {

            bool satisfeito = SubSetoresHospitalares != null && SubSetoresHospitalares.Count > 0 &&
                              SubSetoresHospitalares.Any(c => c.NomeSetor == TicketDeInsumoExcel.subSetor);

            if (!satisfeito)
                candidate.AdicionarDetalhe("SubSetor não encontrado",
                                           "SubSetor: " + TicketDeInsumoExcel.subSetor + " inexistente.",
                                           TicketDeInsumoExcel.Linha, TipoDetalheEnum.erroDeValidacao);

            return satisfeito;
        }
    }
}