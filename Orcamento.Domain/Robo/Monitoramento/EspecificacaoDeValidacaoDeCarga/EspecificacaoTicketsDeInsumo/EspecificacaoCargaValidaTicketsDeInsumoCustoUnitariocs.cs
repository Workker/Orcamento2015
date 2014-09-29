using Orcamento.Domain.ComponentesDeOrcamento.OrcamentoDeProducao;
using Orcamento.Domain.Entities.Monitoramento;
using Orcamento.Domain.Robo.Monitoramento.EspecificacaoDeValidacaoDeCarga;
using Orcamento.Domain.Robo.Monitoramento.EspecificacaoDeValidacaoDeCarga.EspecificacaoTicketsUnitariosDeProducao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orcamento.Domain.Robo.Monitoramento
{
    public class EspecificacaoCargaValidaTicketsDeInsumoCustoUnitario : EspecificacaoCargaValidaTicketsDeInsumoGeral
    {
        public EspecificacaoCargaValidaTicketsDeInsumoCustoUnitario(TicketDeInsumoExcel ticketDeInsumoExcel,
                                                                    List<CustoUnitario> custoUnitarios)
            : base(ticketDeInsumoExcel, custoUnitarios)
        {

        }

        public override bool IsSatisfiedBy(Carga candidate)
        {
            base.IsSatisfiedBy(candidate);

            var satisfeito =
                CustosUnitarios.Any(
                    t =>
                    t.Setor.NomeSetor == TicketDeInsumoExcel.setor &&
                    t.SubSetor.NomeSetor == TicketDeInsumoExcel.subSetor);

            if (!satisfeito)
            {
                //Passar setores e subSertoresa

                candidate.AdicionarDetalhe("Parcela não encontrada",
                                           string.Format("Não foi possível encontrar a Parcela: {0} do subsetor : {1}",
                                                         TicketDeInsumoExcel.mes,
                                                         TicketDeInsumoExcel.subSetor),
                                           TicketDeInsumoExcel.Linha, TipoDetalheEnum.erroDeValidacao);
            }

            return true;
        }
    }
}
