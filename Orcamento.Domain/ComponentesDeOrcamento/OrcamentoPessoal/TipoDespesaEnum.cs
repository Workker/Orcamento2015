using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Orcamento.Domain
{
    public enum TipoDespesaEnum : short
    {
        FGTS = 1,
        Salarios = 2,
        AdicionalPericulosidade = 3,
        AdicionalInsalubridade = 4,
        AdicionalNoturno = 5,
        DecimoTerceiro = 6,
        Ferias = 7
    }
}
