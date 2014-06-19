using System.Collections.Generic;
using System.Linq;

namespace Orcamento.Domain.DTO.Viagem
{
    public class DespesaDeViagemDTO
    {
        public string NomeCidade { get; set; }
        public string Despesa { get; set; }
        public List<ItemDespesaDeViagemDTO> Itens { get; set; }
        public long ValorTotal { get { return Itens.Sum(i => i.Valor); } }

        public long ObterValorTotalDoMes(MesEnum mesEnum)
        {
            List<ItemDespesaDeViagemDTO> itensDoMes = Itens.Where(x => x.Mes == mesEnum).ToList();

            return itensDoMes.Sum(x => x.Valor);
        }

        public long ObterTotal()
        {
            long total = default(long);

            for (int i = 1; i < 13; i++)
            {
                total += ObterValorTotalDoMes((MesEnum)i);
            }

            return total;
        }

        public DespesaDeViagemDTO()
        {
            Itens = new List<ItemDespesaDeViagemDTO>();
        }

        public void AdicionarItem(MesEnum mesEnum, long valor, string tipo)
        {
            ItemDespesaDeViagemDTO item = Itens.FirstOrDefault(i => i.Mes == mesEnum && i.Tipo == tipo);

            if (item != null)
                item.Valor += valor;
            else
                Itens.Add(new ItemDespesaDeViagemDTO { Mes = mesEnum, Tipo = tipo, Valor = valor });
        }
    }
}
