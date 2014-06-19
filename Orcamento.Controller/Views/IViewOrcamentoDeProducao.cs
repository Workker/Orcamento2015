using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Orcamento.Domain.DTO;
using Orcamento.Domain;
using Orcamento.Domain.Gerenciamento;

namespace Orcamento.Controller
{
    public interface IViewOrcamentoDeProducao
    {
        Departamento Departamento { get; set; }
        Orcamento.Domain.Orcamento Orcamento { get; set; }
        List<ContaHospitalarDTO> Contas { get; set; }

        void PreencherOrcamentos();
        void PreencherReceitaBruta();
        void RemoverValidacaoDeDezVersoesDoBotaoIncluirNovaVersao();
        void CarregarValidacaoDeDezVersoesDoBotaoIncluirNovaVersao();
        void InformarNaoExisteVersaoFinal();
    }
}
