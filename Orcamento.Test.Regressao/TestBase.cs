using System;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;

namespace Orcamento.Test.Regressao
{
    public abstract class TestBase
    {
        #region Atributes

        protected String BaseUrl;
        protected IWebDriver Driver;
        protected StringBuilder VerificationErrors = new StringBuilder();

        #endregion

        #region Methods

        public void TestUsingInternetExplorer()
        {
            Driver = new InternetExplorerDriver();
        }

        public void TestUsingFirefox()
        {
            Driver = new FirefoxDriver();
        }

        public void StopTest()
        {
            Driver.Quit();
        }

        public abstract void SetupTest();

        public abstract void TeardownTest();

        #endregion
    }
}