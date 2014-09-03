using System;
using System.Linq;
using Orcamento.Domain.Gerenciamento;
using System.Diagnostics.Contracts;

namespace Orcamento.Domain.ComponentesDeOrcamento.OrcamentoPessoal.Despesas.Normal
{
    public class FGTS : DespesaPessoal
    {
        private const int totalDeMesesACalcularOBeneficio = 12;
        private double aumentoConvencao;
        private int mesAumentoConvencao;
        private double salario;

        public FGTS(Conta conta,NovoOrcamentoPessoal orcamento)
        {
            base.Conta = conta;
            this.Orcamento = orcamento;
        }

        public override void Calcular(double salario, int mesAdmissao, double aumentoConvencao, int mesAumentoConvencao)
        {
            this.salario = salario;
            this.mesAumentoConvencao = mesAumentoConvencao;
            this.aumentoConvencao = aumentoConvencao;

            var percentualDoFGTS = ObterPercentualDoFgts();

            var salarioAcrescidoPelosTickets = base.ObterSalarioAcrescidoDoValorDosTicketsDaContaDaDespesa(salario) + salario;

            var ferias = ObterFerias(salario);
            var decimoTerceiro = ObterDecimoTerceiro(salario);

            var valorDoFGTSaReceberPorMes = ObterValorMensalDoFGTS(decimoTerceiro, percentualDoFGTS, salarioAcrescidoPelosTickets, ferias);

            AdicionarDeAcordoCom(totalDeMesesACalcularOBeneficio,valorDoFGTSaReceberPorMes, mesAdmissao);

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

                if (i + 1 == mesAumentoConvencao)
                {
                    var percentualDeFGTS = ObterPercentualDeFgts();

                    var salarioAumentadoDeConvencao = CalcularAumentoConvencao(salario, aumentoConvencao);
                    
                    var decimoTerceiroNormal = ObterDecimoTerceiro(salario);

                    var decimoTerceiroAumentado = ObterDecimoTerceiro(salarioAumentadoDeConvencao);

                    var decimoTerceiroConvencaoAumentado = ObterValorDoDecimoTerceiroAplicadoSomenteNoMesDeAumento(decimoTerceiroNormal, decimoTerceiroAumentado, mesAumentoConvencao - 1);

                    var valorDeFeriasAumentadasConvencao = ObterValorDeFeriasAplicadoAoMesDeAumento(salario, salarioAumentadoDeConvencao);

                    var valorDoFGTSNoMesDoAumentoConvencao = CalcularValorMensalDoFGTS(salarioAumentadoDeConvencao, percentualDeFGTS, decimoTerceiroConvencaoAumentado, valorDeFeriasAumentadasConvencao);

                    parcela.Valor = valorDoFGTSNoMesDoAumentoConvencao;
                }

                if (i + 1 > mesAumentoConvencao)
                {
                    var percentualDoFGTS = ObterPercentualDoFgts();

                    var salarioAumentoConvencao = CalcularAumentoConvencao(salario, aumentoConvencao);
                    var salarioAcrescidoPelosTickets = base.ObterSalarioAcrescidoDoValorDosTicketsDaContaDaDespesa(salarioAumentoConvencao) + salarioAumentoConvencao;

                    var ferias = ObterFerias(salarioAumentoConvencao);
                    var decimoTerceiro = ObterDecimoTerceiro(salarioAumentoConvencao);

                    parcela.Valor = ObterValorMensalDoFGTS(decimoTerceiro, percentualDoFGTS, salarioAcrescidoPelosTickets, ferias);
                }

                if (i + 1< mesAdmissao && Funcionario.AnoAdmissao == 2015)
                    parcela.Valor = 0;

                VerificarMesFerias(parcela);

                Adicionar(parcela);
            }
        }

        private static double ObterValorMensalDoFGTS(double decimoTerceiro, double percentualDoFGTS, double salarioAcrescidoPelosTickets, double ferias)
        {
            return (salarioAcrescidoPelosTickets + ferias + decimoTerceiro) * (percentualDoFGTS/100);
        }

        private double ObterPercentualDoFgts()
        {
            double percentualDoFGTS;

            var ticketFGTS = Orcamento.Tickets.SingleOrDefault(x => x.Descricao == "FGTS");
            if (ticketFGTS != null)
                percentualDoFGTS = ticketFGTS.Valor;
            else
                throw new Exception("Ticket de FGTS não cadastrado");
            return percentualDoFGTS;
        }
    }
}
