using System.Collections.Generic;

namespace Orcamento.Domain.DB.Repositorio.Interfaces
{
    public interface ITickets
    {
        void Salvar(IAggregateRoot<int> root);
        void Deletar(IAggregateRoot<int> root);
        IList<T> Todos<T>();
        T Obter<T>(int id);
    }
}
