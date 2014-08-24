using Orcamento.Domain.Robo.Monitoramento.EstrategiasDeCargas;

namespace Orcamento.Domain.Robo.Monitoramento.EspecificacaoDeValidacaoDeCarga.EspecificacaoEstruturaOrcamentaria
{
    public abstract class EspecificacaoCargaValidaEstruturaOrcamentaria : EspecificacaoCarga
    {
        internal EstruturaOrcamentariaExcel estruturaOrcamentariaExcel;

        public EspecificacaoCargaValidaEstruturaOrcamentaria(
            EstruturaOrcamentariaExcel estruturaOrcamentariaExcel)
        {
            estruturaOrcamentariaExcel = estruturaOrcamentariaExcel;
        }
    }
}