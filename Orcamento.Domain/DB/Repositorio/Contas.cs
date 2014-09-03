using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;
using Orcamento.Domain.DB.Repositorio;
using Orcamento.Domain.Gerenciamento;
using Orcamento.InfraStructure;
using NHibernate.Criterion;

namespace Orcamento.Domain
{
    public class Contas : BaseRepository, IContas
    {
        public void Salvar(Conta conta)
        {
            Contract.Requires(conta != null, "Conta não informada.");
            base.Salvar(conta);
        }

        public void Alterar(Conta conta)
        {

        }

        public virtual void Deletar(IList<Conta> roots)
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

        public Conta ObterContaPor(string codigo)
        {
            return Session.QueryOver<Conta>().Where(c => c.CodigoDaConta == codigo).SingleOrDefault();
        }

        public Conta ObterContaPor(int id)
        {
            return Session.QueryOver<Conta>().Where(c => c.Id == id).SingleOrDefault();
        }

        public virtual IList<Conta> Todos()
        {
            return base.Todos<Conta>();
        }

        public virtual void SalvarLista(IList<Conta> roots)
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

        public virtual Conta ObterContaPor(string nome, TipoConta ticket)
        {
            var criterio = Session.CreateCriteria<Conta>();
            criterio.Add(Expression.Eq("Nome", nome));
            criterio.Add(Expression.Eq("TipoConta", ticket));

            return criterio.UniqueResult<Conta>();
        }
    }
}
