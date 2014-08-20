using System;

namespace Orcamento.Domain.Session
{
    public interface IConfigurationFactory
    {
        FluentNHibernate.Cfg.FluentConfiguration Build();
    }
}
