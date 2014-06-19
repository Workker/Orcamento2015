using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Orcamento.Domain.ComponentesDeOrcamento.OrcamentoDeProducao;
using System.Xml.Serialization;

namespace Orcamento.Domain
{
    [Serializable]
    public class IncrementoDaComplexidade : IAggregateRoot<int>
    {
        [NonSerialized()]
        private int id;


        public virtual int Id
        {
            get { return id; }
            set { id = value; }
        }

        public virtual MesEnum Mes { get; set; }
        public virtual double Complexidade { get; set; }
        public virtual bool Negativo { get; set; }
        public virtual double Ticket { get; set; }

        #region Receita bruta
        public virtual double ReceitaBruta { get; set; }
        public virtual double ReceitaLiquida { get; set; }
        #endregion

        public IncrementoDaComplexidade(MesEnum mes)
        {
            this.Mes = mes;
            this.Negativo = false;
        }

        protected IncrementoDaComplexidade() { }

        public virtual void CalcularReceitaBruta(List<ServicoHospitalar> servicos, List<TicketDeProducao> tickets)
        {
            double ValorServico = 1;
            bool multiplicaPorMes = false;


            foreach (var servico in servicos.Where(s => s.Conta.TipoValorContaEnum == TipoValorContaEnum.Quantidade && s.Conta.ContabilizaProducao).ToList())
            {
                var ticket = tickets.Where(t => t.Setor.Id == servico.Setor.Id && t.SubSetor.Id == servico.SubSetor.Id).FirstOrDefault();
                foreach (var valor in servico.Valores.Where(v => v.Mes == Mes).ToList())
                {
                    ValorServico *= valor.Valor;
                    this.Ticket = ticket.Parcelas.Where(t => t.Mes == valor.Mes).FirstOrDefault().Valor;
                }

                multiplicaPorMes = servico.Conta.MultiPlicaPorMes;
            }

            foreach (var servico in servicos.Where(s => s.Conta.TipoValorContaEnum == TipoValorContaEnum.Porcentagem && s.Conta.ContabilizaProducao).ToList())
            {
                foreach (var valor in servico.Valores.Where(v => v.Mes == Mes).ToList())
                {
                    if (valor.Valor != 0 && valor.Valor != 100)
                        ValorServico = CalcularPorcentagem(ValorServico, valor);
                }
            }



            ReceitaBruta = 0;
            ReceitaBruta = ValorServico * Ticket;

            if (multiplicaPorMes)
                ReceitaBruta = ReceitaBruta * DateTime.DaysInMonth(2014, (int)this.Mes);
        }

        private double CalcularPorcentagem(double ValorServico, ProducaoHospitalar valor)
        {
            var variavelMumtiplicada = (ValorServico * valor.Valor);
            var variavelDividida = (variavelMumtiplicada / 100);

            return variavelDividida;
        }


        public virtual void CalcularReceitaLiquida(List<ServicoHospitalar> servicos, List<TicketDeProducao> tickets, List<IncrementoDaComplexidade> incrementos, List<TicketParcela> ticketesDeReceita)
        {
            double ValorServico = 1;
            bool multiplicaPorMes = false;

            CalcularContaQuantidade(servicos, tickets, ref ValorServico, ref multiplicaPorMes);
            var servicosPorcentagem = servicos.Where(s => s.Conta.TipoValorContaEnum == TipoValorContaEnum.Porcentagem && s.Conta.ContabilizaProducao).ToList();

            ValorServico = CalcularContaPorcentagem(ValorServico, servicosPorcentagem);

            ReceitaLiquida = 0;

            if (ValorServico > 0)
            {
                var receitaBruta = (ValorServico * Ticket);

                if (Complexidade > 0 || incrementos.Sum(i => i.Complexidade) > 0 || ticketesDeReceita.Sum(p=> p.Valor) > 0)
                    CalcularComplexidade(incrementos, receitaBruta, ticketesDeReceita);
                else
                    ReceitaLiquida = receitaBruta;

                if (multiplicaPorMes)
                    IncrementarMesesNoAno();
            }
        }

        private void CalcularComplexidade(List<IncrementoDaComplexidade> incrementos, double receitaBruta, List<TicketParcela> ticketesDeReceita)
        {
            ReceitaLiquida = CalcularComplexidade(receitaBruta, incrementos,ticketesDeReceita);
        }

        private void IncrementarMesesNoAno()
        {
            ReceitaLiquida = ReceitaLiquida * DateTime.DaysInMonth(2014, (int)this.Mes);
        }

        private double CalcularContaPorcentagem(double ValorServico, List<ServicoHospitalar> servicosPorcentagem)
        {
            foreach (var servicoPorcentagem in servicosPorcentagem)
            {
                foreach (var valorPorcentagem in servicoPorcentagem.Valores.Where(v => v.Mes == Mes).ToList())
                {
                    if (valorPorcentagem.Valor != 0 && valorPorcentagem.Valor != 100)
                        ValorServico = CalcularPorcentagem(ValorServico, valorPorcentagem);

                    if (valorPorcentagem.Valor == 0)
                        ValorServico = 0;
                }
            }
            return ValorServico;
        }

        private void CalcularContaQuantidade(List<ServicoHospitalar> servicos, List<TicketDeProducao> tickets, ref double ValorServico, ref bool multiplicaPorMes)
        {
            foreach (var servico in servicos.Where(s => s.Conta.TipoValorContaEnum == TipoValorContaEnum.Quantidade && s.Conta.ContabilizaProducao).ToList())
            {
                var ticket = tickets.Where(t => t.Setor.Id == servico.Setor.Id && t.SubSetor.Id == servico.SubSetor.Id).FirstOrDefault();
                foreach (var valor in servico.Valores.Where(v => v.Mes == Mes).ToList())
                {
                    ValorServico *= (double)valor.Valor;

                    this.Ticket = ticket.Parcelas.Where(t => t.Mes == valor.Mes).FirstOrDefault().Valor;
                }

                multiplicaPorMes = servico.Conta.MultiPlicaPorMes;
            }
        }

        private double CalcularComplexidade(double receitaBruta, List<IncrementoDaComplexidade> incrementos, List<TicketParcela> ticketesDeReceita)
        {
            foreach (var incremento in incrementos.Where(i => i.Complexidade > 0))
            {
                if (incremento.Negativo)
                    receitaBruta -= (receitaBruta * (incremento.Complexidade) / 100);
                else
                    receitaBruta += (receitaBruta * (incremento.Complexidade) / 100);
            }

            foreach (var ticket in ticketesDeReceita.Where(i => i.Valor > 0))
            {
                if (ticket.Negativo)
                    receitaBruta -= (receitaBruta * (ticket.Valor) / 100);
                else
                    receitaBruta += (receitaBruta * (ticket.Valor) / 100);
            }

            if (Complexidade > 0)
            {
                if (this.Negativo)
                    receitaBruta -= (receitaBruta * (Complexidade) / 100);
                else
                    receitaBruta += (receitaBruta * (Complexidade) / 100);
            }

            return receitaBruta;
        }
    }
}

