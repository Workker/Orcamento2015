using Orcamento.Domain.Entities.Monitoramento;
using Orcamento.Domain.Robo.Monitoramento.EstrategiasDeCargas;

namespace Orcamento.Domain.Robo.Monitoramento.EspecificacaoDeValidacaoDeCarga.EspecificacaoEstruturaOrcamentaria
{
    public class EspecificacaoCargaValidaEstruturaOrcamentariaGrupoConta : EspecificacaoCargaValidaEstruturaOrcamentaria
    {
        public EspecificacaoCargaValidaEstruturaOrcamentariaGrupoConta(
            EstruturaOrcamentariaExcel estruturaOrcamentariaExcel)
            : base(estruturaOrcamentariaExcel)
        {
        }

        public override bool IsSatisfiedBy(Carga candidate)
        {
            throw new System.NotImplementedException();
        }
    }
}