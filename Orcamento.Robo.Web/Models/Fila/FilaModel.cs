using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Orcamento.Robo.Web.Models.Fila
{
    [Serializable]
    public class FilaModel
    {
        public virtual Guid IdFila { get; set; }
        public virtual string NomeArquivo { get; set; }
        public virtual string Status { get; set; }
    }
}