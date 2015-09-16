using System;
using System.Linq;
using Orcamento.Domain.Gerenciamento;

namespace Orcamento.Domain.ComponentesDeOrcamento.OrcamentoPessoal.Despesas.Aumentado
{
    public class INSSComAumento : DespesaPessoal
    {
        private readonly double _percentualDeAumento;
        private readonly int _mesDoAumento;
        private double aumentoConvencao;
        private int mesAumentoConvencao;
        private double salario;
        private double salarioAumentado;


        public INSSComAumento(double percentualDeAumento, int mesDoAumento, Conta conta, NovoOrcamentoPessoal orcamento)
        {
            _percentualDeAumento = percentualDeAumento;
            _mesDoAumento = mesDoAumento;
            base.Conta = conta;
            this.Orcamento = orcamento;
        }

        public override void Calcular(double salario, int mesAdmissao, double aumentoConvencao, int mesAumentoConvencao)
        {
            this.salario = salario;
            this.mesAumentoConvencao = mesAumentoConvencao;
            this.aumentoConvencao = aumentoConvencao;

            var ferias = ObterFerias(salario);
            var decimoTerceiro = ObterDecimoTerceiro(salario);

            var percentualDeINSS = ObterPercentualDoINSS();

            var valorTotalDoINSS = ObterValorMensalDeINSSS(salario, percentualDeINSS, decimoTerceiro, ferias);

            salarioAumentado = ObterSalarioComReajuste(salario);

            var feriasAumentadas = ObterFerias(salarioAumentado);
            var decimoTerceiroAumentado = ObterDecimoTerceiro(salarioAumentado);
            var valorDecimoterceiroNoMesDoAumento = ObterValorDoDecimoTerceiroAplicadoSomenteNoMesDeAumento(decimoTerceiro, decimoTerceiroAumentado, _mesDoAumento - 1);

            var feriasAumentadasNovo = ObterValorDeFeriasAplicadoAoMesDeAumento(salario, salarioAumentado);
            var valorTotalDoINSSAumentado = ObterValorMensalDeINSSS(salarioAumentado, percentualDeINSS, decimoTerceiroAumentado, feriasAumentadas);
            var valorTotalDoINSSNoMesDoAumento = ObterValorMensalDeINSSS(salarioAumentado, percentualDeINSS, valorDecimoterceiroNoMesDoAumento, feriasAumentadasNovo);

            AdicionarParcelasADespesa(valorTotalDoINSSAumentado, valorTotalDoINSS, mesAdmissao, valorTotalDoINSSNoMesDoAumento);

            Orcamento.AdicionarDespesa(this);
        }

        private void AdicionarParcelasADespesa(double valorTotalDoINSSAumentado, double valorTotalDoINSS, int mesAdmissao, double InSsNoMesDoAumento)
        {
            for (int i = 1; i < 13; i++)
            {
                var parcela = new Parcela
                                  {
                                      Mes = i,
                                      Valor =
                                          i < _mesDoAumento ? valorTotalDoINSS : valorTotalDoINSSAumentado
                                  };



                if (i < _mesDoAumento)
                {
                    parcela.Valor = valorTotalDoINSS;

                    if (i == mesAumentoConvencao)
                    {
                        var percentualDeINSS = ObterPercentualDoINSS();
                        var salarioAumentadoDeConvencao = CalcularAumentoConvencao(salario, aumentoConvencao);
                        var decimoTerceiroAumentado = ObterDecimoTerceiro(salarioAumentadoDeConvencao);
                        var decimoTerceiro = ObterDecimoTerceiro(salario);
                        var feriasAumentadasNovo = ObterValorDeFeriasAplicadoAoMesDeAumento(salario, salarioAumentadoDeConvencao);

                        var valorDecimoterceiroNoMesDoAumento = ObterValorDoDecimoTerceiroAplicadoSomenteNoMesDeAumento(decimoTerceiro, decimoTerceiroAumentado, _mesDoAumento - 1);
                        var valorTotalDoINSSNoMesDoAumentoConvencao = ObterValorMensalDeINSSS(salarioAumentadoDeConvencao, percentualDeINSS, valorDecimoterceiroNoMesDoAumento, feriasAumentadasNovo);
                        parcela.Valor = valorTotalDoINSSNoMesDoAumentoConvencao;
                    }

                    if (i > mesAumentoConvencao)
                    {
                        var salarioAumentadoDeConvencao = CalcularAumentoConvencao(salario, aumentoConvencao);

                        var ferias = ObterFerias(salarioAumentadoDeConvencao);
                        var decimoTerceiro = ObterDecimoTerceiro(salarioAumentadoDeConvencao);
                        var percentualDeINSS = ObterPercentualDoINSS();
                        var valorTotalDoINSSConvencao = ObterValorMensalDeINSSS(salarioAumentadoDeConvencao, percentualDeINSS, decimoTerceiro, ferias);
                        parcela.Valor = valorTotalDoINSSConvencao;
                    }
                }
                else if (i == _mesDoAumento)
                {
                    parcela.Valor = InSsNoMesDoAumento;

                    if (i == mesAumentoConvencao)
                    {
                        var percentualDeINSS = ObterPercentualDoINSS();
                        var salarioAumentadoDeConvencao = CalcularAumentoConvencao(salarioAumentado, aumentoConvencao);
                        var decimoTerceiroAumentado = ObterDecimoTerceiro(salarioAumentadoDeConvencao);
                        var decimoTerceiro = ObterDecimoTerceiro(salario);
                        var feriasAumentadasNovo = ObterValorDeFeriasAplicadoAoMesDeAumento(salario, salarioAumentadoDeConvencao);

                        var valorDecimoterceiroNoMesDoAumento = ObterValorDoDecimoTerceiroAplicadoSomenteNoMesDeAumento(decimoTerceiro, decimoTerceiroAumentado, _mesDoAumento - 1);
                        var valorTotalDoINSSNoMesDoAumentoConvencao = ObterValorMensalDeINSSS(salarioAumentadoDeConvencao, percentualDeINSS, valorDecimoterceiroNoMesDoAumento, feriasAumentadasNovo);
                        parcela.Valor = valorTotalDoINSSNoMesDoAumentoConvencao;
                    }

                    if (i > mesAumentoConvencao)
                    {
                        var percentualDeINSS = ObterPercentualDoINSS();
                        var salarioAumentadoDeConvencao = CalcularAumentoConvencao(salarioAumentado, aumentoConvencao);
                        var salarioConvencao = CalcularAumentoConvencao(salario, aumentoConvencao);
                        var decimoTerceiroAumentado = ObterDecimoTerceiro(salarioAumentadoDeConvencao);
                        var decimoTerceiro = ObterDecimoTerceiro(salarioConvencao);
                        var feriasAumentadasNovo = ObterValorDeFeriasAplicadoAoMesDeAumento(salarioConvencao, salarioAumentadoDeConvencao);

                        var valorDecimoterceiroNoMesDoAumento = ObterValorDoDecimoTerceiroAplicadoSomenteNoMesDeAumento(decimoTerceiro, decimoTerceiroAumentado, _mesDoAumento - 1);
                        var valorTotalDoINSSNoMesDoAumentoConvencao = ObterValorMensalDeINSSS(salarioAumentadoDeConvencao, percentualDeINSS, valorDecimoterceiroNoMesDoAumento, feriasAumentadasNovo);
                        parcela.Valor = valorTotalDoINSSNoMesDoAumentoConvencao;
                    }
                }
                else if (i > _mesDoAumento)
                {
                    parcela.Valor = valorTotalDoINSSAumentado;


                    if (i == mesAumentoConvencao)
                    {
                        var percentualDeINSS = ObterPercentualDoINSS();
                        var salarioAumentadoDeConvencao = CalcularAumentoConvencao(salarioAumentado, aumentoConvencao);
                        var decimoTerceiroAumentado = ObterDecimoTerceiro(salarioAumentadoDeConvencao);
                        var decimoTerceiro = ObterDecimoTerceiro(salarioAumentado);
                        var feriasAumentadasNovo = ObterValorDeFeriasAplicadoAoMesDeAumento(salarioAumentado, salarioAumentadoDeConvencao);

                        var valorDecimoterceiroNoMesDoAumento = ObterValorDoDecimoTerceiroAplicadoSomenteNoMesDeAumento(decimoTerceiro, decimoTerceiroAumentado, _mesDoAumento - 1);
                        var valorTotalDoINSSNoMesDoAumentoConvencao = ObterValorMensalDeINSSS(salarioAumentadoDeConvencao, percentualDeINSS, valorDecimoterceiroNoMesDoAumento, feriasAumentadasNovo);
                        parcela.Valor = valorTotalDoINSSNoMesDoAumentoConvencao;
                    }

                    if (i > mesAumentoConvencao)
                    {
                        var percentualDeINSS = ObterPercentualDoINSS();
                        var salarioAumentadoDeConvencao = CalcularAumentoConvencao(salarioAumentado, aumentoConvencao);
                        var decimoTerceiroAumentado = ObterDecimoTerceiro(salarioAumentadoDeConvencao);
                        var feriasAumentadas = ObterFerias(salarioAumentadoDeConvencao);

                        var valorTotalDoINSSAumentadoConvencao = ObterValorMensalDeINSSS(salarioAumentadoDeConvencao, percentualDeINSS, decimoTerceiroAumentado, feriasAumentadas);
                        parcela.Valor = valorTotalDoINSSAumentadoConvencao;
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
            return salario + (salario * (_percentualDeAumento / 100));
        }
    }
}
