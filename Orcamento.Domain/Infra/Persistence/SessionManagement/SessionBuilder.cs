using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;

namespace Orcamento.Domain.Session
{
    public class SessionBuilder : ISessionBuilder
    {
        private const string NHIBERNATE_SESSION = "NHibernate.ISession";
        private readonly HybridInstanceScoper<ISession> _hybridInstanceScoper;
        private ISessionFactoryBuilder _builder;

        public ISessionFactoryBuilder Builder
        {
            get
            {
                if (_builder == null)
                    _builder = new SessionFactoryBuilder();
                return _builder;
            }
            set { _builder = value; }
        }

        public SessionBuilder()
        {
            _hybridInstanceScoper = new HybridInstanceScoper<ISession>();
        }

        public ISession GetSession()
        {
            ISession instance = GetScopedInstance();
            if (!instance.IsOpen)
            {
                _hybridInstanceScoper.ClearScopedInstance(NHIBERNATE_SESSION);
                return GetScopedInstance();
            }
            return instance;
        }

        private ISession GetScopedInstance()
        {
            return _hybridInstanceScoper.GetScopedInstance(NHIBERNATE_SESSION, BuildSession);
        }

        private ISession BuildSession()
        {
            ISessionFactory factory = Builder.GetFactory();
            ISession session = factory.OpenSession();
            session.FlushMode = FlushMode.Commit;
            return session;
        }
    }
}
