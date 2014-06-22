using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Orcamento.Robo.Web.Models.Status
{
    [Serializable]
    public class StatusModel
    {
        public virtual Guid Id { get; set; }
        public virtual string NomeArquivo { get; set; }
        public virtual DateTime DataProcesso { get; set; }
        public virtual string Status { get; set; }
    }

    [Serializable]
    public class StatusListModel
    {
        public virtual IList<StatusModel> StatusList { get; set; }
    }
}