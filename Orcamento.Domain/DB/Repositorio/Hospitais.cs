using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Orcamento.Domain;

namespace Orcamento.Domain.DB.Repositorio
{
    public class Hospitais : BaseRepository, IHospitais
    {
        public virtual Hospital ObterPor(string nome)
        {
            return Session.QueryOver<Hospital>().Where(h => h.Nome == nome).SingleOrDefault();
        }

        public virtual void Adicionar(Hospital hospital)
        {
            this.Salvar(hospital);
        }

        public virtual IList<Hospital> Todos()
        {
            return base.Todos<Hospital>();
        }

        public virtual Hospital ObterPor(int id)
        {
            return base.Obter<Hospital>(id);
        }
    }
}
