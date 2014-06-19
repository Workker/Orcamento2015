using NUnit.Framework;

namespace Orcamento.Test.Regressao.CadastroDeTicketsDePessoal
{
    [TestFixture]
    public class CadastroDeTicketsDePessoal : TestBase
    {
        #region Setup/Teardown

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

        //[Test]
        //public void Cadastrar_Letras_e_caracteres_no_cadastro_tickets_de_pessoal_corporativo_test()
        //{
        //    LogarComUsuarioEmCorporativo();
        //    Selenium.Open("/Pessoal/CadastroDeTicketsPessoal.aspx");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl02_txtValor", "w");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl03_txtValor", "A");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl04_txtValor", "p");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl05_txtValor", "1U");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl06_txtValor", "%");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl07_txtValor", "d");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl08_txtValor", "#1");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl09_txtValor", "(");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl10_txtValor", "1Ç");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl11_txtValor", "ç");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl12_txtValor", "\"");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl13_txtValor", "!");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl15_txtValor", "+");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl16_txtValor", "=");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl17_txtValor", "*");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl14_txtValor", "14%$#");
        //    Selenium.Click("id=ctl00_MainContent_btnSalvarTickets");
        //    Selenium.WaitForPageToLoad("30000");
        //    Assert.AreEqual("Ops!!!, Ocorreu um Problema... Tente Novamente!!!", Selenium.GetText("css=h2"));
        //    DeslogarTest();
        //}

        //[Test]
        //public void Cadastrar_tickets_de_pessoal_corporativo_campos_valor_0_test()
        //{
        //    LogarComUsuarioEmCorporativo();
        //    Selenium.Open("/Pessoal/CadastroDeTicketsPessoal.aspx");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl01_txtValor", "0");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl02_txtValor", "0");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl03_txtValor", "0");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl04_txtValor", "0");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl05_txtValor", "0");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl06_txtValor", "0");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl07_txtValor", "0");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl08_txtValor", "0");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl09_txtValor", "0");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl10_txtValor", "0");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl11_txtValor", "0");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl12_txtValor", "0");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl13_txtValor", "0");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl14_txtValor", "0");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl15_txtValor", "0");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl16_txtValor", "0");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl17_txtValor", "0");
        //    Selenium.Click("id=ctl00_MainContent_btnSalvarTickets");
        //    Selenium.WaitForPageToLoad("30000");
        //    DeslogarTest();
        //}

        //[Test]
        //public void Cadastrar_tickets_de_pessoal_corporativo_campos_vazios_test()
        //{
        //    LogarComUsuarioEmCorporativo();
        //    Selenium.Open("/Pessoal/CadastroDeTicketsPessoal.aspx");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl14_txtValor", "");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl15_txtValor", "");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl17_txtValor", "");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl01_txtValor", "");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl02_txtValor", "");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl06_txtValor", "");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl07_txtValor", "");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl12_txtValor", "");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl13_txtValor", "");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl16_txtValor", "");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl17_txtValor", "");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl03_txtValor", "");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl10_txtValor", "");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl13_txtValor", "");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl14_txtValor", "");
        //    Selenium.Click("id=ctl00_MainContent_btnSalvarTickets");
        //    Selenium.WaitForPageToLoad("30000");
        //    Assert.AreEqual("Ops!!!, Ocorreu um Problema... Tente Novamente!!!", Selenium.GetText("css=h2"));
        //    DeslogarTest();
        //}

        //[Test]
        //public void Cadastrar_Letras_e_caracteres_no_cadastro_tickets_de_pessoal_hospitalar_test()
        //{
        //    LogandoComUsuarioAdministrativoNoHospitalTest();
        //    Selenium.Open("/Pessoal/CadastroDeTicketsPessoal.aspx");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl02_txtValor", "w");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl03_txtValor", "A");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl04_txtValor", "p");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl05_txtValor", "1U");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl06_txtValor", "%");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl07_txtValor", "d");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl08_txtValor", "#1");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl09_txtValor", "(");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl10_txtValor", "1Ç");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl11_txtValor", "ç");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl12_txtValor", "\"");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl13_txtValor", "!");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl15_txtValor", "+");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl16_txtValor", "=");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl17_txtValor", "*");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl14_txtValor", "14%$#");
        //    Selenium.Click("id=ctl00_MainContent_btnSalvarTickets");
        //    Selenium.WaitForPageToLoad("30000");
        //    Assert.AreEqual("Ops!!!, Ocorreu um Problema... Tente Novamente!!!", Selenium.GetText("css=h2"));
        //}

        //[Test]
        //public void Cadastrar_tickets_de_pessoal_hospitalar_campos_valor_0_test()
        //{
        //    LogandoComUsuarioAdministrativoNoHospitalTest();
        //    Selenium.Open("/Pessoal/CadastroDeTicketsPessoal.aspx");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl01_txtValor", "0");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl02_txtValor", "0");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl03_txtValor", "0");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl04_txtValor", "0");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl05_txtValor", "0");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl06_txtValor", "0");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl07_txtValor", "0");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl08_txtValor", "0");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl09_txtValor", "0");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl10_txtValor", "0");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl11_txtValor", "0");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl12_txtValor", "0");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl13_txtValor", "0");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl14_txtValor", "0");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl15_txtValor", "0");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl16_txtValor", "0");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl17_txtValor", "0");
        //    Selenium.Click("id=ctl00_MainContent_btnSalvarTickets");
        //    Selenium.WaitForPageToLoad("30000");
        //    DeslogarTest();
        //}

        //[Test]
        //public void Cadastrar_tickets_de_pessoal_hospitalar_com_campos_vazios_Test()
        //{
        //    LogandoComUsuarioAdministrativoNoHospitalTest();
        //    Selenium.Open("/Pessoal/CadastroDeTicketsPessoal.aspx");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl14_txtValor", "");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl15_txtValor", "");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl17_txtValor", "");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl01_txtValor", "");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl02_txtValor", "");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl06_txtValor", "");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl07_txtValor", "");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl12_txtValor", "");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl13_txtValor", "");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl16_txtValor", "");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl17_txtValor", "");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl03_txtValor", "");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl10_txtValor", "");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl13_txtValor", "");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl14_txtValor", "");
        //    Selenium.Click("id=ctl00_MainContent_btnSalvarTickets");
        //    Selenium.WaitForPageToLoad("30000");
        //    Assert.AreEqual("Ops!!!, Ocorreu um Problema... Tente Novamente!!!", Selenium.GetText("css=h2"));
        //    DeslogarTest();
        //}
    }
}