using System;
using Orcamento.Domain.Gerenciamento;

namespace Orcamento.Domain.ComponentesDeOrcamento.OrcamentoPessoal.Despesas.Demitido
{
    public class SalarioRecisao : DespesaPessoal
    {
        private readonly int mesDeRecisao;
        private double aumentoConvencao;
        private int mesAumentoConvencao;
        private double salario;


        public SalarioRecisao(int mesDeRecisao, Conta conta, NovoOrcamentoPessoal orcamento)
        {
            this.mesDeRecisao = mesDeRecisao;
            base.Conta = conta;
            this.Orcamento = orcamento;
        }

        public override void Calcular(double salario, int mesAdmissao, double aumentoConvencao, int mesAumentoConvencao)
        {
            this.salario = salario;
            this.mesAumentoConvencao = mesAumentoConvencao;
            this.aumentoConvencao = aumentoConvencao;

            for (var i = 1; i < mesDeRecisao; i++)
            {
                var parcela = new Parcela
                                      {
                                          Mes = i,
                                          Valor = salario
                                      };

                if (i >= mesAumentoConvencao)
                {
                    var salarioAumentoConvencao = CalcularAumentoConvencao(salario, aumentoConvencao);
                    parcela.Valor = salarioAumentoConvencao;
                }

                if (i < mesAdmissao && Funcionario.AnoAdmissao == 2014)
                    parcela.Valor = 0;

                VerificarMesFerias(parcela);
                base.Adicionar(parcela);
            }

            Orcamento.AdicionarDespesa(this);
        }
    }
}
