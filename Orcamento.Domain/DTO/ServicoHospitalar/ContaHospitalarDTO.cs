using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Orcamento.Domain.DTO
{
    public class ContaHospitalarDTO
    {
        public int IdServico { get; set; }
        public string Setor { get; set; }
        public string Subsetor { get; set; }
        public string Conta { get; set; }
        public List<ValorContaDTO> Valores { get; set; }
        public int ContaID { get; set; }
        public List<int> ContasAxenadas { get; set; }
    }
}
