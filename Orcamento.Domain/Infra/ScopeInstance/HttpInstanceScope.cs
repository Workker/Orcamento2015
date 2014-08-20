using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Orcamento.Domain.Session
{
    public class HttpContextInstanceScoper<T> : InstanceScoperBase<T>
    {
        public bool IsEnabled()
        {
            return GetHttpContext() != null;
        }

        private HttpContext GetHttpContext()
        {
            return HttpContext.Current;
        }

        protected override IDictionary GetDictionary()
        {
            return GetHttpContext().Items;
        }
    }
}
