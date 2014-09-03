using System;
using Orcamento.Domain.Gerenciamento;

namespace Orcamento.Domain.ComponentesDeOrcamento.OrcamentoPessoal.Despesas.Aumentado
{
    [Serializable]
    public class FeriasComAumento : DespesaPessoal
    {
        private readonly double percentualDeAumento;
        private readonly int mesDoAumento;
        private double aumentoConvencao;
        private int mesAumentoConvencao;
        private double salario;
        private double salarioAumentado;

        public FeriasComAumento(double percentualDeAumento, int mesDoAumento, Conta conta, NovoOrcamentoPessoal orcamento)
        {
            this.percentualDeAumento = percentualDeAumento;
            this.mesDoAumento = mesDoAumento;
            base.Conta = conta;
            this.Orcamento = orcamento;
        }

        public override void Calcular(double salario, int mesAdmissao, double aumentoConvencao, int mesAumentoConvencao)
        {
            this.salario = salario;
            this.mesAumentoConvencao = mesAumentoConvencao;
            this.aumentoConvencao = aumentoConvencao;

            var valordeFeriasAplicadoAntesDoMesDeAumento = ObterValorMensalDeFerias(salario);

            salarioAumentado = ObterSalarioComReajuste(salario);

            var valorDeFeriasAplicadoAposOMesDeAumento = ObterValorMensalDeFerias(salarioAumentado);

            var valorDeFeriasAplicadoSomenteNoMesDeAumento = ObterValorDeFeriasAplicadoAoMesDeAumento(salario, salarioAumentado);

            AdicionarParcelasADespesa(valorDeFeriasAplicadoAposOMesDeAumento, valorDeFeriasAplicadoSomenteNoMesDeAumento, valordeFeriasAplicadoAntesDoMesDeAumento, mesAdmissao);

            Orcamento.AdicionarDespesa(this);
        }

        private void AdicionarParcelasADespesa(double valorDeFeriasAplicadoAposOMesDeAumento,
            double valorDeFeriasAplicadoSomenteNoMesDeAumento,
            double valordeFeriasAplicadoAntesDoMesDeAumento,
            int mesAdmissao)
        {
            for (var i = 1; i < 13; i++)
            {
                var parcela = new Parcela { Mes = i };

                if (i < mesDoAumento)
                {
                    parcela.Valor = valordeFeriasAplicadoAntesDoMesDeAumento;

                    if (i == mesAumentoConvencao)
                    {
                        var salarioAumentadoDeConvencao = CalcularAumentoConvencao(salario, aumentoConvencao);
                        parcela.Valor = ObterValorDeFeriasAplicadoAoMesDeAumento(salario, salarioAumentadoDeConvencao);
                    }

                    if (i > mesAumentoConvencao)
                    {
                        var salarioAumentadoDeConvencao = CalcularAumentoConvencao(salario, aumentoConvencao);
                        parcela.Valor = ObterValorMensalDeFerias(salarioAumentadoDeConvencao);
                    }
                }
                else if (i == mesDoAumento)
                {
                    parcela.Valor = valorDeFeriasAplicadoSomenteNoMesDeAumento;
                    if (i == mesAumentoConvencao)
                    {
                        var salarioAumentadoDeConvencao = CalcularAumentoConvencao(salarioAumentado, aumentoConvencao);
                        parcela.Valor = ObterValorDeFeriasAplicadoAoMesDeAumento(salario, salarioAumentadoDeConvencao);
                    }

                    if (i > mesAumentoConvencao)
                    {
                        var salarioAumentadoDeConvencao = CalcularAumentoConvencao(salarioAumentado, aumentoConvencao);
                        var salarioConvencao = CalcularAumentoConvencao(salario, aumentoConvencao);
                        parcela.Valor = ObterValorDeFeriasAplicadoAoMesDeAumento(salarioConvencao, salarioAumentadoDeConvencao);
                    }
                }
                else if (i > mesDoAumento)
                {
                    parcela.Valor = valorDeFeriasAplicadoAposOMesDeAumento;

                    if (i == mesAumentoConvencao)
                    {
                        var salarioAumentadoDeConvencao = CalcularAumentoConvencao(salarioAumentado, aumentoConvencao);
                        parcela.Valor = ObterValorDeFeriasAplicadoAoMesDeAumento(salarioAumentado, salarioAumentadoDeConvencao);
                    }

                    if (i > mesAumentoConvencao)
                    {
                        var salarioAumentadoDeConvencao = CalcularAumentoConvencao(salarioAumentado, aumentoConvencao);
                        parcela.Valor = ObterValorMensalDeFerias(salarioAumentadoDeConvencao);
                    }
                }

                if (i < mesAdmissao && Funcionario.AnoAdmissao == 2015)
                    parcela.Valor = 0;

                base.Adicionar(parcela);
            }
        }

        private double ObterSalarioComReajuste(double salario)
        {
            return salario + (salario * (percentualDeAumento / 100));
        }

        private double ObterValorMensalDeFerias(double salario)
        {
            return ((ObterSalarioAcrescidoDoValorDosTicketsDaContaDaDespesa(salario) + salario) / 12) * 1.3333;
        }
    }
}
