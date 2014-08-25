using Orcamento.Domain.Entities.Monitoramento;
using Orcamento.Domain.Gerenciamento;
using Orcamento.Domain.Robo.Monitoramento.EstrategiasDeCargas;
using System.Collections.Generic;
using System.Linq;

namespace Orcamento.Domain.Robo.Monitoramento.EspecificacaoDeValidacaoDeCarga.EspecificacaoEstruturaOrcamentaria
{
    public class EspecificacaoCargaValidaEstruturaOrcamentariaCentroDeCusto :
        EspecificacaoCargaValidaEstruturaOrcamentaria
    {
        public EspecificacaoCargaValidaEstruturaOrcamentariaCentroDeCusto(List<EstruturaOrcamentariaExcel> estruturasOrcamentariasExcel,
            EstruturaOrcamentariaExcel estruturaOrcamentariaExcel, CentroDeCusto centroDeCusto)
            : base(estruturasOrcamentariasExcel, estruturaOrcamentariaExcel, centroDeCusto)
        {
        }

        public override bool IsSatisfiedBy(Carga candidate)
        {
            base.IsSatisfiedBy(candidate);

            var centroDeCustoNaoExisteEDeSejaIncluir = this.CentroDeCusto == null
                && EstruturaOrcamentariaExcel.TipoAlteracaoCentroDeCusto == TipoAlteracao.Inclusao;

            var centroDeCustoExisteEDesejaAlterar = this.CentroDeCusto != null
                && EstruturaOrcamentariaExcel.TipoAlteracaoCentroDeCusto == TipoAlteracao.Alteracao;

            var satisfeito = centroDeCustoNaoExisteEDeSejaIncluir || centroDeCustoExisteEDesejaAlterar;

            var mensagemComplementarDeErro = EstruturaOrcamentariaExcel.TipoAlteracaoCentroDeCusto == TipoAlteracao.Inclusao
                                                 ? "já existe no banco de dados"
                                                 : "não existe no banco de dados";

            var alteracaoCorreta = (EstruturasOrcamentariasExcel != null && EstruturaOrcamentariaExcel.TipoAlteracaoConta == TipoAlteracao.Alteracao &&
           EstruturasOrcamentariasExcel.Any(
               p =>
               p.CodigoCentroDeCusto == EstruturaOrcamentariaExcel.CodigoCentroDeCusto &&
               p.TipoAlteracaoCentroDeCusto == TipoAlteracao.Inclusao));

            if (!satisfeito && !alteracaoCorreta)
                candidate.AdicionarDetalhe("Centro de Custo não pode ser salvo", string.Format("Centro de Custo '{0}' {1}", EstruturaOrcamentariaExcel.NomeCentroDeCusto, mensagemComplementarDeErro), EstruturaOrcamentariaExcel.Linha, TipoDetalheEnum.erroDeValidacao);

            return satisfeito;
        }
    }
}