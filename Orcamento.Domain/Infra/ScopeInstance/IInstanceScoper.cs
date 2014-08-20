using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orcamento.Domain.Session
{
    public interface IInstanceScoper<T>
    {
        T GetScopedInstance(string key, Func<T> builder);
        void ClearScopedInstance(string key);
    }
}
