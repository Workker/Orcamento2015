using NUnit.Framework;
using Orcamento.Domain.Gerenciamento;
using Orcamento.Domain;

namespace Orcamento.Test.Domain
{
    [TestFixture]
    public class ContaTest
    {
        [Test]
        public void iniciar_construtor_com_sucesso() 
        {
            var conta = new Conta("nomeConta", new TipoConta { Id = (int)TipoContaEnum.Outros });
            
            Assert.Greater(conta.Nome, string.Empty);
        }
    }
}
