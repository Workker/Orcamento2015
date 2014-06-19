using NUnit.Framework;
using Orcamento.Domain;
using Orcamento.Domain.Gerenciamento;

namespace Orcamento.Test.Domain
{
    [TestFixture]
    public class DespesaTest
    {
        [Test]
        public void iniciar_construtor_com_sucesso()
        {
            var despesa = new Despesa(MesEnum.Abril, new Conta("nome", new TipoConta { Id = (int)TipoContaEnum.Outros }));

            Assert.Greater((int) despesa.Mes, 0, "mes não informado.");
            Assert.NotNull(despesa.Conta,"Conta não informada.");
        }
    }
}
