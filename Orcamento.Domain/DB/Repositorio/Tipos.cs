using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Orcamento.Domain.DB.Repositorio;


namespace Orcamento.Domain
{
    public class Tipos<T> : BaseRepository
    {
        public T Obter(int id)
        {
           return Obter<T>(id);
        }

        public IList<T> Todos()
        {
            return Todos<T>();
        }
    }
}
