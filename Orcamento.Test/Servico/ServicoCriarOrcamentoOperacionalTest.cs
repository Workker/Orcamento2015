using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Orcamento.Domain.Gerenciamento;
using Orcamento.Domain.Servico;
using Orcamento.Domain;
using Rhino.Mocks;
using Orcamento.Domain.DB.Repositorio;

namespace Orcamento.Test.Servico
{
    [TestFixture]
    public class ServicoCriarOrcamentoOperacionalTest
    {
        [Test]
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

        [Test]
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

        [Test]
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

        [Test]
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

        [Test]
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

        [Test]
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

        [Test]
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

        [Test]
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

        [Test]
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

        [Test]
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

        [Test]
        [ExpectedException(UserMessage = "Orçamento já tem dez versões")]
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

        [Test]
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

        [Test]
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

        [Test]
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
        [Test]
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
