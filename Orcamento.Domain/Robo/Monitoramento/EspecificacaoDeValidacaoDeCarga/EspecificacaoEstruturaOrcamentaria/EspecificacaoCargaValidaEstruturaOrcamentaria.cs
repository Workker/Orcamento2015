using Orcamento.Domain.Entities.Monitoramento;
using Orcamento.Domain.Gerenciamento;
using Orcamento.Domain.Robo.Monitoramento.EstrategiasDeCargas;
using System;

namespace Orcamento.Domain.Robo.Monitoramento.EspecificacaoDeValidacaoDeCarga.EspecificacaoEstruturaOrcamentaria
{
    public abstract class EspecificacaoCargaValidaEstruturaOrcamentaria : EspecificacaoCarga
    {
        public EstruturaOrcamentariaExcel EstruturaOrcamentariaExcel { get; set; }
        public Departamento Departamento { get; set; }
        public EstruturaOrcamentariaExcel estruturaOrcamentariaExcel { get; set; }

        public EspecificacaoCargaValidaEstruturaOrcamentaria(
           EstruturaOrcamentariaExcel estruturaOrcamentariaExcel)
        {
            this.EstruturaOrcamentariaExcel = estruturaOrcamentariaExcel;
        }

        public EspecificacaoCargaValidaEstruturaOrcamentaria(
            EstruturaOrcamentariaExcel estruturaOrcamentariaExcel, Departamento departamento)
        {
            EstruturaOrcamentariaExcel = estruturaOrcamentariaExcel;
            Departamento = departamento;
        }

        public override bool IsSatisfiedBy(Carga candidate)
        {
            // TODO: até resolver o problema da referência para workker.domain
            if (candidate == null)
                throw new Exception("Ocorreu um erro ao ler o arquivo. Por favor, recarregue o excel.");

            //var cargaNaoNula = Assertion.NotNull(candidate, "Ocorreu um erro ao ler o arquivo. Por favor, recarregue o excel.");
            //cargaNaoNula.Validate();
            //return cargaNaoNula.IsValid();

            return true;
        }
    }
}