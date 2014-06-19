using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Orcamento.Domain.ComponentesDeOrcamento;

namespace Orcamento.Domain.DB.Mappings.Entidades
{
    public class DRETotalMap : ClassMap<DRETotal>
    {
        public DRETotalMap() 
        {
            Id(d => d.Id);
            References(d => d.Departamento).Not.Nullable();
            Map(d => d.TotalDeducoes);
            Map(d => d.TotalDescontosObtidos);
            Map(d => d.TotalDespesasGerais);
            Map(d => d.TotalDespesasOperacionais);
            Map(d => d.TotalGlosaExterna);
            Map(d => d.TotalGlosaInterna);
            Map(d => d.TotalImpostos);
            Map(d => d.TotalInsumos);
            Map(d => d.TotalMargemBruta);
            Map(d => d.TotalMarketing);
            Map(d => d.TotalOcupacao);
            Map(d => d.TotalPercentualGlosaExterna);
            Map(d => d.TotalPercentualGlosaInterna);
            Map(d => d.TotalPercentualImpostos);
            Map(d => d.TotalPercentualInsumos);
            Map(d => d.TotalPercentualMargemBruta);
            Map(d => d.TotalPessoal);
            Map(d => d.TotalReceitaBruta);
            Map(d => d.TotalReceitaLiquida);
            Map(d => d.TotalServicosContratados);
            Map(d => d.TotalServicosMedicos);
            Map(d => d.TotalUtilidadeServico);
            

        }
    }
}
