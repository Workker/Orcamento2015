using System;
using System.Diagnostics.Contracts;
using System.Linq;
using Orcamento.Domain.Gerenciamento;

namespace Orcamento.Domain.ComponentesDeOrcamento.OrcamentoPessoal.Despesas.Demitido
{
    public sealed class Recisao : DespesaPessoal
    {
        private readonly int _mesDeRecisao;
        private double aumentoConvencao;
        private int mesAumentoConvencao;
        private double salario;

        public Recisao(int mesDeRecisao, Conta conta, NovoOrcamentoPessoal orcamento)
        {
            _mesDeRecisao = mesDeRecisao;
            Conta = conta;
            Orcamento = orcamento;
        }

        public override void Calcular(double salario, int mesAdmissao, double aumentoConvencao, int mesAumentoConvencao)
        {
            this.salario = salario;
            this.mesAumentoConvencao = mesAumentoConvencao;
            this.aumentoConvencao = aumentoConvencao;

            var ticketDeOrcamentoPessoal = Orcamento.Tickets.SingleOrDefault(x => x.Descricao == "Indenização");


            AdicionarDeAcordoCom(12, 0, mesAdmissao);
            if (ticketDeOrcamentoPessoal != null)
            {
                double valorDaIndenizacao = 0;
                if (this.Parcelas.Where(p => p.Mes == _mesDeRecisao).FirstOrDefault().Mes >= mesAumentoConvencao)
                {
                    var salarioAumentoConvencao = CalcularAumentoConvencao(salario, aumentoConvencao);
                    valorDaIndenizacao = salarioAumentoConvencao * (ticketDeOrcamentoPessoal.Valor / 100);
                }
                else
                    valorDaIndenizacao = salario * (ticketDeOrcamentoPessoal.Valor / 100);

                this.Parcelas.Where(p => p.Mes == _mesDeRecisao).FirstOrDefault().Valor = valorDaIndenizacao;
            }
            else
                throw new Exception("Ticket de indenização não cadastrado");



            Orcamento.AdicionarDespesa(this);
        }
    }
}
