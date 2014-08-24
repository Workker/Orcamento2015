using Orcamento.Domain.Entities.Monitoramento;
using Orcamento.Domain.Robo.Monitoramento.EstrategiasDeCargas;

namespace Orcamento.Domain.Robo.Monitoramento.EspecificacaoDeValidacaoDeCarga.EspecificacaoEstruturaOrcamentaria
{
    public class EspecificacaoCargaValidaEstruturaOrcamentariaDepartamento : EspecificacaoCargaValidaEstruturaOrcamentaria
    {
        public EspecificacaoCargaValidaEstruturaOrcamentariaDepartamento(EstruturaOrcamentariaExcel estruturaOrcamentariaExcel)
            : base(estruturaOrcamentariaExcel)
        {
        }

        public override bool IsSatisfiedBy(Carga candidate)
        {
            throw new System.NotImplementedException();
        }
    }
}