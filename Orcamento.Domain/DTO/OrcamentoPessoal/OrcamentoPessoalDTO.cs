using System.Collections.Generic;

namespace Orcamento.Domain.DTO.OrcamentoPessoal
{
    public class OrcamentoPessoalDTO
    {
        public double TotalOrcamento { get; set; }
        public List<GrupoDeContaDTO> GruposDeConta { get; set; }
        public IList<TotalOrcamentoMensalDTO> TotaisOrcamentoMensal { get; set; }
    }
}
