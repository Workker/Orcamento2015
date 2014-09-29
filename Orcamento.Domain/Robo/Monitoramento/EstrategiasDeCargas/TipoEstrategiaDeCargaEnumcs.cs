using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Orcamento.Domain.Robo.Monitoramento.EstrategiasDeCargas
{
    public enum TipoEstrategiaDeCargaEnum : short
    {
        Funcionarios = 0,
        TicketsDeProducao = 1,
        EstruturaOrcamentaria = 2,
        Usuarios = 3,
        AmarracaoDeUsuarios = 4,
        insumos = 5
    }
}
