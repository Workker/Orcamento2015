using System;
namespace Orcamento.Domain.DTO.OrcamentoPessoal
{
    public class DespesaPessoalDTO
    {
        public int Mes { get; set; }
        public double Valor { get; set; }
        public Guid Guid { get; set; }

        public DespesaPessoalDTO()
        {
            this.Guid = Guid.NewGuid();
        }

        public override bool Equals(object obj)
        {
            var outraParcela = (DespesaPessoalDTO)obj;
            return outraParcela.Guid == Guid;
        }
    }
}