using Orcamento.Domain.Entities.Monitoramento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Orcamento.Domain.Util.Specification
{
    public class OrSpecification : Especificacao
    {
        private Especificacao One;
        private Especificacao Other;

        public OrSpecification(Especificacao x, Especificacao y)
        {
            One = x;
            Other = y;
        }

        public override bool IsSatisfiedBy(Carga candidate)
        {
            return One.IsSatisfiedBy(candidate) || Other.IsSatisfiedBy(candidate);
        }
    }
}
