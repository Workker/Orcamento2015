using Orcamento.Domain.DB.Repositorio;
using Orcamento.Domain.Gerenciamento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Orcamento.Domain.Robo.Monitoramento.EstruturaOrcamentaria
{
    public class FabricaDeProcessos
    {
        private List<Processo> processos { get; set; }
        private Departamento departamento { get; set; }

        public List<Processo> CriarProcessos(Departamento departamento)
        {
            processos = new List<Processo>();
            this.departamento = departamento;

            CriarProcesso("Deletar Totais da DRE por Departamento",TipoProcessoEnum.DeletarTotaisdaDREporDepartamento);
            CriarProcesso("Deletar Funcionarios por Departamento", TipoProcessoEnum.DeletarFuncionariosporDepartamento);
            CriarProcesso("Deletar Tickets de Pessoal por Departamento", TipoProcessoEnum.DeletarTicketsdePessoalporDepartamento);
            CriarProcesso("Deletar Tickets De Receita por Departamento", TipoProcessoEnum.DeletarTicketsDeReceitaporDepartamento);
            CriarProcesso("Deletar Tickets De Unitários por Departamento", TipoProcessoEnum.DeletarTicketsDeUnitáriosporDepartamento);
            CriarProcesso("Deletar Orçamentos Pessoais por Departamento", TipoProcessoEnum.DeletarOrcamentosPessoaisporDepartamento);
            CriarProcesso("Deletar Tickets De Insumo por Departamento", TipoProcessoEnum.DeletarTicketsDeInsumosporDepartamento);
            CriarProcesso("Deletar Orçammentos De Produção por Departamento", TipoProcessoEnum.DeletarOrcammentosDeProduçãoporDepartamento);
            CriarProcesso("Deletar Orçammentos De Viagem por Departamento", TipoProcessoEnum.DeletarOrcammentosDeViagemporDepartamento);
            CriarProcesso("Deletar Orçammentos Operacionais por Departamento", TipoProcessoEnum.DeletarOrcammentosOperacionaisporDepartamento);
            CriarProcesso("Deletar Usuarios por Departamento", TipoProcessoEnum.DeletarUsuariosporDepartamento);
            CriarProcesso("Deletar Departamentos por Departamento", TipoProcessoEnum.DeletarDepartamentosporDepartamento);
            CriarProcesso("Deletar Acordos de convenção por Departamento", TipoProcessoEnum.DeletarAcordosdeconvencaoporDepartamento);
            CriarProcesso("Deletar Estrutura por Departamento", TipoProcessoEnum.DeletarEstruturaCompletaPorDepartamento);
         
            Processos repositorio = new Processos();

            repositorio.Salvar(processos);

            return this.processos;
        }

        private void CriarProcesso(string nome, TipoProcessoEnum tipoEnum)
        {
            TiposDeProcesso tipos = new TiposDeProcesso();
            var tipo = tipos.ObterPor(tipoEnum);

            var processo = new Processo()
            {
                Departamento = this.departamento,
                Nome = nome,
                TipoProcesso = tipo
            };

            processos.Add(processo);
        }
    }
}
