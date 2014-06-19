using System.Collections.Generic;
using System.Linq;
using System.Diagnostics.Contracts;
using Orcamento.Domain.ComponentesDeOrcamento.OrcamentoPessoal;
using System;

namespace Orcamento.Domain.Gerenciamento
{
    [Serializable]
    public class CentroDeCusto : IAggregateRoot<int>
    {
        public virtual int Id { get; set; }

        public virtual string Nome { get; private set; }

        public virtual string CodigoDoCentroDeCusto { get; set; }

        public virtual IList<GrupoDeConta> GrupoDeContas { get; set; }

        public virtual IList<Conta> Contas { get; set; }

        public virtual IList<Funcionario> Funcionarios { get; set; }

        protected CentroDeCusto()
        {

        }

        public virtual void AlterarNome(string nome)
        {
            this.Nome = nome;
        }

        public CentroDeCusto(string nome)
        {
            Contract.Requires(!string.IsNullOrEmpty(nome), "Nome deve ser preenchido.");
            Nome = nome;
            Contas = new List<Conta>();
            GrupoDeContas = new List<GrupoDeConta>();
            Funcionarios = new List<Funcionario>();
        }

        public virtual void AdicionarConta(Conta conta)
        {
            Contract.Requires(conta != null, "Conta não informada.");

            Contas.Add(conta);
        }

        public virtual void Adicionar(GrupoDeConta grupoDeConta)
        {
            Contract.Requires(grupoDeConta != null, "grupo de conta não foi informado");

            GrupoDeContas.Add(grupoDeConta);
        }

        public virtual void Adicionar(Funcionario funcionario)
        {
            Contract.Requires(funcionario != null, "funcionario não foi informado");

            Funcionarios.Add(funcionario);
        }

        public virtual Conta ObterContaPor(string codigoDaConta)
        {
            return Contas.Where(c => c.CodigoDaConta == codigoDaConta).SingleOrDefault();
        }
    }
}
