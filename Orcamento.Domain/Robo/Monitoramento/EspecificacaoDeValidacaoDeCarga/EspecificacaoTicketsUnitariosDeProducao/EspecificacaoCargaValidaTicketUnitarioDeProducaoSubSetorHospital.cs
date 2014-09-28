using Orcamento.Domain.Entities.Monitoramento;
using Orcamento.Domain.Robo.Monitoramento.EstrategiasDeCargas;
using System.Collections.Generic;
using System.Linq;

namespace Orcamento.Domain.Robo.Monitoramento.EspecificacaoDeValidacaoDeCarga.EspecificacaoTicketsUnitariosDeProducao
{
    public class EspecificacaoCargaTicketUnitarioDeProducaoSubSetorHospital : EspecificacaoCargaValidaTicketsUnitariosDeProducao
    {
        public EspecificacaoCargaTicketUnitarioDeProducaoSubSetorHospital(TicketDeProducaoExcel ticketDeProducaoExcel,
                                                                  List<SubSetorHospital> subSetoresHospitalares)
            : base(ticketDeProducaoExcel, subSetoresHospitalares)
        {
        }

        public override bool IsSatisfiedBy(Carga candidate)
        {

            bool satisfeito = SubSetoresHospitalares != null && SubSetoresHospitalares.Count > 0 &&
                              SubSetoresHospitalares.Any(c => c.NomeSetor == TicketDeProducaoExcel.subSetor);

            if (!satisfeito)
                candidate.AdicionarDetalhe("SubSetor não encontrado",
                                           "SubSetor: " + TicketDeProducaoExcel.subSetor + " inexistente.",
                                           TicketDeProducaoExcel.Linha, TipoDetalheEnum.erroDeValidacao);

            return satisfeito;
        }
    }
}