using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Orcamento.InfraStructure
{
    [Serializable]
    public class BusinessException : Exception
    {
        public BusinessException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
