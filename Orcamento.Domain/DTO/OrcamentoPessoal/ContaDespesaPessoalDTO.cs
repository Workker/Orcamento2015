using System.Collections.Generic;

namespace Orcamento.Domain.DTO.OrcamentoPessoal
{
    public class ContaDespesaPessoalDTO
    {
        public string Conta { get; set; }
        public IList<DespesaPessoalDTO> Despesas { get; set; }
        public double TotalConta { get; set; }
    }
}
