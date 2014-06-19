using System;
using Orcamento.Domain.Gerenciamento;

namespace Orcamento.Domain.ComponentesDeOrcamento.OrcamentoPessoal.Despesas.Aumentado
{
    [Serializable]
    public class DecimoTerceiroComAumento : DespesaPessoal
    {
        private readonly int mesDoAumento;
        private readonly double percentualDeAumento;
        private double salario;
        private double salarioAumentado;

        public DecimoTerceiroComAumento(double percentualDeAumento, int mesDoAumento, Conta conta, NovoOrcamentoPessoal orcamento)
        {
            this.percentualDeAumento = percentualDeAumento;
            this.mesDoAumento = mesDoAumento;
            base.Conta = conta;
            this.Orcamento = orcamento;
        }

        public override void Calcular(double salario, int mesDeAdmissao, double AumentoConvencao, int mesAumentoConvencao)
        {
            this.salario = salario;

            var valorDoDecimoTerceiroAplicadoAntesDoMesDeAumento = ObterValorMensalDoDecimoTerceiro(salario);

            this.salarioAumentado = ObterSalarioComReajuste(salario);

            var valorDoDecimoTerceiroAplicadoAposOMesDeAumento = ObterValorMensalDoDecimoTerceiro(salarioAumentado);

            var valorDoDecimoTerceiroAplicadoSomenteNoMesDeAumento = ObterValorDoDecimoTerceiroAplicadoSomenteNoMesDeAumento(valorDoDecimoTerceiroAplicadoAntesDoMesDeAumento, valorDoDecimoTerceiroAplicadoAposOMesDeAumento, mesDoAumento - 1);

            AdicionarParcelasADespesa(valorDoDecimoTerceiroAplicadoAposOMesDeAumento, valorDoDecimoTerceiroAplicadoSomenteNoMesDeAumento, valorDoDecimoTerceiroAplicadoAntesDoMesDeAumento, mesDeAdmissao, AumentoConvencao, mesAumentoConvencao);

            Orcamento.AdicionarDespesa(this);
        }

        private void AdicionarParcelasADespesa(double valorDoDecimoTerceiroAplicadoAposOMesDeAumento,
                                               double valorDoDecimoTerceiroAplicadoSomenteNoMesDeAumento,
                                               double valorDoDecimoTerceiroAplicadoAntesDoMesDeAumento, int mesDeAdmissao, double AumentoConvencao, int mesAumentoConvencao)
        {
            for (var i = 1; i < 13; i++)
            {
                var parcela = new Parcela { Mes = i };

                if (i < mesDoAumento)
                {
                    parcela.Valor = valorDoDecimoTerceiroAplicadoAntesDoMesDeAumento;

                    if (i == mesAumentoConvencao)
                    {
                        var salarioAumentadoDeConvencao = salario + (salario * (AumentoConvencao / 100));
                        var valorDoDecimoTerceiroAplicadoAposOMesDeAumentoConvencao = ObterValorMensalDoDecimoTerceiro(salarioAumentadoDeConvencao);
                        parcela.Valor = ObterValorDoDecimoTerceiroAplicadoSomenteNoMesDeAumento(valorDoDecimoTerceiroAplicadoAntesDoMesDeAumento, valorDoDecimoTerceiroAplicadoAposOMesDeAumentoConvencao, mesDoAumento - 1);
                    }

                    if (i > mesAumentoConvencao)
                    {
                        var salarioAumentadoDeConvencao = salario + (salario * (AumentoConvencao / 100));
                        parcela.Valor = ObterValorMensalDoDecimoTerceiro(salarioAumentadoDeConvencao);
                    }
                }
                else if (i == mesDoAumento)
                {
                    parcela.Valor = valorDoDecimoTerceiroAplicadoSomenteNoMesDeAumento;

                    if (i == mesAumentoConvencao)
                    {
                        var salarioAumentadoDeConvencao = salarioAumentado + (salarioAumentado * (AumentoConvencao / 100));
                        var valorDoDecimoTerceiroAplicadoAposOMesDeAumentoConvencao = ObterValorMensalDoDecimoTerceiro(salarioAumentadoDeConvencao);
                        parcela.Valor = ObterValorDoDecimoTerceiroAplicadoSomenteNoMesDeAumento(valorDoDecimoTerceiroAplicadoAntesDoMesDeAumento, valorDoDecimoTerceiroAplicadoAposOMesDeAumentoConvencao, mesDoAumento - 1);
                    }

                    if (i > mesAumentoConvencao)
                    {
                        var salarioAumentadoDeConvencao = salarioAumentado + (salarioAumentado * (AumentoConvencao / 100));
                        var salarioConvencao = CalcularAumentoConvencao(salario, AumentoConvencao);
                        var valorDoDecimoTerceiroAplicadoAposOMesDeAumentoConvencao = ObterValorMensalDoDecimoTerceiro(salarioAumentadoDeConvencao);
                        var valorDecimoTerceiroAntesConvencao = ObterValorMensalDoDecimoTerceiro(salarioConvencao);
                        parcela.Valor = ObterValorDoDecimoTerceiroAplicadoSomenteNoMesDeAumento(valorDecimoTerceiroAntesConvencao, valorDoDecimoTerceiroAplicadoAposOMesDeAumentoConvencao, mesDoAumento - 1);
                    }
                }
                else if (i > mesDoAumento)
                {
                    parcela.Valor = valorDoDecimoTerceiroAplicadoAposOMesDeAumento;

                    if (i == mesAumentoConvencao)
                    {
                        var salarioAumentadoDeConvencao = salarioAumentado + (salarioAumentado * (AumentoConvencao / 100));
                        var valorDoDecimoTerceiroAplicadoAposOMesDeAumentoConvencao = ObterValorMensalDoDecimoTerceiro(salarioAumentadoDeConvencao);
                        parcela.Valor = ObterValorDoDecimoTerceiroAplicadoSomenteNoMesDeAumento(valorDoDecimoTerceiroAplicadoAposOMesDeAumento, valorDoDecimoTerceiroAplicadoAposOMesDeAumentoConvencao, mesDoAumento - 1);
                    }

                    if (i > mesAumentoConvencao)
                    {
                        var salarioAumentadoDeConvencao = salarioAumentado + (salarioAumentado * (AumentoConvencao / 100));
                        var valorDoDecimoTerceiroAplicadoAposOMesDeAumentoConvencao = ObterValorMensalDoDecimoTerceiro(salarioAumentadoDeConvencao);
                        parcela.Valor = valorDoDecimoTerceiroAplicadoAposOMesDeAumentoConvencao;
                    }
                }

                if (i < mesDeAdmissao && Funcionario.AnoAdmissao == 2014)
                    parcela.Valor = 0;

                Adicionar(parcela);
            }
        }

        private double ObterValorDoDecimoTerceiroAplicadoSomenteNoMesDeAumento(double valorDoDecimoTerceiroAplicadoAntesDoMesDeAumento, double valorDoDecimoTerceiroAplicadoAposOMesDeAumento, int mesDoAumento)
        {
            return valorDoDecimoTerceiroAplicadoAposOMesDeAumento + ((valorDoDecimoTerceiroAplicadoAposOMesDeAumento - valorDoDecimoTerceiroAplicadoAntesDoMesDeAumento) * mesDoAumento);
        }

        private double ObterSalarioComReajuste(double salario)
        {
            return salario + (salario * (percentualDeAumento / 100));
        }

        private double ObterValorMensalDoDecimoTerceiro(double salario)
        {
            return ((base.ObterSalarioAcrescidoDoValorDosTicketsDaContaDaDespesa(salario) + salario) / 12);
        }
    }
}