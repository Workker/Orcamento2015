using System;
using System.Collections.Generic;
using NHibernate;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Orcamento.Domain.DB.Mappings;
using System.Configuration;
using NHibernate.Cfg;
using System.Linq;
using System.Web;
namespace Orcamento.Domain.DB.Repositorio
{
    public abstract class BaseRepository
    {
        public const string NHibernateSessionKey = "nhibernate.session.key";

        public static ISessionFactory FACTORY = CreateSessionFactory();
        private static ISession _session;

        public static ISession Session
        {
            get
            {
                return NHibernateSessionPerRequest.GetCurrentSession();
            }

            //get { return _session ?? (_session = GetCurrentSession()); }
            //set { _session = value; }
        }

        private static object syncObj = 1;

        #region Métodos Genericos para acesso ao DB

        public BaseRepository()
        {
        }

        public virtual void Salvar(IAggregateRoot<int> root)
        {
            var transaction = Session.BeginTransaction();

            try
            {
                Session.SaveOrUpdate(root);
                transaction.Commit();
            }
            catch (System.Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }
        }



        public virtual void Salvar(IAggregateRoot<Guid> root)
        {
            var transaction = Session.BeginTransaction();

            try
            {
                Session.SaveOrUpdate(root);
                transaction.Commit();
            }
            catch (System.Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }
        }

        public virtual void SalvarLista<T>(List<T> roots) where T : IAggregateRoot<int>
        {
            var transaction = Session.BeginTransaction();

            try
            {
                foreach (var root in roots)
                {
                    Session.SaveOrUpdate(root);
                }
                transaction.Commit();
            }
            catch (System.Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }
        }

        public virtual void SalvarAndFlush(IAggregateRoot<int> root)
        {
            var transaction = Session.BeginTransaction();

            try
            {
                Session.SaveOrUpdate(root);
                Session.Flush();
                transaction.Commit();
            }
            catch (System.Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }
        }

        public virtual void SalvarLista(List<IAggregateRoot<int>> roots)
        {
            var transaction = Session.BeginTransaction();

            try
            {
                foreach (var root in roots)
                {
                    Session.SaveOrUpdate(root);
                }
                transaction.Commit();
            }
            catch (System.Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }
        }

        public virtual void Deletar(IAggregateRoot<int> root)
        {
            var transaction = Session.BeginTransaction();
            try
            {
                // Session.Flush();
                Session.Delete(root);
                transaction.Commit();
            }
            catch (System.Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }


        }

        public virtual void Deletar(IList<TicketDeProducao> roots)
        {
            var transaction = Session.BeginTransaction();
            try
            {
                // Session.Flush();
                foreach (var root in roots)
                {
                    Session.Delete(root);
                }

                transaction.Commit();
            }
            catch (System.Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }


        }

        public virtual IList<T> Todos<T>()
        {
            var objs = Session.CreateCriteria(typeof(T)).List<T>();
            return objs;
        }

        public virtual T Obter<T>(int id)
        {
            var obj = Session.Get<T>(id);
            return obj;
        }

        #endregion

        #region Métodos de Sessão e Transação

        public static void CloseTransaction(ITransaction transaction)
        {
            transaction.Dispose();
        }

        public static ISession GetCurrentSession()
        {
            ISession currentSession = null;

            lock (syncObj)
                currentSession = FACTORY.OpenSession();

            return currentSession;
        }

        public static ISessionFactory CreateSessionFactory()
        {
            var ambiente = ConfigurationManager.AppSettings["Ambiente"];

            if (ambiente != null && ambiente == "Teste")
                return null;
            else
                return
                Fluently.Configure().Database(MsSqlConfiguration.MsSql2005.ConnectionString(c => c
                    .FromAppSetting("Conexao")
                    ).ShowSql()).Mappings(m => m.FluentMappings.AddFromAssemblyOf<SetorMap>()).BuildSessionFactory();
        }

        #endregion
    }
}
