using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Context;
using System.Web;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Orcamento.Domain.DB.Mappings;

namespace Orcamento.Domain.DB.Repositorio
{
    public class NHibernateSessionPerRequest : IHttpModule
    {
        private static readonly ISessionFactory _sessionFactory;
        static NHibernateSessionPerRequest()
        {
            _sessionFactory = CreateSessionFactory();
        }
        public void Init(HttpApplication context)
        {
            context.BeginRequest += BeginRequest;
            context.EndRequest += EndRequest;
        }
        public static ISession GetCurrentSession()
        {
            return _sessionFactory.GetCurrentSession();
        }
        public void Dispose() { }
        private static void BeginRequest(object sender, EventArgs e)
        {
            ISession session = _sessionFactory.OpenSession();
            session.BeginTransaction();
            CurrentSessionContext.Bind(session);
        }
        private static void EndRequest(object sender, EventArgs e)
        {
            ISession session = CurrentSessionContext.Unbind(_sessionFactory);
            if (session == null) return;
            try
            {
                if (session.IsConnected && session.IsOpen && session.Transaction.IsActive && session.IsDirty())
                    session.Transaction.Commit();
            }
            catch (Exception)
            {
                session.Transaction.Rollback();
            }
            finally
            {
                session.Close();
                session.Dispose();
                session = null;
            }
        }
        public static ISessionFactory CreateSessionFactory()
        {
            return
            Fluently.Configure()
            .ExposeConfiguration(c => c.SetProperty("current_session_context_class", "thread_static"))
            .Database(MsSqlConfiguration.MsSql2005.ConnectionString(c => c
                .FromAppSetting("Conexao")
                )).Mappings(m => m.FluentMappings.AddFromAssemblyOf<SetorMap>()).BuildSessionFactory();
        }
    }
}
