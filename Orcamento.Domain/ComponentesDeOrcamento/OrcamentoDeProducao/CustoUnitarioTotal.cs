using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;
using Orcamento.Domain.DTO;
using System.Xml.Serialization;

namespace Orcamento.Domain.ComponentesDeOrcamento.OrcamentoDeProducao
{
    [Serializable]
    public class CustoHospitalar
    {
        [NonSerialized()]
        private int id;


        public virtual int Id
        {
            get { return id; }
            set { id = value; }
        }

        public virtual MesEnum Mes { get; set; }

        public virtual double Valor { get; set; }

        public CustoHospitalar(MesEnum mes)
        {
            this.Mes = mes;
        }

        protected CustoHospitalar() { }
    }

    [Serializable]
    public class CustoUnitarioTotal
    {
        [XmlIgnoreAttribute]
        public virtual int Id
        {
            get;
            set;
        }

        public virtual SubSetorHospital SubSetor { get; set; }

        public virtual SetorHospitalar Setor { get; set; }

        public virtual IList<CustoHospitalar> Valores { get; set; }

        protected CustoUnitarioTotal() { }

        public CustoUnitarioTotal(SubSetorHospital subSetor, SetorHospitalar setor, List<ValorContaDTO> servicos, List<ProducaoHospitalar> custosUnitarios, TicketDeReceita tickets)
        {
            InformarSubSetor(subSetor);
            InformarSetor(setor);
            Valores = new List<CustoHospitalar>();

            CriarCustosProducao(servicos, custosUnitarios, tickets);
        }

        private void CriarCustosProducao(List<ValorContaDTO> servicos, List<ProducaoHospitalar> custosUnitarios, TicketDeReceita ticket)
        {
            this.Valores = new List<CustoHospitalar>();
            for (short mes = 1; mes < 13; mes++)
            {
                var custoHospitalar = new CustoHospitalar((MesEnum)mes) { Valor = servicos[mes - 1].Valor * custosUnitarios[mes - 1].Valor };

                foreach (var item in ticket.Parcelas.Where(p => p.Mes <= (MesEnum)mes))
                {
                    if (item.Negativo)
                        custoHospitalar.Valor -= custoHospitalar.Valor * (item.Valor / 100);
                    else
                        custoHospitalar.Valor += custoHospitalar.Valor * (item.Valor / 100);
                }

                AdicionarValor(custoHospitalar);
            }
        }

        private void AdicionarValor(CustoHospitalar valorServicoHospitalar)
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
