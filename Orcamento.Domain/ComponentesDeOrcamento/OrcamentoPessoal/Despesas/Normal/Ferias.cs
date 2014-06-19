using System;
using System.Diagnostics.Contracts;
using Orcamento.Domain.Gerenciamento;

namespace Orcamento.Domain.ComponentesDeOrcamento.OrcamentoPessoal.Despesas.Normal
{
    public class Ferias : DespesaPessoal
    {
        private double aumentoConvencao;
        private int mesAumentoConvencao;
        private double salario;

        public Ferias(Conta conta, NovoOrcamentoPessoal orcamento)
        {
            base.Conta = conta;
            this.Orcamento = orcamento;
        }

        public override void Calcular(double salario, int mesDeAdmissao, double aumentoConvencao, int mesAumentoConvencao)
        {
            this.salario = salario;
            this.mesAumentoConvencao = mesAumentoConvencao;
            this.aumentoConvencao = aumentoConvencao;

            Contract.Requires(salario > default(double), "O salário não foi informado.");

            var valorDaParcelaDeFerias = ObterFerias(salario);

            const int mesesAContabilizarOBeneficio = 12;

            AdicionarDeAcordoCom(mesesAContabilizarOBeneficio, valorDaParcelaDeFerias, mesDeAdmissao);

            Orcamento.AdicionarDespesa(this);
        }

        public override void AdicionarDeAcordoCom(int mesesAContabilizarOBeneficio, double valorDaParcela, int mesAdmissao)
        {
            Contract.Requires(mesesAContabilizarOBeneficio > default(int), "Total de meses a receber o benefício não foi informado");

            for (var i = 0; i < mesesAContabilizarOBeneficio; i++)
            {

                var parcela = new Parcela
                {
                    Mes = i + 1,
                    Valor = valorDaParcela
                };

                if (i + 1 == mesAumentoConvencao)
                {
                    var salarioAumentadoDeConvencao = CalcularAumentoConvencao(salario, aumentoConvencao);
                    parcela.Valor = ObterValorDeFeriasAplicadoAoMesDeAumento(salario, salarioAumentadoDeConvencao);
                }

                if (i + 1 > mesAumentoConvencao) 
                {
                    var salarioAumentoConvencao = CalcularAumentoConvencao(salario, aumentoConvencao);
                    var valorDaParcelaDeFerias = ((base.ObterSalarioAcrescidoDoValorDosTicketsDaContaDaDespesa(salarioAumentoConvencao) + salarioAumentoConvencao) / 12) * 1.3333;
                    parcela.Valor = valorDaParcelaDeFerias;
                }

                if (i + 1 < mesAdmissao && Funcionario.AnoAdmissao == 2014)
                    parcela.Valor = 0;


                Adicionar(parcela);
            }
        }
    }
}
