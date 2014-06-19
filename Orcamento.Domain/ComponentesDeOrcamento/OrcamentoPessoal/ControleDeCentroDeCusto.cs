using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Orcamento.Domain.Gerenciamento;

namespace Orcamento.Domain.ComponentesDeOrcamento.OrcamentoPessoal
{
    public class ControleDeCentroDeCusto : IAggregateRoot<int>
    {
        public virtual int Id
        {
            get;
            set;
        }
        public virtual Departamento Departamento { get; set; }
        public virtual CentroDeCusto CentroDeCusto { get; set; }
        public virtual bool Salvo { get; set; }

        public ControleDeCentroDeCusto(Departamento departamento,CentroDeCusto centro, bool salvo) 
        {
            this.CentroDeCusto = centro;
            this.Departamento = departamento;
            this.Salvo = salvo;
        }

        protected ControleDeCentroDeCusto() { }
    }
}
