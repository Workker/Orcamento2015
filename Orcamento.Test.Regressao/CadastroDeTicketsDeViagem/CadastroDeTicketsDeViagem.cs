using NUnit.Framework;

namespace Orcamento.Test.Regressao.CadastroDeTicketsDeViagem
{
    [TestFixture]
    public class CadastroDeTicketsDeViagem : TestBase
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
        //public void Cadastrar_Letras_no_cadastro_tickets_de_viagemTest()
        //{
        //    LogarComUsuarioEmCorporativoTest();
        //    EntrarNoCadastroDeTicketsDeViagemTest();
        //    Selenium.Open("/Tickets.aspx");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl01_txtValor", "Q");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl03_txtValor", "q");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl04_txtValor", "w");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl05_txtValor", "e");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl06_txtValor", "R");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl07_txtValor", "t");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl08_txtValor", "y");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl05_txtValor", "p");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl09_txtValor", "ç");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl09_txtValor", "Ç");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl05_txtValor", "Ã");
        //    Selenium.Click("id=ctl00_MainContent_Button1");
        //    Selenium.WaitForPageToLoad("30000");
        //    DeslogarTest();
        //}

        //[Test]
        //public void Cadastrar_tickets_de_viagemTest()
        //{
        //    LogarComUsuarioEmCorporativoTest();
        //    EntrarNoCadastroDeTicketsDeViagemTest();
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl01_txtValor", "1");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl02_txtValor", "1");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl03_txtValor", "1");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl04_txtValor", "1");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl05_txtValor", "1");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl06_txtValor", "1");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl07_txtValor", "1");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl08_txtValor", "1");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl09_txtValor", "1");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl10_txtValor", "1");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl11_txtValor", "1");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl12_txtValor", "1");
        //    Selenium.Click("id=ctl00_MainContent_Button1");
        //    Selenium.WaitForPageToLoad("30000");
        //    DeslogarTest();
        //}

        //[Test]
        //public void Cadastrar_tickets_de_viagem_campos_com_caracteres_especiaisTest()
        //{
        //    LogarComUsuarioEmCorporativoTest();
        //    EntrarNoCadastroDeTicketsDeViagemTest();
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl08_txtValor", "!");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl01_txtValor", "@");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl02_txtValor", "$");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl03_txtValor", "#");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl04_txtValor", "%");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl05_txtValor", "¨");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl06_txtValor", "&");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl07_txtValor", "*");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl08_txtValor", "(");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl09_txtValor", ")");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl10_txtValor", "_");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl11_txtValor", "+");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl12_txtValor", "=");
        //    Selenium.Click("id=ctl00_MainContent_Button1");
        //    Selenium.WaitForPageToLoad("30000");
        //    DeslogarTest();
        //}

        //[Test]
        //public void Cadastrar_tickets_de_viagem_campos_valor_0Test()
        //{
        //    LogarComUsuarioEmCorporativoTest();
        //    EntrarNoCadastroDeTicketsDeViagemTest();
        //    Selenium.Open("/Tickets.aspx");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl01_txtValor", "0");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl02_txtValor", "0");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl05_txtValor", "0");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl06_txtValor", "0");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl07_txtValor", "0");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl08_txtValor", "0");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl09_txtValor", "0");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl10_txtValor", "0");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl11_txtValor", "0");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl12_txtValor", "0");
        //    Selenium.Click("id=ctl00_MainContent_Button1");
        //    Selenium.WaitForPageToLoad("30000");
        //    DeslogarTest();
        //}

        //[Test]
        //public void Cadastrar_tickets_de_viagem_campos_vaziosTest()
        //{
        //    LogarComUsuarioEmCorporativoTest();
        //    EntrarNoCadastroDeTicketsDeViagemTest();
        //    Selenium.Open("/PainelOrcamento.aspx?alert=on");
        //    Selenium.Click("id=ctl00_MainContent_btnCadastroDeTicketDeViagem");
        //    Selenium.WaitForPageToLoad("30000");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl01_txtValor", "()");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl02_txtValor", "()");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl03_txtValor", "()");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl04_txtValor", "()");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl05_txtValor", "()");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl06_txtValor", "()");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl07_txtValor", "()");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl08_txtValor", "()");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl09_txtValor", "()");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl10_txtValor", "()");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl11_txtValor", "()");
        //    Selenium.Type("id=ctl00_MainContent_rptTickets_ctl12_txtValor", "()");
        //    Selenium.Click("id=ctl00_MainContent_Button1");
        //    Selenium.WaitForPageToLoad("30000");
        //    DeslogarTest();
        //}

        public void EntrarNoCadastroDeTicketsDeViagemTest()
        {
            Driver.Navigate().GoToUrl("/Tickets.aspx");
        }
    }
}