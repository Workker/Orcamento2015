using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Orcamento.Domain.Session
{
    /// <summary>
    /// Gerenciador de Transações
    /// </summary>
    public interface ITransactionManager : IDisposable
    {
        /// <summary>
        /// RollBack
        /// </summary>
        void VoteRollBack();

        /// <summary>
        /// Commit
        /// </summary>
        void VoteCommit();

        /// <summary>
        /// Inicializar a transação
        /// </summary>
        void Initialize();
    }
}
