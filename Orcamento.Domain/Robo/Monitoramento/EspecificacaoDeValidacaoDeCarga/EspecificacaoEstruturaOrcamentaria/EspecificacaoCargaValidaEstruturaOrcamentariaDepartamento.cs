using Orcamento.Domain.Entities.Monitoramento;
using Orcamento.Domain.Gerenciamento;
using Orcamento.Domain.Robo.Monitoramento.EstrategiasDeCargas;
using System.Collections.Generic;
using System.Linq;
namespace Orcamento.Domain.Robo.Monitoramento.EspecificacaoDeValidacaoDeCarga.EspecificacaoEstruturaOrcamentaria
{
    public class EspecificacaoCargaValidaEstruturaOrcamentariaDepartamento : EspecificacaoCargaValidaEstruturaOrcamentaria
    {
        public EspecificacaoCargaValidaEstruturaOrcamentariaDepartamento(List<EstruturaOrcamentariaExcel> estruturasOrcamentariasExcel, EstruturaOrcamentariaExcel estruturaOrcamentariaExcel, Departamento departamento)
            : base(estruturasOrcamentariasExcel, estruturaOrcamentariaExcel, departamento)
        {
        }

        public override bool IsSatisfiedBy(Carga candidate)
        {
            base.IsSatisfiedBy(candidate);

            var departamentoNaoExisteEDeSejaIncluir = this.Departamento == null
                && EstruturaOrcamentariaExcel.TipoAlteracaoDepartamento == TipoAlteracao.Inclusao;

            var contaExisteEDesejaAlterar = this.Departamento != null
                && EstruturaOrcamentariaExcel.TipoAlteracaoDepartamento == TipoAlteracao.Alteracao;

            var satisfeito = departamentoNaoExisteEDeSejaIncluir || contaExisteEDesejaAlterar;

            var mensagemComplementarDeErro = EstruturaOrcamentariaExcel.TipoAlteracaoDepartamento == TipoAlteracao.Inclusao
                                                 ? "já existe no banco de dados"
                                                 : "não existe no banco de dados";

            var alteracaoCorreta = (EstruturasOrcamentariasExcel != null && EstruturaOrcamentariaExcel.TipoAlteracaoDepartamento== TipoAlteracao.Alteracao && 
             EstruturasOrcamentariasExcel.Any(
                 p =>
                 p.Departamento == EstruturaOrcamentariaExcel.Departamento &&
                 p.TipoAlteracaoDepartamento == TipoAlteracao.Inclusao));

            if (!satisfeito && !alteracaoCorreta)
                candidate.AdicionarDetalhe("Departamento não pode ser salvo", string.Format("Departamento '{0}' {1}", EstruturaOrcamentariaExcel.Departamento, mensagemComplementarDeErro), EstruturaOrcamentariaExcel.Linha, TipoDetalheEnum.erroDeValidacao);

            return satisfeito;
        }
    }
}