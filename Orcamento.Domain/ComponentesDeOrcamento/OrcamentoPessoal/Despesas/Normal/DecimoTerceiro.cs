using System.Diagnostics.Contracts;
using Orcamento.Domain.Gerenciamento;

namespace Orcamento.Domain.ComponentesDeOrcamento.OrcamentoPessoal.Despesas.Normal
{
    public class DecimoTerceiro : DespesaPessoal
    {
        public DecimoTerceiro(Conta conta, NovoOrcamentoPessoal orcamento)
        {
            base.Conta = conta;
            this.Orcamento = orcamento;
        }

        public override void Calcular(double salario, int mesAdmissao, double AumentoConvencao, int mesAumentoConvencao)
        {
            Contract.Requires(salario > default(double), "Salário não foi informado");

            var parcelaDecimoTerceiro = ((base.ObterSalarioAcrescidoDoValorDosTicketsDaContaDaDespesa(salario) + salario) / 12);

            for (var i = 1; i < 13; i++)
            {
                var parcela = new Parcela { Mes = i, Valor = parcelaDecimoTerceiro };

                if (i == mesAumentoConvencao)
                {
                    var salarioAumentadoDeConvencao = salario + (salario * (AumentoConvencao / 100));
                    var aposAumento = ((base.ObterSalarioAcrescidoDoValorDosTicketsDaContaDaDespesa(salarioAumentadoDeConvencao) + salarioAumentadoDeConvencao) / 12);
                    parcela.Valor = ObterValorDoDecimoTerceiroAplicadoSomenteNoMesDeAumento(parcelaDecimoTerceiro, aposAumento, mesAumentoConvencao - 1);
                }

                if (i > mesAumentoConvencao)
                {
                    var salarioAumentadoDeConvencao = salario + (salario * (AumentoConvencao / 100));
                    parcela.Valor = ((base.ObterSalarioAcrescidoDoValorDosTicketsDaContaDaDespesa(salarioAumentadoDeConvencao) + salarioAumentadoDeConvencao) / 12);
                }



                if (i < mesAdmissao && Funcionario.AnoAdmissao == 2014)
                    parcela.Valor = 0;

                base.Adicionar(parcela);
            }

            Orcamento.AdicionarDespesa(this);
        }

        private double ObterValorDoDecimoTerceiroAplicadoSomenteNoMesDeAumento(double valorDoDecimoTerceiroAplicadoAntesDoMesDeAumento, double valorDoDecimoTerceiroAplicadoAposOMesDeAumento, int mesDoAumento)
        {
            return valorDoDecimoTerceiroAplicadoAposOMesDeAumento + ((valorDoDecimoTerceiroAplicadoAposOMesDeAumento - valorDoDecimoTerceiroAplicadoAntesDoMesDeAumento) * mesDoAumento);
        }
    }
}
