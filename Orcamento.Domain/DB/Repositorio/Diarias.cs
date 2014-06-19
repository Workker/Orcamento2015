using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Orcamento.Domain.DB.Repositorio
{
    public class Diarias : BaseRepository, IDiarias
    {
        public virtual void Salvar(Diaria diaria) 
        {
            base.Salvar(diaria);
        }

        public virtual List<Diaria> Todos() 
        {
            return base.Todos<Diaria>().ToList();
        }
    }
}
