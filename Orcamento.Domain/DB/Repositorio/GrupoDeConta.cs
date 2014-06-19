using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Orcamento.Domain.Gerenciamento;
using NHibernate.Criterion;

namespace Orcamento.Domain.DB.Repositorio
{
    public class GruposDeConta :BaseRepository
    {
        public GrupoDeConta ObterPor(string Nome)
        {
            var criterio = Session.CreateCriteria<GrupoDeConta>();
            criterio.Add(Expression.Eq("Nome", Nome));

            return criterio.UniqueResult<GrupoDeConta>();
        }


        public virtual List<Object> ObterTodos()
        {
            var criterio = Session.CreateQuery("from DespesaPessoal");
            //)
            //var criterio = Session.CreateQuery("from DespesaPessoal");
            //select GrupoDeConta from DespesaPessoal

            //return criterio.List<GrupoDeConta>().ToList();
            return criterio.List<Object>().ToList();
        }

        //public virtual List<OrcamentoOperacionalVersao> TodosPor(CentroDeCusto centroDeCusto, Setor setor) 
        //{
        //    var criterio = Session.CreateCriteria<OrcamentoOperacionalVersao>();
        //    criterio.Add(Expression.Eq("Setor", setor));
        //    criterio.Add(Expression.Eq("CentroDeCusto", centroDeCusto));

        //    return criterio.List<OrcamentoOperacionalVersao>().ToList();
        //}
    }
}
