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
        //D:\\cargaorcamento2014\\FuncionariosCoorporativoComplementar.xls"
        public Carga ImportarCarga(TipoEstrategiaDeCargaEnum tipo,string diretorio)
        {
            Carga carga = new Carga(FabricaDeImportacao.Criar(tipo));
            switch (tipo)
            {
                case TipoEstrategiaDeCargaEnum.Funcionarios:
                    carga.Diretorio = diretorio;
                    break;
                case TipoEstrategiaDeCargaEnum.TicketsDeProducao:
                    carga.Diretorio = diretorio;
                    break;
            }

            carga.Processar();
            return carga;
        }
    }
}
