using System.Collections.Generic;
using Orcamento.Domain.Entities.Monitoramento;
using Orcamento.Domain.Robo.Monitoramento.EstrategiasDeCargas;

namespace Orcamento.Domain.Robo.Monitoramento.EspecificacaoDeValidacaoDeCarga.EspecificacaoEstruturaOrcamentaria
{
    public class MotorDeValidacaoDeEstruturaOrcamentaria
    {
        private readonly Carga carga;
        private readonly List<EstruturaOrcamentariaExcel> estruturasOrcamentariasExcel;

        public MotorDeValidacaoDeEstruturaOrcamentaria(Carga carga,
                                                       List<EstruturaOrcamentariaExcel> estruturasOrcamentariasExcel)
        {
            this.carga = carga;
            this.estruturasOrcamentariasExcel = estruturasOrcamentariasExcel;
        }

        public void Validar()
        {
            foreach (EstruturaOrcamentariaExcel estruturaOrcamentariaExcel in estruturasOrcamentariasExcel)
            {
                FabricaDeEspecificacaoEstruturaOrcamentaria.ObterEspeficicacao(estruturaOrcamentariaExcel).
                    IsSatisfiedBy(carga);
            }
        }
    }
}