using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Orcamento.Domain
{
    [Serializable]
    public enum TipoValor : short
    {
        Porcentagem = 1,
        Quantidade = 2
    }
}
