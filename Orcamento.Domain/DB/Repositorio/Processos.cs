
using NHibernate.Criterion;
using Orcamento.Domain.Gerenciamento;
using Orcamento.Domain.Robo.Monitoramento.EstruturaOrcamentaria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Orcamento.Domain.DB.Repositorio
{
    public class Processos : BaseRepository
    {
        public Processo ObterProcesso(TipoProcessoEnum processo)
        {
            TiposDeProcesso tipos = new TiposDeProcesso();
            var tipo = tipos.ObterPor(processo);

            var criterio = Session.CreateCriteria<Processo>();
            criterio.Add(Expression.Eq("TipoProcesso", tipo));
            return criterio.UniqueResult<Processo>();
        }

        public Processo ObterProcesso(TipoProcessoEnum processo,Departamento departamento)
        {
            TiposDeProcesso tipos = new TiposDeProcesso();
            var tipo = tipos.ObterPor(processo);

            var criterio = Session.CreateCriteria<Processo>();
            criterio.Add(Expression.Eq("TipoProcesso", tipo));
            criterio.Add(Expression.Eq("Departamento", departamento));
            return criterio.UniqueResult<Processo>();
        }

        public List<Processo> Todos(Gerenciamento.Departamento departamento)
        {
            
            var criterio = Session.CreateCriteria<Processo>();
            criterio.Add(Expression.Eq("Departamento", departamento));
            return criterio.List<Processo>().ToList();
        }

        public virtual void Salvar(List<Processo> roots)
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

        public virtual void Deletar(IList<Processo> roots)
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
    }
}
