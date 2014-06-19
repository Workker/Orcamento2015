using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Orcamento.Domain.Gerenciamento;
using System.Diagnostics.Contracts;
using Orcamento.Domain.DTO;

namespace Orcamento.Domain.ComponentesDeOrcamento.OrcamentoDeProducao
{
    [Serializable]
    public enum TipoTicketDeReceita : short
    {
        GlosaInterna = 1,
        GlosaExterna,
        ReajusteDeConvenios,
        Impostos,
        ReajusteDeInsumos,
        Descontos
    }

    [Serializable]
    public class TicketDeReceita : IAggregateRoot<int>
    {
        public virtual int Id
        {
            get;
            set;
        }

        public virtual Departamento Hospital { get; set; }

        public virtual IList<TicketParcela> Parcelas { get; set; }

        public virtual string Nome { get; set; }

        public virtual TipoTicketDeReceita TipoTicket { get; set; }

        //public virtual List<ReceitaDTO>ReceitaGlosaInterna{get;set;}

        public TicketDeReceita(Departamento hospital, string nome, TipoTicketDeReceita tipoTicketDeReceita)
        {
            InformarHospital(hospital);
            Parcelas = new List<TicketParcela>();
            CriarValoresSemCustoProducao();
            this.Nome = nome;
            this.TipoTicket = tipoTicketDeReceita;
        }

        protected TicketDeReceita() { }

        private void InformarHospital(Departamento hospital)
        {
            Contract.Requires(hospital != null, "Hospital deve ser diferente de nulo.");
            this.Hospital = hospital;
        }

        private void CriarValoresSemCustoProducao()
        {
            for (short mes = 1; mes < 13; mes++)
                AdicionarValor(new TicketParcela((MesEnum)mes));
        }

        private void AdicionarValor(TicketParcela parcela)
        {
            Parcelas.Add(parcela);
        }

        public virtual void CalcularGlosa(List<ReceitaDTO> receitas) 
        {
            List<ReceitaDTO> receitaCalculada = new List<ReceitaDTO>();

            foreach (var parcela in Parcelas.OrderBy(p=> p.Mes))
            {
                if(this.TipoTicket == TipoTicketDeReceita.GlosaExterna)
                receitas.Where(r => r.Mes == parcela.Mes).FirstOrDefault().ValorGlosaExterna +=  ( receitas.Where(r => r.Mes == parcela.Mes).FirstOrDefault().Valor * (parcela.Valor / 100));
                else if(this.TipoTicket == TipoTicketDeReceita.GlosaInterna)
                    receitas.Where(r => r.Mes == parcela.Mes).FirstOrDefault().ValorGlosaInterna +=  ( receitas.Where(r => r.Mes == parcela.Mes).FirstOrDefault().Valor * (parcela.Valor / 100));
                else if(this.TipoTicket == TipoTicketDeReceita.Impostos)
                    receitas.Where(r => r.Mes == parcela.Mes).FirstOrDefault().ValorImpostos += (receitas.Where(r => r.Mes == parcela.Mes).FirstOrDefault().Valor * (parcela.Valor / 100));
                else if(this.TipoTicket == TipoTicketDeReceita.Descontos)
                    receitas.Where(r => r.Mes == parcela.Mes).FirstOrDefault().DescontosObtidos += (receitas.Where(r => r.Mes == parcela.Mes).FirstOrDefault().ValorReceitaLiquida * (parcela.Valor / 100));
            }
        }

    }
}
