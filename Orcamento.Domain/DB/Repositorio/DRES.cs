using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Orcamento.Domain.Gerenciamento;
using Orcamento.Domain.ComponentesDeOrcamento;
using NHibernate.Criterion;

namespace Orcamento.Domain.DB.Repositorio
{
    public class DRES : BaseRepository
    {

        public virtual void Deletar(List<DRETotal> roots)
        {
            var transaction = Session.BeginTransaction();
            try
            {

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


        public DRETotal Obter(Departamento departamento) 
        {
            var criterio = Session.CreateCriteria<DRETotal>();
            criterio.Add(Expression.Eq("Departamento", departamento));

            return criterio.UniqueResult<DRETotal>();
        }

        public  List<DRETotal> Todos()
        {
            var criterio = Session.CreateCriteria<DRETotal>();
            return criterio.List<DRETotal>().ToList();
        }

        public virtual void SalvarLista(List<DRETotal> roots)
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
    }
}
