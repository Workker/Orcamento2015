using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using System.IO;
using NHibernate;
using Orcamento.Domain.DB.Mappings;

namespace Orcamento.Test
{
    [TestFixture]
    [Ignore]
    public class CreateDataBaseTest
    {
        [Test]
        public void Criar_Banco_De_Dados_Por_Modelo()
        {
            try
            {
                Fluently.Configure().Database(MsSqlConfiguration.MsSql2005.ConnectionString(c => c
               .FromAppSetting("Conexao")
                )).Mappings(m => m.FluentMappings.AddFromAssemblyOf<SetorMap>())
                .ExposeConfiguration(BuildSchema).BuildSessionFactory();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void BuildSchema(Configuration config)
        {
            new SchemaExport(config)
                .Create(true, true);
        }
    }
}
