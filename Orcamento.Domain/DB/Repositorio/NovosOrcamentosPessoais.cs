using System.Collections.Generic;
using NHibernate.SqlCommand;
using Orcamento.Domain.ComponentesDeOrcamento.OrcamentoPessoal;
using NHibernate.Criterion;
using NHibernate.Transform;
using System.Linq;
using System.Linq.Expressions;

namespace Orcamento.Domain.DB.Repositorio
{
    public interface INovosOrcamentosPessoais
    {
        IList<NovoOrcamentoPessoal> Todos();
        void Salvar(IAggregateRoot<int> root);
        void Deletar(IAggregateRoot<int> root);
        IList<T> Todos<T>();
        T Obter<T>(int id);
        NovoOrcamentoPessoal ObterPor(int departamentoId, int centroDeCustoId);
    }

    public class NovosOrcamentosPessoais : BaseRepository, INovosOrcamentosPessoais
    {

        public virtual void Deletar(List<NovoOrcamentoPessoal> roots)
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

        public NovoOrcamentoPessoal ObterPor(int departamentoId, int centroDeCustoId)
        {
            return Session.CreateCriteria<NovoOrcamentoPessoal>("n")
                .CreateCriteria("n.Departamento", "d", JoinType.InnerJoin)
                .Add(Restrictions.Eq("d.Id", departamentoId))
                .CreateCriteria("n.CentroDeCusto", "c", JoinType.InnerJoin)
                .Add(Restrictions.Eq("c.Id", centroDeCustoId))
                .SetResultTransformer(new DistinctRootEntityResultTransformer())
                .UniqueResult<NovoOrcamentoPessoal>();
        }


        public virtual void SalvarLista(List<NovoOrcamentoPessoal> roots)
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

        public IList<NovoOrcamentoPessoal> Todos()
        {
            return Todos<NovoOrcamentoPessoal>();
        }

        public List<NovoOrcamentoPessoal> TodosHospitalares()
        {
            var criterio = Session.CreateCriteria<NovoOrcamentoPessoal>();
            var orcamentos = criterio.List<NovoOrcamentoPessoal>().ToList();

            return orcamentos.Where(o => o.Departamento.Tipo == Gerenciamento.TipoDepartamento.hospital).ToList();
        }

        public List<NovoOrcamentoPessoal> TodosCoorporativo()
        {
            var criterio = Session.CreateCriteria<NovoOrcamentoPessoal>();
            var orcamentos = criterio.List<NovoOrcamentoPessoal>().ToList();

            return orcamentos.Where(o => o.Departamento.Tipo == Gerenciamento.TipoDepartamento.setor).ToList();
        }

        public IList<NovoOrcamentoPessoal> TodosPor(int centroDeCustoID)
        {
            return Session.QueryOver<NovoOrcamentoPessoal>().Where(o=> o.CentroDeCusto.Id == centroDeCustoID).List<NovoOrcamentoPessoal>();
        }

        public IList<NovoOrcamentoPessoal> Todos(int departamentoId)
        {
            return Session.CreateCriteria<NovoOrcamentoPessoal>("n")
               .CreateCriteria("n.Departamento", "d", JoinType.InnerJoin)
               .Add(Restrictions.Eq("d.Id", departamentoId))
               .SetResultTransformer(new DistinctRootEntityResultTransformer())
               .List<NovoOrcamentoPessoal>();
        }
    }
}
