using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Orcamento.Domain.DB.Repositorio
{
    public interface ICidades
    {
        List<Cidade> Todas();
        void Salvar(IAggregateRoot<int> root);
        void Deletar(IAggregateRoot<int> root);
        IList<T> Todos<T>();
        T Obter<T>(int id);
    }

    public class Cidades : BaseRepository, ICidades
    {
        public List<Cidade> Todas() 
        {
            return base.Todos<Cidade>().ToList();
        }
    }
}
