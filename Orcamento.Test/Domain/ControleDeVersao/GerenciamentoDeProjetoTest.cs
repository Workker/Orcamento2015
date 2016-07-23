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
    [Ignore]
    public class GerenciadorDeOrcamentosTestMethod
    {
       // [TestMethod]
        public void AoCriarPrimeiroOrcamentoNomeDeveSerIgualAVersaoUm()
        {
            GerenciadorDeOrcamentos gerenciamento = new GerenciadorDeOrcamentos();
            Departamento departamento = new Hospital("Barra Dor");
            var orcamento = new OrcamentoHospitalar(departamento, 2012);
            gerenciamento.InformarNomeOrcamento(new List<Orcamento.Domain.Orcamento>(), orcamento, departamento, TipoOrcamentoEnum.Hospitalar);

            Assert.IsTrue(orcamento.NomeOrcamento == "Versão 1");
        }

        [TestMethod]
        public void AoCriarSegundoOrcamentoNomeDeveSerIgualAVersaoDois()
        {
            Departamento departamento = new Hospital("Barra Dor");
            var orcamento = new OrcamentoHospitalar(departamento, 2012);
            var listaOrcamentos = new List<Orcamento.Domain.Orcamento>();
            listaOrcamentos.Add(orcamento);
            GerenciadorDeOrcamentos gerenciamento = new GerenciadorDeOrcamentos();
            gerenciamento.InformarNomeOrcamento(listaOrcamentos, orcamento, departamento, TipoOrcamentoEnum.Hospitalar);
            Assert.IsTrue(orcamento.NomeOrcamento == "Versão 2");

        }

        [TestMethod]
        public void AoCriarSegundoOrcamentoDeletantoOsegundoOterceiroDeveSerVersaoDois()
        {
 
        }
    }
}
