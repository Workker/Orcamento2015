using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;
using Orcamento.Domain;

namespace Orcamento.Domain
{
    [Serializable]
    public class TicketDeProducao : IAggregateRoot<int> 
    {
        public virtual int Id
        {
            get;
            set;
        }

        public virtual SubSetorHospital SubSetor { get; set; }

        public virtual SetorHospitalar Setor { get; set; }

        public virtual Hospital Hospital { get; set; }

        public virtual IList<TicketParcela> Parcelas { get; set; }

        public TicketDeProducao(SetorHospitalar setor, SubSetorHospital subSetor,Hospital hospital) 
        {
            InformarSetor(setor);
            InformarSubSetor(subSetor);
            InformarHospital(hospital);
            Parcelas = new List<TicketParcela>();
            CriarValoresSemCustoProducao();
        }

        protected TicketDeProducao() { }

        private void InformarHospital(Domain.Hospital hospital)
        {
            Contract.Requires(hospital!= null, "Hospital deve ser diferente de nulo.");
            this.Hospital = hospital;
        }

        private void CriarValoresSemCustoProducao()
        {
            for (short mes = 1; mes < 13; mes++)
                AdicionarValor(new TicketParcela((MesEnum)mes));
        }

        private void AdicionarValor(TicketParcela parcela)
        {
            Parcelas.Add(parcela);
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
