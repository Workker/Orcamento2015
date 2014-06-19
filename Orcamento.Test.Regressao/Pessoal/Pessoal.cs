using NUnit.Framework;

namespace Orcamento.Test.Regressao.Pessoal
{
    public class Pessoal : TestBase
    {
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

        #region Test Cases

        //[Test]
        //public void Verificar_titulo_pessoal()
        //{
        //    Selenium.Open("/Login.aspx");
        //    Selenium.Type("id=txtlogin", "mpina");
        //    Selenium.Type("id=txtSenha", "123456");
        //    Selenium.Click("id=ctl00_MainContent_btnLogar");
        //    Selenium.WaitForPageToLoad("30000");
        //    Selenium.Select("id=ctl00_MainContent_ddlDepartamentos", "label=QUINTA D'OR");
        //    Selenium.Click("css=option[value=\"2\"]");
        //    Selenium.WaitForPageToLoad("30000");
        //    Assert.AreEqual("Orçamento 2014 - Pessoal", Selenium.GetTitle());
        //    DeslogarTest();

        //}

        #endregion

        #region Helppers

        #endregion
    }
}