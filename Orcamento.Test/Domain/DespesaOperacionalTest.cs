using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Orcamento.Domain;
using Orcamento.Domain.Gerenciamento;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Orcamento.TestMethod.Domain
{
    [TestClass]
    public class DespesaOperacionalTestMethod
    {
        [TestMethod]
        public void iniciar_construtor_com_sucesso() 
        {
            OrcamentoOperacionalVersao despesaOperacional = new OrcamentoOperacionalVersao(new Setor("setor"), new CentroDeCusto("centro de custo"), 2014);

            Assert.IsNotNull(despesaOperacional.Setor, "Setor não informado");
            Assert.IsNotNull(despesaOperacional.CentroDeCusto, "Centro de Custo não informado");
            //Assert.Greater(despesaOperacional.Ano,0,"Ano do orcamento nao informada");
        }
    }
}
