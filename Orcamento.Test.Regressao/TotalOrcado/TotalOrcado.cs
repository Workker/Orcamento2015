using NUnit.Framework;
using OpenQA.Selenium;

namespace Orcamento.Test.Regressao.TotalOrcado
{
    public class TotalOrcado : TestBase
    {
        #region Attributes

        private readonly Login.Login login = new Login.Login();

        #endregion

        #region SetUp/TearDown

        [SetUp]
        public override void SetupTest()
        {
            BaseUrl = "http://workker.no-ip.org:8181";
            TestUsingInternetExplorer();
        }

        [TearDown]
        public override void TeardownTest()
        {
            StopTest();
        }

        #endregion

        #region Test Case

        [Test]
        public void Visualisar_total_orcado_test()
        {
            login.LogarComUsuarioEmHospital();
            Driver.FindElement(By.Id("ctl00_MainContent_btnResultadoOrcado")).Click();
            Assert.AreEqual("Orçamento 2014 - Resultado Orçado", Driver.Title);
            login.Deslogar();
        }

        #endregion
    }
}