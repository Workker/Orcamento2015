using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Orcamento.Domain
{
    public class TipoTicket : ValueObject
    {
        public TipoTicket() { }

        public TipoTicket(string descricao)
        {
            base.Descricao = descricao;
        }
    }
}
