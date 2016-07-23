using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Orcamento.Domain.Gerenciamento;
using Orcamento.Domain.Servico;
using Orcamento.Domain;
using Rhino.Mocks;
using Orcamento.Domain.DB.Repositorio;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Orcamento.TestMethod.Servico
{
    [TestClass]
    public class ServicoCriarOrcamentoOperacionalTestMethod
    {
        [TestMethod]
        public void CriarOrcamentoComSucessoDeveTerOrcamentoNomeOrcamentoDeveSerVersaoUm()
        {
            ServicoOrcamentoOperacionalVersao servico = new ServicoOrcamentoOperacionalVersao();
            IOrcamentos orcamentos = MockRepository.GenerateMock<IOrcamentos>();
            orcamentos.Expect(o => o.Salvar(null)).IgnoreArguments();
            servico.Orcamentos = orcamentos;

            Departamento departamento = new Setor("Barra dor");
            CentroDeCusto centroDeCusto = new CentroDeCusto("centroDeCusto");
            departamento.AdicionarCentroDeCusto(centroDeCusto);

            var orcamento = servico.CriarOrcamentoOperacional(new List<Orcamento.Domain.Orcamento>(), departamento, centroDeCusto, 2014);

            Assert.AreEqual(orcamento.NomeOrcamento, "Versão1");
        }

        [TestMethod]
        public void CriarOrcamentoComSucessoComUmORcamentoJaCriadoDeveTeNomeOrcamentoIgualAVersaoDois()
        {
            ServicoOrcamentoOperacionalVersao servico = new ServicoOrcamentoOperacionalVersao();
            IOrcamentos orcamentos = MockRepository.GenerateMock<IOrcamentos>();
            orcamentos.Expect(o => o.Salvar(null)).IgnoreArguments();
            servico.Orcamentos = orcamentos;

            Departamento departamento = new Setor("Barra dor");
            CentroDeCusto centroDeCusto = new CentroDeCusto("centroDeCusto");
            departamento.AdicionarCentroDeCusto(centroDeCusto);

            var orcamentosLista = new List<Orcamento.Domain.Orcamento>();
            orcamentosLista.Add(new OrcamentoOperacionalVersao(departamento, centroDeCusto, 2014));
            var orcamento = servico.CriarOrcamentoOperacional(orcamentosLista, departamento, centroDeCusto, 2014);

            Assert.AreEqual(orcamento.NomeOrcamento, "Versão2");
        }

        [TestMethod]
        public void CriarOrcamentoComSucessoComDoisORcamentoJaCriadoDeveTeNomeOrcamentoIgualAVersaoTres()
        {
            ServicoOrcamentoOperacionalVersao servico = new ServicoOrcamentoOperacionalVersao();
            IOrcamentos orcamentos = MockRepository.GenerateMock<IOrcamentos>();
            orcamentos.Expect(o => o.Salvar(null)).IgnoreArguments();
            servico.Orcamentos = orcamentos;

            Departamento departamento = new Setor("Barra dor");
            CentroDeCusto centroDeCusto = new CentroDeCusto("centroDeCusto");
            departamento.AdicionarCentroDeCusto(centroDeCusto);

            var orcamentosLista = new List<Orcamento.Domain.Orcamento>();
            orcamentosLista.Add(new OrcamentoOperacionalVersao(departamento, centroDeCusto, 2014));
            orcamentosLista.Add(new OrcamentoOperacionalVersao(departamento, centroDeCusto, 2014));
            var orcamento = servico.CriarOrcamentoOperacional(orcamentosLista, departamento, centroDeCusto, 2014);

            Assert.AreEqual(orcamento.NomeOrcamento, "Versão3");
        }

        [TestMethod]
        public void CriarOrcamentoComSucessoComTresORcamentoJaCriadoDeveTeNomeOrcamentoIgualAVersaoQuatro()
        {
            ServicoOrcamentoOperacionalVersao servico = new ServicoOrcamentoOperacionalVersao();
            IOrcamentos orcamentos = MockRepository.GenerateMock<IOrcamentos>();
            orcamentos.Expect(o => o.Salvar(null)).IgnoreArguments();
            servico.Orcamentos = orcamentos;

            Departamento departamento = new Setor("Barra dor");
            CentroDeCusto centroDeCusto = new CentroDeCusto("centroDeCusto");
            departamento.AdicionarCentroDeCusto(centroDeCusto);

            var orcamentosLista = new List<Orcamento.Domain.Orcamento>();
            orcamentosLista.Add(new OrcamentoOperacionalVersao(departamento, centroDeCusto, 2014));
            orcamentosLista.Add(new OrcamentoOperacionalVersao(departamento, centroDeCusto, 2014));
            orcamentosLista.Add(new OrcamentoOperacionalVersao(departamento, centroDeCusto, 2014));
            var orcamento = servico.CriarOrcamentoOperacional(orcamentosLista, departamento, centroDeCusto, 2014);

            Assert.AreEqual(orcamento.NomeOrcamento, "Versão4");
        }

        [TestMethod]
        public void CriarOrcamentoComSucessoComQuatroOrcamentoJaCriadoDeveTeNomeOrcamentoIgualAVersaoCinco()
        {
            ServicoOrcamentoOperacionalVersao servico = new ServicoOrcamentoOperacionalVersao();
            IOrcamentos orcamentos = MockRepository.GenerateMock<IOrcamentos>();
            orcamentos.Expect(o => o.Salvar(null)).IgnoreArguments();
            servico.Orcamentos = orcamentos;

            Departamento departamento = new Setor("Barra dor");
            CentroDeCusto centroDeCusto = new CentroDeCusto("centroDeCusto");
            departamento.AdicionarCentroDeCusto(centroDeCusto);

            var orcamentosLista = new List<Orcamento.Domain.Orcamento>();
            orcamentosLista.Add(new OrcamentoOperacionalVersao(departamento, centroDeCusto, 2014));
            orcamentosLista.Add(new OrcamentoOperacionalVersao(departamento, centroDeCusto, 2014));
            orcamentosLista.Add(new OrcamentoOperacionalVersao(departamento, centroDeCusto, 2014));
            orcamentosLista.Add(new OrcamentoOperacionalVersao(departamento, centroDeCusto, 2014));
            var orcamento = servico.CriarOrcamentoOperacional(orcamentosLista, departamento, centroDeCusto, 2014);

            Assert.AreEqual(orcamento.NomeOrcamento, "Versão5");
        }

        [TestMethod]
        public void CriarOrcamentoComSucessoComCincoOrcamentoJaCriadoDeveTeNomeOrcamentoIgualAVersaoSeis()
        {
            ServicoOrcamentoOperacionalVersao servico = new ServicoOrcamentoOperacionalVersao();
            IOrcamentos orcamentos = MockRepository.GenerateMock<IOrcamentos>();
            orcamentos.Expect(o => o.Salvar(null)).IgnoreArguments();
            servico.Orcamentos = orcamentos;

            Departamento departamento = new Setor("Barra dor");
            CentroDeCusto centroDeCusto = new CentroDeCusto("centroDeCusto");
            departamento.AdicionarCentroDeCusto(centroDeCusto);

            var orcamentosLista = new List<Orcamento.Domain.Orcamento>();
            orcamentosLista.Add(new OrcamentoOperacionalVersao(departamento, centroDeCusto, 2014));
            orcamentosLista.Add(new OrcamentoOperacionalVersao(departamento, centroDeCusto, 2014));
            orcamentosLista.Add(new OrcamentoOperacionalVersao(departamento, centroDeCusto, 2014));
            orcamentosLista.Add(new OrcamentoOperacionalVersao(departamento, centroDeCusto, 2014));
            orcamentosLista.Add(new OrcamentoOperacionalVersao(departamento, centroDeCusto, 2014));
            var orcamento = servico.CriarOrcamentoOperacional(orcamentosLista, departamento, centroDeCusto, 2014);

            Assert.AreEqual(orcamento.NomeOrcamento, "Versão6");
        }

        [TestMethod]
        public void CriarOrcamentoComSucessoComSeisOrcamentoJaCriadoDeveTeNomeOrcamentoIgualAVersaoSete()
        {
            ServicoOrcamentoOperacionalVersao servico = new ServicoOrcamentoOperacionalVersao();
            IOrcamentos orcamentos = MockRepository.GenerateMock<IOrcamentos>();
            orcamentos.Expect(o => o.Salvar(null)).IgnoreArguments();
            servico.Orcamentos = orcamentos;

            Departamento departamento = new Setor("Barra dor");
            CentroDeCusto centroDeCusto = new CentroDeCusto("centroDeCusto");
            departamento.AdicionarCentroDeCusto(centroDeCusto);

            var orcamentosLista = new List<Orcamento.Domain.Orcamento>();
            orcamentosLista.Add(new OrcamentoOperacionalVersao(departamento, centroDeCusto, 2014));
            orcamentosLista.Add(new OrcamentoOperacionalVersao(departamento, centroDeCusto, 2014));
            orcamentosLista.Add(new OrcamentoOperacionalVersao(departamento, centroDeCusto, 2014));
            orcamentosLista.Add(new OrcamentoOperacionalVersao(departamento, centroDeCusto, 2014));
            orcamentosLista.Add(new OrcamentoOperacionalVersao(departamento, centroDeCusto, 2014));
            orcamentosLista.Add(new OrcamentoOperacionalVersao(departamento, centroDeCusto, 2014));
            var orcamento = servico.CriarOrcamentoOperacional(orcamentosLista, departamento, centroDeCusto, 2014);

            Assert.AreEqual(orcamento.NomeOrcamento, "Versão7");
        }

        [TestMethod]
        public void CriarOrcamentoComSucessoComSeteOrcamentoJaCriadoDeveTeNomeOrcamentoIgualAVersaoOito()
        {
            ServicoOrcamentoOperacionalVersao servico = new ServicoOrcamentoOperacionalVersao();
            IOrcamentos orcamentos = MockRepository.GenerateMock<IOrcamentos>();
            orcamentos.Expect(o => o.Salvar(null)).IgnoreArguments();
            servico.Orcamentos = orcamentos;

            Departamento departamento = new Setor("Barra dor");
            CentroDeCusto centroDeCusto = new CentroDeCusto("centroDeCusto");
            departamento.AdicionarCentroDeCusto(centroDeCusto);

            var orcamentosLista = new List<Orcamento.Domain.Orcamento>();
            orcamentosLista.Add(new OrcamentoOperacionalVersao(departamento, centroDeCusto, 2014));
            orcamentosLista.Add(new OrcamentoOperacionalVersao(departamento, centroDeCusto, 2014));
            orcamentosLista.Add(new OrcamentoOperacionalVersao(departamento, centroDeCusto, 2014));
            orcamentosLista.Add(new OrcamentoOperacionalVersao(departamento, centroDeCusto, 2014));
            orcamentosLista.Add(new OrcamentoOperacionalVersao(departamento, centroDeCusto, 2014));
            orcamentosLista.Add(new OrcamentoOperacionalVersao(departamento, centroDeCusto, 2014));
            orcamentosLista.Add(new OrcamentoOperacionalVersao(departamento, centroDeCusto, 2014));
            var orcamento = servico.CriarOrcamentoOperacional(orcamentosLista, departamento, centroDeCusto, 2014);

            Assert.AreEqual(orcamento.NomeOrcamento, "Versão8");
        }

        [TestMethod]
        public void CriarOrcamentoComSucessoComOitoOrcamentoJaCriadoDeveTeNomeOrcamentoIgualAVersaoNove()
        {
            ServicoOrcamentoOperacionalVersao servico = new ServicoOrcamentoOperacionalVersao();
            IOrcamentos orcamentos = MockRepository.GenerateMock<IOrcamentos>();
            orcamentos.Expect(o => o.Salvar(null)).IgnoreArguments();
            servico.Orcamentos = orcamentos;

            Departamento departamento = new Setor("Barra dor");
            CentroDeCusto centroDeCusto = new CentroDeCusto("centroDeCusto");
            departamento.AdicionarCentroDeCusto(centroDeCusto);

            var orcamentosLista = new List<Orcamento.Domain.Orcamento>();
            orcamentosLista.Add(new OrcamentoOperacionalVersao(departamento, centroDeCusto, 2014));
            orcamentosLista.Add(new OrcamentoOperacionalVersao(departamento, centroDeCusto, 2014));
            orcamentosLista.Add(new OrcamentoOperacionalVersao(departamento, centroDeCusto, 2014));
            orcamentosLista.Add(new OrcamentoOperacionalVersao(departamento, centroDeCusto, 2014));
            orcamentosLista.Add(new OrcamentoOperacionalVersao(departamento, centroDeCusto, 2014));
            orcamentosLista.Add(new OrcamentoOperacionalVersao(departamento, centroDeCusto, 2014));
            orcamentosLista.Add(new OrcamentoOperacionalVersao(departamento, centroDeCusto, 2014));
            orcamentosLista.Add(new OrcamentoOperacionalVersao(departamento, centroDeCusto, 2014));
            var orcamento = servico.CriarOrcamentoOperacional(orcamentosLista, departamento, centroDeCusto, 2014);

            Assert.AreEqual(orcamento.NomeOrcamento, "Versão9");
        }

        [TestMethod]
        public void CriarOrcamentoComSucessoComNoveOrcamentoJaCriadoDeveTeNomeOrcamentoIgualAVersaoDez()
        {
            ServicoOrcamentoOperacionalVersao servico = new ServicoOrcamentoOperacionalVersao();
            IOrcamentos orcamentos = MockRepository.GenerateMock<IOrcamentos>();
            orcamentos.Expect(o => o.Salvar(null)).IgnoreArguments();
            servico.Orcamentos = orcamentos;

            Departamento departamento = new Setor("Barra dor");
            CentroDeCusto centroDeCusto = new CentroDeCusto("centroDeCusto");
            departamento.AdicionarCentroDeCusto(centroDeCusto);

            var orcamentosLista = new List<Orcamento.Domain.Orcamento>();
            orcamentosLista.Add(new OrcamentoOperacionalVersao(departamento, centroDeCusto, 2014));
            orcamentosLista.Add(new OrcamentoOperacionalVersao(departamento, centroDeCusto, 2014));
            orcamentosLista.Add(new OrcamentoOperacionalVersao(departamento, centroDeCusto, 2014));
            orcamentosLista.Add(new OrcamentoOperacionalVersao(departamento, centroDeCusto, 2014));
            orcamentosLista.Add(new OrcamentoOperacionalVersao(departamento, centroDeCusto, 2014));
            orcamentosLista.Add(new OrcamentoOperacionalVersao(departamento, centroDeCusto, 2014));
            orcamentosLista.Add(new OrcamentoOperacionalVersao(departamento, centroDeCusto, 2014));
            orcamentosLista.Add(new OrcamentoOperacionalVersao(departamento, centroDeCusto, 2014));
            orcamentosLista.Add(new OrcamentoOperacionalVersao(departamento, centroDeCusto, 2014));
            var orcamento = servico.CriarOrcamentoOperacional(orcamentosLista, departamento, centroDeCusto, 2014);

            Assert.AreEqual(orcamento.NomeOrcamento, "Versão10");
        }

        [TestMethod]
        // [ExpectedException(UserMessage = "Orçamento já tem dez versões")]
        public void CriarOrcamentoComSucessoComDezDeveRetornarExecao()
        {
            ServicoOrcamentoOperacionalVersao servico = new ServicoOrcamentoOperacionalVersao();
            IOrcamentos orcamentos = MockRepository.GenerateMock<IOrcamentos>();
            orcamentos.Expect(o => o.Salvar(null)).IgnoreArguments();
            servico.Orcamentos = orcamentos;

            Departamento departamento = new Setor("Barra dor");
            CentroDeCusto centroDeCusto = new CentroDeCusto("centroDeCusto");
            departamento.AdicionarCentroDeCusto(centroDeCusto);

            var orcamentosLista = new List<Orcamento.Domain.Orcamento>();
            orcamentosLista.Add(new OrcamentoOperacionalVersao(departamento, centroDeCusto, 2014));
            orcamentosLista.Add(new OrcamentoOperacionalVersao(departamento, centroDeCusto, 2014));
            orcamentosLista.Add(new OrcamentoOperacionalVersao(departamento, centroDeCusto, 2014));
            orcamentosLista.Add(new OrcamentoOperacionalVersao(departamento, centroDeCusto, 2014));
            orcamentosLista.Add(new OrcamentoOperacionalVersao(departamento, centroDeCusto, 2014));
            orcamentosLista.Add(new OrcamentoOperacionalVersao(departamento, centroDeCusto, 2014));
            orcamentosLista.Add(new OrcamentoOperacionalVersao(departamento, centroDeCusto, 2014));
            orcamentosLista.Add(new OrcamentoOperacionalVersao(departamento, centroDeCusto, 2014));
            orcamentosLista.Add(new OrcamentoOperacionalVersao(departamento, centroDeCusto, 2014));
            orcamentosLista.Add(new OrcamentoOperacionalVersao(departamento, centroDeCusto, 2014));
            var orcamento = servico.CriarOrcamentoOperacional(orcamentosLista, departamento, centroDeCusto, 2014);

        }

        [TestMethod]
        public void AtribuirVersaoFinalComSucesso()
        {
            ServicoOrcamentoOperacionalVersao servico = new ServicoOrcamentoOperacionalVersao();
            IOrcamentos orcamentos = MockRepository.GenerateMock<IOrcamentos>();
            orcamentos.Expect(o => o.Salvar(null)).IgnoreArguments();
            orcamentos.Expect(o => o.ObterOrcamentoFinalOrcamentoOperacional(null,null)).IgnoreArguments().Return(null);
            servico.Orcamentos = orcamentos;
            Departamento departamento = new Hospital("Barra dor");

            var orcamento = new OrcamentoOperacionalVersao(departamento, new CentroDeCusto("centroDeCusto"), 2014);

            Despesa despesa = new Despesa(MesEnum.Janeiro, null);
            despesa.Valor = 10;

            orcamento.DespesasOperacionais.Add(despesa);
            
            servico.AtribuirVersaoFinal(orcamento);

            Assert.IsTrue(orcamento.VersaoFinal);
        }

        [TestMethod]
        public void DeletarOrcamentoComDoisOrcamentosNaListaPrimeiroItemDaListaDeveTerNomeDeVersaoUm()
        {
            ServicoOrcamentoOperacionalVersao servico = new ServicoOrcamentoOperacionalVersao();
            IOrcamentos orcamentos = MockRepository.GenerateMock<IOrcamentos>();
            orcamentos.Expect(o => o.Salvar(null)).IgnoreArguments();
            orcamentos.Expect(o => o.Deletar(null)).IgnoreArguments();
            servico.Orcamentos = orcamentos;

            Departamento departamento = new Setor("Barra dor");
            CentroDeCusto centroDeCusto = new CentroDeCusto("centroDeCusto");
            departamento.AdicionarCentroDeCusto(centroDeCusto);


            var orcamento = new OrcamentoOperacionalVersao(departamento, centroDeCusto, 2014);

            var orcamentosLista = new List<Orcamento.Domain.Orcamento>();
            orcamentosLista.Add(new OrcamentoOperacionalVersao(departamento, centroDeCusto, 2014));
            orcamentosLista.Add(orcamento);
            servico.DeletarOrcamento(orcamento, orcamentosLista, departamento);

            Assert.IsTrue(orcamentosLista.FirstOrDefault().NomeOrcamento == "Versão1");
        }

        [TestMethod]
        public void DeletarOrcamentoComDoisOrcamentosNaListaDeveRetornarApenasCountUm()
        {
            ServicoOrcamentoOperacionalVersao servico = new ServicoOrcamentoOperacionalVersao();
            IOrcamentos orcamentos = MockRepository.GenerateMock<IOrcamentos>();
            orcamentos.Expect(o => o.Salvar(null)).IgnoreArguments();
            orcamentos.Expect(o => o.Deletar(null)).IgnoreArguments();
            servico.Orcamentos = orcamentos;

            Departamento departamento = new Setor("Barra dor");
            CentroDeCusto centroDeCusto = new CentroDeCusto("centroDeCusto");
            departamento.AdicionarCentroDeCusto(centroDeCusto);


            var orcamento = new OrcamentoOperacionalVersao(departamento, centroDeCusto, 2014);

            var orcamentosLista = new List<Orcamento.Domain.Orcamento>();
            orcamentosLista.Add(new OrcamentoOperacionalVersao(departamento, centroDeCusto, 2014));
            orcamentosLista.Add(orcamento);
            servico.DeletarOrcamento(orcamento, orcamentosLista, departamento);

            Assert.IsTrue(orcamentosLista.Count == 1);
        }
        [TestMethod]
        public void DeletarOrcamentoComDezOrcamentosNaListaDeveRetornarNomesDosORcamentosComSequenciaCerta()
        {
            ServicoOrcamentoOperacionalVersao servico = new ServicoOrcamentoOperacionalVersao();
            IOrcamentos orcamentos = MockRepository.GenerateMock<IOrcamentos>();
            orcamentos.Expect(o => o.Salvar(null)).IgnoreArguments();
            orcamentos.Expect(o => o.Deletar(null)).IgnoreArguments();
            servico.Orcamentos = orcamentos;

            Departamento departamento = new Setor("Barra dor");
            CentroDeCusto centroDeCusto = new CentroDeCusto("centroDeCusto");
            departamento.AdicionarCentroDeCusto(centroDeCusto);


            var orcamento = new OrcamentoOperacionalVersao(departamento, centroDeCusto, 2014);

            var orcamentosLista = new List<Orcamento.Domain.Orcamento>();
            orcamentosLista.Add(new OrcamentoOperacionalVersao(departamento, centroDeCusto, 2014));
            orcamentosLista.Add(new OrcamentoOperacionalVersao(departamento, centroDeCusto, 2014));
            orcamentosLista.Add(new OrcamentoOperacionalVersao(departamento, centroDeCusto, 2014));
            orcamentosLista.Add(new OrcamentoOperacionalVersao(departamento, centroDeCusto, 2014));
            orcamentosLista.Add(new OrcamentoOperacionalVersao(departamento, centroDeCusto, 2014));
            orcamentosLista.Add(new OrcamentoOperacionalVersao(departamento, centroDeCusto, 2014));
            orcamentosLista.Add(new OrcamentoOperacionalVersao(departamento, centroDeCusto, 2014));
            orcamentosLista.Add(new OrcamentoOperacionalVersao(departamento, centroDeCusto, 2014));
            orcamentosLista.Add(new OrcamentoOperacionalVersao(departamento, centroDeCusto, 2014));
            orcamentosLista.Add(orcamento);
            servico.DeletarOrcamento(orcamento, orcamentosLista, departamento);
            for (int i = 0; i < orcamentosLista.Count; i++)
            {
                Assert.AreEqual(orcamentosLista[i].NomeOrcamento, "Versão" + (i + 1).ToString());
            }
        }
    }
}
