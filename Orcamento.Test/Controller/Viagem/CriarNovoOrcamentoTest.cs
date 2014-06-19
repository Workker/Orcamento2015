using System;
using System.Collections.Generic;
using NUnit.Framework;
using Orcamento.Controller;
using Orcamento.Domain;
using Orcamento.Domain.ComponentesDeOrcamento.OrcamentoDeViagem;
using Orcamento.Domain.DB.Repositorio;
using Orcamento.Domain.Gerenciamento;
using Orcamento.Domain.Servico;
using Rhino.Mocks;

namespace Orcamento.Test.Controller.Viagem
{
    [TestFixture]
    [Ignore]
    class CriarNovoOrcamentoViagemTest
    {
        #region atributos
        private IViewOrcamentoDeViagem view;
        private OrcamentoDeViagemController controller;
        private IOrcamentos repositorioOrcamentos;
        private Departamento departamento;

        #endregion

        #region Setup/Teardown

        [Test]
        public void inicializar()
        {
            var centroDeCusto = new CentroDeCusto("TesteCD") { Id = 1 };

            departamento = MockRepository.GenerateMock<Departamento>();
            departamento.Expect(x => x.ObterCentroDeCustoPor(1)).Return(centroDeCusto);
            departamento.Id = 1;
            departamento.Nome = "teste";

            view = MockRepository.GenerateMock<IViewOrcamentoDeViagem>();
            view.Departamento = departamento;
            view.CentroDeCustoId = 1;

            var cidade = new Cidade("Niteroi");

            var diarias = MockRepository.GenerateMock<IDiarias>();
            
            var ticket = MockRepository.GenerateMock<Orcamento.Domain.Ticket>();
            
            var tickets = new List<Ticket> {ticket};
            
            var viagem = new Orcamento.Domain.Viagem {Cidade = cidade, Id = 1, Tickets = tickets};

            var diaria = new Diaria {Id = 1, Cidade = cidade};

            var diariasL = new List<Diaria> {diaria};
            diarias.Expect(x => x.Todos()).Return(diariasL);

            repositorioOrcamentos = MockRepository.GenerateMock<IOrcamentos>();

            var listaDeOrcamentos = new List<Orcamento.Domain.Orcamento> { new OrcamentoDeViagem(departamento, centroDeCusto, 2014) };

            var orcamentoDeViagem = new ServicoOrcamentoDeViagens {Orcamentos = repositorioOrcamentos};

            var servicoOrcamentoDeViagens = new ServicoOrcamentoDeViagens { Diarias = diarias, Orcamentos = repositorioOrcamentos};
            servicoOrcamentoDeViagens.CriarOrcamentoDeViagem(listaDeOrcamentos, departamento, centroDeCusto, 2014);


            repositorioOrcamentos.Expect(x => x.TodosOrcamentosDeViagemPor(centroDeCusto, departamento)).Return(
                listaDeOrcamentos);
            


            controller = new OrcamentoDeViagemController(view) {View = view, Orcamentos = repositorioOrcamentos};
            //controller.Expect(x => x.ServicoOrcamentoDeViagens).Return(orcamentoDeViagem); //???
            controller.CriarNovoOrcamentoOperacional(departamento, centroDeCusto.Id);
        }


        #endregion

        #region metodos de teste
        #endregion
    }
}
