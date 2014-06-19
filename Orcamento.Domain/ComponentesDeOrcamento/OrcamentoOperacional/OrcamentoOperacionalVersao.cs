using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;
using Orcamento.Domain.Gerenciamento;
using Orcamento.InfraStructure;

namespace Orcamento.Domain
{
    public class OrcamentoOperacionalVersao : Orcamento
    {
        public override IList<Despesa> DespesasOperacionais { get; set; }
        public override TipoOrcamentoEnum Tipo
        {
            get
            {
                return TipoOrcamentoEnum.DespesaOperacional;
            }
            set
            {
                base.Tipo = value;
            }
        }
        public override long ValorTotal
        {
            get
            {
                return DespesasOperacionais.Sum(d => d.Valor);
            }
            set
            {
                base.ValorTotal = value;
            }
        }

        public OrcamentoOperacionalVersao(Departamento setor, CentroDeCusto centroDeCusto, int ano)
        {
            this.Ano = ano;
            this.Setor = setor;
            this.CentroDeCusto = centroDeCusto;
            this.DespesasOperacionais = new List<Despesa>();
        }

        public virtual void CriarDespesaSemCusto(MesEnum mes, Conta conta)
        {
            Contract.Requires(!this.DespesasOperacionais.Any(d => d.Mes == mes && d.Conta == conta));

            this.DespesasOperacionais.Add(new Despesa(mes, conta));
        }

        protected OrcamentoOperacionalVersao()
        {

        }

        public virtual void CriarDespesas()
        {
            foreach (var conta in this.CentroDeCusto.Contas)
            {
                for (short mes = 1; mes < 13; mes++)
                    CriarDespesaSemCusto((MesEnum)mes, conta);
            }
        }

        public override void AtualizarDespesas() 
        {
            foreach (var conta in CentroDeCusto.Contas.Where(c=> DespesasOperacionais.All(d=> d.Conta.CodigoDaConta  != c.CodigoDaConta)).ToList())
            {
                for (short mes = 1; mes < 13; mes++)
                    CriarDespesaSemCusto((MesEnum)mes, conta);    
            }
        }

        public override void CalcularTotalDRE()
        {
            this.Legenda = "Orçamento de outras despesas";

            this.ValorTotalDRE = ValorTotal;
        }
    }
}
