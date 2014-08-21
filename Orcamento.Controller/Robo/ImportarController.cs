using Orcamento.Domain.DB.Repositorio.Robo;
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
        public Carga ImportarCarga(TipoEstrategiaDeCargaEnum tipo, string diretorio, string fileName)
        {
            Cargas cargas = new Cargas();
            Carga carga = new Carga(FabricaDeImportacao.Criar(tipo), tipo, fileName,diretorio);

            switch (tipo)
            {
                case TipoEstrategiaDeCargaEnum.Funcionarios:
                    carga.Diretorio = diretorio;
                    break;
                case TipoEstrategiaDeCargaEnum.TicketsDeProducao:
                    carga.Diretorio = diretorio;
                    break;
            }

            cargas.Salvar(carga);
            carga.Processar();
            cargas.Salvar(carga);

            return carga;
        }
    }
}
