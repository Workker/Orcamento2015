using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Orcamento.Domain;
using Orcamento.Domain.ComponentesDeOrcamento.OrcamentoPessoal;
using Orcamento.Domain.Gerenciamento;
using Orcamento.Domain;

namespace Orcamento.Test.Domain
{
    [TestFixture]
    public class CentroDeCustoTest
    {
        [Test]
        public void iniciar_construtor_com_sucesso()
        {
            CentroDeCusto centro = new CentroDeCusto("centro de custo");
            Assert.Greater(centro.Nome, string.Empty, "Nome centro de custo nao informado");
        }

        [Test]
        public void adicionar_grupo_de_conta_com_sucesso()
        {
            CentroDeCusto centroDeCusto = new CentroDeCusto("kjkh");

            centroDeCusto.Adicionar(new GrupoDeConta("nome"));

            Assert.AreEqual(centroDeCusto.GrupoDeContas.Count, 1);
        }

        [Test]
        public void adicionar_funcionario_com_sucesso()
        {
            CentroDeCusto centroDeCusto = new CentroDeCusto("kjkh");

            centroDeCusto.Adicionar(new Funcionario(new Setor("Nome")));

            Assert.AreEqual(centroDeCusto.Funcionarios.Count, 1);
        }
    }
}
