using Orcamento.Domain.Entities.Monitoramento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Orcamento.Domain.Util.Specification
{
    public class NotSpecification : Especificacao
    {
        private Especificacao Wrapped;

        public NotSpecification(Especificacao x)
        {
            Wrapped = x;
        }

        public override bool IsSatisfiedBy(Carga candidate)
        {
            return !Wrapped.IsSatisfiedBy(candidate);
        }
    }
}
