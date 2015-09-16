using System;
using Orcamento.Domain.Gerenciamento;

namespace Orcamento.Domain.ComponentesDeOrcamento.OrcamentoPessoal.Despesas.Demitido
{
    public class FeriasRecisao : DespesaPessoal
    {
        private readonly int mesDeRecisao;

        private double aumentoConvencao;
        private int mesAumentoConvencao;
        private double salario;
       

        public FeriasRecisao(int mesDeRecisao, Conta conta, NovoOrcamentoPessoal orcamento)
        {
            this.mesDeRecisao = mesDeRecisao;
            base.Conta = conta;
            Orcamento = orcamento;
        }

        public override void Calcular(double salario, int mesAdmissao, double aumentoConvencao, int mesAumentoConvencao)
        {
            this.salario = salario;
            this.mesAumentoConvencao = mesAumentoConvencao;
            this.aumentoConvencao = aumentoConvencao;

            var valorMensalDeFerias = ObterValorMensalDeFerias(salario);
            var valorDaParcelaDeFerias = ((base.ObterSalarioAcrescidoDoValorDosTicketsDaContaDaDespesa(salario) + salario) / 12) * 1.3333;

            AdicionarParcelasADespesa(valorMensalDeFerias, mesAdmissao, valorDaParcelaDeFerias);

            Orcamento.AdicionarDespesa(this);
        }

        private double ObterValorMensalDeFerias(double salario)
        {
            return  ((ObterSalarioAcrescidoDoValorDosTicketsDaContaDaDespesa(salario) + salario) /mesDeRecisao)  *1.3333;
        }

        private void AdicionarParcelasADespesa(double valorMensalDeFerias,int mesAdmissao ,double valorParcelaAntesDaDemissao)
        {
            for (var i = 1; i < mesDeRecisao; i++)
            {
                var parcela = new Parcela
                                  {
                                      Mes = i,
                                      Valor = valorParcelaAntesDaDemissao
                                  };

                if (i  == mesAumentoConvencao)
                {
                    var salarioAumentadoDeConvencao = CalcularAumentoConvencao(salario, aumentoConvencao);
                    parcela.Valor = ObterValorDeFeriasAplicadoAoMesDeAumento(salario, salarioAumentadoDeConvencao);
                }

                if (i > mesAumentoConvencao) 
                {
                    var salarioAumentoConvencao = CalcularAumentoConvencao(salario, aumentoConvencao);
                    parcela.Valor = ((base.ObterSalarioAcrescidoDoValorDosTicketsDaContaDaDespesa(salarioAumentoConvencao) + salarioAumentoConvencao) / 12) * 1.3333;
                }
                if (i < mesAdmissao && Funcionario.AnoAdmissao == 2016)
                    parcela.Valor = 0;


                base.Adicionar(parcela);
            }
        }
    }
}
