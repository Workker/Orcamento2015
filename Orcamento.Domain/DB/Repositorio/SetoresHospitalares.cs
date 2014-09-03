
using NHibernate.Criterion;
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

        public  virtual SetorHospitalar Obter(string nome)
        {
            var criterio = Session.CreateCriteria<SetorHospitalar>();
            criterio.Add(Expression.Eq("NomeSetor", nome));

            return criterio.UniqueResult<SetorHospitalar>();
        }

        public virtual SubSetorHospital ObterSub(string nome)
        {
            var criterio = Session.CreateCriteria<SubSetorHospital>();
            criterio.Add(Expression.Eq("NomeSetor", nome));

            return criterio.UniqueResult<SubSetorHospital>();
        }
    }

}
