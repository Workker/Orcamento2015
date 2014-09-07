using Orcamento.Domain.DB.Repositorio;
using Orcamento.Domain.Gerenciamento;
using Orcamento.Robo.Web.Models.Configuracoes;
using Orcamento.Robo.Web.Models.Core;
using Orcamento.Robo.Web.Models.Notificacoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Orcamento.Robo.Web.Models.ConfiguracoesPoDepartamento
{
    public class ConfiguracaoPorDepartamentoModel : Notificacao
    {
        private List<ProcessoModel> processos;
        public List<ProcessoModel> Processos
        {
            get { return processos ?? (new List<ProcessoModel>()); }
            set { this.processos = value; }
        }

        public DepartamentoModel Departamento { get; set; }

        public List<DepartamentoModel> Departamentos { get; set; }

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


        internal void AdicionarDepartamento(Departamento departamento)
        {
            if(this.Departamentos ==  null)
                this.Departamentos = new List<DepartamentoModel>();

            this.Departamentos.Add(new DepartamentoModel(){Id = departamento.Id,Nome = departamento.Nome,Selecionado = false});
        }
    }
}