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
    public class GrupoContaTestMethod
    {
        [TestMethod]
        public  void adicionar_uma_conta_incrementa_a_colecao_de_contas()
        {
            GrupoDeConta grupoDeConta = new GrupoDeConta("nome");

            Conta conta = new Conta("Conta de TestMethode", new TipoConta { Id = (int)TipoContaEnum.Outros }) { Id = 1 };

            grupoDeConta.Adicionar(conta);

            Assert.AreEqual(grupoDeConta.Contas.Count, 1);
        }

        [TestMethod]
        public void adicionar_uma_conta_verificando_se_a_conta_esta_na_colecao_de_contas()
        {
            GrupoDeConta grupoDeConta = new GrupoDeConta("nome");

            Conta conta = new Conta("Conta de TestMethode", new TipoConta { Id = (int)TipoContaEnum.Outros }) { Id = 1 };

            grupoDeConta.Adicionar(conta);

         //   CollectionAssert.Contains(grupoDeConta.Contas, conta);
        }

    }
}
