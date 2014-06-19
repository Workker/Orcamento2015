using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace Orcamento.InfraStructure.Domain
{
    public static class BeneficioUtil
    {
        public static int ObterOTotalDeMesesAReceberOBeneficio(this DateTime dataDeAdmissao, DateTime dataAtual)
        {
            Contract.Requires(dataAtual > DateTime.MinValue, "Data atual tem que ser preenchida");
            Contract.Requires(dataDeAdmissao > DateTime.MinValue, "Data de admissão tem que ser preenchida");

            int totalDeMesesAReceberOBeneficio = default(int);

            if (dataDeAdmissao.Year == dataAtual.Year)
            {
                totalDeMesesAReceberOBeneficio = dataAtual.Month - dataDeAdmissao.Month;
            }
            else
                totalDeMesesAReceberOBeneficio = 12;

            return totalDeMesesAReceberOBeneficio;
        }
    }
}
