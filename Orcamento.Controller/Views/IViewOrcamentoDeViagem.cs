
using System.Collections.Generic;
using Orcamento.Domain;
using Orcamento.Domain.DTO;
using Orcamento.Domain.Gerenciamento;

namespace Orcamento.Controller
{
    public interface IViewOrcamentoDeViagem
    {
        Departamento Departamento { get; set; }
        int CentroDeCustoId { get; set; }
        Domain.Orcamento OrcamentoViagem { get; set; }
        List<ContaDTO> Contas { get; set; }

        void CarregarOrcamento();
        void CarregarVersoesDeOrcamentos(IList<VersaoDeDespesaDTO> versoes);

        void RemoverValidacaoDeDezVersoesDoBotaoIncluirNovaVersao();

        void CarregarValidacaoDeDezVersoesDoBotaoIncluirNovaVersao();

        void ZerarViagens();


        void InformarNaoExisteVersaoFinal();
    }
}
