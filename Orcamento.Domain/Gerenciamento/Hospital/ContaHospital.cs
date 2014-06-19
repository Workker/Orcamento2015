using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace Orcamento.Domain
{
    [Serializable]
    public enum TipoValorContaEnum : short 
    {
        Porcentagem = 1,
        Quantidade = 2
    }
    [Serializable]
    public class ContaHospital : IAggregateRoot<int>
    {
        public virtual string Nome { get; set; }

        public virtual int Id
        {
            get;
            set;
        }

        public virtual TipoValorContaEnum TipoValorContaEnum { get; set; }

        public virtual bool ContabilizaProducao { get; set; }

        public virtual bool MultiPlicaPorMes { get; set; }

        public virtual bool Calculado {get;set;}

        public virtual IList<ContaHospital> ContasAnexadas { get; set; }

        public ContaHospital(string nome, TipoValorContaEnum tipo) 
        {
            this.Nome = nome;
            this.TipoValorContaEnum = tipo;
            this.ContabilizaProducao = true;
            this.MultiPlicaPorMes = false;
            this.Calculado = false;
            ContasAnexadas = new List<ContaHospital>();
        }

        public ContaHospital(string nome, TipoValorContaEnum tipo,bool calculado,bool contabilizaProducao)
        {
            this.Nome = nome;
            this.TipoValorContaEnum = tipo;
            this.ContabilizaProducao = ContabilizaProducao;
            this.MultiPlicaPorMes = false;
            this.Calculado = calculado;
            ContasAnexadas = new List<ContaHospital>();
        }

        protected ContaHospital() { }

        public virtual void AnexarConta(ContaHospital conta) 
        {
            Contract.Requires(conta != null,"Conta não informada.");
            this.ContasAnexadas.Add(conta);
        }
    }
}
