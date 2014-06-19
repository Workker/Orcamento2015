using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Orcamento.Domain.DB.Repositorio
{
    public class ContasHospitalares : BaseRepository
    {
        public ContaHospital ObterContaPor(string nome)
        {
            return Session.QueryOver<ContaHospital>().Where(c => c.Nome == nome).SingleOrDefault();
        }
    }
}
