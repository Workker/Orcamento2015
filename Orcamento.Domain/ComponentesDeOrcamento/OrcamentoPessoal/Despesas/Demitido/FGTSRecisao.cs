using System;
using System.Diagnostics.Contracts;
using System.Linq;
using Orcamento.Domain.Gerenciamento;

namespace Orcamento.Domain.ComponentesDeOrcamento.OrcamentoPessoal.Despesas.Demitido
{
    public sealed class FgtsRecisao : DespesaPessoal
    {
        private readonly int _mesDeDemissao;
        private double aumentoConvencao;
        private int mesAumentoConvencao;
        private double salario;

        public FgtsRecisao(int mesDeRecisao, Conta conta, NovoOrcamentoPessoal orcamento)
        {
            Contract.Requires(mesDeRecisao > default(int), "Mês de demissão não foi informado ");

            _mesDeDemissao = mesDeRecisao;
            Conta = conta;
            this.Orcamento = orcamento;
        }

        public override void Calcular(double salario, int mesAdmissao, double aumentoConvencao, int mesAumentoConvencao)
        {
            this.salario = salario;
            this.mesAumentoConvencao = mesAumentoConvencao;
            this.aumentoConvencao = aumentoConvencao;

            var ferias = ObterFerias(salario);
            var decimoTerceiro = ObterDecimoTerceiro(salario);

            var percentualDeFGTS = ObterPercentualDeFgts();

            double valorMensalFgts = ObterValorMensalDeFGTS(salario, percentualDeFGTS, decimoTerceiro, ferias);

            AdicionarParcelasADespesa(valorMensalFgts, mesAdmissao);

            Orcamento.AdicionarDespesa(this);
        }

        private void AdicionarParcelasADespesa(double valorMensalFgts,int mesAdmissao)
        {
            for (int i = 1; i < _mesDeDemissao; i++)
            {
                var parcela = new Parcela
                                  {
                                      Mes = i,
                                      Valor = valorMensalFgts
                     
                                  };

                if (i  == mesAumentoConvencao)
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

                if (i > mesAumentoConvencao)
                {
                    var salarioAumentoConvencao = CalcularAumentoConvencao(salario, aumentoConvencao);
                    var ferias = ObterFerias(salarioAumentoConvencao);
                    var decimoTerceiro = ObterDecimoTerceiro(salarioAumentoConvencao);

                    var percentualDeFGTS = ObterPercentualDeFgts();

                    parcela.Valor = ObterValorMensalDeFGTS(salarioAumentoConvencao, percentualDeFGTS, decimoTerceiro, ferias);
                }

                if (i < mesAdmissao && Funcionario.AnoAdmissao == 2015)
                    parcela.Valor = 0;

                VerificarMesFerias(parcela);

                Adicionar(parcela);
            }
        }

        private double ObterValorMensalDeFGTS(double salario, double percentualDeFGTS, double decimoTerceiro, double ferias)
        {
            return (ObterSalarioAcrescidoDoValorDosTicketsDaContaDaDespesa(salario) + salario + ferias + decimoTerceiro) * (percentualDeFGTS / 100);
        }

        private double ObterPercentualDeFgts()
        {
            double percentualDeFGTS;

            var ticketFGTS = Orcamento.Tickets.SingleOrDefault(x => x.Descricao == "FGTS");
            if (ticketFGTS != null)
                percentualDeFGTS = ticketFGTS.Valor;
            else
                throw new Exception("Ticket de FGTS não cadastrado");
            return percentualDeFGTS;
        }


    }
}
