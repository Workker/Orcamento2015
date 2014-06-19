using System.Collections.Generic;
using System.Linq;

namespace Orcamento.Domain.DB.Repositorio
{
    public interface ITicketsDeOrcamentoPessoal
    {
        void Adicionar(TicketDeOrcamentoPessoal ticketDeOrcamentoPessoal);
        void Salvar(IAggregateRoot<int> root);
        void Deletar(IAggregateRoot<int> root);
        IList<T> Todos<T>();
        T Obter<T>(int id);

        List<TicketDeOrcamentoPessoal> Todos(Gerenciamento.Departamento departamento);
    }

    public class TicketsDeOrcamentoPessoal : BaseRepository, ITicketsDeOrcamentoPessoal
    {
        public virtual void Deletar(List<TicketDeOrcamentoPessoal> roots)
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

        public void Adicionar(TicketDeOrcamentoPessoal ticketDeOrcamentoPessoal)
        {
            Salvar(ticketDeOrcamentoPessoal);
        }


        public List<TicketDeOrcamentoPessoal> Todos(Gerenciamento.Departamento departamento)
        {
            return Session.QueryOver<TicketDeOrcamentoPessoal>().Where(c => c.Departamento == departamento).List().ToList();
        }
    }
}
