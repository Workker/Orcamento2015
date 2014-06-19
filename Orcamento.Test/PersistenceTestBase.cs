using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;

namespace Orcamento.Test.Mappings
{
    [TestFixture]
    public abstract class PersistenceTestBase
    {
        public DataSession DataSession { get; set; }
        public ISession Session { get; set; }

        [TestFixtureSetUp]
        public void Inicializar()
        {
            DataSession = new DataSession(SQLiteConfiguration.Standard.InMemory());
            Session = DataSession.SessionFactory.OpenSession();
            BuildSchema(Session, DataSession.Configuration);
        }

        public void BuildSchema(ISession session, Configuration configuration)
        {
            var export = new SchemaExport(configuration);

            export.Execute(true, true, false, session.Connection, null);
        }

        [TestFixtureTearDown]
        public void TestFixtureTearDown()
        {
            Session.Close();

            DataSession.SessionFactory.Close();
        }
    }
}
