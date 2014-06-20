using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Orcamento.Robo.Web.Models.Importar
{
    [Serializable]
    public class ImportarModel
    {
        public virtual Guid IdImportacao { get; set; }
        public virtual string NomeImportacao { get; set; }
    }
}