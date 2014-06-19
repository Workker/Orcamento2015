using System.Collections.Generic;

namespace Orcamento.Domain.DB.Repositorio
{
    public interface ITicketsDeViagens
    {
        void Salvar(Ticket ticket);
        void Salvar(IAggregateRoot<int> root);
        void Deletar(IAggregateRoot<int> root);
        IList<T> Todos<T>();
        T Obter<T>(int id);
    }
}