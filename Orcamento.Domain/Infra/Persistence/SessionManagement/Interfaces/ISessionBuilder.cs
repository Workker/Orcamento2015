using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;

namespace Orcamento.Domain.Session
{
    /// <summary>
    /// Interface de construção de sessão
    /// </summary>
    public interface ISessionBuilder
    {
        /// <summary>
        /// Obtem a sessão corrente do contexto
        /// </summary>
        /// <returns><see cref="ISession"/>Sessão</returns>
        ISession GetSession();
    }
}
