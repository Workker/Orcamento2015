using System.Collections.Generic;

using Orcamento.Domain;
using Orcamento.Domain.ComponentesDeOrcamento.OrcamentoPessoal.Despesas;
using Orcamento.Domain.ComponentesDeOrcamento.OrcamentoPessoal.Despesas.Normal;
using Orcamento.Domain.Gerenciamento;
using Orcamento.Domain.ComponentesDeOrcamento.OrcamentoPessoal;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Orcamento.TestMethod.Domain.Orcamentos.OrcamentoPessoalTestMethod
{
    [TestClass]
    public class SalarioTestMethode
    {
        [TestMethod]
        public void TestMethode_calcular_parcela_de_salario_com_sucesso()
        {
            NovoOrcamentoPessoal orcamento = new NovoOrcamentoPessoal(new Setor("Nome"), new CentroDeCusto("nome"), 2014);
            Conta conta = new Conta("TestMethod", new TipoConta { Id = 1 });
            


            var salario = new Salario(conta, orcamento);

            salario.Funcionario = new Funcionario(new Hospital("nome")) { AnoAdmissao = 2014, DataAdmissao = 1 };
            salario.Calcular(0.03, 1, 0, 0);
            

            Parcela parcela = new Parcela
                                  {
                                      Mes = 1,
                                      Valor = 0.03d
                                  };

           // CollectionAssert.Contains(salario.Parcelas, parcela);
        }

        
    }
}
