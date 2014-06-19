using System;
namespace Orcamento.Domain.DTO.OrcamentoPessoal
{
    public class DespesasGrupoDeContaDTO
    {
        public int Mes { get; set; }
        public double Valor { get; set; }
        public Guid Guid { get; set; }

        public DespesasGrupoDeContaDTO()
        {
            this.Guid = Guid.NewGuid();
        }
    }
}
