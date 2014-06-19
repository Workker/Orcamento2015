using System.Collections.Generic;
using Orcamento.Domain;
using Orcamento.Domain.DTO;
using Orcamento.Domain.Gerenciamento;

namespace Orcamento.Controller.Views
{
    public interface IViewDespesaOperacional
    {
        Departamento SetorSelecionado { get; set; }
        int CentroDeCustoId { get; set; }
        Domain.Orcamento OrcamentoOperacional { get; set; }
        IList<ContaDTO> Contas { get; set; }

        List<ContaDTO> ObterDespesasCorrentes();

        void CarregarValidacaoDeDezVersoesDoBotaoIncluirNovaVersao();
        void RemoverValidacaoDeDezVersoesDoBotaoIncluirNovaVersao();
        void PreencherVersoes();
        void InformarVersao();
        void EsconderOBotaoApagar();
        void PreencherRepeaterDespesas(IList<ContaDTO> contas);

        void InformarNaoExisteVersaoFinal();
    }
}
