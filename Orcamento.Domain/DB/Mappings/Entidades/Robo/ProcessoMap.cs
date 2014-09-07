using FluentNHibernate.Mapping;
using Orcamento.Domain.Robo.Monitoramento.EstruturaOrcamentaria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Orcamento.Domain.DB.Mappings.Entidades.Robo
{
    public class ProcessoMap : ClassMap<Processo>
    {
        public ProcessoMap()
        {
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.Nome);
            Map(x => x.Status);
            Map(x => x.Iniciado);
            Map(x => x.Finalizado);
            References(d => d.Departamento);
            References(x => x.TipoProcesso);
        }
    }
}
