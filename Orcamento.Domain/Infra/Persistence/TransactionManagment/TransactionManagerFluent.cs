using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;

namespace Orcamento.Domain.Session
{
    /// <summary>
    /// Gerenciador de transação
    /// </summary>
    public class TransactionManagerFluent : ITransactionManager
    {
        /// <summary>
        /// Session 
        /// </summary>
        private static ISession _session;

        /// <summary>
        /// Transação
        /// </summary>
        private static ITransaction transaction;

        /// <summary>
        /// Session Builder
        /// </summary>
        private ISessionBuilder sessionBuilder;

        /// <summary>
        /// Session Builder
        /// </summary>
        public ISessionBuilder SessionBuilder
        {
            get
            {
                if (sessionBuilder == null) // TODO:Retirar acoplamento
                    sessionBuilder = new SessionBuilder();

                return sessionBuilder;
            }
            set { sessionBuilder = value; }
        }

        /// <summary>
        /// Construtor
        /// </summary>
        public void Initialize()
        {
            _session = SessionBuilder.GetSession();
            transaction = _session.BeginTransaction();
        }

        /// <summary>
        /// RollBack
        /// </summary>
        public void VoteRollBack()
        {
            transaction.Rollback();
        }

        /// <summary>
        /// Commit
        /// </summary>
        public void VoteCommit()
        {
            transaction.Commit();
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            transaction.Dispose();
        }
    }
}
