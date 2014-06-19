using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Orcamento.Domain;
using Orcamento.Domain.Gerenciamento;

namespace Orcamento.Test.Domain
{
    [TestFixture]
    public class DespesaOperacionalTest
    {
        [Test]
        public void iniciar_construtor_com_sucesso() 
        {
            OrcamentoOperacionalVersao despesaOperacional = new OrcamentoOperacionalVersao(new Setor("setor"), new CentroDeCusto("centro de custo"), 2014);

            Assert.NotNull(despesaOperacional.Setor, "Setor não informado");
            Assert.NotNull(despesaOperacional.CentroDeCusto, "Centro de Custo não informado");
            Assert.Greater(despesaOperacional.Ano,0,"Ano do orcamento nao informada");
        }
    }
}
