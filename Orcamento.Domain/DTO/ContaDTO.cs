using System.Collections.Generic;

namespace Orcamento.Domain.DTO
{
    public class ContaDTO
    {
        public int ContaId { get; set; }
        public int DespesaId { get; set; }
        public int DespesaOperacionalId { get; set; }
        public string Conta { get; set; }
        public string Despesa { get; set; }
        public long ValorTotal { get; set; }
        public string MemoriaDeCalculo { get; set; }
        
        public List<DespesaDTO> Despesas { get; set;}
    }
}
