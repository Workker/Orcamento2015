using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Orcamento.Robo.Web.Models.Notificacoes
{
    [Serializable]
    public abstract class Notificacao
    {
        public virtual string Mensagem { get; set; }
        public virtual string Titulo { get; set; }
        public virtual string Tipo { get; set; }
    }
}