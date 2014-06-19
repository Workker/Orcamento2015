using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;
using Orcamento.Domain.ComponentesDeOrcamento.OrcamentoPessoal.Despesas;
using Orcamento.Domain.Gerenciamento;

namespace Orcamento.Domain
{
    public class OrcamentoPessoal : Orcamento
    {
        public override TipoOrcamentoEnum Tipo
        {
            get
            {
                return TipoOrcamentoEnum.Pessoal;
            }
        }

        protected OrcamentoPessoal()
        {

        }

        public OrcamentoPessoal(CentroDeCusto centrosDeCusto, int ano, List<DespesaPessoal> despesasPessoais)
        {
            //this.Ano = ano;
            //InformarCentrosDeCusto(centrosDeCusto);
            //CriarDespesas();
            //ObterDespesasDosFuncionarios(despesasPessoais);
        }

        private void AnexarOutrasDespesas(DespesaPessoal despesaPessoal)
        {
            if (despesaPessoal != null)
                DespesasPessoais.Add(despesaPessoal);
        }

        private void InformarDespesasPessoais(IList<DespesaPessoal> despesasPessoais)
        {
            if (this.DespesasPessoais == null)
                this.DespesasPessoais = new List<DespesaPessoal>();

            foreach (var despesa in despesasPessoais)
            {
                this.DespesasPessoais.Add(despesa);
            }
        }

        private void CriarDespesas()
        {
            //if (this.DespesasPessoais == null)
            //    this.DespesasPessoais = new List<DespesaPessoal>();

            //foreach (var centro in CentrosDeCusto)
            //{
            //    foreach (var grupo in centro.GrupoDeContas)
            //    {
            //        foreach (var conta in grupo.Contas)
            //        {
            //            foreach (var funcionario in centro.Funcionarios)
            //            {
            //                funcionario.CalcularDespesa(centro, conta, grupo);
            //            }
            //        }
            //    }
            //}
        }

        private void ObterDespesasDosFuncionarios(List<DespesaPessoal> despesasInformadas)
        {
            //if (this.DespesasPessoais == null)
            //    this.DespesasPessoais = new List<DespesaPessoal>();

            //if (despesasInformadas == null)
            //    despesasInformadas = new List<DespesaPessoal>();

            //var despesas = new List<DespesaPessoal>();

            //foreach (var centroDeCusto in CentrosDeCusto)
            //{
            //    despesas = new List<DespesaPessoal>();

            //    foreach (var funcionario in centroDeCusto.Funcionarios)
            //    {
            //        despesas.AddRange(funcionario.ObterDespesas());
            //    }

            //    InformarDespesas(despesas, centroDeCusto);
            //    InformarDespesas(despesasInformadas.Where(c => c.CentroDeCusto == centroDeCusto).ToList(), centroDeCusto);
            //}
        }

        private void InformarDespesas(List<DespesaPessoal> despesas, CentroDeCusto centroDeCusto)
        {
            //    foreach (var despesa in despesas.GroupBy(d => d.Conta))
            //    {
            //        foreach (var grupoConta in despesa.GroupBy(d => d.GrupoDeConta))
            //        {
            //            DespesaPessoal despesaOrcamento = new Salario(null, despesa.Key, centroDeCusto);
            //            despesaOrcamento.Parcelas = new List<Parcela>();

            //            foreach (var despesaPessoal in grupoConta)
            //            {
            //                despesaOrcamento.GrupoDeConta = despesaPessoal.GrupoDeConta;
            //                foreach (var parcela in despesaPessoal.Parcelas)
            //                {
            //                    if (despesaOrcamento.Parcelas.Any(p => p.Mes == parcela.Mes))
            //                        despesaOrcamento.Parcelas.Where(p => p.Mes == parcela.Mes).FirstOrDefault().Valor += parcela.Valor;
            //                    else
            //                        despesaOrcamento.Parcelas.Add(new Parcela() { Mes = parcela.Mes, Valor = parcela.Valor });
            //                }
            //            }
            //            this.DespesasPessoais.Add(despesaOrcamento);
            //        }
            //    
        }

        private void InformarCentrosDeCusto(IList<CentroDeCusto> centrosDeCusto)
        {
            //Contract.Requires(centrosDeCusto != null, "Centro de Custo nulo");
            //Contract.Requires(centrosDeCusto.All(c => c != null), "Centro de custo nulo");

            //if (this.CentrosDeCusto == null)
            //    this.CentrosDeCusto = new List<CentroDeCusto>();

            //foreach (var centroDeCusto in centrosDeCusto)
            //{
            //    this.CentrosDeCusto.Add(centroDeCusto);
            //}
        }

        public override void CalcularTotalDRE()
        {
            //    this.Legenda = "Orçamento Pessoal";

            //    this.ValorTotalDRE = this.DespesasPessoais.Sum(x => x.Parcelas.Sum(y => y.Valor));
            //
        }
    }
}
