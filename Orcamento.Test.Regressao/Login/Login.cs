using System.Collections.ObjectModel;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Orcamento.Test.Regressao.Login
{
    [TestFixture]
    public class Login : TestBase
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

        public void Deslogar()
        {
            Driver.FindElement(By.Id("ctl00_btnLogout")).Click();
        }

        public void LogarComUsuarioEmCorporativo()
        {
            Driver.Navigate().GoToUrl(BaseUrl + "/Login.aspx");
            Driver.FindElement(By.Id("txtlogin")).SendKeys("isaac");
            Driver.FindElement(By.Id("txtSenha")).SendKeys("123456");
            Driver.FindElement(By.Id("ctl00_MainContent_btnLogar")).Click();

            IWebElement selectElement = Driver.FindElement(By.Id("ctl00_MainContent_ddlDepartamentos"));
            ReadOnlyCollection<IWebElement> options = selectElement.FindElements(By.TagName("option"));

            foreach (IWebElement option in options)
            {
                if (option.Text.Equals("TI"))
                {
                    option.Click();
                    break;
                }
            }
        }

        public void LogarComUsuarioEmHospital()
        {
            Driver.Navigate().GoToUrl(BaseUrl + "/Login.aspx");
            Driver.FindElement(By.Id("txtlogin")).SendKeys("isaac");
            Driver.FindElement(By.Id("txtSenha")).SendKeys("123456");
            Driver.FindElement(By.Id("ctl00_MainContent_btnLogar")).Click();

            IWebElement selectElement = Driver.FindElement(By.Id("ctl00_MainContent_ddlDepartamentos"));
            ReadOnlyCollection<IWebElement> options = selectElement.FindElements(By.TagName("option"));

            foreach (IWebElement option in options)
            {
                if (option.Text.Equals("BARRA D'OR"))
                {
                    option.Click();
                    break;
                }
            }
        }

        [Test]
        public void a_Verificar_titulo_da_pagina_de_login()
        {
            Driver.Navigate().GoToUrl(BaseUrl + "/");
            Assert.AreEqual("Orçamento 2014 - Login", Driver.Title);
        }

        [Test]
        public void b_Tentar_logar_sem_informar_usuario_e_senha()
        {
            Driver.Navigate().GoToUrl(BaseUrl + "/");
            Driver.FindElement(By.Id("ctl00_MainContent_btnLogar")).Click();

            Assert.AreEqual("O campo login deve ser preenchido.", Driver.SwitchTo().Alert().Text);
        }

        [Test]
        public void c_Tentar_logar_informando_apenas_o_nome_do_usuario()
        {
            Driver.Navigate().GoToUrl(BaseUrl + "/");
            Driver.FindElement(By.Id("txtlogin")).SendKeys("isaac");
            Driver.FindElement(By.Id("ctl00_MainContent_btnLogar")).Click();

            Assert.AreEqual("O campo senha deve ser preenchido.", Driver.SwitchTo().Alert().Text);
        }

        [Test]
        public void d_Tentar_logar_informando_apenas_uma_senha()
        {
            Driver.Navigate().GoToUrl(BaseUrl + "/Login.aspx");
            Driver.FindElement(By.Id("txtSenha")).SendKeys("123456");
            Driver.FindElement(By.Id("ctl00_MainContent_btnLogar")).Click();

            Assert.AreEqual("O campo login deve ser preenchido.", Driver.SwitchTo().Alert().Text);
        }

        [Test]
        public void e_Logando_com_usuario_administrativo()
        {
            Driver.Navigate().GoToUrl(BaseUrl + "/");
            Driver.FindElement(By.Id("txtlogin")).SendKeys("isaac");
            Driver.FindElement(By.Id("txtSenha")).SendKeys("123456");
            Driver.FindElement(By.Id("ctl00_MainContent_btnLogar")).Click();
            try
            {
                Assert.IsTrue(Driver.PageSource.Contains("Isaac"));
            }
            catch (AssertionException e)
            {
                VerificationErrors.Append(e.Message);
            }
            try
            {
                Assert.IsTrue(Driver.PageSource.Contains("Selecione a Unidade / Diretoria"));
            }
            catch (AssertionException e)
            {
                VerificationErrors.Append(e.Message);
            }

            Deslogar();
        }

        [Test]
        public void f_Logando_com_usuario_administrativo_no_hospital()
        {
            Driver.Navigate().GoToUrl(BaseUrl + "/Login.aspx");
            Driver.FindElement(By.Id("txtlogin")).SendKeys("isaac");
            Driver.FindElement(By.Id("txtSenha")).SendKeys("123456");
            Driver.FindElement(By.Id("ctl00_MainContent_btnLogar")).Click();

            IWebElement selectElement = Driver.FindElement(By.Id("ctl00_MainContent_ddlDepartamentos"));
            ReadOnlyCollection<IWebElement> options = selectElement.FindElements(By.TagName("option"));

            foreach (IWebElement option in options)
            {
                if (option.Text.Equals("BARRA D'OR"))
                {
                    option.Click();
                    break;
                }
            }

            Deslogar();
        }
    }
}