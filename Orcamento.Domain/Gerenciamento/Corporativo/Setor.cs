using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;
using Orcamento.Domain.Gerenciamento;
using Orcamento.InfraStructure;

namespace Orcamento.Domain
{
    [Serializable]
    public class Setor : Departamento
    {
        protected Setor() { }

        public override TipoDepartamento Tipo
        {
            get
            {
                return TipoDepartamento.setor;
            }
        }
        public Setor(string nome) 
        {
            Contract.Requires(!string.IsNullOrEmpty(nome), "nome não informado");

            this.Nome = nome;
            CentrosDeCusto = new List<CentroDeCusto>();
        }
    }
}
