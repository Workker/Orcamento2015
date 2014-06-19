using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Orcamento.Domain.DB.Repositorio;

namespace Orcamento.Test.Repositorio
{
    [TestFixture]
    [Ignore]
    public class GrupoDeContaTeste
    {
        [Test]
        [ExpectedException(UserMessage = "Valor não pode ser nulo.")]
        public void verifica_se_o_retorno_do_repositorio_de_grupo_de_conta_nao_esta_vazio()
        {
            GruposDeConta gruposDeConta = new GruposDeConta();
            CollectionAssert.IsNotEmpty(gruposDeConta.ObterTodos());
        }

        [Test]
        [ExpectedException(UserMessage = "Valor não pode ser nulo.")]
        public void verifica_se_todos_itens_do_retorno_do_repositorio_de_grupo_de_conta_nao_estao_nulos()
        {
            GruposDeConta gruposDeConta = new GruposDeConta();
            CollectionAssert.AllItemsAreNotNull(gruposDeConta.ObterTodos());
        }
    }
}
