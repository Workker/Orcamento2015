using System;
using System.Collections.Generic;
using System.Linq;

using Orcamento.Domain;
using Orcamento.Domain.ComponentesDeOrcamento.OrcamentoPessoal;
using Orcamento.Domain.Gerenciamento;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Orcamento.TestMethod.Domain.Orcamentos.OrcamentoPessoalTestMethod.TestMethodesDeFuncionario.Demissao
{
    [TestClass]
    public class TestMethodesDeFuncionarioDemitidoTestMethod
    {
        private Funcionario funcionarioDemitido;

       // [SetUp]
        public void Inicializar()
        {
            funcionarioDemitido = new Funcionario(new Setor("nome"))
            {
                Demitido = true,
                MesDeDemissao = 6,
                Salario = 100,
                DataAdmissao = 06
            };

            var centroDeCustoDeTI = new CentroDeCusto("TI");
            var orcamento =  new NovoOrcamentoPessoal(new Setor("Nome"),centroDeCustoDeTI,2014);
            
            orcamento.Adicionar(new TicketDeOrcamentoPessoal(new Setor("nome")) { Ticket= TipoTicketDePessoal.Indenizacao,  Descricao = "Indenização", Valor = 1 });
            orcamento.Adicionar(new TicketDeOrcamentoPessoal(new Setor("nome")) { Ticket = TipoTicketDePessoal.OutrasDespesas, Descricao = "TestMethod", Valor = 0 });
            orcamento.Adicionar(new TicketDeOrcamentoPessoal(new Setor("nome")) { Ticket = TipoTicketDePessoal.FGTS, Descricao = "TestMethod", Valor = 0 });
            orcamento.Adicionar(new TicketDeOrcamentoPessoal(new Setor("nome")) { Ticket = TipoTicketDePessoal.FGTS, Descricao = "FGTS", Valor = 8 });
            orcamento.Adicionar(new TicketDeOrcamentoPessoal(new Setor("nome")) { Ticket = TipoTicketDePessoal.FGTS, Descricao = "TestMethod", Valor = 0 });

            var contaIndenizacao = new Conta("Indenização", new TipoConta { Id = (int)TipoContaEnum.Indenizacao });
            contaIndenizacao.Adicionar(TipoTicketDePessoal.Indenizacao);

            var contaDecimoTerceiro = new Conta("Decimao Terceiro", new TipoConta { Id = (int)TipoContaEnum.DecimoTerceiro });
            contaDecimoTerceiro.Adicionar(TipoTicketDePessoal.OutrasDespesas);

            var feriasRecisao = new Conta("Ferias de Recisão", new TipoConta { Id = (int)TipoContaEnum.Ferias });
            feriasRecisao.Adicionar(TipoTicketDePessoal.FGTS);

            var FGTSRecisao = new Conta("FGTS de recisão", new TipoConta { Id = (int)TipoContaEnum.FGTS });
            FGTSRecisao.Adicionar(TipoTicketDePessoal.FGTS);

            var salarioDeRecisao = new Conta("Salario de recisao", new TipoConta { Id = (int)TipoContaEnum.Salario });
            salarioDeRecisao.Adicionar(TipoTicketDePessoal.FGTS);

            funcionarioDemitido.CalcularDespesa(centroDeCustoDeTI, contaIndenizacao, orcamento, 1, 0, 0);
            funcionarioDemitido.CalcularDespesa(centroDeCustoDeTI, contaDecimoTerceiro, orcamento, 1, 0, 0);
            funcionarioDemitido.CalcularDespesa(centroDeCustoDeTI, feriasRecisao, orcamento, 1, 0, 0);
            funcionarioDemitido.CalcularDespesa(centroDeCustoDeTI, FGTSRecisao, orcamento, 1, 0, 0);
            funcionarioDemitido.CalcularDespesa(centroDeCustoDeTI, salarioDeRecisao, orcamento, 1, 0, 0);
        }

        #region Indenização

        [TestMethod]
        [Ignore]
        public void recebimento_de_indenizacao()
        {
            var despesaIndenizacao =
                funcionarioDemitido.Despesas.Where(x => x.Conta.TipoConta.TipoContaEnum == TipoContaEnum.Indenizacao).SingleOrDefault();

            Assert.AreEqual(despesaIndenizacao.Parcelas.Count, 1);
        }

        [TestMethod]
        [Ignore]
        public void valor_de_indenizacao_foi_igual_a_100()
        {
            var despesaIndenizacao =
                funcionarioDemitido.Despesas.Where(x => x.Conta.TipoConta.TipoContaEnum == TipoContaEnum.Indenizacao).SingleOrDefault();

            Assert.AreEqual(despesaIndenizacao.Parcelas.First().Valor, 100d);
        }

        #endregion

        #region DecimoTerceiro

        [TestMethod]
        public void decimo_terceiro_de_recisao_recebido()
        {
            var despesaDecimoTerceiro =
                funcionarioDemitido.Despesas.Where(x => x.Conta.TipoConta.TipoContaEnum == TipoContaEnum.DecimoTerceiro).SingleOrDefault();

            Assert.AreEqual(despesaDecimoTerceiro.Parcelas.Count, 5);
        }

        [TestMethod]
        public void valor_do_decimo_terceiro_recebido_igual_a_8_ponto_33()
        {
            var despesaDecimoTerceiro =
                funcionarioDemitido.Despesas.Where(x => x.Conta.TipoConta.TipoContaEnum == TipoContaEnum.DecimoTerceiro).SingleOrDefault();

            Assert.AreEqual(Math.Round( despesaDecimoTerceiro.Parcelas.First().Valor,2), 8.33d);
        }

        #endregion

        #region Ferias relativas

        [TestMethod]
        public void valor_por_mes_das_ferias_ralativas_da_recisao_e_de_22_ponto_22()
        {
            var despesaFerias =
                funcionarioDemitido.Despesas.Where(x => x.Conta.TipoConta.TipoContaEnum == TipoContaEnum.Ferias).SingleOrDefault();

            Assert.AreEqual(Math.Round( despesaFerias.Parcelas.First().Valor,2), 11.11d);
        }

        [TestMethod]
        public void ferias_relativas_gerou_6_parcelas()
        {
            var despesaFerias =
                funcionarioDemitido.Despesas.Where(x => x.Conta.TipoConta.TipoContaEnum == TipoContaEnum.Ferias).SingleOrDefault();

            Assert.AreEqual(despesaFerias.Parcelas.Count, 5);
        }

        #endregion

        #region FGTS de Recisão

        [TestMethod]
        public void FGTS_de_recisao_recebido()
        {
            var despesaFGTS = funcionarioDemitido.Despesas.Where(x => x.Conta.TipoConta.TipoContaEnum == TipoContaEnum.FGTS).SingleOrDefault();

            Assert.AreEqual(despesaFGTS.Parcelas.Count, 5);
        }

        [TestMethod]
        public void valor_do_fgts_recebido_igual_a_100()
        {
            var despesaFGTS = funcionarioDemitido.Despesas.Where(x => x.Conta.TipoConta.TipoContaEnum == TipoContaEnum.FGTS).SingleOrDefault();

            Assert.AreEqual(Math.Round( despesaFGTS.Parcelas.First().Valor,2), 9.56d);
        }

        #endregion

        #region Salário antes da indenização

        [TestMethod]
        public void salario_de_recisao_recebido()
        {
            var despesaSalario = funcionarioDemitido.Despesas.Where(x => x.Conta.TipoConta.TipoContaEnum == TipoContaEnum.Salario).SingleOrDefault();

            Assert.AreEqual(despesaSalario.Parcelas.Count, 5);
        }

        [TestMethod]
        public void valor_do_salario_recebido_igual_a_100()
        {
            var despesaSalario = funcionarioDemitido.Despesas.Where(x => x.Conta.TipoConta.TipoContaEnum == TipoContaEnum.Salario).SingleOrDefault();

            Assert.AreEqual(despesaSalario.Parcelas.First().Valor, 100);
        }

        #endregion
    }
}
