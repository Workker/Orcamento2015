
using NHibernate.Criterion;
using Orcamento.Domain.Robo.Monitoramento.EstruturaOrcamentaria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Orcamento.Domain.DB.Repositorio
{
    public class Processos : BaseRepository
    {
        public Processo ObterProcesso(TipoProcessoEnum processo)
        {
            TiposDeProcesso tipos = new TiposDeProcesso();
            var tipo = tipos.ObterPor(processo);

            var criterio = Session.CreateCriteria<Processo>();
            criterio.Add(Expression.Eq("TipoProcesso", tipo));
            return criterio.UniqueResult<Processo>();
        }
      
    }
}
