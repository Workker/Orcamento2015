using System;
using System.Collections;
namespace Orcamento.Domain.ComponentesDeOrcamento.OrcamentoPessoal.Despesas
{
    public class Parcela
    {
        public virtual double Valor { get; set; }
        public virtual int Mes { get; set; }
        public virtual Guid Guid{get;set;}


        public Parcela() 
        {
            this.Guid = Guid.NewGuid();
        }

        public override bool Equals(object obj)
        {
            var outraParcela = (Parcela)obj;
            return outraParcela.Guid == Guid;
        }

    }
}
