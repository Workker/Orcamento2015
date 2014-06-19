using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Orcamento.Domain.DTO
{
    public class ReceitaDTO
    {
        public double Valor { get; set; }
        public double ValorDespesaPessoal { get; set; }
        public double ValorInsumos { get; set; }
        public double ValorReceitaLiquida { get; set; }
        public double ValorGlosaExterna { get; set; }
        public double ValorGlosaInterna { get; set; }
        public double ValorImpostos { get; set; }
        public double ValorDeducao { get; set; }
        public double DescontosObtidos { get; set; }
        public double ServicosMedicos { get; set; }
        public double ValorMarketing { get; set; }
        public double DespesasGerais { get; set; }
        public double UtilidadeEServico { get; set; }
        public double Ocupacao { get; set; }
        public double ServicosContratados { get; set; }
        public MesEnum Mes { get; set; }
    }
}
