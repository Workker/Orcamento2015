using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Orcamento.Domain.Gerenciamento
{
    [Serializable]
    public class Usuario : IAggregateRoot<int>
    {
        public virtual int Id { get; set; }
        public virtual string Nome { get; set; }
        public virtual string Login { get; set; }
        public virtual string Senha { get; set; }
        public virtual TipoUsuario TipoUsuario { get; set; }

        public virtual IList<Departamento> Departamentos { get; set; }

        public virtual void ParticiparDe(Departamento departamento)
        {
            if (this.Departamentos == null)
                this.Departamentos = new List<Departamento>();

            this.Departamentos.Add(departamento);
        }
    }

    [Serializable]
    public enum TipoUsuarioEnum : int
    {
        Administrador = 1,
        Hospital = 2,
        Corporativo = 3
    }
    [Serializable]
    public class TipoUsuario : IAggregateRoot<int>
    {
        public virtual int Id { get; set; }
        public virtual string Nome { get; set; }
    }
}
