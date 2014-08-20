using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Orcamento.Domain.Session
{
    /// <summary>
    /// Enumeração de tipo de transação
    /// </summary>
    public enum TransactionModeEnum
    {
        /// <summary>
        /// Transação Herdada ou aninhada
        /// </summary>
        Inherits = 0,

        /// <summary>
        /// Nova transação
        /// </summary>
        New = 1,
    }
}
