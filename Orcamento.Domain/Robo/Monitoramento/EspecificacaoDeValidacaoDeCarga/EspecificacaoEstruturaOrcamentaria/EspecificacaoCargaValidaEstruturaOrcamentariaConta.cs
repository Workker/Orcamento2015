using Orcamento.Domain.Entities.Monitoramento;
using Orcamento.Domain.Gerenciamento;
using Orcamento.Domain.Robo.Monitoramento.EstrategiasDeCargas;
using System.Collections.Generic;
using System.Linq;

namespace Orcamento.Domain.Robo.Monitoramento.EspecificacaoDeValidacaoDeCarga.EspecificacaoEstruturaOrcamentaria
{
    public class EspecificacaoCargaValidaEstruturaOrcamentariaConta : EspecificacaoCargaValidaEstruturaOrcamentaria
    {
        public EspecificacaoCargaValidaEstruturaOrcamentariaConta(List<EstruturaOrcamentariaExcel> estruturasOrcamentariasExcel, EstruturaOrcamentariaExcel estruturaOrcamentariaExcel, Conta conta)
            : base(estruturasOrcamentariasExcel, estruturaOrcamentariaExcel, conta)
        {
        }

        public override bool IsSatisfiedBy(Carga candidate)
        {
            base.IsSatisfiedBy(candidate);

            var contaNaoExisteEDeSejaIncluir = this.Conta == null
                && EstruturaOrcamentariaExcel.TipoAlteracaoConta == TipoAlteracao.Inclusao;

            var contaExisteEDesejaAlterar = this.Conta != null
                && EstruturaOrcamentariaExcel.TipoAlteracaoConta == TipoAlteracao.Alteracao;

            var satisfeito = contaNaoExisteEDeSejaIncluir || contaExisteEDesejaAlterar;

            var mensagemComplementarDeErro = EstruturaOrcamentariaExcel.TipoAlteracaoConta == TipoAlteracao.Inclusao
                                                 ? "já existe no banco de dados"
                                                 : "não existe no banco de dados";

            var alteracaoCorreta = (EstruturasOrcamentariasExcel != null && EstruturaOrcamentariaExcel.TipoAlteracaoConta == TipoAlteracao.Alteracao && 
            EstruturasOrcamentariasExcel.Any(
                p =>
                p.CodigoDaConta == EstruturaOrcamentariaExcel.CodigoDaConta && 
                p.TipoAlteracaoConta == TipoAlteracao.Inclusao));

            if (!satisfeito && !alteracaoCorreta)
                candidate.AdicionarDetalhe("Conta não pode ser salvo", string.Format("Conta '{0}' {1}", EstruturaOrcamentariaExcel.NomeDaConta, mensagemComplementarDeErro), EstruturaOrcamentariaExcel.Linha, TipoDetalheEnum.erroDeValidacao);

            return satisfeito;
        }
    }
}