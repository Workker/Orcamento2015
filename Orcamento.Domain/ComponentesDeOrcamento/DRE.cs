using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Orcamento.Domain
{
    [Serializable]
    public class DRE
    {
        public virtual double ValorTotal { get; set; }
        public virtual string Nome { get; set; }
    }
}
