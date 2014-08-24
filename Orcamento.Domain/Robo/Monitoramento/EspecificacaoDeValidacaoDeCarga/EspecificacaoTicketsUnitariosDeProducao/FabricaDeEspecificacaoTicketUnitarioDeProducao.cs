using Orcamento.Domain.Gerenciamento;
using Orcamento.Domain.Robo.Monitoramento.EstrategiasDeCargas;
using Orcamento.Domain.Util.Specification;
using System.Collections.Generic;

namespace Orcamento.Domain.Robo.Monitoramento.EspecificacaoDeValidacaoDeCarga.EspecificacaoTicketsUnitariosDeProducao
{
    public class FabricaDeEspecificacaoTicketUnitarioDeProducao
    {
        public static Especificacao ObterEspecificacao(TicketDeProducaoExcel ticketDeProducaoExcel,
                                                       Departamento departamento, SetorHospitalar setorHospitalar,
                                                       SubSetorHospital subSetorHospital)
        {
            var validaDepartamento = new EspecificacaoCargaValidaTicketDeProducaoDepartamento(ticketDeProducaoExcel,
                                                                                              departamento);

            //var validaSetor = new EspecificacaoCargaValidaTicketDeProducaoSetorHospitalar(ticketDeProducaoExcel, setorHospitalar);
            //var validaSubSetor = new EspecificacaoCargaTicketUnitarioDeProducaoSubSetorHospital(ticketDeProducaoExcel,
            //                                                                            subSetorHospital);

            return validaDepartamento;
        }
    }

    public class FabricaDeEspeficicacaoDepartamento
    {
        public static Especificacao ObterEspeficicacao(TicketDeProducaoExcel ticketDeProducao, Departamento departamento)
        {
            var validaDepartamento = new EspecificacaoCargaValidaTicketDeProducaoDepartamento(ticketDeProducao, departamento);

            return
                validaDepartamento;
        }
    }

    public class FabricaDeEspeficicacaoTicket
    {
        public static Especificacao ObterEspeficicacao(TicketDeProducaoExcel ticketDeProducao, List<TicketDeProducao> ticketsDeProducao)
        {
            var validaTicketDeProducao = new EspecificacaoCargaValidaTicketsUnitariosDeProducaoTicket(ticketDeProducao, ticketsDeProducao);

            return
                validaTicketDeProducao;
        }
    }

    public class FabricaDeEspeficicacaoParcela
    {
        public static Especificacao ObterEspeficicacao(TicketDeProducaoExcel ticketDeProducao, List<TicketParcela> ticketparcela)
        {
            var validaTicketParcela = new EspecificacaoCargaValidaTicketUnitarioDeProducaoParcela(ticketDeProducao,ticketparcela);

            return
                validaTicketParcela;
        }
    }

    public class FabricaDeEspeficicacaoSetor
    {
        public static Especificacao ObterEspeficicacao(TicketDeProducaoExcel ticketDeProducao, List<SetorHospitalar> setores)
        {
            var validaSetor = new EspecificacaoCargaValidaTicketDeProducaoSetorHospitalar(ticketDeProducao, setores);

            return
                validaSetor;
        }
    }
    public class FabricaDeEspeficicacaoSubSetor
    {
        public static Especificacao ObterEspeficicacao(TicketDeProducaoExcel ticketDeProducao, List<SubSetorHospital> subsetores)
        {
            var validaSubSetor = new EspecificacaoCargaTicketUnitarioDeProducaoSubSetorHospital(ticketDeProducao, subsetores);

            return
                validaSubSetor;
        }
    }
}