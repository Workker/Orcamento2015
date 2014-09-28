using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using Orcamento.Domain.DB.Repositorio;
using Orcamento.Domain.Entities.Monitoramento;
using Orcamento.Domain.Gerenciamento;
using Orcamento.Domain.Robo.Monitoramento.EstrategiasDeCargas;
using Orcamento.Domain.Util.Specification;

namespace Orcamento.Domain.Robo.Monitoramento.EspecificacaoDeValidacaoDeCarga.EspecificacaoTicketsUnitariosDeProducao
{
    public class MotorDeValidacaoDeTicketDeProducao
    {
        private readonly Carga carga;
        private readonly List<TicketDeProducaoExcel> ticketsUnitariosDeProducao;

        public MotorDeValidacaoDeTicketDeProducao(Carga carga, List<TicketDeProducaoExcel> ticketsUnitariosDeProducao)
        {
            this.carga = carga;
            this.ticketsUnitariosDeProducao = ticketsUnitariosDeProducao;
        }

        private Departamentos departamentos;
        public virtual Departamentos DepartamentosRepositorio
        {
            get
            {
                if (departamentos == null)
                    departamentos = new Departamentos();

                return departamentos;
            }
        }

        public virtual List<Departamento> Departamentos { get; set; }
        public virtual List<TicketDeProducao> Tickets { get; set; }
        public virtual List<Setor> Setores { get; set; }
        public virtual List<SubSetorHospital> SubSetores { get; set; }

        public void Validar()
        {
            Departamentos = DepartamentosRepositorio.TodosComSetoresESubSetores();
            Contract.Requires(Departamentos != null, "Departamentos não encontrados");

            ValidaDepartamentos();

            if (!carga.Ok())
                return;

            ValidarSetores();

            if (!carga.Ok())
                return;

            ValidarSubSetores();

            if (!carga.Ok())
                return;

            ValidarTicket();

            if (!carga.Ok())
                return;

            ValidarParcela();
        }

        private void ValidarSetores()
        {
            foreach (TicketDeProducaoExcel ticketDeProducaoExcel in ticketsUnitariosDeProducao)
            {
                var setores = ticketDeProducaoExcel.DepartamentoEntidade.Setores.ToList();

                Especificacao especificacaoTicketParcela =
                    FabricaDeEspeficicacaoSetor.ObterEspeficicacao(ticketDeProducaoExcel, setores);

                especificacaoTicketParcela.IsSatisfiedBy(carga);
            }
        }
        private void ValidarSubSetores()
        {
            foreach (TicketDeProducaoExcel ticketDeProducaoExcel in ticketsUnitariosDeProducao)
            {
                var subSetores = ticketDeProducaoExcel.DepartamentoEntidade.Setores.SelectMany(s => s.SubSetores).ToList();

                Especificacao especificacaoTicketParcela =
                    FabricaDeEspeficicacaoSubSetor.ObterEspeficicacao(ticketDeProducaoExcel, subSetores);

                especificacaoTicketParcela.IsSatisfiedBy(carga);

            }
        }

        private void ValidarParcela()
        {
            foreach (TicketDeProducaoExcel ticketDeProducaoExcel in ticketsUnitariosDeProducao)
            {
                List<TicketParcela> ticketsParcela = ticketDeProducaoExcel.Ticket.Parcelas.ToList();

                Especificacao especificacaoTicketParcela =
                    FabricaDeEspeficicacaoParcela.ObterEspeficicacao(ticketDeProducaoExcel, ticketsParcela);

                if (especificacaoTicketParcela.IsSatisfiedBy(carga))
                {
                    ticketDeProducaoExcel.TicketParcela = ticketDeProducaoExcel.Ticket.Parcelas.FirstOrDefault(p => ticketDeProducaoExcel.mes == (int)p.Mes);
                }
            }
        }

        private void ValidarTicket()
        {
            var tickets = new TicketsDeProducao();

            Tickets = new List<TicketDeProducao>();

            foreach (TicketDeProducaoExcel ticketDeProducaoExcel in ticketsUnitariosDeProducao)
            {
                // TODO: evitar que busque + de uma vez o mesmo ticket de produção (pasar pra outro lugar)
                if (!Tickets.Any(t => t.Hospital.Nome == ticketDeProducaoExcel.Departamento && t.Setor.NomeSetor == ticketDeProducaoExcel.setor && t.SubSetor.NomeSetor == ticketDeProducaoExcel.subSetor))
                    Tickets.AddRange(tickets.Todos(ticketDeProducaoExcel.DepartamentoEntidade).ToList());

                Especificacao especificacaoTicket =
                    FabricaDeEspeficicacaoTicket.ObterEspeficicacao(ticketDeProducaoExcel, Tickets);

                if (especificacaoTicket.IsSatisfiedBy(carga))
                {
                    TicketDeProducao ticket =
                        Tickets.FirstOrDefault(t => t.Setor.NomeSetor == ticketDeProducaoExcel.setor && t.SubSetor.NomeSetor == ticketDeProducaoExcel.subSetor);
                    ticketDeProducaoExcel.SetorHospitalar = ticket.Setor;
                    ticketDeProducaoExcel.SubSetorHospital = ticket.SubSetor;
                    ticketDeProducaoExcel.Ticket = ticket;
                }
            }
        }

        #region Departamentos

        public void ValidaDepartamentos()
        {
            foreach (TicketDeProducaoExcel ticketProducaoExcel in ticketsUnitariosDeProducao)
            {
                if (!Departamentos.Any(d => d.Nome == ticketProducaoExcel.Departamento))
                    AdicionarDepartamento(carga, ticketProducaoExcel);
                else
                    ticketProducaoExcel.DepartamentoEntidade =
                        Departamentos.FirstOrDefault(d => d.Nome == ticketProducaoExcel.Departamento);
            }
        }

        private void AdicionarDepartamento(Carga carga, TicketDeProducaoExcel ticketDeProducaoExcel)
        {
            Departamento departamento = Departamentos.FirstOrDefault(p => p.Nome == ticketDeProducaoExcel.Departamento);

            Especificacao espeficicacaoDepartamento = FabricaDeEspeficicacaoDepartamento.ObterEspeficicacao(
                ticketDeProducaoExcel, departamento);

            if (espeficicacaoDepartamento.IsSatisfiedBy(carga))
                ticketDeProducaoExcel.DepartamentoEntidade = departamento;
        }

        #endregion
    }
}