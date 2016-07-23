using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Orcamento.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Orcamento.TestMethod.Domain
{
    [TestClass]
    public class SetorTestMethod
    {
        [TestMethod]
        public void iniciar_constrututor_com_sucesso() 
        {
            Setor setor = new Setor("setor");

            //Assert.Greater(setor.Nome, string.Empty, "Nome do setor não informado");
        }
    }
}
