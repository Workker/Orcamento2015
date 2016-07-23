
using Orcamento.Domain.Gerenciamento;
using Orcamento.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Orcamento.TestMethod.Domain
{
    [TestClass]
    public class ContaTestMethod
    {
        [TestMethod]
        public void iniciar_construtor_com_sucesso() 
        {
            var conta = new Conta("nomeConta", new TipoConta { Id = (int)TipoContaEnum.Outros });
            
           // Assert.Greater(conta.Nome, string.Empty);
        }
    }
}
