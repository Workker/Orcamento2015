using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Orcamento.Domain.Gerenciamento;

namespace Orcamento.Domain
{
    [Serializable]
    public enum MesEnum : short
    {
        
        Janeiro = 1,
        Fevereiro,
        Marco,
        Abril,
        Maio,
        Junho,
        Julho,
        Agosto,
        Setembro,
        Outubro,
        Novembro,
        Dezembro
    }
    [Serializable]
    public class Despesa : IAggregateRoot<int>
    {
        public virtual MesEnum Mes { get; set; }
        //TOTO: decimal
        public virtual long Valor { get; set; }
        public virtual string MemoriaDeCalculo { get; set; }
        public virtual Conta Conta { get; set; }

        public Despesa(MesEnum mes, int valor, Conta conta)
        {
            this.Mes = mes;
            this.Valor = valor;
            this.Conta = conta;
        }

        protected Despesa() { }

        public Despesa(MesEnum mes, Conta conta)
        {
            this.Mes = mes;
            this.Conta = conta;
        }

        public virtual int Id
        {
            get;
            set;
        }
    }
}
