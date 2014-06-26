using Orcamento.Domain.DB.Repositorio.Robo;
using Orcamento.Domain.Entities.Monitoramento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Orcamento.Controller.Robo
{
    public class CargaController
    {
        readonly Cargas _cargas = new Cargas();

        public IList<Carga> Todos()
        {
            return _cargas.Todos();
        }

        public Carga ObterPor(Guid id)
        {
            return _cargas.ObterPor(id); ;
        }

        public void Salvar(Carga carga)
        {
            _cargas.Salvar(carga);
        }

    }
}
