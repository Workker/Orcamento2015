using Orcamento.Domain.Entities.Monitoramento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Orcamento.Domain.Util.Specification
{
    public class NotSpecification : EspecificacaoCargaFuncionario
    {
        private EspecificacaoCargaFuncionario Wrapped;

        public NotSpecification(EspecificacaoCargaFuncionario x)
        {
            Wrapped = x;
        }

        public override bool IsSatisfiedBy(Carga candidate)
        {
            return !Wrapped.IsSatisfiedBy(candidate);
        }
    }
}
