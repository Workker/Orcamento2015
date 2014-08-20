using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;


namespace Orcamento.Domain.Session
{
    public class FullSessionState
    {
        public ISessionFactory FACTORY;
        private object syncObj = 1;
        private ISession _session;
        private IConfigurationFactory configurationFactory;

        public IConfigurationFactory ConfigurationFactory
        {
            get 
            {
                if (configurationFactory == null)
                    configurationFactory = new ConfigurationFactory();
                return configurationFactory; 
            }
            set { configurationFactory = value; }
        }

        public ISession Session
        {
            get { return _session ?? (_session = GetCurrentSession()); }
            set { _session = value; }
        }

        public ISession GetCurrentSession()
        {
            ISession currentSession = null;

            lock (syncObj)
            {
                if (FACTORY == null) FACTORY = CreateSessionFactory();
                currentSession = FACTORY.OpenSession();
            }

            return currentSession;
        }

        public ISessionFactory CreateSessionFactory()
        {
            return ConfigurationFactory.Build().BuildSessionFactory();
        }

        public void CreateDataBase()
        {
            ConfigurationFactory.Build().ExposeConfiguration(BuildSchema).BuildSessionFactory();
        }

        private void BuildSchema(Configuration config)
        {
            new SchemaExport(config)
                .Drop(true, true);

            new SchemaExport(config)
                .Create(true, true);
        }
    }
}
