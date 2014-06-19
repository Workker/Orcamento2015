using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Orcamento.Domain.Gerenciamento;
using Orcamento.Domain.ComponentesDeOrcamento.OrcamentoDeProducao;
using NHibernate.Criterion;

namespace Orcamento.Domain.DB.Repositorio
{
    public interface ITicketsDeProducao
    {
        IList<TicketDeProducao> Todos(Departamento departamento);
    }

    public class TicketsDeProducao : BaseRepository, ITicketsDeProducao
    {
        public IList<TicketDeProducao> Todos(Departamento departamento)


        {
            var criterio = Session.CreateCriteria<TicketDeProducao>();
            criterio.Add(Expression.Eq("Hospital.Id", departamento.Id));

            return criterio.List<TicketDeProducao>().ToList();
        }
    }


    public interface ITicketsDeReceita
    {
        IList<TicketDeReceita> Todos(Departamento departamento);
    }

    public class TicketsDeReceita : BaseRepository, ITicketsDeReceita
    {
        public IList<TicketDeReceita> Todos(Departamento departamento)
        {
            return Session.QueryOver<TicketDeReceita>().Where(c => c.Hospital.Id == departamento.Id).List();
        }

        public virtual void Deletar(IList<TicketDeReceita> roots)
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

        public TicketDeReceita Obter(Departamento departamento, TipoTicketDeReceita tipo)
        {
            var criterio = Session.CreateCriteria<TicketDeReceita>();
            criterio.Add(Expression.Eq("Hospital", departamento));
            criterio.Add(Expression.Eq("TipoTicket", tipo));

            return criterio.UniqueResult<TicketDeReceita>();
        }

        public List<TicketDeReceita> Obter( TipoTicketDeReceita tipo)
        {
            var criterio = Session.CreateCriteria<TicketDeReceita>();
            criterio.Add(Expression.Eq("TipoTicket", tipo));

            return criterio.List<TicketDeReceita>().ToList();
        }
    }
}
