using Orcamento.Domain.DB.Repositorio.Interfaces.Robo;
using Orcamento.Domain.Entities.Monitoramento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Orcamento.Domain.DB.Repositorio.Robo
{
    public class Cargas : BaseRepository, ICargas
    {
        public Carga ObterPor(Guid id)
        {
            return Session.QueryOver<Carga>().Where(c => c.Id == id).SingleOrDefault();
        }

        public void Salvar(Carga carga)
        {
            base.Salvar(carga);
        }

        public IList<Carga> Todos()
        {
            return base.Todos<Carga>();
        }
    }
}
