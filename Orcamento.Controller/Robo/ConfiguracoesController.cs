using Orcamento.Domain.DB.Repositorio;
using Orcamento.Domain.Gerenciamento;
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
            return processos.TodosGerais();
        }

        public List<Processo> ObterProcessos(int IdDepartamento)
        {
            GerenciarProcessosPorDepartamentoService gerenciador = new GerenciarProcessosPorDepartamentoService();
            return gerenciador.ObterProcessos(IdDepartamento);
        }

        public List<Departamento> ObterDepartamentos()
        {
            Departamentos departamentos = new Departamentos();
            return departamentos.Todos();
        }

        public void Deletar(TipoProcessoEnum tipo,int departamentoId)
        {
            var configuracoesDoDepartamento = new ConfiguracoesGeraisDoDepartamentoService();
            Departamentos departamentos = new Departamentos();
            configuracoesDoDepartamento.DeletarEstruturaOrcamentaria(tipo, departamentos.Obter(departamentoId));
        }
    }
}
