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
    }
}
