using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Orcamento.Domain;
using Orcamento.Domain.ComponentesDeOrcamento.OrcamentoPessoal;
using Orcamento.Domain.ComponentesDeOrcamento.OrcamentoPessoal.Despesas;
using Orcamento.Domain.ComponentesDeOrcamento.OrcamentoPessoal.Despesas.Normal;
using Orcamento.Domain.Gerenciamento;

namespace Orcamento.Test.Domain.Orcamentos
{
    [TestFixture]
    public class FuncionarioTest
    {
        [Test]
        public void decimo_terceiro_gerou_12_parcelas()
        {
            var funcionario = new Funcionario(new Setor("Nome"));

            var despesa = new DecimoTerceiro(null, new NovoOrcamentoPessoal(new Setor("Nome"), new CentroDeCusto("novo"), 2014));

            despesa.AdicionarDeAcordoCom(12, 2,1);

            funcionario.Adicionar(despesa);

            CollectionAssert.AllItemsAreNotNull(funcionario.Despesas);
        }

        [Test]
        public  void deletar_todas_as_despesas()
        {
            Salario salario = new Salario(null, new NovoOrcamentoPessoal(new Setor("Nome"), new CentroDeCusto("novo"), 2014));
            salario.Adicionar(new Parcela() {Mes = 1, Valor = 1});

            Salario salario2 = new Salario(null, new NovoOrcamentoPessoal(new Setor("Nome"), new CentroDeCusto("novo"), 2014));
            salario2.Adicionar(new Parcela() { Mes = 1, Valor = 1 });

            Salario salario3 = new Salario(null, new NovoOrcamentoPessoal(new Setor("Nome"), new CentroDeCusto("novo"), 2014));
            salario3.Adicionar(new Parcela() { Mes = 1, Valor = 1 });

            Funcionario funcionario = new Funcionario(new Setor("Nome"));
            funcionario.Adicionar(salario);
            funcionario.Adicionar(salario2);
            funcionario.Adicionar(salario3);

            funcionario.DeletarTodasAsDespesas();

            Assert.AreEqual(funcionario.Despesas.Count, 0);
        }
    }
}
