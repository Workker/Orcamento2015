using System.Linq;
using Orcamento.Domain.Gerenciamento;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Orcamento.Domain.ComponentesDeOrcamento.OrcamentoPessoal.Despesas.Beneficios
{
    public class Beneficios : DespesaPessoal
    {
        private const int totalDeMesesParaAdicionarParcelas = 12;
        public Beneficios(Conta conta, NovoOrcamentoPessoal orcamento)
        {
            base.Conta = conta;
            Orcamento = orcamento;
        }



        public virtual void Calcular(double headCount, Funcionario funcionario)
        {
            var valorTotalDoBeneficio = Orcamento.Tickets.Where(
                       x => x.Descricao != "FGTS" && x.Descricao != "INSS" && x.Descricao != "Indenização" && this.Conta.TiposTickets.Any(t => t.Ticket == x.Ticket)).Sum(t => t.Valor);

            AdicionarParcelasDeAcordoCom(funcionario.Demitido ? funcionario.MesDeDemissao - 1 : totalDeMesesParaAdicionarParcelas, valorTotalDoBeneficio, funcionario.DataAdmissao,funcionario);
        }

        public virtual void AdicionarParcelasDeAcordoCom(int mesesAContabilizarOBeneficio, double valorDaParcela, int mesadmissao,Funcionario funcionario)
        {
            Contract.Requires(mesesAContabilizarOBeneficio > default(int), "Total de meses a receber o benefício não foi informado");

            if (this.Parcelas != null && this.Parcelas.Count > 0)
            {
                for (var i = 0; i < mesesAContabilizarOBeneficio; i++)
                {
                    if (this.Parcelas.Any(p => p.Mes == i + 1))
                        this.Parcelas.Where(p => p.Mes == i + 1).FirstOrDefault().Valor += i + 1 < mesadmissao ? 0 : valorDaParcela;
                    else
                    {
                        var parcela = new Parcela
                        {
                            Mes = i + 1,
                            Valor = valorDaParcela
                        };

                        if (i + 1 < mesadmissao && funcionario.AnoAdmissao == 2016)
                            parcela.Valor = 0;

                        Adicionar(parcela);
                    }
                }
            }
            else
            {
                for (var i = 0; i < mesesAContabilizarOBeneficio; i++)
                {
                    var parcela = new Parcela
                    {
                        Mes = i + 1,
                        Valor = valorDaParcela
                    };

                    if (i + 1 < mesadmissao && funcionario.AnoAdmissao == 2016)
                        parcela.Valor = 0;

                    Adicionar(parcela);
                }
            }

            //if (funcionario.AnoAdmissao < 2014)
            //{
            //    this.Parcelas.Where(p => p.Mes == funcionario.DataAdmissao).FirstOrDefault().Valor = 0;
            //}

            Orcamento.AdicionarDespesa(this);
        }
    }
}
