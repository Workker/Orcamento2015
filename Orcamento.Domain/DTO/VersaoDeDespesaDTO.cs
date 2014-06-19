namespace Orcamento.Domain.DTO
{
    public class VersaoDeDespesaDTO
    {
        public int Id { get; set; }
        public string CentroDeCusto { get; set; }
        public string Versao { get; set; }
        public double ValorTotal { get; set; }
    }
}
