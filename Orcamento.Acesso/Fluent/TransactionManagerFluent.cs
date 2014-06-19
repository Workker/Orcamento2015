using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Orcamento.Domain.DB.Mappings;

namespace Orcamento.Acesso
{
    public class TransactionManagerFluent : ITransactionManager
    {

        public static ISessionFactory FACTORY = CreateSessionFactory();
        private static ISession _session;
        private static ITransaction transaction;

        public void Initialize()
        {
            _session = FACTORY.OpenSession();
            transaction = _session.BeginTransaction();
        }

        public void VoteRollBack()
        {
            transaction.Rollback();
        }

        public void VoteCommit()
        {
            transaction.Commit();
        }

        public void Dispose()
        {
            transaction.Dispose();
        }

        public static ISessionFactory CreateSessionFactory()
        {
            return Fluently.Configure().Database(MsSqlConfiguration.MsSql2005.ConnectionString(c => c
                  .Server(@".\SQLEXPRESS")
                  .Database("ExemploPuro")
                  .TrustedConnection()
                //.Username("sa")
                //.Password("123456")))
                  )).Mappings(m => m.FluentMappings.AddFromAssemblyOf<DespesaMap>())
                  .BuildSessionFactory();

        }
    }
}
