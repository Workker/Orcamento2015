using Orcamento.Domain.Entities.Monitoramento;
using Orcamento.Domain.Robo.Monitoramento.EstrategiasDeCargas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Orcamento.Domain.Robo.Fabricas
{
    public class FabricaDeImportacao
    {
        public static IProcessaCarga Criar(TipoEstrategiaDeCargaEnum tipo)
        {
            switch (tipo)
            {
                case TipoEstrategiaDeCargaEnum.Funcionarios:
                    return new Funcionarios();
                    break;
                case TipoEstrategiaDeCargaEnum.TicketsDeProducao:
                    return new ProcessaTicketsDeProducao();
                    break;
                default:
                    return null;
            }
        }
    }
}
