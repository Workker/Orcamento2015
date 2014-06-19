using System;
using System.Collections.Generic;
using System.Linq;
using Orcamento.Domain.ComponentesDeOrcamento.OrcamentoDeViagem;
using Orcamento.Domain.DB.Repositorio;
using Orcamento.Domain.DTO;
using System.Diagnostics.Contracts;
using Orcamento.Domain.Gerenciamento;

namespace Orcamento.Domain.Servico
{
    public class ServicoOrcamentoDeViagens
    {
        #region Atributos

        private IOrcamentos orcamentos;
        private IDiarias diarias;
        private IViagens viagens;

        #endregion

        #region Propriedades

        public IOrcamentos Orcamentos
        {
            get { return this.orcamentos ?? (this.orcamentos = new Orcamentos()); }
            set { this.orcamentos = value; }
        }

        public IDiarias Diarias
        {
            get { return this.diarias ?? (this.diarias = new Diarias()); }
            set { this.diarias = value; }
        }

        public IViagens Viagens
        {
            get { return this.viagens ?? (this.viagens = new Viagens()); }
            set { this.viagens = value; }
        }

        #endregion

        #region Métodos

        public virtual OrcamentoDeViagem CriarOrcamentoDeViagem(List<Orcamento> orcamentosGerenciamento, Departamento departamento, CentroDeCusto centroDeCusto, int ano)
        {
            Contract.Requires(centroDeCusto != null, "Centro de custo não informado.");
            Contract.Requires(departamento != null, "Departamento não informado.");


            var viagens = Viagens.Todos().ToList();
            var diarias = Diarias.Todos().ToList();

            OrcamentoDeViagem orcamento = new OrcamentoDeViagem(departamento, centroDeCusto, ano);
            orcamento.CriarDespesas(viagens, diarias);

            if (orcamentosGerenciamento == null)
                orcamentosGerenciamento = new List<Orcamento>();

            GerenciadorDeOrcamentos gerenciador = new GerenciadorDeOrcamentos();

            if (!gerenciador.PodeCriarOrcamento(orcamentosGerenciamento, departamento, centroDeCusto, TipoOrcamentoEnum.Viagem))
                throw new Exception("Orçamento já tem dez versões");

            orcamentosGerenciamento.Add(orcamento);

            gerenciador.InformarNomeOrcamento(orcamentosGerenciamento, orcamento, departamento, centroDeCusto, TipoOrcamentoEnum.Viagem);


            foreach (var orcamentoGerenciamento in orcamentosGerenciamento)
            {
                Orcamentos.Salvar(orcamentoGerenciamento);
            }

            return orcamento;
        }

        public virtual void AtribuirVersaoFinal(Orcamento orcamento)
        {
            Contract.Requires(orcamento != null, "Orçamento não informado.0");
            Contract.Requires(orcamento.Despesas != null, "Despesas de viagem não informadas.");
            Contract.Requires(orcamento.ValorTotal > default(int), "É necessário que o total seje maior do que zero para atribuir esta versão como final.");

            var orcamentoFinal = Orcamentos.ObterOrcamentoFinalOrcamentoDeViagem(orcamento.CentroDeCusto);

            if (orcamentoFinal != null)
            {
                orcamentoFinal.VersaoFinal = false;
                Orcamentos.Salvar(orcamentoFinal);
            }

            orcamento.AtribuirVersaoFinal();
            Orcamentos.Salvar(orcamento);
        }

        public virtual void SalvarOrcamento(Orcamento orcamento, List<ContaDTO> contas)
        {
            Contract.Requires(orcamento != null, "Orcamento não informado.");
            Contract.Requires(orcamento.Despesas != null, "Despesas de viagens não informadas.");
            Contract.Requires(contas != null, "Despesas de viagens não informadas.");
            Contract.Requires(contas.All(c => c.Despesas != null), "Despesas de viagens não informadas.");

            foreach (var despesa in orcamento.Despesas)
            {
                if (contas.Any(d => d.Despesas.Any(e => e.DespesaId == despesa.Id)))
                {
                    var despesaDTO = contas.Where(c => c.Despesas.Any(i => i.DespesaId == despesa.Id))
                        .Select(u => u.Despesas.Where(d => d.DespesaId == despesa.Id).FirstOrDefault()).FirstOrDefault();

                    despesa.Quantidade = despesaDTO.Valor;
                }
            }

            if (orcamento.ValorTotal == default(int))
                throw new Exception("O valor total não pode ser zero.");

            Orcamentos.Salvar(orcamento);
        }

        public virtual void DeletarOrcamento(Orcamento orcamento, List<Orcamento> orcamentos, Departamento departamento)
        {
            Contract.Requires(orcamento != null, "Orçamento não informado");
            Contract.Requires(departamento != null, "Departamento não informado");

            Orcamentos.Deletar(orcamento);

            if (orcamentos.Exists(c => c == orcamento))
                orcamentos.Remove(orcamento);

            GerenciadorDeOrcamentos gerenciamento = new GerenciadorDeOrcamentos();
            gerenciamento.InformarNomeOrcamento(orcamentos, orcamento, departamento, TipoOrcamentoEnum.Viagem);

            foreach (var item in orcamentos)
            {
                Orcamentos.Salvar(item);
            }
        }

        #endregion
    }
}
