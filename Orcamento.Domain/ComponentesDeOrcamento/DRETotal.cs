using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Orcamento.Domain.Gerenciamento;

namespace Orcamento.Domain.ComponentesDeOrcamento
{
    [Serializable]
    public class DRETotal : IAggregateRoot<int>
    {
        public virtual int Id{get;set;}

        public virtual Departamento Departamento { get; set; }

        public virtual double TotalReceitaBruta { get; set; }

        public virtual double TotalDeducoes { get; set; }

        public virtual double TotalGlosaInterna { get; set; }

        public virtual double TotalPercentualGlosaInterna { get; set; }

        public virtual double TotalGlosaExterna { get; set; }

        public virtual double TotalPercentualGlosaExterna { get; set; }

        public virtual double TotalImpostos { get; set; }

        public virtual double TotalPercentualImpostos { get; set; }

        public virtual double TotalReceitaLiquida { get; set; }

        public virtual double TotalDespesasOperacionais { get; set; }

        public virtual double TotalPessoal { get; set; }

        public virtual double TotalServicosMedicos { get; set; }

        public virtual double TotalInsumos { get; set; }

        public virtual double TotalPercentualInsumos {get; set; }

        public virtual double TotalServicosContratados { get; set; }

        public virtual double TotalOcupacao { get; set; }

        public virtual double TotalUtilidadeServico { get; set; }

        public virtual double TotalDespesasGerais { get; set; }

        public virtual double TotalMarketing { get; set; }

        public virtual double TotalDescontosObtidos { get; set; }

        public virtual double TotalMargemBruta { get; set; }

        public virtual double TotalPercentualMargemBruta { get; set; }


        public DRETotal(Departamento departamneto)
        {
            this.Departamento = departamneto;
        }



        protected DRETotal() { }
    }
}
