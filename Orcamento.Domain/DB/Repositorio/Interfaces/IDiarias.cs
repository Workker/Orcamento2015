using System.Collections.Generic;

namespace Orcamento.Domain.DB.Repositorio
{
    public interface IDiarias
    {
        void Salvar(Diaria diaria);
        List<Diaria> Todos();
        void Salvar(IAggregateRoot<int> root);
        void Deletar(IAggregateRoot<int> root);
        IList<T> Todos<T>();
        T Obter<T>(int id);
    }
}