using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Criterion;

namespace Orcamento.Domain.DB.Repositorio
{
    public class Setores :BaseRepository
    {

        public virtual Setor ObterPor(string nome)
        {
            var criterio = Session.CreateCriteria<Setor>();
            criterio.Add(Expression.Eq("Nome", nome));
            return criterio.UniqueResult<Setor>();
        }

        public virtual SubSetorHospital ObterSubSetorPor(string nome)
        {
            var criterio = Session.CreateCriteria<SubSetorHospital>();
            criterio.Add(Expression.Eq("NomeSetor", nome));
            return criterio.UniqueResult<SubSetorHospital>();
        }

        public virtual List<Setor> Todos() 

        {
            var criterio = Session.CreateCriteria<Setor>();
            return criterio.List<Setor>().ToList();
        }

        public virtual void Adicionar(Setor setor)
        {
            this.Salvar(setor);
        }
    }
}
