using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;
using System.Xml.Serialization;

namespace Orcamento.Domain
{
    [Serializable]
    public class ServicoHospitalar : IAggregateRoot<int>
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

        public virtual ContaHospital Conta { get; set; }

        public virtual IList<ProducaoHospitalar> Valores { get; set; }

        protected ServicoHospitalar() { }

        public ServicoHospitalar(ContaHospital conta, SubSetorHospital subSetor, SetorHospitalar setor)
        {
            InformarConta(conta);
            InformarSubSetor(subSetor);
            InformarSetor(setor);
            Valores = new List<ProducaoHospitalar>();

            CriarValoresSemCustoProducao();
        }


        public ServicoHospitalar(ContaHospital conta, SubSetorHospital subSetor, SetorHospitalar setor,bool naoCriaValores)
        {
            InformarConta(conta);
            InformarSubSetor(subSetor);
            InformarSetor(setor);
            Valores = new List<ProducaoHospitalar>();
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

        private void InformarConta(ContaHospital conta)
        {
            Contract.Requires(conta != null, "Conta não informada");
            this.Conta = conta;
        }
    }
}
