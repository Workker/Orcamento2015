using NUnit.Framework;

namespace Orcamento.Test.Regressao.Funcionario
{
    public class Funcionario : TestBase
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

        #region Test Case

        //[Test]
        //public void Incluir_novo_funcionario_test()
        //{
        //    LogarComUsuarioHospitalarTest();
        //    Selenium.Open("/PainelOrcamento.aspx");
        //    Selenium.Click("id=ctl00_MainContent_btnFuncionario");
        //    Selenium.WaitForPageToLoad("30000");
        //    Selenium.Select("id=ctl00_MainContent_ddlCentrosDeCusto", "label=ADMINISTRACAO DE PESSOAL DEDICADO");
        //    Selenium.WaitForPageToLoad("30000");
        //    Selenium.Type("id=ctl00_MainContent_txtMatricula", "1253");
        //    Selenium.Type("id=ctl00_MainContent_txtNome", "Jose");
        //    Selenium.Type("id=ctl00_MainContent_txtSalario", "8.000,00");
        //    Selenium.Select("id=ctl00_MainContent_ddlDataDeAdmissao", "label=Janeiro");
        //    Selenium.Click("id=ctl00_MainContent_btnIncluirNovoFuncionario");
        //    Selenium.WaitForPageToLoad("30000");
        //    Selenium.Click("id=ctl00_MainContent_btnSalvar");
        //    Selenium.WaitForPageToLoad("30000");
        //    DeslogarTest();
        //}

        //[Test]
        //public void Aumento_de_salarioTest()
        //{
        //    LogarComUsuarioHospitalarTest();
        //    Selenium.Open("/PainelOrcamento.aspx?alert=on");
        //    Selenium.Click("id=ctl00_MainContent_btnFuncionario");
        //    Selenium.WaitForPageToLoad("30000");
        //    Selenium.Select("id=ctl00_MainContent_ddlCentrosDeCusto", "label=ADMINISTRACAO DE PESSOAL DEDICADO");
        //    Selenium.Click("css=option[value=\"011076\"]");
        //    Selenium.WaitForPageToLoad("30000");
        //    Selenium.Click("id=ctl00_MainContent_rptFuncionarios_ctl01_chkAumentado");
        //    Selenium.Type("id=ctl00_MainContent_rptFuncionarios_ctl01_txtPercentualDeAumento", "10");
        //    Selenium.Select("id=ctl00_MainContent_rptFuncionarios_ctl01_ddlMesesDeAumento", "label=Janeiro");
        //    Selenium.Select("id=ctl00_MainContent_rptFuncionarios_ctl01_ddlMesesDeAumento", "label=Março");
        //    Selenium.Click("id=ctl00_MainContent_btnSalvar");
        //    Selenium.WaitForPageToLoad("30000");
        //    DeslogarTest();
        //}

        #endregion

        #region Helpers

        #endregion
    }
}