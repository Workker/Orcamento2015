using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Orcamento.Domain.DB.Repositorio
{
    public class TicketsDeViagens : BaseRepository, ITicketsDeViagens
    {
        public void Salvar(Ticket ticket) 
        {
            base.Salvar(ticket);
        }
    }
}
