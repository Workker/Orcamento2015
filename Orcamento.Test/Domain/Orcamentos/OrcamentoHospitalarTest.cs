using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Orcamento.Domain;
using Orcamento.Domain.Gerenciamento;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Orcamento.TestMethod.Domain.Gerenciamento
{

    [TestClass]
    public class OrcamentoHospitalarTestMethod
    {
        [TestMethod]
        public void CriarOrcamentoHospitalarComUmSetorComUmaContaDeveTerIncrementosIgualADoze()
        {
            Departamento departamento = new Hospital("Barra Dor");
            var CentroCirurgico = new SetorHospitalar("Cirurgia");
            var SubCentroCirurgico = new SubSetorHospital("Centro Cirúrgico");
            ContaHospital Cirurgia = new ContaHospital("Cirurgia", TipoValorContaEnum.Quantidade);

            CentroCirurgico.AdicionarSetor(SubCentroCirurgico);
            CentroCirurgico.AdicionarConta(Cirurgia);


            departamento.AdicionarSetor(CentroCirurgico);

            OrcamentoHospitalar orcamento = new OrcamentoHospitalar(departamento, 2014);

            Assert.IsTrue(orcamento.FatoresReceita.FirstOrDefault().Incrementos.Count == 12);
        }

        [TestMethod]
        public void CriarOrcamentoHospitalarComUmSetorComUmaContaDeveTeFatoresReceitaIgualAUm()
        {
            Departamento departamento = new Hospital("Barra Dor");
            var CentroCirurgico = new SetorHospitalar("Cirurgia");
            var SubCentroCirurgico = new SubSetorHospital("Centro Cirúrgico");
            ContaHospital Cirurgia = new ContaHospital("Cirurgia", TipoValorContaEnum.Quantidade);

            CentroCirurgico.AdicionarSetor(SubCentroCirurgico);
            CentroCirurgico.AdicionarConta(Cirurgia);


            departamento.AdicionarSetor(CentroCirurgico);

            OrcamentoHospitalar orcamento = new OrcamentoHospitalar(departamento, 2014);
            Assert.IsTrue(orcamento.FatoresReceita.Count == 1);
        }

        [TestMethod]
        public void CriarOrcamentoHospitalarComUmSetorComUmaContaDeveTerProducaoHospitalarIgualADoze()
        {
            Departamento departamento = new Hospital("Barra Dor");
            var CentroCirurgico = new SetorHospitalar("Cirurgia");
            var SubCentroCirurgico = new SubSetorHospital("Centro Cirúrgico");
            ContaHospital Cirurgia = new ContaHospital("Cirurgia", TipoValorContaEnum.Quantidade);

            CentroCirurgico.AdicionarSetor(SubCentroCirurgico);
            CentroCirurgico.AdicionarConta(Cirurgia);


            departamento.AdicionarSetor(CentroCirurgico);

            OrcamentoHospitalar orcamento = new OrcamentoHospitalar(departamento, 2014);

            Assert.IsTrue(orcamento.Servicos.FirstOrDefault().Valores.Count == 12);
        }

        [TestMethod]
        public void CriarOrcamentoHospitalarComUmSetorComUmaContaDeveTeServicosHospitalaresIgualAUm()
        {
            Departamento departamento = new Hospital("Barra Dor");
            var CentroCirurgico = new SetorHospitalar("Cirurgia");
            var SubCentroCirurgico = new SubSetorHospital("Centro Cirúrgico");
            ContaHospital Cirurgia = new ContaHospital("Cirurgia", TipoValorContaEnum.Quantidade);

            CentroCirurgico.AdicionarSetor(SubCentroCirurgico);
            CentroCirurgico.AdicionarConta(Cirurgia);


            departamento.AdicionarSetor(CentroCirurgico);

            OrcamentoHospitalar orcamento = new OrcamentoHospitalar(departamento, 2014);
            Assert.IsTrue(orcamento.Servicos.Count == 1);
        }

        [TestMethod]
        public void CalcularReceitaBrutaOrcamentoHospitalarCOmUmSetorComUmaContaEContaDeQuantidadeEvalorDaContaEmJaneiroIgualAUmEUnitarioIgualACemValorReceitaBrutaDeveSerIgualACem()
        {
            Departamento departamento = new Hospital("Barra Dor");
            var CentroCirurgico = new SetorHospitalar("Cirurgia");
            var SubCentroCirurgico = new SubSetorHospital("Centro Cirúrgico");
            ContaHospital Cirurgia = new ContaHospital("Cirurgia", TipoValorContaEnum.Quantidade);

            CentroCirurgico.AdicionarSetor(SubCentroCirurgico);
            CentroCirurgico.AdicionarConta(Cirurgia);


            departamento.AdicionarSetor(CentroCirurgico);

            OrcamentoHospitalar orcamento = new OrcamentoHospitalar(departamento, 2014);

            var producaoHospitalar = orcamento.Servicos.FirstOrDefault().Valores.Where(v => v.Mes == MesEnum.Janeiro).FirstOrDefault();
            producaoHospitalar.Valor = 1;
            var incremento = orcamento.FatoresReceita.FirstOrDefault().Incrementos.Where(i => i.Mes == MesEnum.Janeiro).FirstOrDefault();
          
            

            orcamento.CalcularReceitaBruta(Tickets(100,orcamento));

            Assert.IsTrue(incremento.ReceitaBruta == 100);
        }

        private List<TicketDeProducao> Tickets(float valor, Orcamento.Domain.Orcamento orcamento)
        {
            List<TicketDeProducao> tickets = new List<TicketDeProducao>();

            TicketDeProducao ticket = new TicketDeProducao(orcamento.FatoresReceita.FirstOrDefault().Setor, orcamento.FatoresReceita.FirstOrDefault().SubSetor, (Hospital)orcamento.Setor);
            ticket.Parcelas.FirstOrDefault().Valor = valor;

            tickets.Add(ticket);

            return tickets;
        }

        private List<TicketDeProducao> Tickets1(float valor, Orcamento.Domain.Orcamento orcamento)
        {
            List<TicketDeProducao> tickets = new List<TicketDeProducao>();

            TicketDeProducao ticket = new TicketDeProducao(orcamento.FatoresReceita.FirstOrDefault().Setor, orcamento.FatoresReceita.FirstOrDefault().SubSetor, (Hospital)orcamento.Setor);
            ticket.Parcelas[1].Valor = valor;

            tickets.Add(ticket);

            return tickets;
        }

        [TestMethod]
        public void CalcularReceitaBrutaOrcamentoHospitalarCOmUmSetorComUmaContaEContaDeQuantidadeEvalorDaContaEmJaneiroIgualADoisEUnitarioIgualAQuinhentosValorReceitaBrutaDeveSerIgualAMil()
        {
            Departamento departamento = new Hospital("Barra Dor");
            var CentroCirurgico = new SetorHospitalar("Cirurgia");
            var SubCentroCirurgico = new SubSetorHospital("Centro Cirúrgico");
            ContaHospital Cirurgia = new ContaHospital("Cirurgia", TipoValorContaEnum.Quantidade);

            CentroCirurgico.AdicionarSetor(SubCentroCirurgico);
            CentroCirurgico.AdicionarConta(Cirurgia);


            departamento.AdicionarSetor(CentroCirurgico);

            OrcamentoHospitalar orcamento = new OrcamentoHospitalar(departamento, 2014);

            var producaoHospitalar = orcamento.Servicos.FirstOrDefault().Valores.Where(v => v.Mes == MesEnum.Janeiro).FirstOrDefault();

            producaoHospitalar.Valor = 2;

            var incremento = orcamento.FatoresReceita.FirstOrDefault().Incrementos.Where(i => i.Mes == MesEnum.Janeiro).FirstOrDefault();
            incremento.Ticket = 500;

            orcamento.CalcularReceitaBruta(Tickets(500, orcamento));

            Assert.IsTrue(incremento.ReceitaBruta == 1000);
        }


        [TestMethod]
        public void CalcularReceitaLiquidaOrcamentoHospitalarCOmUmSetorComUmaContaEContaDeQuantidadeEvalorDaContaEmJaneiroIgualAUmEUnitarioIgualACemEValorComplexidadeIgualA10PorCentoValorReceitaLiquidaDeveSerIgualACentoEDez()
        {
            Departamento departamento = new Hospital("Barra Dor");
            var CentroCirurgico = new SetorHospitalar("Cirurgia");
            var SubCentroCirurgico = new SubSetorHospital("Centro Cirúrgico");
            ContaHospital Cirurgia = new ContaHospital("Cirurgia", TipoValorContaEnum.Quantidade);

            CentroCirurgico.AdicionarSetor(SubCentroCirurgico);
            CentroCirurgico.AdicionarConta(Cirurgia);


            departamento.AdicionarSetor(CentroCirurgico);

            OrcamentoHospitalar orcamento = new OrcamentoHospitalar(departamento, 2014);

            var producaoHospitalar = orcamento.Servicos.FirstOrDefault().Valores.Where(v => v.Mes == MesEnum.Janeiro).FirstOrDefault();

            producaoHospitalar.Valor = 1;

            var incremento = orcamento.FatoresReceita.FirstOrDefault().Incrementos.Where(i => i.Mes == MesEnum.Janeiro).FirstOrDefault();
            incremento.Ticket = 100;
            incremento.Complexidade = 10;

            orcamento.CalcularReceitaLiquida(Tickets(100, orcamento),null);

            Assert.IsTrue(incremento.ReceitaLiquida == 110);
        }

        [TestMethod]
        public void CalcularReceitaLiquidaOrcamentoHospitalarCOmUmSetorComDuasContasEContaDeQuantidadeEUmaDePorcentagemEvalorDaContaEmJaneiroIgualAUmEporcentagemIgualAcemEUnitarioIgualACemEValorComplexidadeIgualA10PorCentoValorReceitaLiuidaDeveSerIgualACentoEDez()
        {
            Departamento departamento = new Hospital("Barra Dor");
            var Uti = new SetorHospitalar("UTI");
            var SubUtiAdulto = new SubSetorHospital("UTIAdulto");
            ContaHospital leito = new ContaHospital("Leito", TipoValorContaEnum.Quantidade);
            ContaHospital ocupacao = new ContaHospital("Ocupação", TipoValorContaEnum.Porcentagem);
            leito.MultiPlicaPorMes = true;

            Uti.AdicionarSetor(SubUtiAdulto);
            Uti.AdicionarConta(leito);
            Uti.AdicionarConta(ocupacao);


            departamento.AdicionarSetor(Uti);

            OrcamentoHospitalar orcamento = new OrcamentoHospitalar(departamento, 2014);

            var leitoProducao = orcamento.Servicos.FirstOrDefault().Valores.Where(v => v.Mes == MesEnum.Janeiro).FirstOrDefault();

            var ocupacaoProducao = orcamento.Servicos[1].Valores.Where(v => v.Mes == MesEnum.Janeiro).FirstOrDefault();

            leitoProducao.Valor = 1;
            ocupacaoProducao.Valor = 100;

            var incremento = orcamento.FatoresReceita.FirstOrDefault().Incrementos.Where(i => i.Mes == MesEnum.Janeiro).FirstOrDefault();
            incremento.Complexidade = 10;

            orcamento.CalcularReceitaLiquida(Tickets(100, orcamento),null);

            Assert.IsTrue(incremento.ReceitaLiquida == 3410);
        }

        [TestMethod]
        public void CalcularReceitaLiquidaOrcamentoHospitalarCOmUmSetorComDuasContasEContaDeQuantidadeEUmaDePorcentagemEvalorDaContaEmJaneiroIgualAUmEporcentagemIgualACinquentaEUnitarioIgualACemEValorComplexidadeIgualA10PorCentoValorReceitaLiuidaDeveSerIgualACentoEDez()
        {
            Departamento departamento = new Hospital("Barra Dor");
            var Uti = new SetorHospitalar("UTI");
            var SubUtiAdulto = new SubSetorHospital("UTIAdulto");
            ContaHospital leito = new ContaHospital("Leito", TipoValorContaEnum.Quantidade);
            ContaHospital ocupacao = new ContaHospital("Ocupação", TipoValorContaEnum.Porcentagem);
            leito.MultiPlicaPorMes = true;

            Uti.AdicionarSetor(SubUtiAdulto);
            Uti.AdicionarConta(leito);
            Uti.AdicionarConta(ocupacao);


            departamento.AdicionarSetor(Uti);

            OrcamentoHospitalar orcamento = new OrcamentoHospitalar(departamento, 2014);

            var leitoProducao = orcamento.Servicos.FirstOrDefault().Valores.Where(v => v.Mes == MesEnum.Janeiro).FirstOrDefault();

            var ocupacaoProducao = orcamento.Servicos[1].Valores.Where(v => v.Mes == MesEnum.Janeiro).FirstOrDefault();

            leitoProducao.Valor = 1;
            ocupacaoProducao.Valor = 50;

            var incremento = orcamento.FatoresReceita.FirstOrDefault().Incrementos.Where(i => i.Mes == MesEnum.Janeiro).FirstOrDefault();
            incremento.Complexidade = 10;

            orcamento.CalcularReceitaLiquida(Tickets(100, orcamento),null);

            Assert.IsTrue(incremento.ReceitaLiquida == 1705);
        }

        [TestMethod]
        public void CalcularReceitaLiquidaOrcamentoHospitalarCOmUmSetorComDuasContasEContaDeQuantidadeEUmaDePorcentagemEvalorDaContaEmJaneiroIgualAUmEporcentagemIgualANoventaEUnitarioIgualACemEValorComplexidadeIgualA10PorCentoValorReceitaLiuidaDeveSerIgualDoisMilSeticentosEnoventa()
        {
            Departamento departamento = new Hospital("Barra Dor");
            var Uti = new SetorHospitalar("UTI");
            var SubUtiAdulto = new SubSetorHospital("UTIAdulto");
            ContaHospital leito = new ContaHospital("Leito", TipoValorContaEnum.Quantidade);
            ContaHospital ocupacao = new ContaHospital("Ocupação", TipoValorContaEnum.Porcentagem);
            leito.MultiPlicaPorMes = true;

            Uti.AdicionarSetor(SubUtiAdulto);
            Uti.AdicionarConta(leito);
            Uti.AdicionarConta(ocupacao);


            departamento.AdicionarSetor(Uti);

            OrcamentoHospitalar orcamento = new OrcamentoHospitalar(departamento, 2014);

            var leitoProducao = orcamento.Servicos.FirstOrDefault().Valores.Where(v => v.Mes == MesEnum.Janeiro).FirstOrDefault();

            var ocupacaoProducao = orcamento.Servicos[1].Valores.Where(v => v.Mes == MesEnum.Janeiro).FirstOrDefault();
            
            leitoProducao.Valor = 1;
            ocupacaoProducao.Valor = 90;

            var incremento = orcamento.FatoresReceita.FirstOrDefault().Incrementos.Where(i => i.Mes == MesEnum.Janeiro).FirstOrDefault();

            orcamento.CalcularReceitaLiquida(Tickets(100, orcamento),null);

            Assert.IsTrue(incremento.ReceitaLiquida == 2790);
        }

        [TestMethod]
        public void CalcularReceitaLiquidaOrcamentoHospitalarEmFevereiroComUmSetorComDuasContasEContaDeQuantidadeEUmaDePorcentagemEvalorDaContaEmJaneiroIgualAUmEporcentagemIgualANoventaEUnitarioIgualACemEValorComplexidadeIgualA10PorCentoValorReceitaLiuidaDeveSerIgualDoisMilSeticentosEnoventa()
        {
            Departamento departamento = new Hospital("Barra Dor");
            var Uti = new SetorHospitalar("UTI");
            var SubUtiAdulto = new SubSetorHospital("UTIAdulto");
            ContaHospital leito = new ContaHospital("Leito", TipoValorContaEnum.Quantidade);
            ContaHospital ocupacao = new ContaHospital("Ocupação", TipoValorContaEnum.Porcentagem);
            leito.MultiPlicaPorMes = true;

            Uti.AdicionarSetor(SubUtiAdulto);
            Uti.AdicionarConta(leito);
            Uti.AdicionarConta(ocupacao);


            departamento.AdicionarSetor(Uti);

            OrcamentoHospitalar orcamento = new OrcamentoHospitalar(departamento, 2014);

            var leitoProducao = orcamento.Servicos.FirstOrDefault().Valores.Where(v => v.Mes == MesEnum.Fevereiro).FirstOrDefault();

            var ocupacaoProducao = orcamento.Servicos[1].Valores.Where(v => v.Mes == MesEnum.Fevereiro).FirstOrDefault();

            leitoProducao.Valor = 1;
            ocupacaoProducao.Valor = 100;

            var incremento = orcamento.FatoresReceita.FirstOrDefault().Incrementos.Where(i => i.Mes == MesEnum.Janeiro).FirstOrDefault();
            incremento.Complexidade = 10;
            
            var fevereiro =  orcamento.FatoresReceita.FirstOrDefault().Incrementos.Where(i => i.Mes == MesEnum.Fevereiro).FirstOrDefault();
            orcamento.CalcularReceitaLiquida(Tickets1(100, orcamento),null);

            Assert.IsTrue(fevereiro.ReceitaLiquida == 3080);
        }

        [TestMethod]
        public void CalcularReceitaLiquidaOrcamentoHospitalarEmFevereiroComUmSetorComDuasContasEContaDeQuantidadeEUmaDePorcentagemEvalorDaContaEmJaneiroIgualAUmEporcentagemIgualANoventaEUnitarioIgualACemEValorComplexidadeIgualA10PorCentoNegativaValorReceitaLiuidaDeveSerIgualDoisMilSeticentosEnoventa()
        {
            Departamento departamento = new Hospital("Barra Dor");
            var Uti = new SetorHospitalar("UTI");
            var SubUtiAdulto = new SubSetorHospital("UTIAdulto");
            ContaHospital leito = new ContaHospital("Leito", TipoValorContaEnum.Quantidade);
            ContaHospital ocupacao = new ContaHospital("Ocupação", TipoValorContaEnum.Porcentagem);
            leito.MultiPlicaPorMes = true;

            Uti.AdicionarSetor(SubUtiAdulto);
            Uti.AdicionarConta(leito);
            Uti.AdicionarConta(ocupacao);


            departamento.AdicionarSetor(Uti);

            OrcamentoHospitalar orcamento = new OrcamentoHospitalar(departamento, 2014);

            var leitoProducao = orcamento.Servicos.FirstOrDefault().Valores.Where(v => v.Mes == MesEnum.Fevereiro).FirstOrDefault();

            var ocupacaoProducao = orcamento.Servicos[1].Valores.Where(v => v.Mes == MesEnum.Fevereiro).FirstOrDefault();

            leitoProducao.Valor = 1;
            ocupacaoProducao.Valor = 100;

            var incremento = orcamento.FatoresReceita.FirstOrDefault().Incrementos.Where(i => i.Mes == MesEnum.Janeiro).FirstOrDefault();
            incremento.Complexidade = 10;
            incremento.Negativo = true;

            var fevereiro = orcamento.FatoresReceita.FirstOrDefault().Incrementos.Where(i => i.Mes == MesEnum.Fevereiro).FirstOrDefault();
            orcamento.CalcularReceitaLiquida(Tickets1(100, orcamento),null);

            Assert.IsTrue(fevereiro.ReceitaLiquida == 2520);
        }

        [TestMethod]
        public void CalcularReceitaBrutaOrcamentoHospitalarCOmUmSetorComDuasContasEContaDeQuantidadeEUmaDePorcentagemEvalorDaContaEmJaneiroIgualAUmEporcentagemIgualACinquentaEUnitarioIgualACemEValorComplexidadeIgualA10PorCentoValorReceitaLiuidaDeveSerIgualACentoEDez()
        {
            Departamento departamento = new Hospital("Barra Dor");
            var Uti = new SetorHospitalar("UTI");
            var SubUtiAdulto = new SubSetorHospital("UTIAdulto");
            ContaHospital leito = new ContaHospital("Leito", TipoValorContaEnum.Quantidade);
            ContaHospital ocupacao = new ContaHospital("Ocupação", TipoValorContaEnum.Porcentagem);
            leito.MultiPlicaPorMes = true;

            Uti.AdicionarSetor(SubUtiAdulto);
            Uti.AdicionarConta(leito);
            Uti.AdicionarConta(ocupacao);


            departamento.AdicionarSetor(Uti);

            OrcamentoHospitalar orcamento = new OrcamentoHospitalar(departamento, 2014);

            var leitoProducao = orcamento.Servicos.FirstOrDefault().Valores.Where(v => v.Mes == MesEnum.Janeiro).FirstOrDefault();

            var ocupacaoProducao = orcamento.Servicos[1].Valores.Where(v => v.Mes == MesEnum.Janeiro).FirstOrDefault();

            leitoProducao.Valor = 1;
            ocupacaoProducao.Valor = 50;

            var incremento = orcamento.FatoresReceita.FirstOrDefault().Incrementos.Where(i => i.Mes == MesEnum.Janeiro).FirstOrDefault();
            incremento.Ticket = 100;

            orcamento.CalcularReceitaBruta(Tickets(100, orcamento));

            Assert.IsTrue(incremento.ReceitaBruta == 1550);
        }

        [TestMethod]
        public void CalcularReceitaBrutaOrcamentoHospitalarReceitaBrutaDeveSerIgualATrintaEUmMil()
        {
            Departamento departamento = new Hospital("Barra Dor");
            var Uti = new SetorHospitalar("UTI");
            var SubUtiAdulto = new SubSetorHospital("UTIAdulto");
            ContaHospital leito = new ContaHospital("Leito", TipoValorContaEnum.Quantidade);
            ContaHospital ocupacao = new ContaHospital("Ocupação", TipoValorContaEnum.Porcentagem);
            leito.MultiPlicaPorMes = true;

            Uti.AdicionarSetor(SubUtiAdulto);
            Uti.AdicionarConta(leito);
            Uti.AdicionarConta(ocupacao);


            departamento.AdicionarSetor(Uti);

            OrcamentoHospitalar orcamento = new OrcamentoHospitalar(departamento, 2014);

            var leitoProducao = orcamento.Servicos.FirstOrDefault().Valores.Where(v => v.Mes == MesEnum.Janeiro).FirstOrDefault();

            var ocupacaoProducao = orcamento.Servicos[1].Valores.Where(v => v.Mes == MesEnum.Janeiro).FirstOrDefault();

            leitoProducao.Valor = 10;
            ocupacaoProducao.Valor = 100;

            var incremento = orcamento.FatoresReceita.FirstOrDefault().Incrementos.Where(i => i.Mes == MesEnum.Janeiro).FirstOrDefault();
            incremento.Ticket = 100;

            orcamento.CalcularReceitaBruta(Tickets(100, orcamento));

            Assert.IsTrue(incremento.ReceitaBruta == 31000);
        }


        [TestMethod]
        public void CalcularReceitaBrutaOrcamentoHospitalarReceitaBrutaDeveSerIgualAQUinzeMilEQuinhentos()
        {
            Departamento departamento = new Hospital("Barra Dor");
            var Uti = new SetorHospitalar("UTI");
            var SubUtiAdulto = new SubSetorHospital("UTIAdulto");
            ContaHospital leito = new ContaHospital("Leito", TipoValorContaEnum.Quantidade);
            ContaHospital ocupacao = new ContaHospital("Ocupação", TipoValorContaEnum.Porcentagem);
            leito.MultiPlicaPorMes = true;

            Uti.AdicionarSetor(SubUtiAdulto);
            Uti.AdicionarConta(leito);
            Uti.AdicionarConta(ocupacao);


            departamento.AdicionarSetor(Uti);

            OrcamentoHospitalar orcamento = new OrcamentoHospitalar(departamento, 2014);

            var leitoProducao = orcamento.Servicos.FirstOrDefault().Valores.Where(v => v.Mes == MesEnum.Janeiro).FirstOrDefault();

            var ocupacaoProducao = orcamento.Servicos[1].Valores.Where(v => v.Mes == MesEnum.Janeiro).FirstOrDefault();

            leitoProducao.Valor = 10;
            ocupacaoProducao.Valor = 50;

            var incremento = orcamento.FatoresReceita.FirstOrDefault().Incrementos.Where(i => i.Mes == MesEnum.Janeiro).FirstOrDefault();
            incremento.Ticket = 100;

            orcamento.CalcularReceitaBruta(Tickets(100, orcamento));

            Assert.IsTrue(incremento.ReceitaBruta == 15500);
        }

        [TestMethod]
        public void CalcularReceitaLiquidaOrcamentoHospitalarCOmUmSetorComUmaContaEContaDeQuantidadeEvalorDaContaEmJaneiroIgualADoisEUnitarioIgualA500EValorComplexidadeIgualA10PorCentoValorReceitaLiquidaDeveSerIgualAMilECem()
        {
            Departamento departamento = new Hospital("Barra Dor");
            var CentroCirurgico = new SetorHospitalar("Cirurgia");
            var SubCentroCirurgico = new SubSetorHospital("Centro Cirúrgico");
            ContaHospital Cirurgia = new ContaHospital("Cirurgia", TipoValorContaEnum.Quantidade);

            CentroCirurgico.AdicionarSetor(SubCentroCirurgico);
            CentroCirurgico.AdicionarConta(Cirurgia);


            departamento.AdicionarSetor(CentroCirurgico);

            OrcamentoHospitalar orcamento = new OrcamentoHospitalar(departamento, 2014);

            var producaoHospitalar = orcamento.Servicos.FirstOrDefault().Valores.Where(v => v.Mes == MesEnum.Janeiro).FirstOrDefault();

            producaoHospitalar.Valor = 2;

            var incremento = orcamento.FatoresReceita.FirstOrDefault().Incrementos.Where(i => i.Mes == MesEnum.Janeiro).FirstOrDefault();
            incremento.Ticket = 500;
            incremento.Complexidade = 10;

            orcamento.CalcularReceitaLiquida(Tickets(500, orcamento),null);

            Assert.IsTrue(incremento.ReceitaLiquida == 1100);
        }
    }
}
