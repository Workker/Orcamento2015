using System.Collections.Generic;

namespace Orcamento.Domain.DB.Repositorio
{
    public interface IViagens
    {
        void Salvar(Viagem viagem);
        List<Viagem> Todos();
        void Salvar(IAggregateRoot<int> root);
        void Deletar(IAggregateRoot<int> root);
        IList<T> Todos<T>();
        T Obter<T>(int id);
    }
}