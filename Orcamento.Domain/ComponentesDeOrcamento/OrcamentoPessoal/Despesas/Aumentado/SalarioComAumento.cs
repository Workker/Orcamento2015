using System;
using Orcamento.Domain.Gerenciamento;

namespace Orcamento.Domain.ComponentesDeOrcamento.OrcamentoPessoal.Despesas.Aumentado
{
    public class SalarioComAumento : DespesaPessoal
    {
        private readonly double percentualDeAumento;
        private readonly int mesDoAumento;
        private double aumentoConvencao;
        private int mesAumentoConvencao;
        private double salario;

        public SalarioComAumento(double percentualDeAumento, int mesDoAumento, Conta conta, NovoOrcamentoPessoal orcamento)
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

            var salarioAumentado = ObterSalarioComReajuste(salario);

            AdicionarParcelasADespesa(salario, salarioAumentado,mesAdmissao);

            Orcamento.AdicionarDespesa(this);
        }

        private void AdicionarParcelasADespesa(double salario, double salarioAumentado,int mesAdmissao)
        {
            for (var i = 1; i < 13; i++)
            {
                var parcela = new Parcela {Mes = i, Valor = i < mesDoAumento ? salario : salarioAumentado};

                if (i < mesDoAumento && i >= mesAumentoConvencao) 
                {
                    var salarioAumentoConvencao = CalcularAumentoConvencao(salario, aumentoConvencao);
                    parcela.Valor = salarioAumentoConvencao;
                }

                if (i >= mesDoAumento && i >= mesAumentoConvencao)
                {
                    var salarioAumentoConvencao = CalcularAumentoConvencao(salarioAumentado, aumentoConvencao);
                    parcela.Valor = salarioAumentoConvencao;
                }

                if (i < mesAdmissao && Funcionario.AnoAdmissao == 2016)
                    parcela.Valor = 0;

                VerificarMesFerias(parcela);

                Adicionar(parcela);
            }
        }

        private double ObterSalarioComReajuste(double salario)
        {
            return salario + (salario*(percentualDeAumento/100));
        }
    }
}
