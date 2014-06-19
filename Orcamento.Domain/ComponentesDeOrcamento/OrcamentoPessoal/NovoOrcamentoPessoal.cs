using Orcamento.Domain.Gerenciamento;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Orcamento.Domain.ComponentesDeOrcamento.OrcamentoPessoal.Despesas;
using System.Linq;

namespace Orcamento.Domain.ComponentesDeOrcamento.OrcamentoPessoal
{
    public class NovoOrcamentoPessoal : IAggregateRoot<int>
    {
        public virtual int Id { get; set; }
        public virtual int Ano { get; set; }
        public virtual Departamento Departamento { get; set; }
        public virtual CentroDeCusto CentroDeCusto { get; set; }
        public virtual IList<DespesaPessoal> Despesas { get; set; }
        public virtual IList<TicketDeOrcamentoPessoal> Tickets { get; set; }
        public virtual string Justificativa { get; set; }

        protected NovoOrcamentoPessoal() { }

        public NovoOrcamentoPessoal(Departamento departamento, CentroDeCusto centroDecusto, int ano)
        {
            Tickets = new List<TicketDeOrcamentoPessoal>();
            this.Departamento = departamento;
            this.CentroDeCusto = centroDecusto;
            this.Ano = ano;
        }

        public virtual void AdicionarDespesa(DespesaPessoal despesa)
        {
            Contract.Requires(despesa != null, "Despesa não pode ser nula.)");

            if (this.Despesas == null)
                Despesas = new List<DespesaPessoal>();

            this.Despesas.Add(despesa);
        }

        public virtual void RemoverDespesas()
        {
            if (Despesas != null)
                while (Despesas.Count != 0)
                {
                    Despesas.Remove(Despesas[0]);
                }
        }

        public virtual void Adicionar(TicketDeOrcamentoPessoal t)
        {
            Contract.Requires(t != null, "Ticket está nulo.");

            if (Tickets == null)
                Tickets = new List<TicketDeOrcamentoPessoal>();

            Tickets.Add(t);
        }
    }
}