using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Orcamento.Domain.Gerenciamento;
using Orcamento.Domain.Servico;
using Orcamento.Domain.DB.Repositorio;
using Orcamento.Domain;
using Rhino.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Orcamento.TestMethod.Servico
{
    [TestClass]
    public class ServicoCriarOrcamentoHospitalarTestMethod
    {
        [TestMethod]
        public void CriarOrcamentoComSucessoDeveTerOrcamentoDiferenteDeNulo()
        {
            ServicoCriarOrcamentoHospitalar servico = new ServicoCriarOrcamentoHospitalar();
            IOrcamentos orcamentos = MockRepository.GenerateMock<IOrcamentos>();
            orcamentos.Expect(o => o.Salvar(null)).IgnoreArguments();
            servico.Orcamentos = orcamentos;

            Departamento departamento = new Hospital("Barra dor");
            var orcamento = servico.CriarOrcamentoHospitalar(new List<Orcamento.Domain.Orcamento>(), departamento, 2014);

            Assert.IsNotNull(orcamento);
        }

        [TestMethod]
        public void CriarOrcamentoComSucessoDeveTerOrcamentoNomeOrcamentoDeveSerVersaoUm()
        {
            ServicoCriarOrcamentoHospitalar servico = new ServicoCriarOrcamentoHospitalar();
            IOrcamentos orcamentos = MockRepository.GenerateMock<IOrcamentos>();
            orcamentos.Expect(o => o.Salvar(null)).IgnoreArguments();
            servico.Orcamentos = orcamentos;

            Departamento departamento = new Hospital("Barra dor");
            var orcamento = servico.CriarOrcamentoHospitalar(new List<Orcamento.Domain.Orcamento>(), departamento, 2014);

            Assert.AreEqual(orcamento.NomeOrcamento, "Versão1");
        }

        [TestMethod]
        public void CriarOrcamentoComSucessoComUmORcamentoJaCriadoDeveTeNomeOrcamentoIgualAVersaoDois()
        {
            ServicoCriarOrcamentoHospitalar servico = new ServicoCriarOrcamentoHospitalar();
            IOrcamentos orcamentos = MockRepository.GenerateMock<IOrcamentos>();
            orcamentos.Expect(o => o.Salvar(null)).IgnoreArguments();
            servico.Orcamentos = orcamentos;

            Departamento departamento = new Hospital("Barra dor");

            var orcamentosLista = new List<Orcamento.Domain.Orcamento>();
            orcamentosLista.Add(new OrcamentoHospitalar(departamento, 2014));
            var orcamento = servico.CriarOrcamentoHospitalar(orcamentosLista, departamento, 2014);

            Assert.AreEqual(orcamento.NomeOrcamento, "Versão2");
        }

        [TestMethod]
        public void CriarOrcamentoComSucessoComDoisORcamentoJaCriadoDeveTeNomeOrcamentoIgualAVersaoTres()
        {
            ServicoCriarOrcamentoHospitalar servico = new ServicoCriarOrcamentoHospitalar();
            IOrcamentos orcamentos = MockRepository.GenerateMock<IOrcamentos>();
            orcamentos.Expect(o => o.Salvar(null)).IgnoreArguments();
            servico.Orcamentos = orcamentos;

            Departamento departamento = new Hospital("Barra dor");

            var orcamentosLista = new List<Orcamento.Domain.Orcamento>();
            orcamentosLista.Add(new OrcamentoHospitalar(departamento, 2014));
            orcamentosLista.Add(new OrcamentoHospitalar(departamento, 2014));
            var orcamento = servico.CriarOrcamentoHospitalar(orcamentosLista, departamento, 2014);

            Assert.AreEqual(orcamento.NomeOrcamento, "Versão3");
        }

        [TestMethod]
        public void CriarOrcamentoComSucessoComTresORcamentoJaCriadoDeveTeNomeOrcamentoIgualAVersaoQuatro()
        {
            ServicoCriarOrcamentoHospitalar servico = new ServicoCriarOrcamentoHospitalar();
            IOrcamentos orcamentos = MockRepository.GenerateMock<IOrcamentos>();
            orcamentos.Expect(o => o.Salvar(null)).IgnoreArguments();
            servico.Orcamentos = orcamentos;

            Departamento departamento = new Hospital("Barra dor");

            var orcamentosLista = new List<Orcamento.Domain.Orcamento>();
            orcamentosLista.Add(new OrcamentoHospitalar(departamento, 2014));
            orcamentosLista.Add(new OrcamentoHospitalar(departamento, 2014));
            orcamentosLista.Add(new OrcamentoHospitalar(departamento, 2014));
            var orcamento = servico.CriarOrcamentoHospitalar(orcamentosLista, departamento, 2014);

            Assert.AreEqual(orcamento.NomeOrcamento, "Versão4");
        }

        [TestMethod]
        public void CriarOrcamentoComSucessoComQuatroOrcamentoJaCriadoDeveTeNomeOrcamentoIgualAVersaoCinco()
        {
            ServicoCriarOrcamentoHospitalar servico = new ServicoCriarOrcamentoHospitalar();
            IOrcamentos orcamentos = MockRepository.GenerateMock<IOrcamentos>();
            orcamentos.Expect(o => o.Salvar(null)).IgnoreArguments();
            servico.Orcamentos = orcamentos;

            Departamento departamento = new Hospital("Barra dor");

            var orcamentosLista = new List<Orcamento.Domain.Orcamento>();
            orcamentosLista.Add(new OrcamentoHospitalar(departamento, 2014));
            orcamentosLista.Add(new OrcamentoHospitalar(departamento, 2014));
            orcamentosLista.Add(new OrcamentoHospitalar(departamento, 2014));
            orcamentosLista.Add(new OrcamentoHospitalar(departamento, 2014));
            var orcamento = servico.CriarOrcamentoHospitalar(orcamentosLista, departamento, 2014);

            Assert.AreEqual(orcamento.NomeOrcamento, "Versão5");
        }

        [TestMethod]
        public void CriarOrcamentoComSucessoComCincoOrcamentoJaCriadoDeveTeNomeOrcamentoIgualAVersaoSeis()
        {
            ServicoCriarOrcamentoHospitalar servico = new ServicoCriarOrcamentoHospitalar();
            IOrcamentos orcamentos = MockRepository.GenerateMock<IOrcamentos>();
            orcamentos.Expect(o => o.Salvar(null)).IgnoreArguments();
            servico.Orcamentos = orcamentos;

            Departamento departamento = new Hospital("Barra dor");

            var orcamentosLista = new List<Orcamento.Domain.Orcamento>();
            orcamentosLista.Add(new OrcamentoHospitalar(departamento, 2014));
            orcamentosLista.Add(new OrcamentoHospitalar(departamento, 2014));
            orcamentosLista.Add(new OrcamentoHospitalar(departamento, 2014));
            orcamentosLista.Add(new OrcamentoHospitalar(departamento, 2014));
            orcamentosLista.Add(new OrcamentoHospitalar(departamento, 2014));
            var orcamento = servico.CriarOrcamentoHospitalar(orcamentosLista, departamento, 2014);

            Assert.AreEqual(orcamento.NomeOrcamento, "Versão6");
        }

        [TestMethod]
        public void CriarOrcamentoComSucessoComSeisOrcamentoJaCriadoDeveTeNomeOrcamentoIgualAVersaoSete()
        {
            ServicoCriarOrcamentoHospitalar servico = new ServicoCriarOrcamentoHospitalar();
            IOrcamentos orcamentos = MockRepository.GenerateMock<IOrcamentos>();
            orcamentos.Expect(o => o.Salvar(null)).IgnoreArguments();
            servico.Orcamentos = orcamentos;

            Departamento departamento = new Hospital("Barra dor");

            var orcamentosLista = new List<Orcamento.Domain.Orcamento>();
            orcamentosLista.Add(new OrcamentoHospitalar(departamento, 2014));
            orcamentosLista.Add(new OrcamentoHospitalar(departamento, 2014));
            orcamentosLista.Add(new OrcamentoHospitalar(departamento, 2014));
            orcamentosLista.Add(new OrcamentoHospitalar(departamento, 2014));
            orcamentosLista.Add(new OrcamentoHospitalar(departamento, 2014));
            orcamentosLista.Add(new OrcamentoHospitalar(departamento, 2014));
            var orcamento = servico.CriarOrcamentoHospitalar(orcamentosLista, departamento, 2014);

            Assert.AreEqual(orcamento.NomeOrcamento, "Versão7");
        }

        [TestMethod]
        public void CriarOrcamentoComSucessoComSeteOrcamentoJaCriadoDeveTeNomeOrcamentoIgualAVersaoOito()
        {
            ServicoCriarOrcamentoHospitalar servico = new ServicoCriarOrcamentoHospitalar();
            IOrcamentos orcamentos = MockRepository.GenerateMock<IOrcamentos>();
            orcamentos.Expect(o => o.Salvar(null)).IgnoreArguments();
            servico.Orcamentos = orcamentos;

            Departamento departamento = new Hospital("Barra dor");

            var orcamentosLista = new List<Orcamento.Domain.Orcamento>();
            orcamentosLista.Add(new OrcamentoHospitalar(departamento, 2014));
            orcamentosLista.Add(new OrcamentoHospitalar(departamento, 2014));
            orcamentosLista.Add(new OrcamentoHospitalar(departamento, 2014));
            orcamentosLista.Add(new OrcamentoHospitalar(departamento, 2014));
            orcamentosLista.Add(new OrcamentoHospitalar(departamento, 2014));
            orcamentosLista.Add(new OrcamentoHospitalar(departamento, 2014));
            orcamentosLista.Add(new OrcamentoHospitalar(departamento, 2014));
            var orcamento = servico.CriarOrcamentoHospitalar(orcamentosLista, departamento, 2014);

            Assert.AreEqual(orcamento.NomeOrcamento, "Versão8");
        }

        [TestMethod]
        public void CriarOrcamentoComSucessoComOitoOrcamentoJaCriadoDeveTeNomeOrcamentoIgualAVersaoNove()
        {
            ServicoCriarOrcamentoHospitalar servico = new ServicoCriarOrcamentoHospitalar();
            IOrcamentos orcamentos = MockRepository.GenerateMock<IOrcamentos>();
            orcamentos.Expect(o => o.Salvar(null)).IgnoreArguments();
            servico.Orcamentos = orcamentos;

            Departamento departamento = new Hospital("Barra dor");

            var orcamentosLista = new List<Orcamento.Domain.Orcamento>();
            orcamentosLista.Add(new OrcamentoHospitalar(departamento, 2014));
            orcamentosLista.Add(new OrcamentoHospitalar(departamento, 2014));
            orcamentosLista.Add(new OrcamentoHospitalar(departamento, 2014));
            orcamentosLista.Add(new OrcamentoHospitalar(departamento, 2014));
            orcamentosLista.Add(new OrcamentoHospitalar(departamento, 2014));
            orcamentosLista.Add(new OrcamentoHospitalar(departamento, 2014));
            orcamentosLista.Add(new OrcamentoHospitalar(departamento, 2014));
            orcamentosLista.Add(new OrcamentoHospitalar(departamento, 2014));
            var orcamento = servico.CriarOrcamentoHospitalar(orcamentosLista, departamento, 2014);

            Assert.AreEqual(orcamento.NomeOrcamento, "Versão9");
        }

        [TestMethod]
        public void CriarOrcamentoComSucessoComNoveOrcamentoJaCriadoDeveTeNomeOrcamentoIgualAVersaoDez()
        {
            ServicoCriarOrcamentoHospitalar servico = new ServicoCriarOrcamentoHospitalar();
            IOrcamentos orcamentos = MockRepository.GenerateMock<IOrcamentos>();
            orcamentos.Expect(o => o.Salvar(null)).IgnoreArguments();
            servico.Orcamentos = orcamentos;

            Departamento departamento = new Hospital("Barra dor");

            var orcamentosLista = new List<Orcamento.Domain.Orcamento>();
            orcamentosLista.Add(new OrcamentoHospitalar(departamento, 2014));
            orcamentosLista.Add(new OrcamentoHospitalar(departamento, 2014));
            orcamentosLista.Add(new OrcamentoHospitalar(departamento, 2014));
            orcamentosLista.Add(new OrcamentoHospitalar(departamento, 2014));
            orcamentosLista.Add(new OrcamentoHospitalar(departamento, 2014));
            orcamentosLista.Add(new OrcamentoHospitalar(departamento, 2014));
            orcamentosLista.Add(new OrcamentoHospitalar(departamento, 2014));
            orcamentosLista.Add(new OrcamentoHospitalar(departamento, 2014));
            orcamentosLista.Add(new OrcamentoHospitalar(departamento, 2014));
            var orcamento = servico.CriarOrcamentoHospitalar(orcamentosLista, departamento, 2014);

            Assert.AreEqual(orcamento.NomeOrcamento, "Versão10");
        }

        [TestMethod]
        // [ExpectedException(UserMessage = "Orçamento já tem dez versões")]
        public void CriarOrcamentoComSucessoComDezDeveRetornarExecao()
        {
            ServicoCriarOrcamentoHospitalar servico = new ServicoCriarOrcamentoHospitalar();
            IOrcamentos orcamentos = MockRepository.GenerateMock<IOrcamentos>();
            orcamentos.Expect(o => o.Salvar(null)).IgnoreArguments();
            servico.Orcamentos = orcamentos;

            Departamento departamento = new Hospital("Barra dor");

            var orcamentosLista = new List<Orcamento.Domain.Orcamento>();
            orcamentosLista.Add(new OrcamentoHospitalar(departamento, 2014));
            orcamentosLista.Add(new OrcamentoHospitalar(departamento, 2014));
            orcamentosLista.Add(new OrcamentoHospitalar(departamento, 2014));
            orcamentosLista.Add(new OrcamentoHospitalar(departamento, 2014));
            orcamentosLista.Add(new OrcamentoHospitalar(departamento, 2014));
            orcamentosLista.Add(new OrcamentoHospitalar(departamento, 2014));
            orcamentosLista.Add(new OrcamentoHospitalar(departamento, 2014));
            orcamentosLista.Add(new OrcamentoHospitalar(departamento, 2014));
            orcamentosLista.Add(new OrcamentoHospitalar(departamento, 2014));
            orcamentosLista.Add(new OrcamentoHospitalar(departamento, 2014));
            var orcamento = servico.CriarOrcamentoHospitalar(orcamentosLista, departamento, 2014);
        }

        [TestMethod]
        [Ignore]
        public void AtribuirVersaoFinalComSucesso()
        {
            ServicoCriarOrcamentoHospitalar servico = new ServicoCriarOrcamentoHospitalar();
            IOrcamentos orcamentos = MockRepository.GenerateMock<IOrcamentos>();
            orcamentos.Expect(o => o.Salvar(null)).IgnoreArguments();
            orcamentos.Expect(o => o.ObterOrcamentoHospitalarFinal(null)).IgnoreArguments().Return(null);
            servico.Orcamentos = orcamentos;
            Departamento departamento = new Hospital("Barra dor");

            var orcamento = new OrcamentoHospitalar(departamento, 2014);
            servico.AtribuirVersaoFinal(orcamento);

            Assert.IsTrue(orcamento.VersaoFinal);
        }

        [TestMethod]
        public void DeletarOrcamentoComDoisOrcamentosNaListaPrimeiroItemDaListaDeveTerNomeDeVersaoUm()
        {
            ServicoCriarOrcamentoHospitalar servico = new ServicoCriarOrcamentoHospitalar();
            IOrcamentos orcamentos = MockRepository.GenerateMock<IOrcamentos>();
            orcamentos.Expect(o => o.Salvar(null)).IgnoreArguments();
            orcamentos.Expect(o => o.Deletar(null)).IgnoreArguments();
            servico.Orcamentos = orcamentos;
            Departamento departamento = new Hospital("Barra dor");

            var orcamento = new OrcamentoHospitalar(departamento, 2014);

            var orcamentosLista = new List<Orcamento.Domain.Orcamento>();
            orcamentosLista.Add(new OrcamentoHospitalar(departamento, 2014));
            orcamentosLista.Add(orcamento);
            servico.DeletarOrcamento(orcamento, orcamentosLista, departamento);

            Assert.IsTrue(orcamentosLista.FirstOrDefault().NomeOrcamento == "Versão1");
        }

        [TestMethod]
        public void DeletarOrcamentoComDoisOrcamentosNaListaDeveRetornarApenasCountUm()
        {
            ServicoCriarOrcamentoHospitalar servico = new ServicoCriarOrcamentoHospitalar();
            IOrcamentos orcamentos = MockRepository.GenerateMock<IOrcamentos>();
            orcamentos.Expect(o => o.Salvar(null)).IgnoreArguments();
            orcamentos.Expect(o => o.Deletar(null)).IgnoreArguments();
            servico.Orcamentos = orcamentos;
            Departamento departamento = new Hospital("Barra dor");

            var orcamento = new OrcamentoHospitalar(departamento, 2014);

            var orcamentosLista = new List<Orcamento.Domain.Orcamento>();
            orcamentosLista.Add(new OrcamentoHospitalar(departamento, 2014));
            orcamentosLista.Add(orcamento);
            servico.DeletarOrcamento(orcamento, orcamentosLista, departamento);

            Assert.IsTrue(orcamentosLista.Count == 1);
        }
        [TestMethod]
        public void DeletarOrcamentoComDezOrcamentosNaListaDeveRetornarNomesDosORcamentosComSequenciaCerta()
        {
            ServicoCriarOrcamentoHospitalar servico = new ServicoCriarOrcamentoHospitalar();
            IOrcamentos orcamentos = MockRepository.GenerateMock<IOrcamentos>();
            orcamentos.Expect(o => o.Salvar(null)).IgnoreArguments();
            orcamentos.Expect(o => o.Deletar(null)).IgnoreArguments();
            servico.Orcamentos = orcamentos;
            Departamento departamento = new Hospital("Barra dor");
            var orcamento = new OrcamentoHospitalar(departamento, 2014);

            var orcamentosLista = new List<Orcamento.Domain.Orcamento>();
            orcamentosLista.Add(new OrcamentoHospitalar(departamento, 2014));
            orcamentosLista.Add(new OrcamentoHospitalar(departamento, 2014));
            orcamentosLista.Add(new OrcamentoHospitalar(departamento, 2014));
            orcamentosLista.Add(new OrcamentoHospitalar(departamento, 2014));
            orcamentosLista.Add(new OrcamentoHospitalar(departamento, 2014));
            orcamentosLista.Add(new OrcamentoHospitalar(departamento, 2014));
            orcamentosLista.Add(new OrcamentoHospitalar(departamento, 2014));
            orcamentosLista.Add(new OrcamentoHospitalar(departamento, 2014));
            orcamentosLista.Add(new OrcamentoHospitalar(departamento, 2014));
            orcamentosLista.Add(orcamento);
            servico.DeletarOrcamento(orcamento, orcamentosLista, departamento);
            for (int i = 0; i < orcamentosLista.Count; i++)
            {
                Assert.AreEqual(orcamentosLista[i].NomeOrcamento, "Versão" + (i + 1).ToString());
            }
        }
    }
}
