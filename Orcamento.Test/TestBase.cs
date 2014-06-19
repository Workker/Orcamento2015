using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rhino.Mocks;
using NUnit.Framework;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate.Cfg;
using System.IO;
using NHibernate.Tool.hbm2ddl;
using Orcamento.Domain.DB.Mappings;

namespace Orcamento.Test
{
    public abstract class TestBase
    {
        private MockRepository repository;
        protected MockRepository Repository
        {
            get
            {
                return repository;

            }
        }

        [SetUp]
        protected virtual void SetUp()
        {
            log4net.Config.XmlConfigurator.Configure();

            Criar_Banco_De_Dados_Por_Modelo();

            repository = new MockRepository();
        }

        public void Criar_Banco_De_Dados_Por_Modelo()
        {
            try
            {
                Fluently.Configure().Database(
                SQLiteConfiguration.Standard
                .UsingFile("Orcamento.db")).Mappings(m => m.FluentMappings.AddFromAssemblyOf<SetorMap>())
                .ExposeConfiguration(BuildSchema).BuildSessionFactory();

            }
            catch (Exception)
            {

                throw;
            }
        }

        private static void BuildSchema(Configuration config)
        {
            // delete the existing db on each run
            if (File.Exists("Orcamento.db"))
                File.Delete("Orcamento.db");

            // this NHibernate tool takes a configuration (with mapping info in)
            // and exports a database schema from it
            new SchemaExport(config)
              .Create(false, true);
        }
    }
}
