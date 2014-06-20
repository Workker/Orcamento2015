using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Orcamento.Robo.Web.Models.Exportar
{
    [Serializable]
    public class ExportarModel
    {
        public virtual Guid IdExportacao { get; set; }
        public virtual string NomeExportacao { get; set; }
    }
}