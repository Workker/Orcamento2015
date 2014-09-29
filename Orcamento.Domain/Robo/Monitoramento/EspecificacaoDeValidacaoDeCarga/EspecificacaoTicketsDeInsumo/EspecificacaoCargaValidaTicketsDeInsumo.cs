using Orcamento.Domain.ComponentesDeOrcamento.OrcamentoDeProducao;
using Orcamento.Domain.Entities.Monitoramento;
using Orcamento.Domain.Robo.Monitoramento.EstrategiasDeCargas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Orcamento.Domain.Robo.Monitoramento.EspecificacaoDeValidacaoDeCarga
{
    public class EspecificacaoCargaValidaTicketsDeInsumo : EspecificacaoCargaValidaTicketsDeInsumoGeral
    {
        public EspecificacaoCargaValidaTicketsDeInsumo(TicketDeInsumoExcel ticketDeInsumoExcel, List<Insumo> insumos)
            : base(ticketDeInsumoExcel, insumos)
        {

        }

        public override bool IsSatisfiedBy(Carga candidate)
        {
            base.IsSatisfiedBy(candidate);

            var satisfeito = Insumos.Any(t => t.Departamento.Nome == TicketDeInsumoExcel.Departamento);

            if (!satisfeito)
            {
                candidate.AdicionarDetalhe("Não foi encontrado",
                                           string.Format("Não foi encontrado parcelas de insumos no hospital: {0} do subsetor : {1}",
                                           TicketDeInsumoExcel.Departamento,
                                           TicketDeInsumoExcel.subSetor),
                                           TicketDeInsumoExcel.Linha, TipoDetalheEnum.erroDeValidacao);
            }

            return satisfeito;
        }
    }
}
