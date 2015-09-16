using System;
using System.Linq;
using Orcamento.Domain.Gerenciamento;

namespace Orcamento.Domain.ComponentesDeOrcamento.OrcamentoPessoal.Despesas.Demitido
{
    public class INSSRecisao : DespesaPessoal
    {
        private readonly int _mesDeRecisao;
        private double aumentoConvencao;
        private int mesAumentoConvencao;
        private double salario;


        public INSSRecisao(int mesDeRecisao, Conta conta,NovoOrcamentoPessoal orcamento)
        {
            _mesDeRecisao = mesDeRecisao;
            base.Conta = conta;
            this.Orcamento = orcamento;
            
        }
        public override void Calcular(double salario, int mesAdmissao, double aumentoConvencao, int mesAumentoConvencao)
        {
            this.salario = salario;
            this.mesAumentoConvencao = mesAumentoConvencao;
            this.aumentoConvencao = aumentoConvencao;

            var ferias = ObterFerias(salario);
            var decimoTerceiro = ObterDecimoTerceiro(salario);

            var percentualDeINSS = ObterPercentualDeINSS();

            var valorTotalINSS = ObterValorTotalDoINSS(salario, percentualDeINSS, decimoTerceiro, ferias);

            AdicionarParcelasParaADespesa(valorTotalINSS,mesAdmissao);

            Orcamento.AdicionarDespesa(this);
        }

        private double ObterValorTotalDoINSS(double salario, double percentualDeINSS, double decimoTerceiro, double ferias)
        {
            return (base.ObterSalarioAcrescidoDoValorDosTicketsDaContaDaDespesa(salario) + salario + ferias + decimoTerceiro) * (percentualDeINSS / 100);
        }



        private void AdicionarParcelasParaADespesa(double valorTotalINSS, int mesAdmissao)
        {
            for (int i = 1; i < _mesDeRecisao; i++)
            {
                var parcela = new Parcela
                                  {
                                      Mes = i,
                                      Valor = valorTotalINSS
                                  };

                if (i == mesAumentoConvencao)
                {
                    var percentualDeINSS = ObterPercentualDoINSS();
                    var salarioAumentadoDeConvencao = CalcularAumentoConvencao(salario, aumentoConvencao);
                    var decimoTerceiroAumentado = ObterDecimoTerceiro(salarioAumentadoDeConvencao);
                    var decimoTerceiro = ObterDecimoTerceiro(salario);
                    var feriasAumentadas = ObterValorDeFeriasAplicadoAoMesDeAumento(salario, salarioAumentadoDeConvencao);

                    var valorDecimoterceiroNoMesDoAumento = ObterValorDoDecimoTerceiroAplicadoSomenteNoMesDeAumento(decimoTerceiro, decimoTerceiroAumentado, mesAumentoConvencao - 1);
                    var valorTotalDoINSSNoMesDoAumentoConvencao = ObterValorMensalDeINSSS(salarioAumentadoDeConvencao, percentualDeINSS, valorDecimoterceiroNoMesDoAumento, feriasAumentadas);
                    parcela.Valor = valorTotalDoINSSNoMesDoAumentoConvencao;
                }

                if (i > mesAumentoConvencao) 
                {
                    var salarioAumentoConvencao = CalcularAumentoConvencao(salario, aumentoConvencao);
                    var ferias = ObterFerias(salarioAumentoConvencao);
                    var decimoTerceiro = ObterDecimoTerceiro(salarioAumentoConvencao);

                    var percentualDeINSS = ObterPercentualDeINSS();

                    parcela.Valor = ObterValorTotalDoINSS(salarioAumentoConvencao, percentualDeINSS, decimoTerceiro, ferias);
                }

                if (i < mesAdmissao && Funcionario.AnoAdmissao == 2016)
                    parcela.Valor = 0;

                VerificarMesFerias(parcela);

                Adicionar(parcela);
            }
        }

        private double ObterPercentualDeINSS()
        {
            var valorDoINSS = default(double);

            var ticketINSS = Orcamento.Tickets.SingleOrDefault(x => x.Descricao == "INSS");
            if (ticketINSS != null)
                valorDoINSS = ticketINSS.Valor;
            else
                throw new Exception("Ticket de INSS não cadastrado");

            return valorDoINSS;
        }
    }
}
