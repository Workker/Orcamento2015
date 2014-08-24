using Orcamento.Domain.Entities.Monitoramento;
using Orcamento.Domain.Robo.Monitoramento.EstrategiasDeCargas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Orcamento.Domain.Robo.Monitoramento.EspecificacaoDeValidacaoDeCarga.EspecificacaoTicketsUnitariosDeProducao
{
    public class EspecificacaoCargaValidaTicketDeProducaoSetorHospitalar : EspecificacaoCargaValidaTicketsUnitariosDeProducao
    {
        public EspecificacaoCargaValidaTicketDeProducaoSetorHospitalar(TicketDeProducaoExcel ticketDeProducaoExcel,
                                                                  List<SetorHospitalar> setoresHospitalares)
            : base(ticketDeProducaoExcel, setoresHospitalares)
        {
        }

        public override bool IsSatisfiedBy(Carga candidate)
        {

            bool satisfeito = SetoresHospitalares != null && SetoresHospitalares.Count > 0 &&
                              SetoresHospitalares.Any(s => s.NomeSetor == TicketDeProducaoExcel.setor);

            if (!satisfeito)
                candidate.AdicionarDetalhe("Setor não encontrado",
                                           "Setor: " + TicketDeProducaoExcel.setor + " inexistente.",
                                           TicketDeProducaoExcel.Linha, TipoDetalheEnum.erroDeValidacao);

            return satisfeito;
        }
    }
}
