using System;
using System.Collections.Generic;
using System.Linq;
using Orcamento.Domain.DB.Repositorio;
using Orcamento.Domain.DTO;
using System.Diagnostics.Contracts;
using Orcamento.Domain.Gerenciamento;

namespace Orcamento.Domain.Servico
{
    public class ServicoOrcamentoOperacionalVersao
    {
        private IOrcamentos _orcamentos;

        public IOrcamentos Orcamentos
        {
            get { return _orcamentos ?? (_orcamentos = new Orcamentos()); }
            set { _orcamentos = value; }
        }

        //TODO: colocar a regra de negocio no Negocio
        public void AtribuirVersaoFinal(Orcamento orcamento)
        {
            Contract.Requires(orcamento != null, "Orçamento não informado.0");
            Contract.Requires(orcamento.DespesasOperacionais != null, "Despesas operacionais não informadas.");
            Contract.Requires(orcamento.ValorTotal > default(int), "É necessário que o total seje maior do que zero para atribuir esta versão como final.");


            var orcamentoFinal = Orcamentos.ObterOrcamentoFinalOrcamentoOperacional(orcamento.CentroDeCusto,orcamento.Setor);

            if (orcamentoFinal != null)
            {
                orcamentoFinal.VersaoFinal = false;
                Orcamentos.Salvar(orcamentoFinal);
            }

            orcamento.AtribuirVersaoFinal();
            Orcamentos.Salvar(orcamento);
        }

        public OrcamentoOperacionalVersao CriarOrcamentoOperacional(List<Orcamento> orcamentosGerenciamento, Departamento setor, CentroDeCusto centroDeCusto, int ano)
        {
            Contract.Requires(setor != null, "Departamento não informado.");
            Contract.Requires(centroDeCusto != null, "Centro de custo não informado.");

            var orcamento = new OrcamentoOperacionalVersao(setor, centroDeCusto, ano);
            orcamento.CriarDespesas();

            if (orcamentosGerenciamento == null)
                orcamentosGerenciamento = new List<Orcamento>();

            var gerenciador = new GerenciadorDeOrcamentos();

            if (!gerenciador.PodeCriarOrcamento(orcamentosGerenciamento, setor, centroDeCusto, TipoOrcamentoEnum.DespesaOperacional))
                throw new Exception("Orçamento já tem dez versões");

            orcamentosGerenciamento.Add(orcamento);
            gerenciador.InformarNomeOrcamento(orcamentosGerenciamento, orcamento, setor, centroDeCusto, TipoOrcamentoEnum.DespesaOperacional);

            foreach (var orcamentoGerenciamento in orcamentosGerenciamento)
            {
                Orcamentos.Salvar(orcamentoGerenciamento);
            }

            return orcamento;
        }

        public void DeletarOrcamento(Orcamento orcamento, List<Orcamento> orcamentos, Departamento departamento)
        {
            Contract.Requires(orcamento != null, "Orçamento não informado");
            Contract.Requires(departamento != null, "Departamento não informado");

            Orcamentos.Deletar(orcamento);

            if (orcamentos.Exists(c => c == orcamento))
                orcamentos.Remove(orcamento);

            var gerenciamento = new GerenciadorDeOrcamentos();
            gerenciamento.InformarNomeOrcamento(orcamentos, orcamento, departamento, TipoOrcamentoEnum.DespesaOperacional);

            foreach (var item in orcamentos)
            {
                Orcamentos.Salvar(item);
            }
        }

        public void SalvarOrcamento(Orcamento orcamento, List<ContaDTO> contas)
        {
            Contract.Requires(orcamento != null, "Orcamento não informado.");
            Contract.Requires(orcamento.DespesasOperacionais != null, "Despesas operacionais não informadas.");
            Contract.Requires(contas != null, "Despesas do orçamento não informadas.");
            Contract.Requires(contas.All(c => c.Despesas != null), "Despesas do orçamento não informadas.");

            Orcamentos.Refresh(orcamento);
            if (contas != null && contas.Count > 0)
            {
                foreach (var despesa in orcamento.DespesasOperacionais)
                {
                   
                    despesa.Valor = contas
                        .Where(c => c.ContaId == despesa.Conta.Id)
                        .Select(c =>
                                    {
                                        var firstOrDefault = c.Despesas.FirstOrDefault(d => d.DespesaId == despesa.Id);
                                        return firstOrDefault != null ? firstOrDefault.Valor : 0;
                                    })
                        .FirstOrDefault();
                }
            }

            if(orcamento.ValorTotal == 0) throw new Exception("O valor total não pode ser zero");
            Orcamentos.Salvar(orcamento);
        }
    }
}
