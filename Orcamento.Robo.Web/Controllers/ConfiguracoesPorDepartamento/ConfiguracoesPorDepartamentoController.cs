using Orcamento.Domain.Gerenciamento;
using Orcamento.Domain.Robo.Monitoramento.EstruturaOrcamentaria;
using Orcamento.Robo.Web.Models.Configuracoes;
using Orcamento.Robo.Web.Models.ConfiguracoesPoDepartamento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Orcamento.Robo.Web.Controllers.ConfiguracoesPorDepartamento
{
    public class ConfiguracoesPorDepartamentoController : ControllerBase
    {
        private Orcamento.Controller.Robo.ConfiguracoesController controller = new Controller.Robo.ConfiguracoesController();

        //
        // GET: /ConfiguracoesPorDepartamento/

        public ActionResult Index()
        {
            var configuracao = ObterProcessos();

            return View(configuracao);
        }

        private ConfiguracaoPorDepartamentoModel ObterProcessos(int IdDepartamento)
        {
            List<Orcamento.Domain.Robo.Monitoramento.EstruturaOrcamentaria.Processo> processos = controller.ObterProcessos(IdDepartamento);

            ConfiguracaoPorDepartamentoModel configuracao = new ConfiguracaoPorDepartamentoModel();
            foreach (var processo in processos)
            {
                configuracao.AdicionarProcesso(processo);
            }
            return configuracao;
        }

        private ConfiguracaoPorDepartamentoModel ObterProcessos()
        {
            ConfiguracaoPorDepartamentoModel configuracao = new ConfiguracaoPorDepartamentoModel();
            configuracao.Processos = new List<ProcessoModel>();

            var departamentos = controller.ObterDepartamentos();

            foreach (var departamento in departamentos)
            {
                configuracao.AdicionarDepartamento(departamento);
            }

            AdicionarProcessosAoPrimeiroDepartamento(departamentos, configuracao);

            return configuracao;
        }

        private void AdicionarProcessosAoPrimeiroDepartamento(List<Departamento> departamentos, ConfiguracaoPorDepartamentoModel configuracao)
        {
            if (departamentos != null && departamentos.Count > 0)
            {
                List<Orcamento.Domain.Robo.Monitoramento.EstruturaOrcamentaria.Processo> processos =
                    controller.ObterProcessos(departamentos.FirstOrDefault().Id);

                foreach (var processo in processos)
                {
                    configuracao.AdicionarProcesso(processo);
                }
            }
        }

        public ActionResult Deletar(int departamentoId)
        {
            try
            {
                controller.Deletar(TipoProcessoEnum.DeletarEstruturaCompletaPorDepartamento,departamentoId);

                this.ShowMessage(MessageTypeEnum.success, "Estrutura deletada com sucesso!", true);

                var configuracao = new ConfiguracaoPorDepartamentoModel();

                configuracao.Tipo = "success";
                configuracao.Mensagem = "Estrutura deletada com sucesso.";
                configuracao.Titulo = "Estrutura deletada.";

                return View("Index",configuracao);
            }
            catch (Exception)
            {

                throw;
            }

        }

        public ActionResult Delete(int tipoProcesso, int departamentoId)
        {
            try
            {
                controller.Deletar((TipoProcessoEnum)tipoProcesso, departamentoId);

                this.ShowMessage(MessageTypeEnum.success, "Estrutura deletada com sucesso!");
                var configuracao = ObterProcessos(departamentoId);

                CriarConfiguracao(tipoProcesso, configuracao, "success", "Processo " +
                                    configuracao.Processos.FirstOrDefault(p => p.TipoProcesso == tipoProcesso).Nome +
                                    " foi concluido.", "Processo: " + configuracao.Processos.FirstOrDefault(p => p.TipoProcesso == tipoProcesso).Nome);

                return PartialView("_Processos", configuracao);
            }
            catch (Exception)
            {

                throw;
            }

        }

        public ActionResult Processos(int idDepartamento)
        {
            try
            {
                var configuracao = ObterProcessos(idDepartamento);
                return PartialView("_Processos", configuracao);
            }
            catch (Exception ex)
            {
                this.ShowMessage(MessageTypeEnum.danger, "Erro ao obter Processos.");
                return null;
            }
        }

    }
}
