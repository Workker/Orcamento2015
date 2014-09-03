using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System;
using System.Linq;

namespace Orcamento.Domain.Gerenciamento
{
    [Serializable]
    public class GrupoDeConta : IAggregateRoot<int>
    {
        public virtual int Id { get; set; }
        public virtual string Nome { get; set; }
        public virtual IList<Conta> Contas { get; set; }

        public GrupoDeConta()
        { }


        public GrupoDeConta(string nome)
        {
            Nome = nome;
        }

        public virtual void Adicionar(Conta conta)
        {
            Contract.Requires(conta != null, "conta não foi informada");

            if (Contas == null)
                Contas = new List<Conta>();

            if(this.Contas.Any(c=> c.Nome == conta.Nome  && c.TipoConta.TipoContaEnum == conta.TipoConta.TipoContaEnum))
                return;

            Contas.Add(conta);
        }

    }
}
