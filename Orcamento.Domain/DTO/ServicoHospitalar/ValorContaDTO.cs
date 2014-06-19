using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Orcamento.Domain.DTO
{
    public class ValorContaDTO
    {
        public MesEnum Mes { get; set; }
        public double Valor { get; set; }
        public TipoValorContaEnum TipoValor { get; set;}
        public int ValorID { get; set; }
        public int ContaId { get; set; }
        public bool Calculado { get; set; }
        public bool Negativo { get; set; }
    }
}
