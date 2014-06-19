using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Orcamento.Domain;
using Orcamento.Domain.Gerenciamento;

namespace Orcamento.Test.Domain.Orcamentos
{
    [TestFixture]
    public class OrcamentoOperacionalTest
    {
        [Test]
        public void OrcamentoOperacionalCriadoDeveTerSetorDiferenteDeNulo()
        {
            var setor = new Setor("TI");
            var centroDeCusto = new CentroDeCusto("Gerenciamento de Projetos");
            var RecursosExternos = new Conta("Recursos Externos", new TipoConta { Id = (int)TipoContaEnum.Outros });
            centroDeCusto.AdicionarConta(RecursosExternos);
            setor.AdicionarCentroDeCusto(centroDeCusto);

            var orcamento = new OrcamentoOperacionalVersao(setor, centroDeCusto, 2014);

            Assert.NotNull(orcamento.Setor);
        }

        [Test]
        public void OrcamentoOperacionalCriadoDeveTerCentroDeCustoDiferenteDeNulo()
        {
            var setor = new Setor("TI");
            var centroDeCusto = new CentroDeCusto("Gerenciamento de Projetos");
            var RecursosExternos = new Conta("Recursos Externos", new TipoConta { Id = (int)TipoContaEnum.Outros });
            centroDeCusto.AdicionarConta(RecursosExternos);
            setor.AdicionarCentroDeCusto(centroDeCusto);

            var orcamento = new OrcamentoOperacionalVersao(setor, centroDeCusto, 2014);

            Assert.NotNull(orcamento.CentroDeCusto);
        }

        [Test]
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
