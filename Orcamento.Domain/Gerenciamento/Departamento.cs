using System.Collections.Generic;
using System.Linq;
using System.Diagnostics.Contracts;
using System;

namespace Orcamento.Domain.Gerenciamento
{

    [Serializable]
    public enum TipoDepartamento
    {
        setor = 2,
        hospital = 1,
        defo = 3
    }

    [Serializable]
    public abstract class Departamento : IAggregateRoot<int>
    {
        public virtual int Id { get; set; }

        public virtual TipoDepartamento Tipo
        {
            get
            {
                return TipoDepartamento.defo;
            }
        }
        public virtual string Nome { get; set; }

        public virtual IList<CentroDeCusto> CentrosDeCusto { get; set; }

        public virtual IList<SetorHospitalar> Setores { get; set; }

        public virtual void Adicionar(CentroDeCusto centroDeCusto)
        {
            if (CentrosDeCusto == null)
                CentrosDeCusto = new List<CentroDeCusto>();

            CentrosDeCusto.Add(centroDeCusto);
        }

        public virtual void AdicionarSetor(SetorHospitalar setor)
        {
            Contract.Requires(setor != null, "Setor não informado.");

            if (Setores == null)
                Setores = new List<SetorHospitalar>();

            Setores.Add(setor);
        }

        public virtual CentroDeCusto ObterCentroDeCustoPor(int id)
        {
            return CentrosDeCusto.FirstOrDefault(c => c.Id == id);
        }

        public virtual void AdicionarCentroDeCusto(CentroDeCusto centroDeCusto)
        {
            Contract.Requires(centroDeCusto != null, "Centro de custo não informado.");

            if (CentrosDeCusto == null)
                CentrosDeCusto = new List<CentroDeCusto>();

            CentrosDeCusto.Add(centroDeCusto);
        }

        public virtual void Remover(CentroDeCusto centroDeCusto)
        {
            if (CentrosDeCusto.Count > 0)
                CentrosDeCusto.Remove(centroDeCusto);
        }

        public virtual CentroDeCusto ObterCentroDeCustoPor(string codigoDoCentroCusto)
        {
            if (CentrosDeCusto == null)
                CentrosDeCusto = new List<CentroDeCusto>();

            return CentrosDeCusto.SingleOrDefault(c => c.CodigoDoCentroDeCusto == codigoDoCentroCusto);
        }
    }
}
