using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Orcamento.Domain.Gerenciamento;
using Orcamento.Domain.DB.Repositorio;

namespace Orcamento.Domain.Servico.OutrasDespesas
{
    public class ServicoAtualizarDespesasOperacionais
    {
        public void AtualizarDespesas(CentroDeCusto centro)
        {
            Orcamentos orcamentos = new Orcamentos();

            var orcamentosOperacionais = orcamentos.TodosOrcamentosOperacionaisPor(centro);

            foreach (var orcamento in orcamentosOperacionais)
            {
                orcamento.AtualizarDespesas();
            }

            orcamentos.Salvar(orcamentosOperacionais);
        }
    }
}
