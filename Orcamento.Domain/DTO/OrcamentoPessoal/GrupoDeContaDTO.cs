using System.Collections.Generic;

namespace Orcamento.Domain.DTO.OrcamentoPessoal
{
    public class GrupoDeContaDTO
    {
        public string GrupoConta { get; set; }
        public IList<ContaDespesaPessoalDTO> Contas { get; set; }
        public IList<DespesasGrupoDeContaDTO> DespesasGrupoDeConta { get; set; }
        public double TotalGrupoConta { get; set; }
    }


}