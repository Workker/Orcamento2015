using Orcamento.Domain.Entities.Monitoramento;
using Orcamento.Domain.Robo.Monitoramento.EstrategiasDeCargas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Orcamento.Domain.Robo.Monitoramento.EspecificacaoDeValidacaoDeCarga
{
    public class EspecificacaoCargaValidaTicketDeInsumoSetorHospitalar : EspecificacaoCargaValidaTicketsDeInsumoGeral
    {
        public EspecificacaoCargaValidaTicketDeInsumoSetorHospitalar(TicketDeInsumoExcel ticketDeInsumoExcel,
                                                                  List<SetorHospitalar> setoresHospitalares)
            : base(ticketDeInsumoExcel, setoresHospitalares)
        {
        }

        public override bool IsSatisfiedBy(Carga candidate)
        {

            bool satisfeito = SetoresHospitalares != null && SetoresHospitalares.Count > 0 &&
                              SetoresHospitalares.Any(s => s.NomeSetor == TicketDeInsumoExcel.setor);

            if (!satisfeito)
                candidate.AdicionarDetalhe("Setor não encontrado",
                                           "Setor: " + TicketDeInsumoExcel.setor + " inexistente.",
                                           TicketDeInsumoExcel.Linha, TipoDetalheEnum.erroDeValidacao);

            return satisfeito;
        }
    }
}
