using System;
using Orcamento.Domain.Gerenciamento;

namespace Orcamento.Domain.ComponentesDeOrcamento.OrcamentoPessoal.Despesas.Demitido
{
    public class DecimoTerceiroRecisao : DespesaPessoal
    {
        private readonly int mesDeRecisao;
        private double salario;

        public DecimoTerceiroRecisao(int mesDeRecisao, Conta conta, NovoOrcamentoPessoal orcamento)
        {
            this.mesDeRecisao = mesDeRecisao;
            Conta = conta;
            this.Orcamento = orcamento;
        }

        public override void Calcular(double salario, int mesAdmissao, double AumentoConvencao, int mesAumentoConvencao)
        {
            this.salario = salario;

            var parcelaDoDecimoTerceiro = ObterValorDaParcelaDoDecimoTerceiro(salario);

            AdicionarParcelaADespesa(parcelaDoDecimoTerceiro, mesAdmissao,AumentoConvencao,mesAumentoConvencao);

            Orcamento.AdicionarDespesa(this);
        }

        private void AdicionarParcelaADespesa(double parcelaDoDecimoTerceiro, int mesAdmissao, double AumentoConvencao, int mesAumentoConvencao)
        {
            for (int i = 1; i < mesDeRecisao; i++)
            {
                var parcela = new Parcela
                                  {
                                      Mes = i,
                                      Valor = parcelaDoDecimoTerceiro
                                  };

                if (i == mesAumentoConvencao)
                {
                    var salarioAumentadoDeConvencao = salario + (salario * (AumentoConvencao / 100));
                    var aposAumento = ((base.ObterSalarioAcrescidoDoValorDosTicketsDaContaDaDespesa(salarioAumentadoDeConvencao) + salarioAumentadoDeConvencao) / 12);
                    parcela.Valor = ObterValorDoDecimoTerceiroAplicadoSomenteNoMesDeAumento(parcelaDoDecimoTerceiro, aposAumento, mesAumentoConvencao - 1);
                }

                if (i > mesAumentoConvencao)
                {
                    var salarioAumentadoDeConvencao = salario + (salario * (AumentoConvencao / 100));
                    parcela.Valor = ObterValorDaParcelaDoDecimoTerceiro(salarioAumentadoDeConvencao);
                }


                if (i < mesAdmissao && Funcionario.AnoAdmissao == 2016)
                    parcela.Valor = 0;

                Adicionar(parcela);
            }
        }

        private double ObterValorDaParcelaDoDecimoTerceiro(double salario)
        {
            return (base.ObterSalarioAcrescidoDoValorDosTicketsDaContaDaDespesa(salario) + salario) / 12;
        }
    }
}
