using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Orcamento.Domain.ComponentesDeOrcamento.OrcamentoPessoal.Despesas;
using System.Linq;
using System;

namespace Orcamento.Domain.Gerenciamento
{
    [Serializable]
    public enum TipoContaEnum
    {
        Outros = 1,
        Beneficios,
        FGTS,
        INSS,
        Ferias,
        Indenizacao,
        DecimoTerceiro,
        Salario,
        BolsasDeEstagio,
        Extras
    }

    [Serializable]
    public class TipoConta : IAggregateRoot<int>
    {
        public virtual int Id { get; set; }
        public virtual string Nome { get; set; }
        public virtual TipoContaEnum TipoContaEnum { get { return (TipoContaEnum)Id; } }
    }

    [Serializable]
    public class ContaTicket : IAggregateRoot<int>
    {
        public virtual int Id { get; set; }
        public virtual TipoTicketDePessoal Ticket { get; set; }
    }

    [Serializable]
    public class Conta : IAggregateRoot<int>
    {
        public virtual int Id { get; set; }

        public virtual string CodigoDaConta { get; set; }

        public virtual string Nome { get; set; }

        public virtual TipoConta TipoConta { get; set; }

        public virtual IList<ContaTicket> TiposTickets { get; set; }

        public Conta()
        {
        }

        public Conta(string nome, TipoConta tipoConta)
        {
            Nome = nome;
            TipoConta = tipoConta;
        }

        public Conta(string nome, TipoConta tipoConta, string codigo)
        {
            Nome = nome;
            TipoConta = tipoConta;
            CodigoDaConta = codigo;
        }

        public virtual void Adicionar(TipoTicketDePessoal ticket)
        {
            if (TiposTickets == null)
                TiposTickets = new List<ContaTicket>();

            if (TiposTickets.Any(t => t.Ticket == ticket))
                return;

            var conta = new ContaTicket() { Ticket = ticket };

            TiposTickets.Add(conta);
        }


        public override bool Equals(object obj)
        {
            var outraConta = (Conta)obj;

            return outraConta.TipoConta == TipoConta;
        }

        public override int GetHashCode()
        {
            return Id;
        }
    }
}
