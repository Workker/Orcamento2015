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
        public Cargas CargasRepo { get{return new Cargas();} }
        public Carga Carga { get; set; }

        public Carga ImportarCarga(TipoEstrategiaDeCargaEnum tipo, string diretorio, string fileName, bool atualizaEntidade)
        {
            CriarCarga(tipo, diretorio, fileName, atualizaEntidade);
            ProcessarCarga();

            return Carga;
        }

        private void CriarCarga(TipoEstrategiaDeCargaEnum tipo, string diretorio, string fileName, bool atualizaEntidade)
        {
            Carga = new Carga(FabricaDeImportacao.Criar(tipo), tipo, fileName, diretorio, atualizaEntidade);
            CargasRepo.Salvar(Carga);
        }

        private void ProcessarCarga()
        {
            Carga.Processar();
            CargasRepo.Salvar(Carga);
        }

    }
}
