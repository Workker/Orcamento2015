using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Orcamento.Domain.Gerenciamento;

namespace Orcamento.Domain.ComponentesDeOrcamento.OrcamentoPessoal
{
    public class AcordoDeConvencao : IAggregateRoot<int>
    {
        public virtual int Id { get; set; }

        public virtual Departamento Departamento { get; set; }
        public virtual int MesAumento { get; set; }
        public virtual double Porcentagem { get; set; }

        public AcordoDeConvencao(Departamento departamento, int mesAumento, double porcentagem)
        {
            this.Departamento = departamento;
            this.MesAumento = mesAumento;
            this.Porcentagem = porcentagem;
        }

        protected AcordoDeConvencao()
        {

        }
    }

}
