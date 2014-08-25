using Orcamento.Domain.Entities.Monitoramento;
using Orcamento.Domain.Gerenciamento;
using Orcamento.Domain.Robo.Monitoramento.EstrategiasDeCargas;
using System.Collections.Generic;
using System.Linq;

namespace Orcamento.Domain.Robo.Monitoramento.EspecificacaoDeValidacaoDeCarga.EspecificacaoEstruturaOrcamentaria
{
    public class EspecificacaoCargaValidaEstruturaOrcamentariaGrupoConta : EspecificacaoCargaValidaEstruturaOrcamentaria
    {
        public EspecificacaoCargaValidaEstruturaOrcamentariaGrupoConta(List<EstruturaOrcamentariaExcel> estruturasOrcamentariasExcel,
            EstruturaOrcamentariaExcel estruturaOrcamentariaExcel, GrupoDeConta grupoDeConta)
            : base(estruturasOrcamentariasExcel, estruturaOrcamentariaExcel, grupoDeConta)
        {
        }

        public override bool IsSatisfiedBy(Carga candidate)
        {
            var grupoDeContaNaoExisteEDeSejaIncluir = this.GrupoDeConta == null
                && EstruturaOrcamentariaExcel.TipoAlteracaoGrupoDeConta == TipoAlteracao.Inclusao;

            var grupoDeContaExisteEDesejaAlterar = this.GrupoDeConta != null
                && EstruturaOrcamentariaExcel.TipoAlteracaoGrupoDeConta == TipoAlteracao.Alteracao;

            var satisfeito = grupoDeContaNaoExisteEDeSejaIncluir || grupoDeContaExisteEDesejaAlterar;

            var mensagemComplementarDeErro = EstruturaOrcamentariaExcel.TipoAlteracaoGrupoDeConta == TipoAlteracao.Inclusao
                                                 ? "já existe no banco de dados"
                                                 : "não existe no banco de dados";

            var alteracaoCorreta = (EstruturasOrcamentariasExcel != null && EstruturaOrcamentariaExcel.TipoAlteracaoConta == TipoAlteracao.Alteracao &&
            EstruturasOrcamentariasExcel.Any(
                p =>
                p.NomeDoGrupoDeConta == EstruturaOrcamentariaExcel.NomeDoGrupoDeConta &&
                p.TipoAlteracaoGrupoDeConta == TipoAlteracao.Inclusao));

            if (!satisfeito && !alteracaoCorreta)
                candidate.AdicionarDetalhe("Grupo de Conta não pode ser salvo", string.Format("Grupo de Conta {0} {1}", EstruturaOrcamentariaExcel.NomeDoGrupoDeConta, mensagemComplementarDeErro), EstruturaOrcamentariaExcel.Linha, TipoDetalheEnum.erroDeValidacao);

            return satisfeito;
        }
    }
}