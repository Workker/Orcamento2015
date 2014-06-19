using System;
using System.Collections.ObjectModel;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Orcamento.Test.Regressao.Viagem
{
    [TestFixture]
    public class OrcamentoDeViagem : TestBase
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

        private readonly Login.Login login = new Login.Login();

        private void CadastroVersaoDeViagem()
        {
            Driver.FindElement(By.Id("ctl00_MainContent_btnViagemHospedagem")).Click();

            IWebElement selectElement = Driver.FindElement(By.Id("ctl00_MainContent_dropCentrosDeCusto"));
            ReadOnlyCollection<IWebElement> options = selectElement.FindElements(By.TagName("option"));

            foreach (IWebElement option in options)
            {
                if (option.Text.Equals("Contas a Pagar"))
                {
                    option.Click();
                    break;
                }
            }

            Driver.FindElement(By.Id("ctl00_MainContent_btnIncluirNovaVersao")).Click();

            for (int numeroDaLinha = 1; numeroDaLinha <= 6; numeroDaLinha++)
            {
                for (int numeroDaColuna = 0; numeroDaColuna <= 11; numeroDaColuna++)
                {
                    String idDoTextField = "";
                    if (numeroDaColuna.ToString().Length < 2)
                    {
                        idDoTextField = "ctl00_MainContent_rptQuantitativosDeViagem_ctl0" + numeroDaLinha +
                                        "_rptContas_ctl" + "0" + numeroDaColuna + "_valorDespesa";
                    }
                    else
                    {
                        idDoTextField = "ctl00_MainContent_rptQuantitativosDeViagem_ctl0" + numeroDaLinha +
                                        "_rptContas_ctl" + numeroDaColuna + "_valorDespesa";
                    }
                    Driver.FindElement(By.Id(idDoTextField)).Clear();
                    Driver.FindElement(By.Id(idDoTextField)).SendKeys("1");
                }
            }
            Driver.FindElement(By.Id("ctl00_MainContent_btnVersaoFInal")).Click();
        }

        private void DeletarVersao()
        {
            Driver.FindElement(By.Id("ctl00_MainContent_btnApagarVersao")).Click();
            Assert.IsTrue(Driver.SwitchTo().Alert().Text.Equals("Deseja apagar esta versão do orçamento?"));
            Driver.SwitchTo().Alert().Accept();
        }

        [Test]
        public void Cadastrar_versao_de_viagem_com_campos_letras_e_caracteres_test()
        {
            login.LogarComUsuarioEmCorporativo();

            Driver.FindElement(By.Id("ctl00_MainContent_btnViagemHospedagem")).Click();

            //Selenium.Select("id=ctl00_MainContent_dropCentrosDeCusto", "label=Contas a Pagar");
            //Selenium.Click("css=option[value=\"94\"]");

            Driver.FindElement(By.Id("ctl00_MainContent_btnIncluirNovaVersao")).Click();

            //Selenium.Type("id=ctl00_MainContent_rptQuantitativosDeViagem_ctl01_rptContas_ctl00_valorDespesa", "d");
            //Selenium.Type("id=ctl00_MainContent_rptQuantitativosDeViagem_ctl01_rptContas_ctl01_valorDespesa", "t");
            //Selenium.Type("id=ctl00_MainContent_rptQuantitativosDeViagem_ctl01_rptContas_ctl02_valorDespesa", "f");
            //Selenium.Type("id=ctl00_MainContent_rptQuantitativosDeViagem_ctl01_rptContas_ctl03_valorDespesa", "#");
            //Selenium.Type("id=ctl00_MainContent_rptQuantitativosDeViagem_ctl01_rptContas_ctl04_valorDespesa", "@");
            //Selenium.Type("id=ctl00_MainContent_rptQuantitativosDeViagem_ctl01_rptContas_ctl05_valorDespesa", "T");
            //Selenium.Type("id=ctl00_MainContent_rptQuantitativosDeViagem_ctl01_rptContas_ctl06_valorDespesa", "*");
            //Selenium.Type("id=ctl00_MainContent_rptQuantitativosDeViagem_ctl01_rptContas_ctl07_valorDespesa", ")");
            //Selenium.Type("id=ctl00_MainContent_rptQuantitativosDeViagem_ctl01_rptContas_ctl08_valorDespesa", "#");
            //Selenium.Type("id=ctl00_MainContent_rptQuantitativosDeViagem_ctl01_rptContas_ctl09_valorDespesa", ":");
            //Selenium.Type("id=ctl00_MainContent_rptQuantitativosDeViagem_ctl01_rptContas_ctl10_valorDespesa", ";");
            //Selenium.Type("id=ctl00_MainContent_rptQuantitativosDeViagem_ctl01_rptContas_ctl11_valorDespesa", "^~");
            //Selenium.Type("id=ctl00_MainContent_rptQuantitativosDeViagem_ctl02_rptContas_ctl00_valorDespesa", "+");
            //Selenium.Type("id=ctl00_MainContent_rptQuantitativosDeViagem_ctl02_rptContas_ctl01_valorDespesa", "1");
            //Selenium.Type("id=ctl00_MainContent_rptQuantitativosDeViagem_ctl02_rptContas_ctl02_valorDespesa", "D");
            //Selenium.Type("id=ctl00_MainContent_rptQuantitativosDeViagem_ctl02_rptContas_ctl03_valorDespesa", "QW");
            //Selenium.Type("id=ctl00_MainContent_rptQuantitativosDeViagem_ctl02_rptContas_ctl04_valorDespesa", "rt");
            //Selenium.Type("id=ctl00_MainContent_rptQuantitativosDeViagem_ctl02_rptContas_ctl05_valorDespesa", "45");
            //Selenium.Type("id=ctl00_MainContent_rptQuantitativosDeViagem_ctl02_rptContas_ctl06_valorDespesa", "u34");
            //Selenium.Type("id=ctl00_MainContent_rptQuantitativosDeViagem_ctl02_rptContas_ctl07_valorDespesa", "8");
            //Selenium.Type("id=ctl00_MainContent_rptQuantitativosDeViagem_ctl02_rptContas_ctl08_valorDespesa", "09");
            //Selenium.Type("id=ctl00_MainContent_rptQuantitativosDeViagem_ctl02_rptContas_ctl09_valorDespesa", ")(");
            //Selenium.Type("id=ctl00_MainContent_rptQuantitativosDeViagem_ctl02_rptContas_ctl10_valorDespesa", "12");
            //Selenium.Type("id=ctl00_MainContent_rptQuantitativosDeViagem_ctl02_rptContas_ctl11_valorDespesa", "!@");
            //Selenium.Type("id=ctl00_MainContent_rptQuantitativosDeViagem_ctl03_rptContas_ctl00_valorDespesa", "!@");
            //Selenium.Type("id=ctl00_MainContent_rptQuantitativosDeViagem_ctl03_rptContas_ctl01_valorDespesa", "%");
            //Selenium.Type("id=ctl00_MainContent_rptQuantitativosDeViagem_ctl03_rptContas_ctl02_valorDespesa", "¨4");
            //Selenium.Type("id=ctl00_MainContent_rptQuantitativosDeViagem_ctl03_rptContas_ctl03_valorDespesa", "@");
            //Selenium.Type("id=ctl00_MainContent_rptQuantitativosDeViagem_ctl03_rptContas_ctl04_valorDespesa", "*");
            //Selenium.Type("id=ctl00_MainContent_rptQuantitativosDeViagem_ctl03_rptContas_ctl05_valorDespesa", "/");
            //Selenium.Type("id=ctl00_MainContent_rptQuantitativosDeViagem_ctl03_rptContas_ctl06_valorDespesa", "-");
            //Selenium.Type("id=ctl00_MainContent_rptQuantitativosDeViagem_ctl03_rptContas_ctl07_valorDespesa", ".");
            //Selenium.Type("id=ctl00_MainContent_rptQuantitativosDeViagem_ctl03_rptContas_ctl08_valorDespesa", "-*/");
            //Selenium.Type("id=ctl00_MainContent_rptQuantitativosDeViagem_ctl03_rptContas_ctl09_valorDespesa", "!@#");
            //Selenium.Type("id=ctl00_MainContent_rptQuantitativosDeViagem_ctl03_rptContas_ctl10_valorDespesa", "8");
            //Selenium.Type("id=ctl00_MainContent_rptQuantitativosDeViagem_ctl03_rptContas_ctl11_valorDespesa", "34");
            //Selenium.Type("id=ctl00_MainContent_rptQuantitativosDeViagem_ctl04_rptContas_ctl00_valorDespesa", "_+");
            //Selenium.Type("id=ctl00_MainContent_rptQuantitativosDeViagem_ctl04_rptContas_ctl01_valorDespesa", "(*");
            //Selenium.Type("id=ctl00_MainContent_rptQuantitativosDeViagem_ctl04_rptContas_ctl02_valorDespesa", "+");
            //Selenium.Type("id=ctl00_MainContent_rptQuantitativosDeViagem_ctl04_rptContas_ctl03_valorDespesa", "=");
            //Selenium.Type("id=ctl00_MainContent_rptQuantitativosDeViagem_ctl04_rptContas_ctl04_valorDespesa", "3");
            //Selenium.Type("id=ctl00_MainContent_rptQuantitativosDeViagem_ctl04_rptContas_ctl05_valorDespesa", "*&");
            //Selenium.Type("id=ctl00_MainContent_rptQuantitativosDeViagem_ctl04_rptContas_ctl06_valorDespesa", "#");
            //Selenium.Type("id=ctl00_MainContent_rptQuantitativosDeViagem_ctl04_rptContas_ctl07_valorDespesa", "M");
            //Selenium.Type("id=ctl00_MainContent_rptQuantitativosDeViagem_ctl04_rptContas_ctl08_valorDespesa", "Ç");
            //Selenium.Type("id=ctl00_MainContent_rptQuantitativosDeViagem_ctl04_rptContas_ctl09_valorDespesa", "ç");
            //Selenium.Type("id=ctl00_MainContent_rptQuantitativosDeViagem_ctl04_rptContas_ctl10_valorDespesa", "[");
            //Selenium.Type("id=ctl00_MainContent_rptQuantitativosDeViagem_ctl04_rptContas_ctl11_valorDespesa", "]");
            //Selenium.Type("id=ctl00_MainContent_rptQuantitativosDeViagem_ctl05_rptContas_ctl00_valorDespesa", "{");
            //Selenium.Type("id=ctl00_MainContent_rptQuantitativosDeViagem_ctl05_rptContas_ctl01_valorDespesa", "}");
            //Selenium.Type("id=ctl00_MainContent_rptQuantitativosDeViagem_ctl05_rptContas_ctl02_valorDespesa", "1");
            //Selenium.Type("id=ctl00_MainContent_rptQuantitativosDeViagem_ctl05_rptContas_ctl03_valorDespesa", "e");
            //Selenium.Type("id=ctl00_MainContent_rptQuantitativosDeViagem_ctl05_rptContas_ctl04_valorDespesa", "7");
            //Selenium.Type("id=ctl00_MainContent_rptQuantitativosDeViagem_ctl05_rptContas_ctl05_valorDespesa", "g");
            //Selenium.Type("id=ctl00_MainContent_rptQuantitativosDeViagem_ctl05_rptContas_ctl06_valorDespesa", "r");
            //Selenium.Type("id=ctl00_MainContent_rptQuantitativosDeViagem_ctl05_rptContas_ctl07_valorDespesa", "p");
            //Selenium.Type("id=ctl00_MainContent_rptQuantitativosDeViagem_ctl05_rptContas_ctl08_valorDespesa", "S");
            //Selenium.Type("id=ctl00_MainContent_rptQuantitativosDeViagem_ctl05_rptContas_ctl09_valorDespesa", "B");
            //Selenium.Type("id=ctl00_MainContent_rptQuantitativosDeViagem_ctl05_rptContas_ctl10_valorDespesa", "b");
            //Selenium.Type("id=ctl00_MainContent_rptQuantitativosDeViagem_ctl05_rptContas_ctl11_valorDespesa", "V");
            //Selenium.Type("id=ctl00_MainContent_rptQuantitativosDeViagem_ctl06_rptContas_ctl00_valorDespesa", "c");
            //Selenium.Type("id=ctl00_MainContent_rptQuantitativosDeViagem_ctl06_rptContas_ctl01_valorDespesa", "D");
            //Selenium.Type("id=ctl00_MainContent_rptQuantitativosDeViagem_ctl06_rptContas_ctl02_valorDespesa", ">");
            //Selenium.Type("id=ctl00_MainContent_rptQuantitativosDeViagem_ctl06_rptContas_ctl03_valorDespesa", "<");
            //Selenium.Type("id=ctl00_MainContent_rptQuantitativosDeViagem_ctl06_rptContas_ctl04_valorDespesa", "?");
            //Selenium.Type("id=ctl00_MainContent_rptQuantitativosDeViagem_ctl06_rptContas_ctl05_valorDespesa", "e");
            //Selenium.Type("id=ctl00_MainContent_rptQuantitativosDeViagem_ctl06_rptContas_ctl06_valorDespesa", "P");
            //Selenium.Type("id=ctl00_MainContent_rptQuantitativosDeViagem_ctl06_rptContas_ctl07_valorDespesa", "W");
            //Selenium.Type("id=ctl00_MainContent_rptQuantitativosDeViagem_ctl06_rptContas_ctl08_valorDespesa", "à");
            //Selenium.Type("id=ctl00_MainContent_rptQuantitativosDeViagem_ctl06_rptContas_ctl09_valorDespesa", "Á");
            //Selenium.Type("id=ctl00_MainContent_rptQuantitativosDeViagem_ctl06_rptContas_ctl10_valorDespesa", "e");
            //Selenium.Type("id=ctl00_MainContent_rptQuantitativosDeViagem_ctl06_rptContas_ctl11_valorDespesa", "e");

            Driver.FindElement(By.Id("ctl00_MainContent_btnVersaoFInal")).Click();

            //Assert.IsTrue(Regex.IsMatch(Selenium.GetConfirmation(), "^Deseja atribuir este orçamento como versão final[\\s\\S]$"));

            DeletarVersao();
            login.Deslogar();
        }

        [Test]
        public void Cadastrar_versao_de_viagem_com_campos_vazios_test()
        {
            login.LogarComUsuarioEmCorporativo();

            Driver.FindElement(By.Id("ctl00_MainContent_btnViagemHospedagem")).Click();


            //Selenium.Select("id=ctl00_MainContent_dropCentrosDeCusto", "label=Contas a Pagar");
            //Selenium.Click("css=option[value=\"94\"]");


            Driver.FindElement(By.Id("ctl00_MainContent_btnIncluirNovaVersao")).Click();

            for (int numeroDaLinha = 1; numeroDaLinha <= 6; numeroDaLinha++)
            {
                for (int numeroDaColuna = 0; numeroDaColuna <= 11; numeroDaColuna++)
                {
                    String idDoTextField = "";
                    if (numeroDaColuna.ToString().Length < 2)
                    {
                        idDoTextField = "ctl00_MainContent_rptQuantitativosDeViagem_ctl0" + numeroDaLinha +
                                        "_rptContas_ctl" + "0" + numeroDaColuna + "_valorDespesa";
                    }
                    else
                    {
                        idDoTextField = "ctl00_MainContent_rptQuantitativosDeViagem_ctl0" + numeroDaLinha +
                                        "_rptContas_ctl" + numeroDaColuna + "_valorDespesa";
                    }
                    Driver.FindElement(By.Id(idDoTextField)).Clear();
                }
            }

            Driver.FindElement(By.Id("ctl00_MainContent_btnVersaoFInal")).Click();

            //Assert.IsTrue(Regex.IsMatch(Selenium.GetConfirmation(), "^Deseja atribuir este orçamento como versão final[\\s\\S]$"));

            DeletarVersao();
            login.Deslogar();
        }

        [Test]
        public void Cadastro_de_Viagem_hospedagemTest()
        {
            login.LogarComUsuarioEmCorporativo();
            CadastroVersaoDeViagem();

            Assert.IsTrue(Driver.SwitchTo().Alert().Text.Equals("Deseja atribuir este orçamento como versão final?"));

            DeletarVersao();
            login.Deslogar();
        }

        [Test]
        public void Editar_cadastro_de_versao_de_viagemTest()
        {
            login.LogarComUsuarioEmCorporativo();
            CadastroVersaoDeViagem();

            Assert.IsTrue(Driver.SwitchTo().Alert().Text.Equals("Deseja atribuir este orçamento como versão final?"));

            for (int numeroDaLinha = 1; numeroDaLinha <= 6; numeroDaLinha++)
            {
                for (int numeroDaColuna = 0; numeroDaColuna <= 11; numeroDaColuna++)
                {
                    String idDoTextField = "";
                    if (numeroDaColuna.ToString().Length < 2)
                    {
                        idDoTextField = "ctl00_MainContent_rptQuantitativosDeViagem_ctl0" + numeroDaLinha +
                                        "_rptContas_ctl" + "0" + numeroDaColuna + "_valorDespesa";
                    }
                    else
                    {
                        idDoTextField = "ctl00_MainContent_rptQuantitativosDeViagem_ctl0" + numeroDaLinha +
                                        "_rptContas_ctl" + numeroDaColuna + "_valorDespesa";
                    }
                    Driver.FindElement(By.Id(idDoTextField)).Clear();
                    Driver.FindElement(By.Id(idDoTextField)).SendKeys("8");
                }
            }

            Driver.FindElement(By.Id("ctl00_MainContent_btnVersaoFInal")).Click();

            //Assert.IsTrue(Regex.IsMatch(Selenium.GetConfirmation(), "^Deseja atribuir este orçamento como versão final[\\s\\S]$"));

            DeletarVersao();
            login.Deslogar();
        }
    }
}