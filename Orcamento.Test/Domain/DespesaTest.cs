using Microsoft.VisualStudio.TestTools.UnitTesting;

using Orcamento.Domain;
using Orcamento.Domain.Gerenciamento;

namespace Orcamento.TestMethod.Domain
{
    [TestClass]
    public class DespesaTestMethod
    {
        [TestMethod]
        public void iniciar_construtor_com_sucesso()
        {
            var despesa = new Despesa(MesEnum.Abril, new Conta("nome", new TipoConta { Id = (int)TipoContaEnum.Outros }));

           // Assert.Greater((int) despesa.Mes, 0, "mes não informado.");
            Assert.IsNotNull(despesa.Conta,"Conta não informada.");
        }
    }
}
