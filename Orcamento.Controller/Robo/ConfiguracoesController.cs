using Orcamento.Domain.DB.Repositorio;
using Orcamento.Domain.Robo.Monitoramento.EstruturaOrcamentaria;
using Orcamento.Domain.Servico.Robo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Orcamento.Controller.Robo
{
    public class ConfiguracoesController
    {
        private ConfiguracoesGeraisService service; 

        public void Deletar() 
        {
            service = new ConfiguracoesGeraisService();
            service.DeletarEstruturaOrcamentaria();
        }

        public void Deletar(TipoProcessoEnum tipo)
        {
            service = new ConfiguracoesGeraisService();
            service.DeletarEstruturaOrcamentaria(tipo);
        }

        public List<Domain.Robo.Monitoramento.EstruturaOrcamentaria.Processo> ObterProcessos()
        {
            Processos processos = new Processos();
            return processos.Todos<Processo>().ToList();
        }
    }
}
