using System.Collections.Generic;
using System.Linq;

using Orcamento.Domain;
using Orcamento.Domain.ComponentesDeOrcamento.OrcamentoPessoal.Despesas;
using Orcamento.Domain.ComponentesDeOrcamento.OrcamentoPessoal.Despesas.Normal;
using Orcamento.Domain.Gerenciamento;
using Orcamento.Domain.ComponentesDeOrcamento.OrcamentoPessoal;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Orcamento.TestMethod.Domain.Orcamentos.OrcamentoPessoalTestMethod
{
  

    [TestClass]
    public class FGTSTestMethode
    {
        [TestMethod]
        public void TestMethode_calcular_parcela_de_fgts_com_sucesso()
        {

            var orcamento =  new NovoOrcamentoPessoal(new Setor("Nome"), new CentroDeCusto("novo"), 2014);
            var conta = new Conta("TestMethod", new TipoConta { Id = 1 });
            conta.Adicionar(TipoTicketDePessoal.FGTS);
            
            orcamento.Adicionar(new TicketDeOrcamentoPessoal(new Setor("nome")) { Ticket = TipoTicketDePessoal.FGTS, Descricao = "TestMethode", Valor = 0 });
            orcamento.Adicionar(new TicketDeOrcamentoPessoal(new Setor("nome")) { Ticket = TipoTicketDePessoal.FGTS, Descricao = "FGTS", Valor = 8 });

            var fgts = new FGTS(conta, orcamento);

            fgts.Funcionario = new Funcionario(new Hospital("nome")) { AnoAdmissao = 2014, DataAdmissao = 1 };
            fgts.Calcular(2, 1, 0, 0);
            
            Parcela parcela = new Parcela { Mes = 1, Valor = 0.19111066666666665d};

            Assert.AreEqual(fgts.Parcelas.FirstOrDefault().Valor, parcela.Valor);
        }

        [TestMethod]
        public  void calculo_de_fgts_gera_doze_parcelas()
        {
            var orcamento = new NovoOrcamentoPessoal(new Setor("Nome"), new CentroDeCusto("novo"), 2014);
            Conta conta = new Conta("TestMethod", new TipoConta { Id = 1 });
            conta.Adicionar(TipoTicketDePessoal.FGTS);

            orcamento.Adicionar(new TicketDeOrcamentoPessoal(new Setor("nome")) { Ticket = TipoTicketDePessoal.FGTS, Descricao = "TestMethode", Valor = 0 });
            orcamento.Adicionar(new TicketDeOrcamentoPessoal(new Setor("nome")) { Ticket = TipoTicketDePessoal.FGTS, Descricao = "FGTS", Valor = 8 });

            var fgts = new FGTS(conta, orcamento);

            fgts.Funcionario = new Funcionario(new Hospital("nome")) { AnoAdmissao = 2014, DataAdmissao = 1 };
            fgts.Calcular(2, 1, 0, 0);
            

            Assert.AreEqual(fgts.Parcelas.Count, 12);
        }

        [TestMethod]
        //// [ExpectedException(UserMessage = "Salário não foi informado")]
        public void calcular_fgts_com_salario_default_deve_retornar_excecao()
        {
            var fgts = new FGTS(null, new NovoOrcamentoPessoal(new Setor("Nome"), new CentroDeCusto("novo"), 2014));

            fgts.Calcular(default(double), 1, 0, 0);
        }
    }
}
