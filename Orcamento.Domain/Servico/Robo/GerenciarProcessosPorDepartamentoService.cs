using Orcamento.Domain.DB.Repositorio;
using Orcamento.Domain.Robo.Monitoramento.EstruturaOrcamentaria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workker.Framework.Domain;

namespace Orcamento.Domain.Servico.Robo
{
    public class GerenciarProcessosPorDepartamentoService
    {
        public List<Processo> ObterProcessos(int IdDepartamento)
        {
            try
            {
                Processos processos = new Processos();
                Departamentos departamentos = new Departamentos();

                var departamento = departamentos.Obter(IdDepartamento);

                Assertion.NotNull(departamento, "Não foi possivel obter processos com este departamento selecionado.").Validate();

                var todos = processos.Todos(departamento);

                if (todos.Count == 14)
                    return todos;

                if (todos != null && todos.Count > 0)
                    processos.Deletar(todos);

                FabricaDeProcessos fabrica = new FabricaDeProcessos();
                return fabrica.CriarProcessos(departamento);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
