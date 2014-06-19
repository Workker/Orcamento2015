using System.Collections.Generic;
using NUnit.Framework;
using Orcamento.Domain;
using Orcamento.Domain.ComponentesDeOrcamento.OrcamentoPessoal.Despesas;
using Orcamento.Domain.ComponentesDeOrcamento.OrcamentoPessoal.Despesas.Normal;
using Orcamento.Domain.Gerenciamento;
using Orcamento.Domain.ComponentesDeOrcamento.OrcamentoPessoal;

namespace Orcamento.Test.Domain.Orcamentos.OrcamentoPessoalTest
{
    [TestFixture]
    public class FeriasTeste
    {
        [Test]
        public void calculo_de_ferias_gera_doze_parcelas()
        {
            Conta conta = new Conta("Test", new TipoConta { Id = 1 });
          

            var ferias = new Ferias(conta, new NovoOrcamentoPessoal(new Setor("nome"), new CentroDeCusto("novo"), 2014));

            ferias.Calcular(100,1,0,0);

            Assert.AreEqual(ferias.Parcelas.Count, 12);
        }

        [Test]
        public void calcular_ferias_de_um_ano_deve_ter_10_reais_de_parcela()
        {
            Conta conta = new Conta("Test", new TipoConta { Id = 1 });


            var ferias = new Ferias(conta, new NovoOrcamentoPessoal(new Setor("nome"), new CentroDeCusto("novo"), 2014));

            ferias.Calcular(100, 1, 0, 0);

            var parcela = new Parcela { Mes = 1, Valor = 11.110833333333334d };

            CollectionAssert.Contains(ferias.Parcelas, parcela);
        }
    }
}
