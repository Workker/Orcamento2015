using Orcamento.Domain.Entities.Monitoramento;
using Orcamento.Domain.Robo.Monitoramento.EstrategiasDeCargas;

namespace Orcamento.Domain.Robo.Fabricas
{
    public class FabricaDeImportacao
    {
        public static ProcessaCarga Criar(TipoEstrategiaDeCargaEnum tipo)
        {
            switch (tipo)
            {
                case TipoEstrategiaDeCargaEnum.Funcionarios:
                    return new Funcionarios();
                    break;

                case TipoEstrategiaDeCargaEnum.TicketsDeProducao:
                    return new ProcessaTicketsDeProducao();
                    break;

                case TipoEstrategiaDeCargaEnum.EstruturaOrcamentaria:
                    return new EstruturaOrcamentaria();
                    break;

                case TipoEstrategiaDeCargaEnum.Usuarios:
                    return new Usuarios();
                    break;

                default:
                    return null;
            }
        }
    }
}