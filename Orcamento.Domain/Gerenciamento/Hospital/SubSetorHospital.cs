using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace Orcamento.Domain
{
    [Serializable]
    public class SubSetorHospital : IAggregateRoot<int>
    {
        public virtual int Id
        {
            get;
            set;
        }

        public virtual string NomeSetor { get; set; }

        public SubSetorHospital(string nome) 
        {
            this.NomeSetor = nome;
        }

        protected SubSetorHospital() { }
    }
}
