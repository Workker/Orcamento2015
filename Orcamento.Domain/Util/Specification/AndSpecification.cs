using Orcamento.Domain.Entities.Monitoramento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Orcamento.Domain.Util.Specification
{
    public class AndSpecification : EspecificacaoCargaFuncionario
    {
        private EspecificacaoCargaFuncionario One;
        private EspecificacaoCargaFuncionario Other;

        public AndSpecification(EspecificacaoCargaFuncionario x, EspecificacaoCargaFuncionario y)
        {
            One = x;
            Other = y;
        }

        public override bool IsSatisfiedBy(Carga candidate)
        {
            return One.IsSatisfiedBy(candidate) && Other.IsSatisfiedBy(candidate);
        }
    }
}
