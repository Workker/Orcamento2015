using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Orcamento.Domain.Gerenciamento;
using NHibernate.Criterion;
using Orcamento.Domain.ComponentesDeOrcamento.OrcamentoDeProducao;

namespace Orcamento.Domain.DB.Repositorio
{
    public class Insumos : BaseRepository
    {
        public virtual Insumo ObterInsumo(Departamento departamento)
        {
            var criterio = Session.CreateCriteria<Insumo>();
            criterio.Add(Expression.Eq("Departamento", departamento));

            return criterio.UniqueResult<Insumo>();
        }

        public virtual List<Insumo> ObterInsumos()
        {
            var criterio = Session.CreateCriteria<Insumo>();

            return criterio.List<Insumo>().ToList();
        }

        public virtual void SalvarLista(List<Insumo> roots)
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

        public virtual void Deletar(IList<Insumo> roots)
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
