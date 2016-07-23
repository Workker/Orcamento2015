using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Orcamento.Domain.ComponentesDeOrcamento.OrcamentoDeViagem;
using Orcamento.Domain.Gerenciamento;
using Orcamento.Domain.Servico;
using Orcamento.Domain;
using Orcamento.Domain.DB.Repositorio;
using Rhino.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Orcamento.TestMethod.Servico
{
    [TestClass]
    public class ServicoOrcamentoDeViagensTestMethod
    {
        [TestMethod]
        public void CriarOrcamentoComSucessoDeveTerOrcamentoNomeOrcamentoDeveSerVersaoUm()
        {
            ServicoOrcamentoDeViagens servico = new ServicoOrcamentoDeViagens();

            IOrcamentos orcamentos = MockRepository.GenerateMock<IOrcamentos>();
            orcamentos.Expect(o => o.Salvar(null)).IgnoreArguments();

            var diarias = MockRepository.GenerateMock<IDiarias>();
            diarias.Expect(c => c.Todos()).Return(new List<Diaria>());

            var viagens = MockRepository.GenerateMock<IViagens>();
            viagens.Expect(c => c.Todos()).Return(new List<Viagem>());

            servico.Orcamentos = orcamentos;
            servico.Diarias = diarias;
            servico.Viagens = viagens;

            Departamento departamento = new Setor("Barra dor");
            CentroDeCusto centroDeCusto = new CentroDeCusto("centroDeCusto");
            departamento.AdicionarCentroDeCusto(centroDeCusto);

            var orcamento = servico.CriarOrcamentoDeViagem(new List<Orcamento.Domain.Orcamento>(), departamento, centroDeCusto, 2014);

            Assert.AreEqual(orcamento.NomeOrcamento, "Versão1");
        }

        [TestMethod]
        public void CriarOrcamentoComSucessoComUmORcamentoJaCriadoDeveTeNomeOrcamentoIgualAVersaoDois()
        {
            ServicoOrcamentoDeViagens servico = new ServicoOrcamentoDeViagens();

            IOrcamentos orcamentos = MockRepository.GenerateMock<IOrcamentos>();
            orcamentos.Expect(o => o.Salvar(null)).IgnoreArguments();

            var diarias = MockRepository.GenerateMock<IDiarias>();
            diarias.Expect(c => c.Todos()).Return(new List<Diaria>());

            var viagens = MockRepository.GenerateMock<IViagens>();
            viagens.Expect(c => c.Todos()).Return(new List<Viagem>());

            servico.Orcamentos = orcamentos;
            servico.Diarias = diarias;
            servico.Viagens = viagens;

            Departamento departamento = new Setor("Barra dor");
            CentroDeCusto centroDeCusto = new CentroDeCusto("centroDeCusto");
            departamento.AdicionarCentroDeCusto(centroDeCusto);

            var orcamentosLista = new List<Orcamento.Domain.Orcamento>();
            orcamentosLista.Add(new OrcamentoDeViagem(departamento, centroDeCusto, 2014));
            var orcamento = servico.CriarOrcamentoDeViagem(orcamentosLista, departamento, centroDeCusto, 2014);

            Assert.AreEqual(orcamento.NomeOrcamento, "Versão2");
        }

        [TestMethod]
        public void CriarOrcamentoComSucessoComDoisORcamentoJaCriadoDeveTeNomeOrcamentoIgualAVersaoTres()
        {
            ServicoOrcamentoDeViagens servico = new ServicoOrcamentoDeViagens();

            IOrcamentos orcamentos = MockRepository.GenerateMock<IOrcamentos>();
            orcamentos.Expect(o => o.Salvar(null)).IgnoreArguments();

            var diarias = MockRepository.GenerateMock<IDiarias>();
            diarias.Expect(c => c.Todos()).Return(new List<Diaria>());

            var viagens = MockRepository.GenerateMock<IViagens>();
            viagens.Expect(c => c.Todos()).Return(new List<Viagem>());

            servico.Orcamentos = orcamentos;
            servico.Diarias = diarias;
            servico.Viagens = viagens;

            Departamento departamento = new Setor("Barra dor");
            CentroDeCusto centroDeCusto = new CentroDeCusto("centroDeCusto");
            departamento.AdicionarCentroDeCusto(centroDeCusto);

            var orcamentosLista = new List<Orcamento.Domain.Orcamento>();
            orcamentosLista.Add(new OrcamentoDeViagem(departamento, centroDeCusto, 2014));
            orcamentosLista.Add(new OrcamentoDeViagem(departamento, centroDeCusto, 2014));
            var orcamento = servico.CriarOrcamentoDeViagem(orcamentosLista, departamento, centroDeCusto, 2014);

            Assert.AreEqual(orcamento.NomeOrcamento, "Versão3");
        }

        [TestMethod]
        public void CriarOrcamentoComSucessoComTresORcamentoJaCriadoDeveTeNomeOrcamentoIgualAVersaoQuatro()
        {
            ServicoOrcamentoDeViagens servico = new ServicoOrcamentoDeViagens();

            IOrcamentos orcamentos = MockRepository.GenerateMock<IOrcamentos>();
            orcamentos.Expect(o => o.Salvar(null)).IgnoreArguments();

            var diarias = MockRepository.GenerateMock<IDiarias>();
            diarias.Expect(c => c.Todos()).Return(new List<Diaria>());

            var viagens = MockRepository.GenerateMock<IViagens>();
            viagens.Expect(c => c.Todos()).Return(new List<Viagem>());

            servico.Orcamentos = orcamentos;
            servico.Diarias = diarias;
            servico.Viagens = viagens;

            Departamento departamento = new Setor("Barra dor");
            CentroDeCusto centroDeCusto = new CentroDeCusto("centroDeCusto");
            departamento.AdicionarCentroDeCusto(centroDeCusto);

            var orcamentosLista = new List<Orcamento.Domain.Orcamento>();
            orcamentosLista.Add(new OrcamentoDeViagem(departamento, centroDeCusto, 2014));
            orcamentosLista.Add(new OrcamentoDeViagem(departamento, centroDeCusto, 2014));
            orcamentosLista.Add(new OrcamentoDeViagem(departamento, centroDeCusto, 2014));
            var orcamento = servico.CriarOrcamentoDeViagem(orcamentosLista, departamento, centroDeCusto, 2014);

            Assert.AreEqual(orcamento.NomeOrcamento, "Versão4");
        }

        [TestMethod]
        public void CriarOrcamentoComSucessoComQuatroOrcamentoJaCriadoDeveTeNomeOrcamentoIgualAVersaoCinco()
        {
            ServicoOrcamentoDeViagens servico = new ServicoOrcamentoDeViagens();

            IOrcamentos orcamentos = MockRepository.GenerateMock<IOrcamentos>();
            orcamentos.Expect(o => o.Salvar(null)).IgnoreArguments();

            var diarias = MockRepository.GenerateMock<IDiarias>();
            diarias.Expect(c => c.Todos()).Return(new List<Diaria>());

            var viagens = MockRepository.GenerateMock<IViagens>();
            viagens.Expect(c => c.Todos()).Return(new List<Viagem>());

            servico.Orcamentos = orcamentos;
            servico.Diarias = diarias;
            servico.Viagens = viagens;

            Departamento departamento = new Setor("Barra dor");
            CentroDeCusto centroDeCusto = new CentroDeCusto("centroDeCusto");
            departamento.AdicionarCentroDeCusto(centroDeCusto);

            var orcamentosLista = new List<Orcamento.Domain.Orcamento>();
            orcamentosLista.Add(new OrcamentoDeViagem(departamento, centroDeCusto, 2014));
            orcamentosLista.Add(new OrcamentoDeViagem(departamento, centroDeCusto, 2014));
            orcamentosLista.Add(new OrcamentoDeViagem(departamento, centroDeCusto, 2014));
            orcamentosLista.Add(new OrcamentoDeViagem(departamento, centroDeCusto, 2014));
            var orcamento = servico.CriarOrcamentoDeViagem(orcamentosLista, departamento, centroDeCusto, 2014);

            Assert.AreEqual(orcamento.NomeOrcamento, "Versão5");
        }

        [TestMethod]
        public void CriarOrcamentoComSucessoComCincoOrcamentoJaCriadoDeveTeNomeOrcamentoIgualAVersaoSeis()
        {
            ServicoOrcamentoDeViagens servico = new ServicoOrcamentoDeViagens();

            IOrcamentos orcamentos = MockRepository.GenerateMock<IOrcamentos>();
            orcamentos.Expect(o => o.Salvar(null)).IgnoreArguments();

            var diarias = MockRepository.GenerateMock<IDiarias>();
            diarias.Expect(c => c.Todos()).Return(new List<Diaria>());

            var viagens = MockRepository.GenerateMock<IViagens>();
            viagens.Expect(c => c.Todos()).Return(new List<Viagem>());

            servico.Orcamentos = orcamentos;
            servico.Diarias = diarias;
            servico.Viagens = viagens;

            Departamento departamento = new Setor("Barra dor");
            CentroDeCusto centroDeCusto = new CentroDeCusto("centroDeCusto");
            departamento.AdicionarCentroDeCusto(centroDeCusto);

            var orcamentosLista = new List<Orcamento.Domain.Orcamento>();
            orcamentosLista.Add(new OrcamentoDeViagem(departamento, centroDeCusto, 2014));
            orcamentosLista.Add(new OrcamentoDeViagem(departamento, centroDeCusto, 2014));
            orcamentosLista.Add(new OrcamentoDeViagem(departamento, centroDeCusto, 2014));
            orcamentosLista.Add(new OrcamentoDeViagem(departamento, centroDeCusto, 2014));
            orcamentosLista.Add(new OrcamentoDeViagem(departamento, centroDeCusto, 2014));
            var orcamento = servico.CriarOrcamentoDeViagem(orcamentosLista, departamento, centroDeCusto, 2014);

            Assert.AreEqual(orcamento.NomeOrcamento, "Versão6");
        }

        [TestMethod]
        public void CriarOrcamentoComSucessoComSeisOrcamentoJaCriadoDeveTeNomeOrcamentoIgualAVersaoSete()
        {
            ServicoOrcamentoDeViagens servico = new ServicoOrcamentoDeViagens();

            IOrcamentos orcamentos = MockRepository.GenerateMock<IOrcamentos>();
            orcamentos.Expect(o => o.Salvar(null)).IgnoreArguments();

            var diarias = MockRepository.GenerateMock<IDiarias>();
            diarias.Expect(c => c.Todos()).Return(new List<Diaria>());

            var viagens = MockRepository.GenerateMock<IViagens>();
            viagens.Expect(c => c.Todos()).Return(new List<Viagem>());

            servico.Orcamentos = orcamentos;
            servico.Diarias = diarias;
            servico.Viagens = viagens;

            Departamento departamento = new Setor("Barra dor");
            CentroDeCusto centroDeCusto = new CentroDeCusto("centroDeCusto");
            departamento.AdicionarCentroDeCusto(centroDeCusto);

            var orcamentosLista = new List<Orcamento.Domain.Orcamento>();
            orcamentosLista.Add(new OrcamentoDeViagem(departamento, centroDeCusto, 2014));
            orcamentosLista.Add(new OrcamentoDeViagem(departamento, centroDeCusto, 2014));
            orcamentosLista.Add(new OrcamentoDeViagem(departamento, centroDeCusto, 2014));
            orcamentosLista.Add(new OrcamentoDeViagem(departamento, centroDeCusto, 2014));
            orcamentosLista.Add(new OrcamentoDeViagem(departamento, centroDeCusto, 2014));
            orcamentosLista.Add(new OrcamentoDeViagem(departamento, centroDeCusto, 2014));
            var orcamento = servico.CriarOrcamentoDeViagem(orcamentosLista, departamento, centroDeCusto, 2014);

            Assert.AreEqual(orcamento.NomeOrcamento, "Versão7");
        }

        [TestMethod]
        public void CriarOrcamentoComSucessoComSeteOrcamentoJaCriadoDeveTeNomeOrcamentoIgualAVersaoOito()
        {
            ServicoOrcamentoDeViagens servico = new ServicoOrcamentoDeViagens();

            IOrcamentos orcamentos = MockRepository.GenerateMock<IOrcamentos>();
            orcamentos.Expect(o => o.Salvar(null)).IgnoreArguments();

            var diarias = MockRepository.GenerateMock<IDiarias>();
            diarias.Expect(c => c.Todos()).Return(new List<Diaria>());

            var viagens = MockRepository.GenerateMock<IViagens>();
            viagens.Expect(c => c.Todos()).Return(new List<Viagem>());

            servico.Orcamentos = orcamentos;
            servico.Diarias = diarias;
            servico.Viagens = viagens;

            Departamento departamento = new Setor("Barra dor");
            CentroDeCusto centroDeCusto = new CentroDeCusto("centroDeCusto");
            departamento.AdicionarCentroDeCusto(centroDeCusto);

            var orcamentosLista = new List<Orcamento.Domain.Orcamento>();
            orcamentosLista.Add(new OrcamentoDeViagem(departamento, centroDeCusto, 2014));
            orcamentosLista.Add(new OrcamentoDeViagem(departamento, centroDeCusto, 2014));
            orcamentosLista.Add(new OrcamentoDeViagem(departamento, centroDeCusto, 2014));
            orcamentosLista.Add(new OrcamentoDeViagem(departamento, centroDeCusto, 2014));
            orcamentosLista.Add(new OrcamentoDeViagem(departamento, centroDeCusto, 2014));
            orcamentosLista.Add(new OrcamentoDeViagem(departamento, centroDeCusto, 2014));
            orcamentosLista.Add(new OrcamentoDeViagem(departamento, centroDeCusto, 2014));
            var orcamento = servico.CriarOrcamentoDeViagem(orcamentosLista, departamento, centroDeCusto, 2014);

            Assert.AreEqual(orcamento.NomeOrcamento, "Versão8");
        }

        [TestMethod]
        public void CriarOrcamentoComSucessoComOitoOrcamentoJaCriadoDeveTeNomeOrcamentoIgualAVersaoNove()
        {
            ServicoOrcamentoDeViagens servico = new ServicoOrcamentoDeViagens();

            IOrcamentos orcamentos = MockRepository.GenerateMock<IOrcamentos>();
            orcamentos.Expect(o => o.Salvar(null)).IgnoreArguments();

            var diarias = MockRepository.GenerateMock<IDiarias>();
            diarias.Expect(c => c.Todos()).Return(new List<Diaria>());

            var viagens = MockRepository.GenerateMock<IViagens>();
            viagens.Expect(c => c.Todos()).Return(new List<Viagem>());

            servico.Orcamentos = orcamentos;
            servico.Diarias = diarias;
            servico.Viagens = viagens;

            Departamento departamento = new Setor("Barra dor");
            CentroDeCusto centroDeCusto = new CentroDeCusto("centroDeCusto");
            departamento.AdicionarCentroDeCusto(centroDeCusto);

            var orcamentosLista = new List<Orcamento.Domain.Orcamento>();
            orcamentosLista.Add(new OrcamentoDeViagem(departamento, centroDeCusto, 2014));
            orcamentosLista.Add(new OrcamentoDeViagem(departamento, centroDeCusto, 2014));
            orcamentosLista.Add(new OrcamentoDeViagem(departamento, centroDeCusto, 2014));
            orcamentosLista.Add(new OrcamentoDeViagem(departamento, centroDeCusto, 2014));
            orcamentosLista.Add(new OrcamentoDeViagem(departamento, centroDeCusto, 2014));
            orcamentosLista.Add(new OrcamentoDeViagem(departamento, centroDeCusto, 2014));
            orcamentosLista.Add(new OrcamentoDeViagem(departamento, centroDeCusto, 2014));
            orcamentosLista.Add(new OrcamentoDeViagem(departamento, centroDeCusto, 2014));
            var orcamento = servico.CriarOrcamentoDeViagem(orcamentosLista, departamento, centroDeCusto, 2014);

            Assert.AreEqual(orcamento.NomeOrcamento, "Versão9");
        }

        [TestMethod]
        public void CriarOrcamentoComSucessoComNoveOrcamentoJaCriadoDeveTeNomeOrcamentoIgualAVersaoDez()
        {
            ServicoOrcamentoDeViagens servico = new ServicoOrcamentoDeViagens();

            IOrcamentos orcamentos = MockRepository.GenerateMock<IOrcamentos>();
            orcamentos.Expect(o => o.Salvar(null)).IgnoreArguments();

            var diarias = MockRepository.GenerateMock<IDiarias>();
            diarias.Expect(c => c.Todos()).Return(new List<Diaria>());

            var viagens = MockRepository.GenerateMock<IViagens>();
            viagens.Expect(c => c.Todos()).Return(new List<Viagem>());

            servico.Orcamentos = orcamentos;
            servico.Diarias = diarias;
            servico.Viagens = viagens;

            Departamento departamento = new Setor("Barra dor");
            CentroDeCusto centroDeCusto = new CentroDeCusto("centroDeCusto");
            departamento.AdicionarCentroDeCusto(centroDeCusto);

            var orcamentosLista = new List<Orcamento.Domain.Orcamento>();
            orcamentosLista.Add(new OrcamentoDeViagem(departamento, centroDeCusto, 2014));
            orcamentosLista.Add(new OrcamentoDeViagem(departamento, centroDeCusto, 2014));
            orcamentosLista.Add(new OrcamentoDeViagem(departamento, centroDeCusto, 2014));
            orcamentosLista.Add(new OrcamentoDeViagem(departamento, centroDeCusto, 2014));
            orcamentosLista.Add(new OrcamentoDeViagem(departamento, centroDeCusto, 2014));
            orcamentosLista.Add(new OrcamentoDeViagem(departamento, centroDeCusto, 2014));
            orcamentosLista.Add(new OrcamentoDeViagem(departamento, centroDeCusto, 2014));
            orcamentosLista.Add(new OrcamentoDeViagem(departamento, centroDeCusto, 2014));
            orcamentosLista.Add(new OrcamentoDeViagem(departamento, centroDeCusto, 2014));
            var orcamento = servico.CriarOrcamentoDeViagem(orcamentosLista, departamento, centroDeCusto, 2014);

            Assert.AreEqual(orcamento.NomeOrcamento, "Versão10");
        }

        [TestMethod]
        // [ExpectedException(UserMessage = "Orçamento já tem dez versões")]
        public void CriarOrcamentoComSucessoComDezDeveRetornarExecao()
        {
            ServicoOrcamentoDeViagens servico = new ServicoOrcamentoDeViagens();

            IOrcamentos orcamentos = MockRepository.GenerateMock<IOrcamentos>();
            orcamentos.Expect(o => o.Salvar(null)).IgnoreArguments();

            var diarias = MockRepository.GenerateMock<IDiarias>();
            diarias.Expect(c => c.Todos()).Return(new List<Diaria>());

            var viagens = MockRepository.GenerateMock<IViagens>();
            viagens.Expect(c => c.Todos()).Return(new List<Viagem>());

            servico.Orcamentos = orcamentos;
            servico.Diarias = diarias;
            servico.Viagens = viagens;

            Departamento departamento = new Setor("Barra dor");
            CentroDeCusto centroDeCusto = new CentroDeCusto("centroDeCusto");
            departamento.AdicionarCentroDeCusto(centroDeCusto);

            var orcamentosLista = new List<Orcamento.Domain.Orcamento>();
            orcamentosLista.Add(new OrcamentoDeViagem(departamento, centroDeCusto, 2014));
            orcamentosLista.Add(new OrcamentoDeViagem(departamento, centroDeCusto, 2014));
            orcamentosLista.Add(new OrcamentoDeViagem(departamento, centroDeCusto, 2014));
            orcamentosLista.Add(new OrcamentoDeViagem(departamento, centroDeCusto, 2014));
            orcamentosLista.Add(new OrcamentoDeViagem(departamento, centroDeCusto, 2014));
            orcamentosLista.Add(new OrcamentoDeViagem(departamento, centroDeCusto, 2014));
            orcamentosLista.Add(new OrcamentoDeViagem(departamento, centroDeCusto, 2014));
            orcamentosLista.Add(new OrcamentoDeViagem(departamento, centroDeCusto, 2014));
            orcamentosLista.Add(new OrcamentoDeViagem(departamento, centroDeCusto, 2014));
            orcamentosLista.Add(new OrcamentoDeViagem(departamento, centroDeCusto, 2014));
            var orcamento = servico.CriarOrcamentoDeViagem(orcamentosLista, departamento, centroDeCusto, 2014);

        }

        [TestMethod]
        [Ignore]
        public void AtribuirVersaoFinalComSucesso()
        {
            ServicoOrcamentoDeViagens servico = new ServicoOrcamentoDeViagens();

            IOrcamentos orcamentos = MockRepository.GenerateMock<IOrcamentos>();
            orcamentos.Expect(o => o.Salvar(null)).IgnoreArguments();

            var diarias = MockRepository.GenerateMock<IDiarias>();

            Diaria diaria = new Diaria();
            diaria.Cidade = new Cidade("ljk") {Descricao = "kjh"};
            Ticket ticket = new Ticket(new TipoTicket("1"), diaria.Cidade);
            ticket.Valor = 10;

            diaria.Tickets = new List<Ticket>();
            diaria.Tickets.Add(ticket);

            List<Diaria> listaDiarias = new List<Diaria>();

            listaDiarias.Add(diaria);

            diarias.Expect(c => c.Todos()).Return(listaDiarias);

            var viagens = MockRepository.GenerateMock<IViagens>();
            viagens.Expect(c => c.Todos()).Return(new List<Viagem>());

            servico.Orcamentos = orcamentos;
            servico.Diarias = diarias;
            servico.Viagens = viagens;

            Departamento departamento = new Hospital("Barra dor");

            var orcamento = new OrcamentoDeViagem(departamento, new CentroDeCusto("centroDeCusto"), 2014);

            DiariaViagem despesa = new DiariaViagem();
            despesa.ValorTotal = 10;
            despesa.Diaria = diaria;
            orcamento.Despesas.Add(despesa);
            
            servico.AtribuirVersaoFinal(orcamento);

            Assert.IsTrue(orcamento.VersaoFinal);
        }

        [TestMethod]
        public void DeletarOrcamentoComDoisOrcamentosNaListaPrimeiroItemDaListaDeveTerNomeDeVersaoUm()
        {
            ServicoOrcamentoDeViagens servico = new ServicoOrcamentoDeViagens();

            IOrcamentos orcamentos = MockRepository.GenerateMock<IOrcamentos>();
            orcamentos.Expect(o => o.Salvar(null)).IgnoreArguments();

            var diarias = MockRepository.GenerateMock<IDiarias>();
            diarias.Expect(c => c.Todos()).Return(new List<Diaria>());

            var viagens = MockRepository.GenerateMock<IViagens>();
            viagens.Expect(c => c.Todos()).Return(new List<Viagem>());

            servico.Orcamentos = orcamentos;
            servico.Diarias = diarias;
            servico.Viagens = viagens;

            Departamento departamento = new Setor("Barra dor");
            CentroDeCusto centroDeCusto = new CentroDeCusto("centroDeCusto");
            departamento.AdicionarCentroDeCusto(centroDeCusto);


            var orcamento = new OrcamentoDeViagem(departamento, centroDeCusto, 2014);

            var orcamentosLista = new List<Orcamento.Domain.Orcamento>();
            orcamentosLista.Add(new OrcamentoDeViagem(departamento, centroDeCusto, 2014));
            orcamentosLista.Add(orcamento);
            servico.DeletarOrcamento(orcamento, orcamentosLista, departamento);

            Assert.IsTrue(orcamentosLista.FirstOrDefault().NomeOrcamento == "Versão1");
        }

        [TestMethod]
        public void DeletarOrcamentoComDoisOrcamentosNaListaDeveRetornarApenasCountUm()
        {
            ServicoOrcamentoDeViagens servico = new ServicoOrcamentoDeViagens();

            IOrcamentos orcamentos = MockRepository.GenerateMock<IOrcamentos>();
            orcamentos.Expect(o => o.Salvar(null)).IgnoreArguments();

            var diarias = MockRepository.GenerateMock<IDiarias>();
            diarias.Expect(c => c.Todos()).Return(new List<Diaria>());

            var viagens = MockRepository.GenerateMock<IViagens>();
            viagens.Expect(c => c.Todos()).Return(new List<Viagem>());

            servico.Orcamentos = orcamentos;
            servico.Diarias = diarias;
            servico.Viagens = viagens;

            Departamento departamento = new Setor("Barra dor");
            CentroDeCusto centroDeCusto = new CentroDeCusto("centroDeCusto");
            departamento.AdicionarCentroDeCusto(centroDeCusto);


            var orcamento = new OrcamentoDeViagem(departamento, centroDeCusto, 2014);

            var orcamentosLista = new List<Orcamento.Domain.Orcamento>();
            orcamentosLista.Add(new OrcamentoDeViagem(departamento, centroDeCusto, 2014));
            orcamentosLista.Add(orcamento);
            servico.DeletarOrcamento(orcamento, orcamentosLista, departamento);

            Assert.IsTrue(orcamentosLista.Count == 1);
        }
        [TestMethod]
        public void DeletarOrcamentoComDezOrcamentosNaListaDeveRetornarNomesDosORcamentosComSequenciaCerta()
        {
            ServicoOrcamentoDeViagens servico = new ServicoOrcamentoDeViagens();

            IOrcamentos orcamentos = MockRepository.GenerateMock<IOrcamentos>();
            orcamentos.Expect(o => o.Salvar(null)).IgnoreArguments();

            var diarias = MockRepository.GenerateMock<IDiarias>();
            diarias.Expect(c => c.Todos()).Return(new List<Diaria>());

            var viagens = MockRepository.GenerateMock<IViagens>();
            viagens.Expect(c => c.Todos()).Return(new List<Viagem>());

            servico.Orcamentos = orcamentos;
            servico.Diarias = diarias;
            servico.Viagens = viagens;

            Departamento departamento = new Setor("Barra dor");
            CentroDeCusto centroDeCusto = new CentroDeCusto("centroDeCusto");
            departamento.AdicionarCentroDeCusto(centroDeCusto);


            var orcamento = new OrcamentoDeViagem(departamento, centroDeCusto, 2014);

            var orcamentosLista = new List<Orcamento.Domain.Orcamento>();
            orcamentosLista.Add(new OrcamentoDeViagem(departamento, centroDeCusto, 2014));
            orcamentosLista.Add(new OrcamentoDeViagem(departamento, centroDeCusto, 2014));
            orcamentosLista.Add(new OrcamentoDeViagem(departamento, centroDeCusto, 2014));
            orcamentosLista.Add(new OrcamentoDeViagem(departamento, centroDeCusto, 2014));
            orcamentosLista.Add(new OrcamentoDeViagem(departamento, centroDeCusto, 2014));
            orcamentosLista.Add(new OrcamentoDeViagem(departamento, centroDeCusto, 2014));
            orcamentosLista.Add(new OrcamentoDeViagem(departamento, centroDeCusto, 2014));
            orcamentosLista.Add(new OrcamentoDeViagem(departamento, centroDeCusto, 2014));
            orcamentosLista.Add(new OrcamentoDeViagem(departamento, centroDeCusto, 2014));
            orcamentosLista.Add(orcamento);
            servico.DeletarOrcamento(orcamento, orcamentosLista, departamento);
            for (int i = 0; i < orcamentosLista.Count; i++)
            {
                Assert.AreEqual(orcamentosLista[i].NomeOrcamento, "Versão" + (i + 1).ToString());
            }
        }
    }
}
