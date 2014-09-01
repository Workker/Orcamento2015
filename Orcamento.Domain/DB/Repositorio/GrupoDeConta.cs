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
            
            return criterio.List<Object>().ToList();
        }

        public virtual void SalvarLista(List<GrupoDeConta> roots)
        {
            var transaction = Session.BeginTransaction();

            try
            {
                foreach (var root in roots)
                {
                    Session.SaveOrUpdate(root);
                }
                transaction.Commit();
            }
            catch (System.Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }
        }

        public virtual void Deletar(IList<GrupoDeConta> roots)
        {
            var transaction = Session.BeginTransaction();
            try
            {
                // Session.Flush();
                foreach (var root in roots)
                {
                    Session.Delete(root);
                }

                transaction.Commit();
            }
            catch (System.Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }


        }
    }
}
