using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Orcamento.Domain.Gerenciamento;
using NHibernate.SqlCommand;
using NHibernate.Criterion;
using Orcamento.Domain.ComponentesDeOrcamento.OrcamentoDeProducao;

namespace Orcamento.Domain.DB.Repositorio
{
    public interface IDepartamentos
    {
        List<Departamento> Todos();
        Departamento Obter(int id);
        void Salvar(IAggregateRoot<int> root);
        void Deletar(IAggregateRoot<int> root);
        IList<T> Todos<T>();
        T Obter<T>(int id);
    }

    public class Departamentos : BaseRepository, IDepartamentos
    {

        public virtual void Deletar(List<Departamento> roots)
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

        public virtual void Deletar(List<Setor> roots)
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


        public virtual void SalvarLista(List<TicketDeProducao> roots)
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

        public virtual void SalvarLista(List<TicketDeReceita> roots)
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

        public Departamento Obter(int id) 
        {
            return base.Obter<Departamento>(id);
        }

        public virtual Departamento ObterPor(string nome)
        {
            return Session.QueryOver<Departamento>().Where(h => h.Nome == nome).SingleOrDefault();
        }

        public List<Departamento> Todos() 
        {
            return base.Todos<Departamento>().ToList();
        }
    }
}
