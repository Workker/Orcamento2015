using Orcamento.Domain.Gerenciamento;
using System.Linq;
using System.Collections.Generic;
using NHibernate.SqlCommand;
using NHibernate.Criterion;
using NHibernate.Transform;

namespace Orcamento.Domain.DB.Repositorio
{
    public class Usuarios : BaseRepository
    {
        public virtual void SalvarLista(List<Usuario> roots)
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

        public virtual Usuario ObterAcesso(string login, string senha)
        {
            return Session.QueryOver<Usuario>().Where(x => x.Login == login).And(y => y.Senha == senha).SingleOrDefault();
        }

        public virtual Usuario ObterAcesso(string login)
        {
            return Session.QueryOver<Usuario>().Where(x => x.Login == login).SingleOrDefault();
        }

        public virtual List<Usuario> ObterPor(Departamento departamento)
        {
            var criterio = Session.CreateCriteria<Usuario>("u");
            criterio.CreateCriteria("u.Departamentos", "d", JoinType.InnerJoin);
            criterio.Add(Expression.Eq("d.Id", departamento.Id));
            criterio.SetResultTransformer(new DistinctRootEntityResultTransformer());

            return criterio.List<Usuario>().ToList();
        }

        public virtual Usuario ObterUsuarioPorId(int id)
        {
            return Session.QueryOver<Usuario>().Where(x => x.Id == id).SingleOrDefault();
        }

        public virtual List<Usuario> TodosPor(TipoUsuario tipo)
        {
            return Session.QueryOver<Usuario>().Where(u => u.TipoUsuario == tipo).List<Usuario>().ToList();
        }

        public virtual List<Usuario> TodosPor(Departamento departamento)
        {
            var criterio = Session.CreateCriteria<Usuario>("u");
            criterio.CreateCriteria("u.Departamentos", "d", JoinType.InnerJoin);
            criterio.Add(Expression.Eq("d.Id", departamento.Id));
            criterio.SetResultTransformer(new DistinctRootEntityResultTransformer());

            return criterio.List<Usuario>().ToList();
        }

        public virtual void Deletar(List<Usuario> roots)
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

        public virtual void Salvar(List<Usuario> roots)
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