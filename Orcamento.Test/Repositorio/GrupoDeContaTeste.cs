using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Orcamento.Domain.DB.Repositorio;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Orcamento.TestMethod.Repositorio
{
    [TestClass]
    [Ignore]
    public class GrupoDeContaTestMethode
    {
        [TestMethod]
        // [ExpectedException(UserMessage = "Valor não pode ser nulo.")]
        public void verifica_se_o_retorno_do_repositorio_de_grupo_de_conta_nao_esta_vazio()
        {
            GruposDeConta gruposDeConta = new GruposDeConta();
           // CollectionAssert.IsNotEmpty(gruposDeConta.ObterTodos());
        }

        [TestMethod]
        // [ExpectedException(UserMessage = "Valor não pode ser nulo.")]
        public void verifica_se_todos_itens_do_retorno_do_repositorio_de_grupo_de_conta_nao_estao_nulos()
        {
            GruposDeConta gruposDeConta = new GruposDeConta();
            //CollectionAssert.AllItemsAreIsNotNull(gruposDeConta.ObterTodos());
        }
    }
}
