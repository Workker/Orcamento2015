using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Orcamento.Domain;
using Orcamento.Domain.ComponentesDeOrcamento.OrcamentoDeViagem;
using Orcamento.Domain.Gerenciamento;

namespace Orcamento.Test.Domain.Orcamentos
{
    [TestFixture]
    public class OrcamentoDeViagemTest
    {
        public List<Cidade> Cidades { get; set; }
        public List<TipoTicket> Tipos { get; set; }
        public List<Ticket> TicketsViagem { get; set; }
        public List<Ticket> TicketsDiaria { get; set; }
        public List<Viagem> Viagens { get; set; }
        public List<Diaria> Diarias { get; set; }

        private void InserirCidadesTiposTicketETicketEViagem()
        {
            var saoPaulo = new Cidade("São Paulo");
            var recife = new Cidade("Recife");

            Cidades = new List<Cidade>();
            Cidades.Add(saoPaulo);
            Cidades.Add(recife);

            InserirTipoTickets();
            InserirTickets();
            InserirViagens();
            InserirDiarias();
        }

        private void InserirDiarias()
        {
            Diarias = new List<Diaria>();
            foreach (Cidade item in Cidades)
            {
                var diaria = new Diaria(item, TicketsDiaria.Where(t => t.Cidade.Descricao == item.Descricao).ToList());
                Diarias.Add(diaria);
            }
        }

        private void InserirViagens()
        {
            Viagens = new List<Viagem>();
            foreach (Cidade item in Cidades)
            {
                var viagem = new Viagem(item, TicketsViagem.Where(t => t.Cidade.Descricao == item.Descricao).ToList());
                Viagens.Add(viagem);
            }
        }

        public void InserirTipoTickets()
        {
            var diaria = new TipoTicket("Diaria");
            var taxi = new TipoTicket("Taxi");
            var refeicao = new TipoTicket("Refeição");
            var passagem = new TipoTicket("Passagem");

            Tipos = new List<TipoTicket>();
            Tipos.Add(diaria);
            Tipos.Add(taxi);
            Tipos.Add(refeicao);
            Tipos.Add(passagem);
        }

        public void InserirTickets()
        {
            TicketsDiaria = new List<Ticket>();
            TicketsViagem = new List<Ticket>();

            foreach (Cidade item in Cidades)
            {
                var diaria = new Ticket(Tipos[0], item);
                var taxi = new Ticket(Tipos[1], item);
                var refeicao = new Ticket(Tipos[2], item);
                var passagem = new Ticket(Tipos[3], item);

                TicketsDiaria.Add(diaria);
                TicketsDiaria.Add(taxi);
                TicketsDiaria.Add(taxi);
                TicketsDiaria.Add(refeicao);
                TicketsDiaria.Add(refeicao);

                TicketsViagem.Add(passagem);
                TicketsViagem.Add(passagem);
                TicketsViagem.Add(taxi);
                TicketsViagem.Add(taxi);
                TicketsViagem.Add(taxi);
                TicketsViagem.Add(taxi);
            }
        }

        [Test]
        public void OrcamentoDeViagemCriadoComDuasCidadesDuasViagensEDuasDiariasDeveTerDespesasCountIgualAQuarentaEOito()
        {
            Departamento Ti = new Setor("TI");
            var gerenciamentoDeProjetos = new CentroDeCusto("Gerenciamento de Projetos");
            var recursosExternos = new Conta("RecursosExternos", new TipoConta{Id = (int)TipoContaEnum.Outros});
            var luz = new Conta("Luz", new TipoConta{Id = (int) TipoContaEnum.Outros});

            gerenciamentoDeProjetos.AdicionarConta(luz);
            gerenciamentoDeProjetos.AdicionarConta(recursosExternos);

            Ti.AdicionarCentroDeCusto(gerenciamentoDeProjetos);

            var orcamentoDeViagem = new OrcamentoDeViagem(Ti, gerenciamentoDeProjetos, 2014);
            InserirCidadesTiposTicketETicketEViagem();
            InserirDiarias();
            InserirViagens();
            InserirTipoTickets();
            InserirTickets();

            orcamentoDeViagem.CriarDespesas(Viagens, Diarias);
            Assert.IsTrue(orcamentoDeViagem.Despesas.Count == 48);
        }

        [Test]
        public void OrcamentoDeViagemCriadoDeveTerCentroDeCustoDiferenteDeNulo()
        {
            Departamento Ti = new Setor("TI");
            var gerenciamentoDeProjetos = new CentroDeCusto("Gerenciamento de Projetos");
            var recursosExternos = new Conta("RecursosExternos", new TipoConta { Id = (int)TipoContaEnum.Outros });
            var luz = new Conta("Luz", new TipoConta { Id = (int)TipoContaEnum.Outros });

            gerenciamentoDeProjetos.AdicionarConta(luz);
            gerenciamentoDeProjetos.AdicionarConta(recursosExternos);

            Ti.AdicionarCentroDeCusto(gerenciamentoDeProjetos);

            var orcamentoDeViagem = new OrcamentoDeViagem(Ti, gerenciamentoDeProjetos, 2014);

            Assert.IsTrue(orcamentoDeViagem.CentroDeCusto != null);
        }

        [Test]
        public void OrcamentoDeViagemCriadoDeveTerSetorDiferenteDeNulo()
        {
            Departamento Ti = new Setor("TI");
            var gerenciamentoDeProjetos = new CentroDeCusto("Gerenciamento de Projetos");
            var recursosExternos = new Conta("RecursosExternos", new TipoConta { Id = (int)TipoContaEnum.Outros });
            var luz = new Conta("Luz", new TipoConta { Id = (int)TipoContaEnum.Outros });

            gerenciamentoDeProjetos.AdicionarConta(luz);
            gerenciamentoDeProjetos.AdicionarConta(recursosExternos);

            Ti.AdicionarCentroDeCusto(gerenciamentoDeProjetos);

            var orcamentoDeViagem = new OrcamentoDeViagem(Ti, gerenciamentoDeProjetos, 2014);

            Assert.IsTrue(orcamentoDeViagem.Setor != null);
        }
    }
}