using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Orcamento.Domain;
using Orcamento.Domain.Gerenciamento;

namespace Orcamento.Test.Domain.Gerenciamento
{
    [TestFixture]
    public class GrupoContaTest
    {
        [Test]
        public  void adicionar_uma_conta_incrementa_a_colecao_de_contas()
        {
            GrupoDeConta grupoDeConta = new GrupoDeConta("nome");

            Conta conta = new Conta("Conta de teste", new TipoConta { Id = (int)TipoContaEnum.Outros }) { Id = 1 };

            grupoDeConta.Adicionar(conta);

            Assert.AreEqual(grupoDeConta.Contas.Count, 1);
        }

        [Test]
        public void adicionar_uma_conta_verificando_se_a_conta_esta_na_colecao_de_contas()
        {
            GrupoDeConta grupoDeConta = new GrupoDeConta("nome");

            Conta conta = new Conta("Conta de teste", new TipoConta { Id = (int)TipoContaEnum.Outros }) { Id = 1 };

            grupoDeConta.Adicionar(conta);

            CollectionAssert.Contains(grupoDeConta.Contas, conta);
        }

    }
}
