using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;
using System.Xml.Serialization;

namespace Orcamento.Domain
{
    [Serializable]
    public class FatorReceita : IAggregateRoot<int>
    {
        [NonSerialized()]
        private int id;


        public virtual int Id
        {
            get { return id; }
            set { id = value; }
        }


        public virtual SubSetorHospital SubSetor { get; set; }

        public virtual SetorHospitalar Setor { get; set; }

        public virtual IList<IncrementoDaComplexidade> Incrementos { get; set; }

        public FatorReceita(SetorHospitalar setor, SubSetorHospital subSetor) 
        {
            InformarSetor(setor);
            InformarSubSetor(subSetor);
            Incrementos = new List<IncrementoDaComplexidade>();

            CriarValoresSemCustoProducao();
        }

        public FatorReceita(SetorHospitalar setor, SubSetorHospital subSetor,bool naoCriaValores)
        {
            InformarSetor(setor);
            InformarSubSetor(subSetor);
            Incrementos = new List<IncrementoDaComplexidade>();
        }

        protected FatorReceita() { }

        private void CriarValoresSemCustoProducao()
        {
               for (short mes = 1; mes < 13; mes++)
                   AdicionarValor(new IncrementoDaComplexidade((MesEnum)mes));
        }

        private void AdicionarValor(IncrementoDaComplexidade incremento)
        {
            Incrementos.Add(incremento);
        }

        public virtual void CalcularReceitaBruta(List<ServicoHospitalar> servicos, List<TicketDeProducao> tickets)
        {
            foreach (var incremento in Incrementos)
            {
                incremento.CalcularReceitaBruta(servicos,tickets);
            }
        }

        public virtual void CalcularReceitaLiquida(List<ServicoHospitalar> servicos,List<TicketDeProducao> tickets,List<TicketParcela> parcelas)
        {
            foreach (var incremento in Incrementos)
            {
                incremento.CalcularReceitaLiquida(servicos,tickets,Incrementos.Where(i=> i.Mes < incremento.Mes).ToList(),parcelas.Where(p=> p.Mes <= incremento.Mes).ToList());
            }
        }

        private void InformarSetor(SetorHospitalar setor)
        {
            Contract.Requires(setor != null, "Setor setor deve ser diferente de nulo.");
            this.Setor = setor;
        }

        private void InformarSubSetor(SubSetorHospital subSetor)
        {
            Contract.Requires(subSetor != null, "Sub setor deve ser diferente de nulo.");
            this.SubSetor = subSetor;
        }
    }
}
