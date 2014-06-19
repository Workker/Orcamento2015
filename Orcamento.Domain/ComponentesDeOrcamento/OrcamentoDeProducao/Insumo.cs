using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Orcamento.Domain.Gerenciamento;

namespace Orcamento.Domain.ComponentesDeOrcamento.OrcamentoDeProducao
{
    [Serializable]
    public class Insumo : IAggregateRoot<int>
    {
        public virtual int Id
        {
            get;
            set;
        }

        private IList<CustoUnitario> custos;

        public virtual Departamento Departamento { get; set; }

        public virtual IList<CustoUnitario> CustosUnitarios
        {
            get { return custos ?? (custos = new List<CustoUnitario>()); }
            set { custos = value; }
        }

        public Insumo(Departamento departamento)
        {
            this.Departamento = departamento;
            CriarCustoUnitarioHospitalarNoAno();
        }

        protected Insumo()
        { }

        private void CriarCustoUnitarioHospitalarNoAno()
        {
            foreach (var setor in this.Departamento.Setores)
            {
                //foreach (var conta in setor.Contas.Where(c => c.Calculado == false && c.TipoValorContaEnum == TipoValorContaEnum.Quantidade).ToList())
                //{
                foreach (var subSetor in setor.SubSetores)
                {
                    CriarCusto(subSetor, setor);
                }
                //}
            }
        }

        public virtual void CriarCustoUnitarioHospitalarPor(SetorHospitalar setor)
        {
            foreach (var subSetor in setor.SubSetores)
            {
                CriarCusto(subSetor, setor);
            }
        }

        private void CriarCusto(SubSetorHospital subSetor, SetorHospitalar setor)
        {
            this.CustosUnitarios.Add(new CustoUnitario(subSetor, setor));
        }

        public virtual void CriarCustoUnitarioUTIHospitalarPor(SetorHospitalar setor)
        {
            foreach (var subSetor in setor.SubSetores.Where
                (
                s => s.NomeSetor == "UTI Neuro-Intensiva" ||
                    s.NomeSetor == "UTI Pós-Operatória" ||
                    s.NomeSetor == "UTI Ventilatória" ||
                    s.NomeSetor == "UTI Hepático" ||
                    s.NomeSetor == "Suporte"
                )
                )
            {
                CriarCusto(subSetor, setor);
            }
        }
        public virtual void CriarCustoUnitarioOncologiaQuimioTerapiaHospitalarPor(SetorHospitalar setor)
        {
            foreach (var subSetor in setor.SubSetores.Where
                (
                  s =>
                   s.NomeSetor == "Quimioterapia"
                )
                )
            {
                CriarCusto(subSetor, setor);
            }
        }



    }
}
