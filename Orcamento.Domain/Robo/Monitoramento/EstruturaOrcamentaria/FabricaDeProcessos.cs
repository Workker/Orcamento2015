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

            CriarProcesso("Deletar Totais da DRE",TipoProcessoEnum.DeletarTotaisdaDREporDepartamento);
            CriarProcesso("Deletar Funcionarios", TipoProcessoEnum.DeletarFuncionariosporDepartamento);
            CriarProcesso("Deletar Tickets de Pessoal",TipoProcessoEnum.DeletarTicketsdePessoalporDepartamento);
            CriarProcesso("Deletar Tickets De Receita", TipoProcessoEnum.DeletarTicketsDeReceitaporDepartamento);
            CriarProcesso("Deletar Tickets De Unitários",TipoProcessoEnum.DeletarTicketsDeUnitáriosporDepartamento);
            CriarProcesso("Deletar Orçamentos Pessoais", TipoProcessoEnum.DeletarOrcamentosPessoaisporDepartamento);
            CriarProcesso("Deletar Tickets De Insumo", TipoProcessoEnum.DeletarTicketsDeInsumosporDepartamento);
            CriarProcesso("Deletar Orçammentos De Produção",TipoProcessoEnum.DeletarOrcammentosDeProduçãoporDepartamento);
            CriarProcesso("Deletar Orçammentos De Viagem",TipoProcessoEnum.DeletarOrcammentosDeViagemporDepartamento);
            CriarProcesso("Deletar Orçammentos Operacionais", TipoProcessoEnum.DeletarOrcammentosOperacionaisporDepartamento);
            CriarProcesso("Deletar Usuarios", TipoProcessoEnum.DeletarUsuariosporDepartamento);
            CriarProcesso("Deletar Departamentos", TipoProcessoEnum.DeletarDepartamentosporDepartamento);
            CriarProcesso("Deletar Acordos de convenção",TipoProcessoEnum.DeletarAcordosdeconvencao);
            CriarProcesso("Deletar Estrutura por departamento/Hospital", TipoProcessoEnum.DeletarEstruturaCompleta);
         
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
