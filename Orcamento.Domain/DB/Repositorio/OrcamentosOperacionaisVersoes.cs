using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Criterion;
using Orcamento.Domain.DB.Repositorio;
using Orcamento.Domain.Gerenciamento;

namespace Orcamento.Domain
{
    public class OrcamentosOperacionaisVersoes : BaseRepository
    {
        public virtual void Salvar(OrcamentoOperacionalVersao orcamento)
        {
            base.Salvar(orcamento);
        }

        public virtual List<OrcamentoOperacionalVersao> TodosPor(CentroDeCusto centroDeCusto, Setor setor) 
        {
            var criterio = Session.CreateCriteria<OrcamentoOperacionalVersao>();
            criterio.Add(Expression.Eq("Setor", setor));
            criterio.Add(Expression.Eq("CentroDeCusto", centroDeCusto));

            return criterio.List<OrcamentoOperacionalVersao>().ToList();
        }

        public virtual OrcamentoOperacionalVersao ObterOrcamentoFinal(CentroDeCusto centroDeCusto)
        {
            var criterio = Session.CreateCriteria<OrcamentoOperacionalVersao>();
            criterio.Add(Expression.Eq("VersaoFinal", true));
            criterio.Add(Expression.Eq("CentroDeCusto", centroDeCusto));

            return criterio.UniqueResult<OrcamentoOperacionalVersao>();
        }
    }
}
