//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Web;
//using NHibernate;
//using NHibernate.Engine;
//using NHibernate.Cfg;
//using FluentNHibernate.Cfg;
//using Orcamento.Domain.DB.Mappings;
//using FluentNHibernate.Cfg.Db;
//using Castle.MicroKernel.Registration;
//using Castle.MicroKernel.SubSystems.Configuration;
//using Castle.Windsor;
//using Castle.Facilities.TypedFactory;
//using NHibernate.Context;

//namespace Orcamento.Domain.DB.Repositorio
//{
//    public class NHibernateSessionModule : IHttpModule
//    {
//        private HttpApplication app;
//        private ISessionFactoryProvider sfp;

//        public void Init(HttpApplication context)
//        {
//            app = context;
//            sfp = (ISessionFactoryProvider)
//                      context.Application[SessionFactoryProvider.Key];
//            context.BeginRequest += ContextBeginRequest;
//            context.EndRequest += ContextEndRequest;
//        }

//        private void ContextBeginRequest(object sender, EventArgs e)
//        {
//            foreach (var sf in sfp.GetSessionFactories())
//            {
//                var localFactory = sf;
//                LazySessionContext.Bind(
//                    new Lazy<ISession>(() => BeginSession(localFactory)),
//                    sf);
//            }
//        }

//        private static ISession BeginSession(ISessionFactory sf)
//        {
//            var session = sf.OpenSession();
//            session.BeginTransaction();
//            return session;
//        }

//        private void ContextEndRequest(object sender, EventArgs e)
//        {
//            foreach (var sf in sfp.GetSessionFactories())
//            {
//                var session = LazySessionContext.UnBind(sf);
//                if (session == null) continue;
//                EndSession(session);
//            }
//        }

//        private static void EndSession(ISession session)
//        {
//            if (session.Transaction != null && session.Transaction.IsActive)
//            {
//                session.Transaction.Commit();
//            }
//            session.Dispose();
//        }

//        public void Dispose()
//        {
//            app.BeginRequest -= ContextBeginRequest;
//            app.EndRequest -= ContextEndRequest;
//        }
//    }

//    public class LazySessionContext : ICurrentSessionContext
//    {
//        private readonly ISessionFactoryImplementor factory;
//        private const string CurrentSessionContextKey = "NHibernateCurrentSession";

//        public LazySessionContext(ISessionFactoryImplementor factory)
//        {
//            this.factory = factory;
//        }

//        /// <summary>
//        /// Retrieve the current session for the session factory.
//        /// </summary>
//        /// <returns></returns>
//        public ISession CurrentSession()
//        {
//            Lazy<ISession> initializer;
//            var currentSessionFactoryMap = GetCurrentFactoryMap();
//            if (currentSessionFactoryMap == null ||
//                !currentSessionFactoryMap.TryGetValue(factory, out initializer))
//            {
//                return null;
//            }
//            return initializer.Value;
//        }

//        /// <summary>
//        /// Bind a new sessionInitializer to the context of the sessionFactory.
//        /// </summary>
//        /// <param name="sessionInitializer"></param>
//        /// <param name="sessionFactory"></param>
//        public static void Bind(Lazy<ISession> sessionInitializer, ISessionFactory sessionFactory)
//        {
//            var map = GetCurrentFactoryMap();
//            map[sessionFactory] = sessionInitializer;
//        }

//        /// <summary>
//        /// Unbind the current session of the session factory.
//        /// </summary>
//        /// <param name="sessionFactory"></param>
//        /// <returns></returns>
//        public static ISession UnBind(ISessionFactory sessionFactory)
//        {
//            var map = GetCurrentFactoryMap();
//            var sessionInitializer = map[sessionFactory];
//            map[sessionFactory] = null;
//            if (sessionInitializer == null || !sessionInitializer.IsValueCreated) return null;
//            return sessionInitializer.Value;
//        }

//        /// <summary>
//        /// Provides the CurrentMap of SessionFactories.
//        /// If there is no map create/store and return a new one.
//        /// </summary>
//        /// <returns></returns>
//        public static IDictionary<ISessionFactory, Lazy<ISession>> GetCurrentFactoryMap()
//        {
//            var currentFactoryMap = (IDictionary<ISessionFactory, Lazy<ISession>>)
//                                    HttpContext.Current.Items[CurrentSessionContextKey];
//            if (currentFactoryMap == null)
//            {
//                currentFactoryMap = new Dictionary<ISessionFactory, Lazy<ISession>>();
//                HttpContext.Current.Items[CurrentSessionContextKey] = currentFactoryMap;
//            }
//            return currentFactoryMap;
//        }
//    }

//    public interface ISessionFactoryProvider
//    {
//        IEnumerable<ISessionFactory> GetSessionFactories();
//    }

//    public class SessionFactoryProvider
//    {
//        public const string Key = "NHibernateSessionFactoryProvider";
//    }

//    public class NHibernateInstaller : IWindsorInstaller
//    {
//        #region IWindsorInstaller Members

//        public void Install(IWindsorContainer container, IConfigurationStore store)
//        {
//            container.Register(Component.For<ISessionFactory>()
//                                   .UsingFactoryMethod(k => BuildSessionFactory()));

//            container.Register(Component.For<NHibernateSessionModule>());

//            container.Register(Component.For<ISessionFactoryProvider>().AsFactory());

//            container.Register(Component.For<IEnumerable<ISessionFactory>>()
//                                        .UsingFactoryMethod(k => k.ResolveAll<ISessionFactory>()));

//            HttpContext.Current.Application[SessionFactoryProvider.Key]
//                            = container.Resolve<ISessionFactoryProvider>();
//        }

//        #endregion

//        public ISessionFactory BuildSessionFactory()
//        {
//            //configuration.Properties[Environment.CurrentSessionContextClass]
//            //= typeof (LazySessionContext).AssemblyQualifiedName; 
//            return
//            Fluently.Configure()
//            //.ExposeConfiguration(c => c.SetProperty("current_session_context_class", "thread_static"))
//            .ExposeConfiguration(c => c.SetProperty("current_session_context_class", "Orcamento.Domain.DB.Repositorio.LazySessionContext"))
//            .Database(MsSqlConfiguration.MsSql2005.ConnectionString(c => c
//                .FromAppSetting("Conexao")
//                )).Mappings(m => m.FluentMappings.AddFromAssemblyOf<SetorMap>()).BuildSessionFactory();
//        }
//    }
//}
