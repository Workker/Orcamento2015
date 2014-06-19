using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Criterion;
using Orcamento.Domain.ComponentesDeOrcamento.OrcamentoPessoal;
using Orcamento.Domain.Gerenciamento;

namespace Orcamento.Domain.DB.Repositorio
{
    public class AcordosDeConvencao : BaseRepository
    {
        public virtual AcordoDeConvencao ObterAcordoDeConvencao(Departamento departamento)
        {
            var criterio = Session.CreateCriteria<AcordoDeConvencao>();
            criterio.Add(Expression.Eq("Departamento", departamento));

            return criterio.UniqueResult<AcordoDeConvencao>();
        }

        public virtual void SalvarLista(List<AcordoDeConvencao> roots)
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
