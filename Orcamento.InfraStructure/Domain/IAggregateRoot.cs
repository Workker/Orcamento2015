using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Orcamento.Domain
{
    public interface IAggregateRoot<T>
    {
        T Id { get; }
    }
}
