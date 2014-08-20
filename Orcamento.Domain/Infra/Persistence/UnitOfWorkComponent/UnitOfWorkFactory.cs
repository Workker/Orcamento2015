using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orcamento.Domain.Session
{
    public class UnitOfWorkFactory
    {
        public static IUnitOfWork GetDefault()
        {
            return new UnitOfWork(new SessionBuilder());
        }
    }
}
