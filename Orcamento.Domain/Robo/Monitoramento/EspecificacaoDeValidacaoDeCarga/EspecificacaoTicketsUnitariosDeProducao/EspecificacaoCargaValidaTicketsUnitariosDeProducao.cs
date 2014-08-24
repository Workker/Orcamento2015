using System;
using Orcamento.Domain.Entities.Monitoramento;
using Orcamento.Domain.Gerenciamento;
using Orcamento.Domain.Robo.Monitoramento.EstrategiasDeCargas;
using System.Collections.Generic;

namespace Orcamento.Domain.Robo.Monitoramento.EspecificacaoDeValidacaoDeCarga.EspecificacaoTicketsUnitariosDeProducao
{
    public abstract class EspecificacaoCargaValidaTicketsUnitariosDeProducao : EspecificacaoCarga
    {
        public virtual TicketDeProducaoExcel TicketDeProducaoExcel { get; set; }
        public virtual Departamento Departamento { get; set; }
        public virtual List<SubSetorHospital> SubSetoresHospitalares { get; set; }
        public virtual List<SetorHospitalar> SetoresHospitalares { get; set; }
        public virtual List<TicketDeProducao> TicketsDeProducao { get; set; }
        
        public virtual List<TicketParcela> TicketsParcela { get; set; }

        public EspecificacaoCargaValidaTicketsUnitariosDeProducao(TicketDeProducaoExcel ticketDeProducaoExcel,
                                                            Departamento departamento)
        {
            TicketDeProducaoExcel = ticketDeProducaoExcel;
            Departamento = departamento;
        }

        public EspecificacaoCargaValidaTicketsUnitariosDeProducao(TicketDeProducaoExcel ticketDeProducaoExcel,
                                                            List<SubSetorHospital> subSetoresHospitalares)
        {
            TicketDeProducaoExcel = ticketDeProducaoExcel;
            SubSetoresHospitalares = subSetoresHospitalares;
        }

        public EspecificacaoCargaValidaTicketsUnitariosDeProducao(TicketDeProducaoExcel ticketDeProducaoExcel,
                                                            List<SetorHospitalar> setoresHospitalares)
        {
            TicketDeProducaoExcel = ticketDeProducaoExcel;
            SetoresHospitalares = setoresHospitalares;
        }

        public EspecificacaoCargaValidaTicketsUnitariosDeProducao(TicketDeProducaoExcel ticketDeProducaoExcel, List<TicketDeProducao> ticketsDeProducao)
         {
             TicketDeProducaoExcel = ticketDeProducaoExcel;
             TicketsDeProducao = ticketsDeProducao;
         }

        public EspecificacaoCargaValidaTicketsUnitariosDeProducao(TicketDeProducaoExcel ticketDeProducaoExcel, List<TicketParcela> ticketsparcela)
        {
            TicketDeProducaoExcel = ticketDeProducaoExcel;
            TicketsParcela = ticketsparcela;
        }

        public override bool IsSatisfiedBy(Carga candidate)
        {
            // TODO: até resolver o problema da referência para workker.domain
            if(candidate==null)
                throw new Exception("Ocorreu um erro ao ler o arquivo. Por favor, recarregue o excel.");

            //var cargaNaoNula = Assertion.NotNull(candidate, "Ocorreu um erro ao ler o arquivo. Por favor, recarregue o excel.");
            //cargaNaoNula.Validate();
            //return cargaNaoNula.IsValid();

            return true;
        }
    }
}