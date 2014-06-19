using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace Orcamento.Domain
{
    public class DiariaViagem : DespesaDeViagem
    {
        public virtual Diaria Diaria { get; set; }
        public override string NomeCidade
        {
            get
            {
                return Diaria.Cidade.Descricao;
            }
        }
        public override string Despesa
        {
            get { return "Diaria de Hotel"; }
        }
        public override long ValorTotal
        {
            get
            {
                return Diaria.Tickets.Sum(t => t.Valor) * base.Quantidade;
            }
        }
        public override long ValorTotalTaxi
        {
            get
            {
                return Diaria.Tickets.Where(t => t.TipoTicket.Id == 2).Sum(d => d.Valor) * Quantidade;
            }
        }
        public override long ValorTotalRefeicao
        {
            get
            {
                return Diaria.Tickets.Where(t => t.TipoTicket.Id == 3).Sum(d => d.Valor) * Quantidade;
            }
        }
        public override long ValorTotalDiaria
        {
            get
            {
                return Diaria.Tickets.Where(t => t.TipoTicket.Id == 1).Sum(d => d.Valor) * Quantidade;
            }
        }

        public override List<Ticket> Tickets
        {
            get
            {
                return Diaria.Tickets.ToList();
            }
        }

        public DiariaViagem() { }

        public DiariaViagem(Diaria diaria, MesEnum mes)
        {
            Contract.Requires(diaria != null, "Diaria não informada");
            this.Diaria = diaria;
            this.Mes = mes;
        }
    }
}
