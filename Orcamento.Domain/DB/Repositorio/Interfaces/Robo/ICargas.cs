using Orcamento.Domain.Entities.Monitoramento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Orcamento.Domain.DB.Repositorio.Interfaces.Robo
{
    public interface ICargas
    {
        Carga ObterPor(Guid id);
        void Salvar(Carga carga);
        IList<Carga> Todos();
    }
}
