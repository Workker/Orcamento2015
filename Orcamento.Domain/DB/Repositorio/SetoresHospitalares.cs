using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Orcamento.Domain.DB.Repositorio
{

    public class SetoresHospitalares : BaseRepository
    {
        public virtual void Salvar(SetorHospitalar setor)
        {
            base.Salvar(setor);
        }
    }

}
