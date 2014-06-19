using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Orcamento.Domain.DB.Repositorio
{
    public class Viagens : BaseRepository, IViagens
    {
        public void Salvar(Viagem viagem)
        {
            base.Salvar(viagem);
        }

        public virtual List<Viagem> Todos()
        {
            return base.Todos<Viagem>().ToList();
        }
    }
}
