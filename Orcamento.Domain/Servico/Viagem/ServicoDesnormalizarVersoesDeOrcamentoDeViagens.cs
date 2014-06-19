using System.Collections.Generic;
using System.Linq;
using Orcamento.Domain.DTO;

namespace Orcamento.Domain.Servico
{
    public class ServicoDesnormalizarVersoesDeOrcamentoDeViagens
    {
        #region Atributos

        private readonly IList<Orcamento> orcamentos;
        private readonly IList<VersaoDeDespesaDTO> versoes;

        #endregion

        #region Construtores

        public ServicoDesnormalizarVersoesDeOrcamentoDeViagens(IList<Orcamento> orcamentos)
        {
            this.orcamentos = orcamentos;
            versoes = new List<VersaoDeDespesaDTO>();
        }

        #endregion

        #region Métodos

        public IList<VersaoDeDespesaDTO> ObterOrcamentoDeVersoesDesnormalizados()
        {
            foreach (Orcamento orcamento in orcamentos)
            {
                var versao = DesnormalizarVersao(orcamento);

                versoes.Add(versao);
            }

            return versoes;
        }

        private VersaoDeDespesaDTO DesnormalizarVersao(Orcamento orcamento)
        {
            return new VersaoDeDespesaDTO
                       {
                           Id = orcamento.Id,
                           CentroDeCusto = orcamento.CentroDeCusto.Nome,
                           Versao = orcamento.VersaoFinal ? "Versão Final" : orcamento.NomeOrcamento,
                           ValorTotal = orcamento.Despesas.Sum(d => d.ValorTotal)
                       };
        }

        #endregion
    }
}
