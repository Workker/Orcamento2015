using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Orcamento.Domain.Robo.Monitoramento.EstruturaOrcamentaria
{
    public enum TipoProcessoEnum : short
    {
        DeletarTotaisDaDRE = 1,
        DeletarFuncionarios = 2,
        DeletarOrcamentosPessoais = 3,
        DeletarTicketsDeReceita = 4,
        DeletarTicketsDeUnitários = 5,
        DeletarTicketsDeInsumos = 6,
        DeletarOrcammentosDeProdução = 7,
        DeletarOrcammentosOperacionais = 8,
        DeletarOrcammentosDeViagem = 9,
        DeletarAcordosdeconvencao = 10,
        DeletarUsuarios = 11,
        DeletarDepartamentos = 12,
        DeletarCentrosDeCusto = 13,
        DeletarGruposDeContas = 14,
        DeletarConta = 15,
        DeletarEstruturaCompleta = 16,
        DeletarTicketsDePessoal = 17,
        DeletarTotaisdaDREporDepartamento = 18,
        DeletarFuncionariosporDepartamento = 19,
        DeletarOrcamentosPessoaisporDepartamento = 20,
        DeletarTicketsDeReceitaporDepartamento = 21,
        DeletarTicketsDeUnitáriosporDepartamento = 22,
        DeletarTicketsDeInsumosporDepartamento = 23,
        DeletarOrcammentosDeProduçãoporDepartamento = 24,
        DeletarOrcammentosOperacionaisporDepartamento = 25,
        DeletarOrcammentosDeViagemporDepartamento = 26,
        DeletarAcordosdeconvencaoporDepartamento = 27,
        DeletarUsuariosporDepartamento = 28,
        DeletarDepartamentosporDepartamento = 29,
        DeletarTicketsdePessoalporDepartamento = 30,
        DeletarEstruturaCompletaPorDepartamento = 31
    }

    public class TipoProcesso : IAggregateRoot<int>
    {
        public virtual int Id { get; set; }
        public virtual string Tipo { get; set; }
    }
}
