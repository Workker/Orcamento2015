using System;
using Orcamento.Domain.Entities.Monitoramento;
using Orcamento.Domain.Gerenciamento;
using Orcamento.Domain.Robo.Monitoramento.EstrategiasDeCargas;
using System.Collections.Generic;
using Orcamento.Domain.ComponentesDeOrcamento.OrcamentoDeProducao;

namespace Orcamento.Domain.Robo.Monitoramento.EspecificacaoDeValidacaoDeCarga
{
    public abstract class EspecificacaoCargaValidaTicketsDeInsumoGeral: EspecificacaoCarga
    {
        public virtual TicketDeInsumoExcel TicketDeInsumoExcel { get; set; }
        public virtual Departamento Departamento { get; set; }
        public virtual List<SubSetorHospital> SubSetoresHospitalares { get; set; }
        public virtual List<SetorHospitalar> SetoresHospitalares { get; set; }
        public virtual List<Insumo> Insumos { get; set; }
        
        public virtual List<CustoUnitario> CustosUnitarios { get; set; }
        public virtual List<ProducaoHospitalar> ProducoesHospitalares { get; set; }

        public EspecificacaoCargaValidaTicketsDeInsumoGeral(TicketDeInsumoExcel TicketDeInsumoExcel,
                                                            Departamento departamento)
        {
            this.TicketDeInsumoExcel = TicketDeInsumoExcel;
            Departamento = departamento;
        }

        public EspecificacaoCargaValidaTicketsDeInsumoGeral(TicketDeInsumoExcel TicketDeInsumoExcel,
                                                            List<SubSetorHospital> subSetoresHospitalares)
        {
            this.TicketDeInsumoExcel = TicketDeInsumoExcel;
            SubSetoresHospitalares = subSetoresHospitalares;
        }

        public EspecificacaoCargaValidaTicketsDeInsumoGeral(TicketDeInsumoExcel TicketDeInsumoExcel,
                                                            List<SetorHospitalar> setoresHospitalares)
        {
            this.TicketDeInsumoExcel = TicketDeInsumoExcel;
            SetoresHospitalares = setoresHospitalares;
        }

        public EspecificacaoCargaValidaTicketsDeInsumoGeral(TicketDeInsumoExcel TicketDeInsumoExcel, List<Insumo> insumos)
         {
             this.TicketDeInsumoExcel = TicketDeInsumoExcel;
             Insumos = insumos;
         }

        public EspecificacaoCargaValidaTicketsDeInsumoGeral(TicketDeInsumoExcel TicketDeInsumoExcel, List<CustoUnitario> custosUnitarios)
        {
            this.TicketDeInsumoExcel = TicketDeInsumoExcel;
            CustosUnitarios = custosUnitarios;
        }

        public EspecificacaoCargaValidaTicketsDeInsumoGeral(TicketDeInsumoExcel TicketDeInsumoExcel, List<ProducaoHospitalar> producoesHospitalares)
        {
            this.TicketDeInsumoExcel = TicketDeInsumoExcel;
            ProducoesHospitalares = producoesHospitalares;
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