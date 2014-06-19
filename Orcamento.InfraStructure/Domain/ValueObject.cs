using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace Orcamento.Domain
{
    public abstract class ValueObject : IAggregateRoot<int>
    {
        private int id;

        public virtual int Id
        {
            get { return id; }
        }

        public virtual string Descricao
        {
            get;
            set;
        }

        protected ValueObject() { }

        protected ValueObject(int id, string descricao)
        {
            Contract.Requires(!string.IsNullOrEmpty(descricao), "Descrição não Informada.");
            Contract.Requires(id > 0, "Id não informado.");
            this.id = id;
            this.Descricao = descricao;
        }
    }
}
