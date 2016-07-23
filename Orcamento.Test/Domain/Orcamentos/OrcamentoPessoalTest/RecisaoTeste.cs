using System.Collections.Generic;

using Orcamento.Domain;
using Orcamento.Domain.ComponentesDeOrcamento.OrcamentoPessoal.Despesas;
using Orcamento.Domain.ComponentesDeOrcamento.OrcamentoPessoal.Despesas.Demitido;
using Orcamento.Domain.Gerenciamento;
using Orcamento.Domain.ComponentesDeOrcamento.OrcamentoPessoal;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Orcamento.TestMethod.Domain.Orcamentos.OrcamentoPessoalTestMethod
{
    [TestClass]
    public class RecisaoTestMethode
    {
        [TestMethod]
        public void valor_da_parcela_de_recisao_calculada_com_sucesso()
        {
            var orcamento = new NovoOrcamentoPessoal(new Setor("Nome"),new CentroDeCusto("nome"),2014);
            Conta conta = new Conta("TestMethod", new TipoConta { Id = 1 });
            orcamento.Adicionar(new TicketDeOrcamentoPessoal(new Setor("nome")) { Descricao = "Indenização", Valor = 1 });

            Recisao recisao = new Recisao(2, conta, orcamento);

            recisao.Calcular(2, 1, 0, 0);

            Parcela parcela = new Parcela
                                  {
                                      Mes = 2,
                                      Valor = 0.02d
                                  };

          //  CollectionAssert.Contains(recisao.Parcelas, parcela);
        }

        [TestMethod]
       // // [ExpectedException(UserMessage = "Salário não foi informado")]
        public void TestMethode_calcular_recisao_deve_retornar_excecao()
        {
            Conta conta = new Conta("TestMethod", new TipoConta { Id = 1 });

            Recisao recisao = new Recisao(2, conta, new NovoOrcamentoPessoal(new Setor("Nome"), new CentroDeCusto("novo"), 2014));

            recisao.Calcular(default(double), 1, 0, 0);
        }
    }
}
