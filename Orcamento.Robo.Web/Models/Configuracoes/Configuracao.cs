using Orcamento.Robo.Web.Models.Notificacoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Orcamento.Robo.Web.Models.Configuracoes
{
    [Serializable]
    public class ConfiguracaoModel : Notificacao
    {
        private List<ProcessoModel> processos;
        public List<ProcessoModel> Processos
        {
            get { return processos ?? (new List<ProcessoModel>()); }
            set { this.processos = value; }
        }

        public virtual void AdicionarProcesso(Domain.Robo.Monitoramento.EstruturaOrcamentaria.Processo processo)
        {
            if (this.processos == null)
                this.processos = new List<ProcessoModel>();

            var novo = new ProcessoModel()
                           {
                               Nome = processo.Nome,
                               Iniciado = processo.Iniciado.HasValue ? processo.Iniciado.Value.ToString() : "N/D",
                               Status = processo.Status,
                               Finalizado = processo.Finalizado.HasValue ? processo.Finalizado.Value.ToString() : "N/D",
                               TipoProcesso = processo.TipoProcesso.Id
                           };

            this.processos.Add(novo);
        }


    }

    [Serializable]
    public class ProcessoModel
    {
        public string Nome { get; set; }
        public string Status { get; set; }
        public string Iniciado { get; set; }
        public String Finalizado { get; set; }

        public int TipoProcesso { get; set; }
    }
}