using Orcamento.Domain.Entities.Monitoramento;
using Orcamento.Domain.Robo.Fabricas;
using Orcamento.Domain.Robo.Monitoramento.EstrategiasDeCargas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Orcamento.Controller.Robo
{
    public class ImportarController
    {
        public void ImportarCarga(TipoEstrategiaDeCargaEnum tipo)
        {
            Carga carga =new Carga(FabricaDeImportacao.Criar(tipo));
            carga.Diretorio = "D:\\cargaorcamento2014\\FuncionariosCoorporativoComplementar.xls";
            carga.Processar();

        }
    }
}
