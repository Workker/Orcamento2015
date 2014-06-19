using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Orcamento.Domain;

namespace Orcamento.Test.Domain
{
    [TestFixture]
    public class SetorTest
    {
        [Test]
        public void iniciar_constrututor_com_sucesso() 
        {
            Setor setor = new Setor("setor");

            Assert.Greater(setor.Nome, string.Empty, "Nome do setor não informado");
        }
    }
}
