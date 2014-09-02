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
        DeletarEstruturaCompleta = 16
    }

    public class TipoProcesso : IAggregateRoot<int>
    {
        public virtual int Id { get; set; }
        public virtual string Tipo { get; set; }
    }
}
