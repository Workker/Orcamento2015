using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Orcamento.Robo.Web.Models.Configuracoes
{
    [Serializable]
    public class Configuracao
    {
        private List<Processo> processos;
        public List<Processo> Processos
        {
            get { return processos ?? (new List<Processo>()); }
            set { this.processos = value; }
        }

        public virtual string Mensagem { get; set; }
        public virtual string Titulo { get; set; }

        public virtual void AdicionarProcesso(Domain.Robo.Monitoramento.EstruturaOrcamentaria.Processo processo)
        {
            if( this.processos == null)
                this.processos = new List<Processo>();

            var novo = new Processo()
                           {
                               Nome = processo.Nome,
                               Iniciado = processo.Iniciado.HasValue? processo.Iniciado.Value.ToString():"N/D",
                               Status = processo.Status,
                               Finalizado = processo.Finalizado.HasValue ? processo.Finalizado.Value.ToString() : "N/D",
                               TipoProcesso = processo.TipoProcesso.Id
                           };

            this.processos.Add(novo);
        }

        public string Tipo { get; set; }
    }

    [Serializable]
    public class Processo
    {
        public string Nome { get; set; }
        public string Status { get; set; }
        public string Iniciado { get; set; }
        public String Finalizado { get; set; }

        public int TipoProcesso { get; set; }
    }
}