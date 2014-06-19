using System.Diagnostics.Contracts;
using Orcamento.Domain.Gerenciamento;

namespace Orcamento.Domain.ComponentesDeOrcamento.OrcamentoPessoal.Despesas.Normal
{
    public class Salario : DespesaPessoal
    {
        private double aumentoConvencao;
        private int mesAumentoConvencao;
        private double salario;

        public Salario(Conta conta,NovoOrcamentoPessoal orcamento)
        {
            Conta = conta;
            this.Orcamento = orcamento;
        }

        public override void Calcular(double salario, int mesAdmissao, double aumentoConvencao, int mesAumentoConvencao)
        {
            this.salario = salario;
            this.mesAumentoConvencao = mesAumentoConvencao;
            this.aumentoConvencao = aumentoConvencao;


            AdicionarDeAcordoCom(12, salario, mesAdmissao);

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

                if (i + 1 >= mesAumentoConvencao) 
                {
                    var salarioAumentoConvencao = CalcularAumentoConvencao(salario, aumentoConvencao);
                    parcela.Valor = salarioAumentoConvencao;
                }

                if (i+ 1 < mesAdmissao && Funcionario.AnoAdmissao == 2014)
                    parcela.Valor = 0;

                VerificarMesFerias(parcela);

                Adicionar(parcela);
            }
        }
    }
}
