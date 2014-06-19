using System.Collections.Generic;
using Orcamento.Domain.Gerenciamento;

namespace Orcamento.Domain.DB.Repositorio
{
    public interface ICentrosDeCusto
    {
        CentroDeCusto ObterPor(string codigo);
        void Salvar(IAggregateRoot<int> root);
        void Deletar(IAggregateRoot<int> root);
        IList<T> Todos<T>();
        T Obter<T>(int id);
    }

    public class CentrosDeCusto : BaseRepository, ICentrosDeCusto
    {
        public virtual void Deletar(List<CentroDeCusto> roots)
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


        public virtual CentroDeCusto ObterPor(string codigo)
        {
            return Session.QueryOver<CentroDeCusto>().Where(c => c.CodigoDoCentroDeCusto == codigo).SingleOrDefault();
        }

        public virtual IList<CentroDeCusto> Todos()
        {
            return base.Todos<CentroDeCusto>();
        }

        public virtual void SalvarLista(List<CentroDeCusto> roots)
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
