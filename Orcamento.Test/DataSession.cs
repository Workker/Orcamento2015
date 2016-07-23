using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using Orcamento.Domain;
using Orcamento.Domain.DB.Mappings;

namespace Orcamento.TestMethod
{
    public class DataSession
    {
        private readonly IPersistenceConfigurer _dbType;

        public DataSession(IPersistenceConfigurer dbType)
        {
            _dbType = dbType;

            CreateSessionFactory();
        }

        private ISessionFactory _sessionFactory;

        private Configuration _configuration;

        public ISessionFactory SessionFactory
        {
            get { return _sessionFactory; }
        }

        public Configuration Configuration
        {
            get { return _configuration; }
        }

        private void CreateSessionFactory()
        {
            _sessionFactory = Fluently
            .Configure()
            .Database(_dbType)
            .Mappings(m => m.FluentMappings.AddFromAssemblyOf<TicketDeOrcamentoPessoal>())
            .ExposeConfiguration(cfg => _configuration = cfg)
            .BuildSessionFactory();
        }
    }
}
