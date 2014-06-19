using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace Orcamento.Domain
{
    public class ViagemCidade : DespesaDeViagem
    {
        public virtual Viagem Viagem { get; set; }
        public override string NomeCidade
        {
            get
            {
                return Viagem.Cidade.Descricao;
            }
        }
        public override string Despesa
        {
            get { return "Viagem"; }
        }
        public override long ValorTotal
        {
            get
            {
                return Viagem.Tickets.Sum(t => t.Valor) * base.Quantidade;
            }
        }
        public override long ValorTotalTaxi
        {
            get
            {
                return Viagem.Tickets.Where(t => t.TipoTicket.Id == 2).Sum(d => d.Valor) * Quantidade;
            }
        }
        public override long ValorTotalRefeicao
        {
            get
            {
                return Viagem.Tickets.Where(t => t.TipoTicket.Id == 3).Sum(d => d.Valor) * Quantidade;
            }
        }
        public override long ValorTotalPassagem
        {
            get
            {
                return Viagem.Tickets.Where(t => t.TipoTicket.Id == 4).Sum(d => d.Valor) * Quantidade;
            }
        }

        public override List<Ticket> Tickets
        {
            get
            {
                return Viagem.Tickets.ToList();
            }
        }

        public ViagemCidade() { }

        public ViagemCidade(Viagem viagem, MesEnum mes)
        {
            Contract.Requires(viagem != null, "Viagem não informada");
            this.Viagem = viagem;
            this.Mes = mes;
        }
    }
}
