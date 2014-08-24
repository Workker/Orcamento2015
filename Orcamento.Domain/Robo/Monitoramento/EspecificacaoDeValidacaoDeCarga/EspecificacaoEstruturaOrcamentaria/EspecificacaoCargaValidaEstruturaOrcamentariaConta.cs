using Orcamento.Domain.Entities.Monitoramento;
using Orcamento.Domain.Robo.Monitoramento.EstrategiasDeCargas;

namespace Orcamento.Domain.Robo.Monitoramento.EspecificacaoDeValidacaoDeCarga.EspecificacaoEstruturaOrcamentaria
{
    public class EspecificacaoCargaValidaEstruturaOrcamentariaConta : EspecificacaoCargaValidaEstruturaOrcamentaria
    {
        public EspecificacaoCargaValidaEstruturaOrcamentariaConta(EstruturaOrcamentariaExcel estruturaOrcamentariaExcel)
            : base(estruturaOrcamentariaExcel)
        {
        }

        public override bool IsSatisfiedBy(Carga candidate)
        {
            throw new System.NotImplementedException();
        }
    }
}