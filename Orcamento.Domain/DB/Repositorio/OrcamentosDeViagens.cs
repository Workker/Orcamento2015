using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Criterion;
using Orcamento.Domain.ComponentesDeOrcamento.OrcamentoDeViagem;
using Orcamento.Domain.Gerenciamento;

namespace Orcamento.Domain.DB.Repositorio
{
    public class OrcamentosDeViagens : BaseRepository
    {
        public void Salvar(OrcamentoDeViagem orcamento)
        {
            base.Salvar(orcamento);
        }

        public List<OrcamentoDeViagem> Todos()
        {
            return base.Todos<OrcamentoDeViagem>().ToList();
        }

        public virtual OrcamentoDeViagem ObterOrcamentoFinal(CentroDeCusto centroDeCusto)
        {
            var criterio = Session.CreateCriteria<OrcamentoDeViagem>();
            criterio.Add(Expression.Eq("VersaoFinal", true));
            criterio.Add(Expression.Eq("CentroDeCusto", centroDeCusto));

            return criterio.UniqueResult<OrcamentoDeViagem>();
        }

        public virtual List<OrcamentoDeViagem> TodosPor(CentroDeCusto centroDeCusto, Setor setor)
        {
            var criterio = Session.CreateCriteria<OrcamentoDeViagem>();
            criterio.Add(Expression.Eq("Setor", setor));
            criterio.Add(Expression.Eq("CentroDeCusto", centroDeCusto));

            return criterio.List<OrcamentoDeViagem>().ToList();
        }
    }
}
