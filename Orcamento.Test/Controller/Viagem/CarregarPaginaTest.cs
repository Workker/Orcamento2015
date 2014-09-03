using System.Collections.Generic;
using NUnit.Framework;
using Orcamento.Controller;
using Orcamento.Domain;
using Orcamento.Domain.ComponentesDeOrcamento.OrcamentoDeViagem;
using Orcamento.Domain.DB.Repositorio;
using Orcamento.Domain.Gerenciamento;
using Rhino.Mocks;

namespace Orcamento.Test.Controller.Viagem
{
    [TestFixture]
    public class CarregarPaginaTest
    {
        #region Atributos
        
        private IViewOrcamentoDeViagem view;
        private IOrcamentos repositorioOrcamentos;
        private CentroDeCusto centroDeCusto;
        private OrcamentoDeViagemController controller;
        
        #endregion
        
        #region Setup/Teardown
        
        [SetUp]
        public void Inicializar()
        {
            repositorioOrcamentos = MockRepository.GenerateMock<IOrcamentos>();
            view = MockRepository.GenerateStub<IViewOrcamentoDeViagem>();

            view.CentroDeCustoId = 1;
            view.Departamento = MockRepository.GenerateMock<Departamento>();
            view.Departamento.Nome = "TI";

            centroDeCusto = new CentroDeCusto("maquinas") {Id = 1};

            view.Departamento.Expect(x => x.ObterCentroDeCustoPor(view.CentroDeCustoId)).Return(centroDeCusto);

            controller = new OrcamentoDeViagemController(view);

            repositorioOrcamentos.Expect(x => x.TodosOrcamentosDeViagemPor(centroDeCusto, view.Departamento)).Return(
                ObterOrcamentosParaCarregamento());

            controller.Orcamentos = repositorioOrcamentos;

            controller.CarregarPaginaDeAcordoComOCentroDeCustoSelecionado();
        }

        #endregion

        #region metodos de teste

        [Test]
        public void a_view_executou_todas_as_instrucoes_com_sucesso()
        {
            view.VerifyAllExpectations();
        }

        [Test]
        public void o_repositorio_de_orcamento_executou_todas_as_instrucoes_com_sucesso()
        {
            repositorioOrcamentos.VerifyAllExpectations();
        }

        private List<Orcamento.Domain.Orcamento> ObterOrcamentosParaCarregamento()
        {
            var orcamentoDeViagem = new OrcamentoDeViagem
                {
                    Ano = 2015,
                    CentroDeCusto = centroDeCusto,
                    NomeOrcamento = "Orçamento de Viagem"
                };

            orcamentoDeViagem.Despesas = new List<DespesaDeViagem>();

            var diaria = new DiariaViagem {ValorTotal = 10, Quantidade = 3};

            diaria.Diaria = new Diaria();

            diaria.Diaria.Tickets = new List<Ticket>();
            diaria.Diaria.Tickets.Add(new Ticket {Valor = 10});

            orcamentoDeViagem.Despesas.Add(diaria);

            var viagens = new List<Orcamento.Domain.Orcamento>();

            viagens.Add(orcamentoDeViagem);

            return viagens;
        }

        #endregion
    }
}