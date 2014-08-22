using Orcamento.Domain.DB.Repositorio;
using Orcamento.Domain.Entities.Monitoramento;
using Orcamento.Domain.Robo.Monitoramento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Orcamento.Domain.Util.Specification
{
    //public class EspecificacaoCargaFuncionarioCentroDeCustoNaoEncontrado : Especificacao
    //{
    //    public override bool IsSatisfiedBy(Carga candidate)
    //    {
    //        CentrosDeCusto centros = new CentrosDeCusto();

    //        var centro = centros.ObterPor(Funcionario.Departamento);

    //        var satisfeito = this.CentroDeCusto != null;

    //        if (!satisfeito)
    //            candidate.AdicionarDetalhe("Centro de custo nao encontrado",
    //                                   "centro de custo: " + Funcionario.CodigoCentroDeCusto + " inexistente.",
    //                                   Funcionario.Linha, TipoDetalheEnum.erroDeProcesso);

    //        return satisfeito;
    //    }
    //}
}
