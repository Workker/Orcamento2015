using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Orcamento.Domain;
using Orcamento.Domain.Gerenciamento;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Orcamento.TestMethod.Domain.Orcamentos
{
    [TestClass]
    public class OrcamentoOperacionalTestMethod
    {
        [TestMethod]
        public void OrcamentoOperacionalCriadoDeveTerSetorDiferenteDeNulo()
        {
            var setor = new Setor("TI");
            var centroDeCusto = new CentroDeCusto("Gerenciamento de Projetos");
            var RecursosExternos = new Conta("Recursos Externos", new TipoConta { Id = (int)TipoContaEnum.Outros });
            centroDeCusto.AdicionarConta(RecursosExternos);
            setor.AdicionarCentroDeCusto(centroDeCusto);

            var orcamento = new OrcamentoOperacionalVersao(setor, centroDeCusto, 2014);

            Assert.IsNotNull(orcamento.Setor);
        }

        [TestMethod]
        public void OrcamentoOperacionalCriadoDeveTerCentroDeCustoDiferenteDeNulo()
        {
            var setor = new Setor("TI");
            var centroDeCusto = new CentroDeCusto("Gerenciamento de Projetos");
            var RecursosExternos = new Conta("Recursos Externos", new TipoConta { Id = (int)TipoContaEnum.Outros });
            centroDeCusto.AdicionarConta(RecursosExternos);
            setor.AdicionarCentroDeCusto(centroDeCusto);

            var orcamento = new OrcamentoOperacionalVersao(setor, centroDeCusto, 2014);

            Assert.IsNotNull(orcamento.CentroDeCusto);
        }

        [TestMethod]
        public void CriarDespesasComUmCentroDeCustoComUmaContaDeveTerDespesasIguamADoze()
        {
            var setor = new Setor("TI");
            var centroDeCusto = new CentroDeCusto("Gerenciamento de Projetos");
            var RecursosExternos = new Conta("Recursos Externos", new TipoConta { Id = (int)TipoContaEnum.Outros });
            centroDeCusto.AdicionarConta(RecursosExternos);
            setor.AdicionarCentroDeCusto(centroDeCusto);

            var orcamento = new OrcamentoOperacionalVersao(setor, centroDeCusto, 2014);

            orcamento.CriarDespesas();
            Assert.IsTrue(orcamento.DespesasOperacionais.Count == 12);
        }

    }
}
