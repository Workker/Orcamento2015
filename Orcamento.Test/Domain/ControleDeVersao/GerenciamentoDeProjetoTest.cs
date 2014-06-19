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
    [Ignore]
    public class GerenciadorDeOrcamentosTest
    {
       // [Test]
        public void AoCriarPrimeiroOrcamentoNomeDeveSerIgualAVersaoUm()
        {
            GerenciadorDeOrcamentos gerenciamento = new GerenciadorDeOrcamentos();
            Departamento departamento = new Hospital("Barra Dor");
            var orcamento = new OrcamentoHospitalar(departamento, 2012);
            gerenciamento.InformarNomeOrcamento(new List<Orcamento.Domain.Orcamento>(), orcamento, departamento, TipoOrcamentoEnum.Hospitalar);

            Assert.IsTrue(orcamento.NomeOrcamento == "Versão 1");
        }

        [Test]
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

        [Test]
        public void AoCriarSegundoOrcamentoDeletantoOsegundoOterceiroDeveSerVersaoDois()
        {
 
        }
    }
}
