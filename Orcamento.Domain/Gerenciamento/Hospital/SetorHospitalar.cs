using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace Orcamento.Domain
{
    [Serializable]
    public class SetorHospitalar : IAggregateRoot<int>
    {
        public virtual IList<SubSetorHospital> SubSetores { get; set; }

        public virtual IList<ContaHospital> Contas { get; set; }

        public virtual string NomeSetor { get; set; }

        public virtual int Id
        {
            get;
            set;
        }

        public virtual void AdicionarSetor(SubSetorHospital setor)
        {
            Contract.Requires(setor != null, "Setor não informado.");

            if (SubSetores == null)
                SubSetores = new List<SubSetorHospital>();

            SubSetores.Add(setor);
        }

        public virtual void AdicionarConta(ContaHospital conta)
        {
            Contract.Requires(conta != null, "Conta não informada.");

            if (Contas == null)
                Contas = new List<ContaHospital>();

            Contas.Add(conta);
        }


        protected SetorHospitalar() { }

        public SetorHospitalar(string nome) 
        {
            SubSetores = new List<SubSetorHospital>();
            Contas = new List<ContaHospital>();
            this.NomeSetor = nome;
        }


    }
}
