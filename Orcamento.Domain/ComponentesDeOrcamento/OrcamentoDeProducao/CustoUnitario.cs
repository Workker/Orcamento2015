using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;
using Orcamento.Domain.Gerenciamento;

namespace Orcamento.Domain.ComponentesDeOrcamento.OrcamentoDeProducao
{
    [Serializable]
    public class CustoUnitario : IAggregateRoot<int>
    {
        public virtual int Id
        {
            get;
            set;
        }

        public virtual SubSetorHospital SubSetor { get; set; }

        public virtual SetorHospitalar Setor { get; set; }

        public virtual IList<ProducaoHospitalar> Valores { get; set; }

        public virtual Departamento Departamento { get; set; }

        protected CustoUnitario() { }

        public CustoUnitario( SubSetorHospital subSetor, SetorHospitalar setor)
        {
            
            InformarSubSetor(subSetor);
            InformarSetor(setor);
            Valores = new List<ProducaoHospitalar>();

            CriarValoresSemCustoProducao();
        }

        private void CriarValoresSemCustoProducao()
        {
            for (short mes = 1; mes < 13; mes++)
                AdicionarValor(new ProducaoHospitalar((MesEnum)mes));
        }

        private void AdicionarValor(ProducaoHospitalar valorServicoHospitalar)
        {
            Valores.Add(valorServicoHospitalar);
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
