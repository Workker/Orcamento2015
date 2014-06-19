using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Orcamento.Domain.Gerenciamento
{
     [Serializable]
    public class GrupoResumo : IAggregateRoot<int>
    {
        private IList<Conta> contas;
        public virtual int Id { get; set; }
        public virtual string Nome{get;set;}
        public virtual IList<Conta> Contas
        {
            get { return contas ?? (contas = new List<Conta>()); }
            set { contas = value; }
        }

        public GrupoResumo(string nome) 
        {
            this.Nome = nome;
        }

        protected GrupoResumo()
        {
           
        }
        
    }
}
