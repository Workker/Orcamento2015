using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Orcamento.Domain.DB.Repositorio
{
    public class DespesasOperacionais : BaseRepository
    {
        public void Salvar(Despesa despesa)
        {
            base.Salvar(despesa);
        }

        public Despesa Obter(int id)
        {
            return base.Obter<Despesa>(id);
        }
    }
}
