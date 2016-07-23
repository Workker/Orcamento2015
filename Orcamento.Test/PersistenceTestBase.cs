using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Orcamento.TestMethod.Mappings
{
    [TestClass]
    public abstract class PersistenceTestMethodBase
    {
        public DataSession DataSession { get; set; }
        public ISession Session { get; set; }

      //  [SetUp]
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

        //[TearDown]
        public void TestClassTearDown()
        {
            Session.Close();

            DataSession.SessionFactory.Close();
        }
    }
}
