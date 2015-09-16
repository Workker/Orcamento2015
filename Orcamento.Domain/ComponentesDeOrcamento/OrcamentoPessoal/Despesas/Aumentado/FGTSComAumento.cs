using System;
using System.Diagnostics.Contracts;
using System.Linq;
using Orcamento.Domain.Gerenciamento;

namespace Orcamento.Domain.ComponentesDeOrcamento.OrcamentoPessoal.Despesas.Aumentado
{
    public class FGTSComAumento : DespesaPessoal
    {
        private readonly double percentualDeAumento;
        private readonly int mesDoAumento;
        private double aumentoConvencao;
        private int mesAumentoConvencao;
        private double salario;
        private double salarioAumentado;

        public FGTSComAumento(double percentualDeAumento, int mesDoAumento, Conta conta, NovoOrcamentoPessoal orcamento)
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

            var percentualDeFGTS = ObterPercentualDeFgts();

            var ferias = ObterFerias(salario);
            var decimoTerceiro = ObterDecimoTerceiro(salario);

            var valorDoFGTSaReceberPorMes = CalcularValorMensalDoFGTS(salario, percentualDeFGTS, decimoTerceiro, ferias);

            salarioAumentado = ObterSalarioComReajuste(salario);

            var feriasAumentadas = ObterFerias(salarioAumentado);
            var decimoTerceiroAumentado = ObterDecimoTerceiro(salarioAumentado);

            var valorDecimoterceiroNoMesDoAumento = ObterValorDoDecimoTerceiroAplicadoSomenteNoMesDeAumento(decimoTerceiro, decimoTerceiroAumentado, mesDoAumento - 1);

            var valorDeFeriasAumentadasNovas = ObterValorDeFeriasAplicadoAoMesDeAumento(salario, salarioAumentado);

            var valorDoFGTSaReceberPorMesComSalarioAumentado = CalcularValorMensalDoFGTS(salarioAumentado, percentualDeFGTS, decimoTerceiroAumentado, feriasAumentadas);
            var valorDoFGTSNoMesDoAumento = CalcularValorMensalDoFGTS(salarioAumentado, percentualDeFGTS, valorDecimoterceiroNoMesDoAumento, valorDeFeriasAumentadasNovas);

            AdicionarParcelasADespesa(valorDoFGTSaReceberPorMesComSalarioAumentado, valorDoFGTSaReceberPorMes, mesAdmissao, valorDoFGTSNoMesDoAumento, decimoTerceiro);

            Orcamento.AdicionarDespesa(this);
        }

        private void AdicionarParcelasADespesa(double valorDoFGTSaReceberPorMesComSalarioAumentado, double valorDoFGTSaReceberPorMes, int mesAdmissao, double FgtNoMesAumento, double decimoTerceiro)
        {
            for (var i = 1; i < 13; i++)
            {
                var parcela = new Parcela
                                  {
                                      Mes = i,
                                      Valor =
                                          i < mesDoAumento
                                              ? valorDoFGTSaReceberPorMes
                                              : valorDoFGTSaReceberPorMesComSalarioAumentado
                                  };


                if (i < mesDoAumento)
                {
                    parcela.Valor = valorDoFGTSaReceberPorMes;

                    if (i == mesAumentoConvencao)
                    {
                        var percentualDeFGTS = ObterPercentualDeFgts();

                        var salarioAumentadoDeConvencao = CalcularAumentoConvencao(salario, aumentoConvencao);

                        var decimoTerceiroAumentado = ObterDecimoTerceiro(salarioAumentadoDeConvencao);

                        var decimoTerceiroConvencao = ObterValorDoDecimoTerceiroAplicadoSomenteNoMesDeAumento(decimoTerceiro, decimoTerceiroAumentado, mesDoAumento - 1);

                        var valorDeFeriasAumentadasNovas = ObterValorDeFeriasAplicadoAoMesDeAumento(salario, salarioAumentadoDeConvencao);

                        var valorDoFGTSNoMesDoAumentoConvencao = CalcularValorMensalDoFGTS(salarioAumentadoDeConvencao, percentualDeFGTS, decimoTerceiroConvencao, valorDeFeriasAumentadasNovas);

                        parcela.Valor = valorDoFGTSNoMesDoAumentoConvencao;
                    }

                    if (i > mesAumentoConvencao)
                    {
                        var salarioAumentadoDeConvencao = CalcularAumentoConvencao(salario, aumentoConvencao);

                        var percentualDeFGTS = ObterPercentualDeFgts();

                        var ferias = ObterFerias(salarioAumentadoDeConvencao);
                        var decimoTerceiroAumentoConvencao = ObterDecimoTerceiro(salarioAumentadoDeConvencao);

                        var valorDoFGTSaReceberPorMesAumentoConvencao = CalcularValorMensalDoFGTS(salarioAumentadoDeConvencao, percentualDeFGTS, decimoTerceiroAumentoConvencao, ferias);
                        parcela.Valor = valorDoFGTSaReceberPorMesAumentoConvencao;
                    }
                }
                else if (i == mesDoAumento)
                {
                    parcela.Valor = FgtNoMesAumento;

                    if (i == mesAumentoConvencao)
                    {
                        var percentualDeFGTS = ObterPercentualDeFgts();

                        var salarioAumentadoDeConvencao = CalcularAumentoConvencao(salarioAumentado, aumentoConvencao);

                        var decimoTerceiroAumentado = ObterDecimoTerceiro(salarioAumentadoDeConvencao);

                        var decimoTerceiroConvencao = ObterValorDoDecimoTerceiroAplicadoSomenteNoMesDeAumento(decimoTerceiro, decimoTerceiroAumentado, mesDoAumento - 1);

                        var valorDeFeriasAumentadasNovas = ObterValorDeFeriasAplicadoAoMesDeAumento(salario, salarioAumentadoDeConvencao);

                        var valorDoFGTSNoMesDoAumentoConvencao = CalcularValorMensalDoFGTS(salarioAumentadoDeConvencao, percentualDeFGTS, decimoTerceiroConvencao, valorDeFeriasAumentadasNovas);

                        parcela.Valor = valorDoFGTSNoMesDoAumentoConvencao;
                    }

                    if (i > mesAumentoConvencao)
                    {
                        var percentualDeFGTS = ObterPercentualDeFgts();

                        var salarioAumentadoDeConvencao = CalcularAumentoConvencao(salarioAumentado, aumentoConvencao);
                        var salarioConvencao = CalcularAumentoConvencao(salario, aumentoConvencao);
                        var decimoTerceiroConvencao = ObterDecimoTerceiro(salarioConvencao);

                        var decimoTerceiroAumentado = ObterDecimoTerceiro(salarioAumentadoDeConvencao);

                        var decimoTerceiroConvencaoAumentado = ObterValorDoDecimoTerceiroAplicadoSomenteNoMesDeAumento(decimoTerceiroConvencao, decimoTerceiroAumentado, mesDoAumento - 1);

                        var valorDeFeriasAumentadasNovas = ObterValorDeFeriasAplicadoAoMesDeAumento(salarioConvencao, salarioAumentadoDeConvencao);

                        var valorDoFGTSNoMesDoAumentoConvencao = CalcularValorMensalDoFGTS(salarioAumentadoDeConvencao, percentualDeFGTS, decimoTerceiroConvencaoAumentado, valorDeFeriasAumentadasNovas);

                        parcela.Valor = valorDoFGTSNoMesDoAumentoConvencao;
                    }
                }
                else if (i > mesDoAumento)
                {
                    parcela.Valor = valorDoFGTSaReceberPorMesComSalarioAumentado;

                    if (i == mesAumentoConvencao)
                    {
                        var percentualDeFGTS = ObterPercentualDeFgts();

                        var salarioAumentadoDeConvencao = CalcularAumentoConvencao(salarioAumentado, aumentoConvencao);

                        var decimoTerceiroAumentado = ObterDecimoTerceiro(salarioAumentadoDeConvencao);
                        var decimoTerceiroAumentadoConvencao = ObterDecimoTerceiro(salarioAumentado);

                        var decimoTerceiroConvencao = ObterValorDoDecimoTerceiroAplicadoSomenteNoMesDeAumento(decimoTerceiroAumentadoConvencao, decimoTerceiroAumentado, mesDoAumento - 1);

                        var valorDeFeriasAumentadasNovas = ObterValorDeFeriasAplicadoAoMesDeAumento(salarioAumentado, salarioAumentadoDeConvencao);

                        var valorDoFGTSNoMesDoAumentoConvencao = CalcularValorMensalDoFGTS(salarioAumentadoDeConvencao, percentualDeFGTS, decimoTerceiroConvencao, valorDeFeriasAumentadasNovas);

                        parcela.Valor = valorDoFGTSNoMesDoAumentoConvencao;
                    }

                    if (i > mesAumentoConvencao)
                    {
                        var salarioAumentadoDeConvencao = CalcularAumentoConvencao(salarioAumentado, aumentoConvencao);
                        var percentualDeFGTS = ObterPercentualDeFgts();
                        var decimoTerceiroAumentado = ObterDecimoTerceiro(salarioAumentadoDeConvencao);
                        var feriasAumentadas = ObterFerias(salarioAumentadoDeConvencao);

                        var valorDoFGTSaReceberPorMesComSalarioAumentadoConvencao = CalcularValorMensalDoFGTS(salarioAumentadoDeConvencao, percentualDeFGTS, decimoTerceiroAumentado, feriasAumentadas);
                        parcela.Valor = valorDoFGTSaReceberPorMesComSalarioAumentadoConvencao;
                    }
                }

                if (i < mesAdmissao && Funcionario.AnoAdmissao == 2016)
                    parcela.Valor = 0;

                VerificarMesFerias(parcela);

                base.Adicionar(parcela);
            }
        }

        private double ObterSalarioComReajuste(double salario)
        {
            return salario + (salario * (percentualDeAumento / 100));
        }

        public double CalcularValorMensalDoFGTS(double salario, double percentualDeFGTS, double decimoTerceiro, double ferias)
        {
            return (ObterSalarioAcrescidoDoValorDosTicketsDaContaDaDespesa(salario) + salario + ferias + decimoTerceiro) * (percentualDeFGTS / 100);
        }
    }
}
