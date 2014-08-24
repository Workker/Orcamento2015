using Orcamento.Domain.Entities.Monitoramento;
using Orcamento.Domain.Gerenciamento;
using Orcamento.Domain.Robo.Monitoramento.EstrategiasDeCargas;

namespace Orcamento.Domain.Robo.Monitoramento.EspecificacaoDeValidacaoDeCarga.EspecificacaoEstruturaOrcamentaria
{
    public class EspecificacaoCargaValidaEstruturaOrcamentariaDepartamento : EspecificacaoCargaValidaEstruturaOrcamentaria
    {
        public EspecificacaoCargaValidaEstruturaOrcamentariaDepartamento(EstruturaOrcamentariaExcel estruturaOrcamentariaExcel, Departamento departamento)
            : base(estruturaOrcamentariaExcel, departamento)
        {
        }

        public override bool IsSatisfiedBy(Carga candidate)
        {
            base.IsSatisfiedBy(candidate);

            var satisfeito = Departamento != null;

            if (!satisfeito)
                candidate.AdicionarDetalhe("Departamento não encontrado",
                                       "Departamento: " + EstruturaOrcamentariaExcel.Departamento + " inexistente.",
                                       EstruturaOrcamentariaExcel.Linha, TipoDetalheEnum.erroDeValidacao);

            return satisfeito;
        }
    }
}