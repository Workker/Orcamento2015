using System.Configuration;
using System.Reflection;
using System.Web;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Orcamento.Domain.DB.Mappings;

namespace Orcamento.Domain.Session
{
    /// <summary>
    /// Fábrica de Contrução de configuração de conexões entre o Hibernate e o banco de dados
    /// </summary>
    public class ConfigurationFactory : IConfigurationFactory
    {
        /// <summary>
        /// Constrói a configuração do hibernate para iniciar sessões
        /// </summary>
        /// <returns>Configuração</returns>
        public FluentConfiguration Build()
        {
            return Fluently.Configure().Database(MsSqlConfiguration.MsSql2008.ConnectionString(c => c
                                                                                                        .FromAppSetting(
                                                                                                            "Conexao")))
                           .Mappings(m => m.FluentMappings.AddFromAssemblyOf<SetorMap>())
                           .Mappings(m => m.MergeMappings())
                           .ExposeConfiguration(c => c.SetProperty("current_session_context_class", "web"));
        }
    }
}
