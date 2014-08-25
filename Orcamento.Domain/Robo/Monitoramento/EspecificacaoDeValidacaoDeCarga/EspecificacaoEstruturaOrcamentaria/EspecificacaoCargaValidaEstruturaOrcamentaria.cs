using Orcamento.Domain.Entities.Monitoramento;
using Orcamento.Domain.Gerenciamento;
using Orcamento.Domain.Robo.Monitoramento.EstrategiasDeCargas;
using System;
using System.Collections.Generic;

namespace Orcamento.Domain.Robo.Monitoramento.EspecificacaoDeValidacaoDeCarga.EspecificacaoEstruturaOrcamentaria
{
    public abstract class EspecificacaoCargaValidaEstruturaOrcamentaria : EspecificacaoCarga
    {
        public List<EstruturaOrcamentariaExcel> EstruturasOrcamentariasExcel { get; set; }
        public EstruturaOrcamentariaExcel EstruturaOrcamentariaExcel { get; set; }
        public Departamento Departamento { get; set; }
        public Conta Conta { get; set; }
        public GrupoDeConta GrupoDeConta { get; set; }
        public CentroDeCusto CentroDeCusto { get; set; }

        public EspecificacaoCargaValidaEstruturaOrcamentaria(List<EstruturaOrcamentariaExcel> estruturasOrcamentariasExcel,
           EstruturaOrcamentariaExcel estruturaOrcamentariaExcel)
        {
            EstruturasOrcamentariasExcel = estruturasOrcamentariasExcel;
            EstruturaOrcamentariaExcel = estruturaOrcamentariaExcel;
        }

        public EspecificacaoCargaValidaEstruturaOrcamentaria(List<EstruturaOrcamentariaExcel> estruturasOrcamentariasExcel,
            EstruturaOrcamentariaExcel estruturaOrcamentariaExcel, Departamento departamento)
        {
            EstruturasOrcamentariasExcel = estruturasOrcamentariasExcel;
            EstruturaOrcamentariaExcel = estruturaOrcamentariaExcel;
            Departamento = departamento;
        }

        public EspecificacaoCargaValidaEstruturaOrcamentaria(List<EstruturaOrcamentariaExcel> estruturasOrcamentariasExcel,
           EstruturaOrcamentariaExcel estruturaOrcamentariaExcel, Conta conta)
        {
            EstruturasOrcamentariasExcel = estruturasOrcamentariasExcel;
            EstruturaOrcamentariaExcel = estruturaOrcamentariaExcel;
            Conta = conta;
        }

        public EspecificacaoCargaValidaEstruturaOrcamentaria(List<EstruturaOrcamentariaExcel> estruturasOrcamentariasExcel,
           EstruturaOrcamentariaExcel estruturaOrcamentariaExcel, GrupoDeConta grupoDeConta)
        {
            EstruturasOrcamentariasExcel = estruturasOrcamentariasExcel;
            EstruturaOrcamentariaExcel = estruturaOrcamentariaExcel;
            GrupoDeConta = grupoDeConta;
        }

        public EspecificacaoCargaValidaEstruturaOrcamentaria(List<EstruturaOrcamentariaExcel> estruturasOrcamentariasExcel,EstruturaOrcamentariaExcel estruturaOrcamentariaExcel, CentroDeCusto centroDeCusto)
        {
            EstruturasOrcamentariasExcel = estruturasOrcamentariasExcel;
            EstruturaOrcamentariaExcel = estruturaOrcamentariaExcel;
            CentroDeCusto = centroDeCusto;
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