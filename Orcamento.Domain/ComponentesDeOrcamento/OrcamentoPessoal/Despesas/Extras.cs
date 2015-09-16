using System.Linq;
using Orcamento.Domain.Gerenciamento;

namespace Orcamento.Domain.ComponentesDeOrcamento.OrcamentoPessoal.Despesas
{
    public class Extras : DespesaPessoal
    {
        private const int totalDeMesesParaAdicionarParcelas = 12;
        private readonly int _mesDeDemissao;
        private double aumentoConvencao;
        private int mesAumentoConvencao;
        private double salario;

        public Extras(Conta conta, NovoOrcamentoPessoal orcamento)
        {
            base.Conta = conta;
            this.Orcamento = orcamento;
        }

        public Extras(Conta conta, NovoOrcamentoPessoal orcamento, int mesDemissao)
        {

            base.Conta = conta;
            this.Orcamento = orcamento;
            _mesDeDemissao = mesDemissao;
        }

        public override void Calcular(double salario, int MesAdmissao, double aumentoConvencao, int mesAumentoConvencao)
        {
            this.salario = salario;
            this.mesAumentoConvencao = mesAumentoConvencao;
            this.aumentoConvencao = aumentoConvencao;

            double total = Orcamento.Tickets.Where(x => this.Conta.TiposTickets.Any(t => t.Ticket == x.Ticket)).Sum(ticketDeOrcamentoPessoal => (ticketDeOrcamentoPessoal.Valor / 100) * salario);

            if (_mesDeDemissao > 0)
                AdicionarDeAcordoCom(_mesDeDemissao - 1, total, MesAdmissao);
            else
                AdicionarDeAcordoCom(totalDeMesesParaAdicionarParcelas, total, MesAdmissao);

            Orcamento.AdicionarDespesa(this);
        }

        public override void AdicionarDeAcordoCom(int mesesAContabilizarOBeneficio, double valorDaParcela, int mesAdmissao)
        {
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
                    parcela.Valor = Orcamento.Tickets.Where(x => this.Conta.TiposTickets.Any(t => t.Ticket == x.Ticket)).Sum(ticketDeOrcamentoPessoal => (ticketDeOrcamentoPessoal.Valor / 100) * salarioAumentoConvencao);

                }

                if (i+ 1 < mesAdmissao && Funcionario.AnoAdmissao == 2016)
                    parcela.Valor = 0;

                VerificarMesFerias(parcela);


                Adicionar(parcela);
            }
        }


        public override void Calcular(double salario, int MesAdmissao, bool aumentado, double salarioAumentado, int mesAumento, double aumentoConvencao, int mesAumentoConvencao)
        {
            this.salario = salario;
            this.mesAumentoConvencao = mesAumentoConvencao;
            this.aumentoConvencao = aumentoConvencao;

            if (_mesDeDemissao > 0)
            {
                CalcularDespesas(salario, MesAdmissao, aumentado, salarioAumentado, mesAumento, _mesDeDemissao - 1);
            }
            else
                CalcularDespesas(salario, MesAdmissao, aumentado, salarioAumentado, mesAumento, totalDeMesesParaAdicionarParcelas);

            Orcamento.AdicionarDespesa(this);
        }

        private void CalcularDespesas(double salario, int MesAdmissao, bool aumentado, double salarioAumentado, int mesAumento, int mesesAcontabilizar)
        {
            for (var i = 0; i < mesesAcontabilizar; i++)
            {
                double total = 0;
                if (aumentado && mesAumento <= i + 1)
                {
                    total = Orcamento.Tickets.Where(x => this.Conta.TiposTickets.Any(t => t.Ticket == x.Ticket)).Sum(ticketDeOrcamentoPessoal => (ticketDeOrcamentoPessoal.Valor / 100) * salarioAumentado);
                    if (i + 1 >= mesAumentoConvencao)
                    {
                        total = Orcamento.Tickets.Where(x => this.Conta.TiposTickets.Any(t => t.Ticket == x.Ticket)).Sum(ticketDeOrcamentoPessoal => (ticketDeOrcamentoPessoal.Valor / 100) * CalcularAumentoConvencao(salarioAumentado, aumentoConvencao));
                    }
                }
                else
                {
                    total = Orcamento.Tickets.Where(x => this.Conta.TiposTickets.Any(t => t.Ticket == x.Ticket)).Sum(ticketDeOrcamentoPessoal => (ticketDeOrcamentoPessoal.Valor / 100) * salario);
                    if (i + 1 >= mesAumentoConvencao)
                    {
                        total = Orcamento.Tickets.Where(x => this.Conta.TiposTickets.Any(t => t.Ticket == x.Ticket)).Sum(ticketDeOrcamentoPessoal => (ticketDeOrcamentoPessoal.Valor / 100) * CalcularAumentoConvencao(salario, aumentoConvencao));
                    }
                }

                var parcela = new Parcela
                {
                    Mes = i + 1,
                    Valor = total
                };

                if (i + 1 < MesAdmissao && Funcionario.AnoAdmissao == 2016)
                    parcela.Valor = 0;

                VerificarMesFerias(parcela);

                Adicionar(parcela);
            }
        }

        
    }
}
