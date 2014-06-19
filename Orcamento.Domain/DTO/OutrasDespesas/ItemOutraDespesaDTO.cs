using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Orcamento.Domain.DTO.OutrasDespesas
{
    public class ItemOutraDespesaDTO
    {
        public int Valor { get; set; }
        public MesEnum Mes { get; set; }
        public string Tipo { get; set; }
    }
}
