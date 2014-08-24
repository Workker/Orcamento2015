using Orcamento.Domain.Robo.Monitoramento.EstrategiasDeCargas;
using Orcamento.Domain.Util.Specification;

namespace Orcamento.Domain.Robo.Monitoramento.EspecificacaoDeValidacaoDeCarga.EspecificacaoEstruturaOrcamentaria
{
    public class FabricaDeEspecificacaoEstruturaOrcamentaria
    {
        public static Especificacao ObterEspeficicacao(EstruturaOrcamentariaExcel estruturaOrcamentaria)
        {
            var validaDepartamento =
                new EspecificacaoCargaValidaEstruturaOrcamentariaDepartamento(estruturaOrcamentaria);

            var validaCentroDeCusto =
                new EspecificacaoCargaValidaEstruturaOrcamentariaCentroDeCusto(estruturaOrcamentaria);

            var validaConta =
                new EspecificacaoCargaValidaEstruturaOrcamentariaConta(estruturaOrcamentaria);

            var validaGrupoConta =
                new EspecificacaoCargaValidaEstruturaOrcamentariaGrupoConta(estruturaOrcamentaria);

            return
                validaDepartamento.And(validaCentroDeCusto).And(validaConta).And(validaGrupoConta);
        }
    }
}