using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using Orcamento.Domain.Gerenciamento;
using System;

namespace Orcamento.Domain.ComponentesDeOrcamento.OrcamentoPessoal.Despesas
{
    public class DespesaPessoal
    {
        public virtual Guid Guid { get; set; }
        public virtual IList<Parcela> Parcelas { get; set; }
        public virtual void Calcular(double salario, int mesDeAdmissao, double AumentoConvencao, int mesAumentoConvencao) { }
        public virtual Conta Conta { get; set; }
        //public virtual CentroDeCusto CentroDeCusto { get; set; }
        public virtual NovoOrcamentoPessoal Orcamento { get; set; }
        public virtual Funcionario Funcionario { get; set; }


        public DespesaPessoal()
        {
            Guid = Guid.NewGuid();
        }

        public virtual void Calcular(double salario, int MesAdmissao, bool aumentado, double salarioAumentado, int mesAumento, double AumentoConvencao, int mesAumentoConvencao) { }

        public virtual double ObterValorDeFeriasAplicadoAoMesDeAumento(double salarioAntigo, double salarioNovo)
        {
            var salarioDividido = (ObterSalarioAcrescidoDoValorDosTicketsDaContaDaDespesa(salarioNovo) + salarioNovo) / 12;
            var resultadoDiferencaSalarial = ((ObterSalarioAcrescidoDoValorDosTicketsDaContaDaDespesa(salarioNovo) + salarioNovo) - (ObterSalarioAcrescidoDoValorDosTicketsDaContaDaDespesa(salarioAntigo) + salarioAntigo));
            var resultadoSalarioAcrecidoDeAdicionalvezesUmPontoOito = salarioDividido + (resultadoDiferencaSalarial * 1.8);

            return resultadoSalarioAcrecidoDeAdicionalvezesUmPontoOito * 1.3333;
        }

        public virtual double ObterValorDoDecimoTerceiroAplicadoSomenteNoMesDeAumento(double valorDoDecimoTerceiroAplicadoAntesDoMesDeAumento, double valorDoDecimoTerceiroAplicadoAposOMesDeAumento, int mesDoAumento)
        {
            return valorDoDecimoTerceiroAplicadoAposOMesDeAumento + ((valorDoDecimoTerceiroAplicadoAposOMesDeAumento - valorDoDecimoTerceiroAplicadoAntesDoMesDeAumento) * mesDoAumento);
        }

        public virtual double CalcularAumentoConvencao(double salario, double aumentoConvencao)
        {
            var salarioAumentadoDeConvencao = salario + (salario * (aumentoConvencao / 100));
            return salarioAumentadoDeConvencao;
        }

        public virtual void AdicionarDeAcordoCom(int mesesAContabilizarOBeneficio, double valorDaParcela, int mesAdmissao)
        {
            Contract.Requires(mesesAContabilizarOBeneficio > default(int), "Total de meses a receber o benefício não foi informado");

            for (var i = 0; i < mesesAContabilizarOBeneficio; i++)
            {

                var parcela = new Parcela
                {
                    Mes = i + 1,
                    Valor = valorDaParcela
                };

                if (i + 1 < mesAdmissao && Funcionario.AnoAdmissao == 2014)
                    parcela.Valor = 0;


                Adicionar(parcela);
            }
        }

        public virtual void Adicionar(Parcela parcela)
        {
            Contract.Requires(parcela != null, "A parcela não foi informada");

            if (Parcelas == null)
                Parcelas = new List<Parcela>();

            Parcelas.Add(parcela);
        }

        public virtual double ObterFerias(double salario)
        {
            return ((ObterSalarioAcrescidoDoValorDosTicketsDaContaDaDespesa(salario) + salario) / 12) * 1.3333;
        }

        public virtual double ObterDecimoTerceiro(double salario)
        {
            return (ObterSalarioAcrescidoDoValorDosTicketsDaContaDaDespesa(salario) + salario) / 12;
        }

        protected virtual double ObterSalarioAcrescidoDoValorDosTicketsDaContaDaDespesa(double salario)
        {
            return Orcamento.Tickets.Where(
                       x => x.Descricao != "FGTS" && x.Descricao != "INSS" && x.Descricao != "Indenização" && this.Conta.TiposTickets.Any(t => t.Ticket == x.Ticket)).Sum(t => (t.Valor / 100) * salario);
        }


        public virtual double ObterPercentualDeFgts()
        {
            double percentualDeFGTS;

            var ticketFGTS = Orcamento.Tickets.SingleOrDefault(x => x.Descricao == "FGTS");
            if (ticketFGTS != null)
                percentualDeFGTS = ticketFGTS.Valor;
            else
                throw new Exception("Ticket de FGTS não cadastrado");
            return percentualDeFGTS;
        }

        public virtual double ObterPercentualDoINSS()
        {
            double percentualDoINSS;

            var ticketINSS = Orcamento.Tickets.SingleOrDefault(x => x.Descricao == "INSS");
            if (ticketINSS != null)
                percentualDoINSS = ticketINSS.Valor;
            else
                throw new Exception("Ticket de INSS não cadastrado");
            return percentualDoINSS;
        }

        public virtual void VerificarMesFerias(Parcela parcela)
        {
            if (Funcionario.AnoAdmissao < 2014 && parcela.Mes == Funcionario.DataAdmissao)
                parcela.Valor = 0;
        }

        public virtual double CalcularValorMensalDoFGTS(double salario, double percentualDeFGTS, double decimoTerceiro, double ferias)
        {
            return (ObterSalarioAcrescidoDoValorDosTicketsDaContaDaDespesa(salario) + salario + ferias + decimoTerceiro) * (percentualDeFGTS / 100);
        }

        public virtual double ObterValorMensalDeINSSS(double salario, double percentualDeINSS, double decimoTerceiro, double ferias)
        {
            return (ObterSalarioAcrescidoDoValorDosTicketsDaContaDaDespesa(salario) + salario + ferias + decimoTerceiro) * (percentualDeINSS / 100);
        }
    }
}
