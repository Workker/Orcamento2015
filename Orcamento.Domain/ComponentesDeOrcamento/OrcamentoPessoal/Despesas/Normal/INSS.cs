using System;
using System.Linq;
using Orcamento.Domain.Gerenciamento;

namespace Orcamento.Domain.ComponentesDeOrcamento.OrcamentoPessoal.Despesas.Normal
{
    public class INSS : DespesaPessoal
    {
        private const int quantidadeDeMesesAReceberOBeneficio = 12;
        private double aumentoConvencao;
        private int mesAumentoConvencao;
        private double salario;

        public INSS(Conta conta, NovoOrcamentoPessoal orcamento)
        {
            base.Conta = conta;
            Orcamento = orcamento;
        }

        public override void Calcular(double salario, int mesAdmissao, double aumentoConvencao, int mesAumentoConvencao)
        {
            this.salario = salario;
            this.mesAumentoConvencao = mesAumentoConvencao;
            this.aumentoConvencao = aumentoConvencao;

            var ferias = ObterFerias(salario);
            var decimoTerceiro = ObterDecimoTerceiro(salario);

            var percentualDoINSS = ObterPercentualDoINSS();

            var totalINSS = ObterValorMensalDoINSS(salario, percentualDoINSS, decimoTerceiro, ferias);
            AdicionarDeAcordoCom(quantidadeDeMesesAReceberOBeneficio, totalINSS, mesAdmissao);

            Orcamento.AdicionarDespesa(this);
        }

        public virtual void AdicionarDeAcordoCom(int mesesAContabilizarOBeneficio, double valorDaParcela, int mesAdmissao)
        {
            for (var i = 0; i < mesesAContabilizarOBeneficio; i++)
            {
                var parcela = new Parcela
                {
                    Mes = i + 1,
                    Valor = valorDaParcela
                };

                if (i + 1 == mesAumentoConvencao)
                {
                    var percentualDeINSS = ObterPercentualDoINSS();
                    var salarioAumentadoDeConvencao = CalcularAumentoConvencao(salario, aumentoConvencao);
                    var decimoTerceiroAumentado = ObterDecimoTerceiro(salarioAumentadoDeConvencao);
                    var decimoTerceiro = ObterDecimoTerceiro(salario);
                    var feriasAumentadas= ObterValorDeFeriasAplicadoAoMesDeAumento(salario, salarioAumentadoDeConvencao);

                    var valorDecimoterceiroNoMesDoAumento = ObterValorDoDecimoTerceiroAplicadoSomenteNoMesDeAumento(decimoTerceiro, decimoTerceiroAumentado, mesAumentoConvencao - 1);
                    var valorTotalDoINSSNoMesDoAumentoConvencao = ObterValorMensalDeINSSS(salarioAumentadoDeConvencao, percentualDeINSS, valorDecimoterceiroNoMesDoAumento, feriasAumentadas);
                    parcela.Valor = valorTotalDoINSSNoMesDoAumentoConvencao;
                }

                if (i + 1 > mesAumentoConvencao)
                {
                    var salarioAumentoConvencao = CalcularAumentoConvencao(salario, aumentoConvencao);
                    var ferias = ObterFerias(salarioAumentoConvencao);
                    var decimoTerceiro = ObterDecimoTerceiro(salarioAumentoConvencao);

                    var percentualDoINSS = ObterPercentualDoINSS();

                    parcela.Valor = ObterValorMensalDoINSS(salarioAumentoConvencao, percentualDoINSS, decimoTerceiro, ferias);
                }

                if (i + 1 < mesAdmissao && Funcionario.AnoAdmissao == 2015)
                    parcela.Valor = 0;

                VerificarMesFerias(parcela);

                Adicionar(parcela);
            }
        }

        private double ObterValorMensalDoINSS(double salario, double percentualDoINSS, double decimoTerceiro, double ferias)
        {
            return (base.ObterSalarioAcrescidoDoValorDosTicketsDaContaDaDespesa(salario) + salario + ferias + decimoTerceiro) * (percentualDoINSS / 100);
        }

       

    }
}
