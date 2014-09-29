using Orcamento.Domain.ComponentesDeOrcamento.OrcamentoDeProducao;
using Orcamento.Domain.Entities.Monitoramento;
using Orcamento.Domain.Gerenciamento;
using Orcamento.Domain.Robo.Monitoramento.EspecificacaoDeValidacaoDeCarga.EspecificacaoTicketsUnitariosDeProducao;
using Orcamento.Domain.Util.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orcamento.Domain.Robo.Monitoramento.EspecificacaoDeValidacaoDeCarga.EspecificacaoTicketsDeInsumo
{

    public class FabricaDeEspeficicacaoDepartamento
    {
        public static Especificacao ObterEspeficicacao(TicketDeInsumoExcel ticketDeProducao, Departamento departamento)
        {
            var validaDepartamento = new EspecificacaoCargaValidaTicketDeInsumoDepartamento(ticketDeProducao, departamento);

            return
                validaDepartamento;
        }
    }

    public class FabricaDeEspeficicacaoCustoUnitario
    {
        public static Especificacao ObterEspeficicacao(TicketDeInsumoExcel ticketsDeInsumo, List<CustoUnitario> custosUnitarios)
        {
            var validaTicketDeProducao = new EspecificacaoCargaValidaTicketsDeInsumoCustoUnitario(ticketsDeInsumo, custosUnitarios);

            return
                validaTicketDeProducao;
        }
    }

    public class FabricaDeEspeficicacaoProducaoHospitalar
    {
        public static Especificacao ObterEspeficicacao(TicketDeInsumoExcel ticketDeProducao, List<ProducaoHospitalar> producoesHospitalares)
        {
            var validaParcelasdosTicketsDeInsumos = new EspecificacaoCargaValidaTicketDeInsumoProducaoHospitalar(ticketDeProducao, producoesHospitalares);

            return
                validaParcelasdosTicketsDeInsumos;
        }
    }

    public class FabricaDeEspeficicacaoInsumo
    {
        public static Especificacao ObterEspeficicacao(TicketDeInsumoExcel ticketesDeInsumo, List<Insumo> insumos)
        {
            var validaInsumo = new EspecificacaoCargaValidaTicketsDeInsumo(ticketesDeInsumo, insumos);

            return
                validaInsumo;
        }
    }

    public class FabricaDeEspeficicacaoSetor
    {
        public static Especificacao ObterEspeficicacao(TicketDeInsumoExcel ticketDeProducao, List<SetorHospitalar> setores)
        {
            var validaSetor = new EspecificacaoCargaValidaTicketDeInsumoSetorHospitalar(ticketDeProducao, setores);

            return
                validaSetor;
        }
    }

    public class FabricaDeEspeficicacaoSubSetor
    {
        public static Especificacao ObterEspeficicacao(TicketDeInsumoExcel ticketsDeInsumos, List<SubSetorHospital> subsetores)
        {
            var validaSubSetor = new EspecificacaoCargaValidaTicketUnitarioDeInsumoSubSetorHospital(ticketsDeInsumos, subsetores);

            return
                validaSubSetor;
        }
    }
}
