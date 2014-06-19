using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Orcamento.Domain.DTO.Viagem;

namespace Orcamento.Domain.DTO.OutrasDespesas
{
    public class OutraDespesaDTO
    {
        public string NomeCidade { get; set; }
        public string Despesa { get; set; }
        public List<ItemOutraDespesaDTO> Itens { get; set; }
        public int ValorTotal { get { return Itens.Sum(i => i.Valor); } }

        public int ObterValorTotalDoMes(MesEnum mesEnum)
        {
            List<ItemOutraDespesaDTO> itensDoMes = Itens.Where(x => x.Mes == mesEnum).ToList();

            return itensDoMes.Sum(x => x.Valor);
        }

        public int ObterTotal()
        {
            int total = default(int);

            for (int i = 1; i < 13; i++)
            {
                total += ObterValorTotalDoMes((MesEnum)i);
            }

            return total;
        }

        public OutraDespesaDTO()
        {
            Itens = new List<ItemOutraDespesaDTO>();
        }

        public void AdicionarItem(MesEnum mesEnum, int valor, string tipo)
        {
            ItemOutraDespesaDTO item = Itens.FirstOrDefault(i => i.Mes == mesEnum && i.Tipo == tipo);

            if (item != null)
                item.Valor += valor;
            else
                Itens.Add(new ItemOutraDespesaDTO { Mes = mesEnum, Tipo = tipo, Valor = valor });
        }
    }
}
