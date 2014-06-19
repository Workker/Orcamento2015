using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;
using Orcamento.Domain.Gerenciamento;

namespace Orcamento.Domain
{
    [Serializable]
    public class Hospital : Departamento
    {
        public override IList<SetorHospitalar> Setores { get; set; }

        public override TipoDepartamento Tipo
        {
            get
            {
                return TipoDepartamento.hospital;
            }
        }

        public Hospital()
        {
            Setores = new List<SetorHospitalar>();
            CentrosDeCusto = new List<CentroDeCusto>();
        }

        public Hospital(string nome)
        {
            Setores = new List<SetorHospitalar>();
            CentrosDeCusto = new List<CentroDeCusto>();
            this.Nome = nome;
        }
    }
}
