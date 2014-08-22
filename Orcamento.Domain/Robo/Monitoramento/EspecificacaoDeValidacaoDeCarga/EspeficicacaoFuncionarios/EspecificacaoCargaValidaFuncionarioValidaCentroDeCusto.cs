using Orcamento.Domain.Gerenciamento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Orcamento.Domain.Robo.Monitoramento.EspecificacaoDeValidacaoDeCarga
{
    public class EspecificacaoCargaValidaFuncionarioValidaCentroDeCusto : EspecificacaoCargaValidaFuncionario
    {

        public EspecificacaoCargaValidaFuncionarioValidaCentroDeCusto(Domain.Entities.Monitoramento.Funcionarios.FuncionarioExcel funcionario, CentroDeCusto centroDeCusto)
        {
            this.FuncionarioExcel = funcionario;
            this.CentroDeCusto = centroDeCusto;
        }

      

        public override bool IsSatisfiedBy(Entities.Monitoramento.Carga candidate)
        {
            var satisfeito = this.CentroDeCusto != null;

            if (!satisfeito)
                candidate.AdicionarDetalhe("Centro de custo nao encontrado",
                                       "centro de custo: " + FuncionarioExcel.CodigoCentroDeCusto + " inexistente.",
                                       FuncionarioExcel.Linha, TipoDetalheEnum.erroDeProcesso);

            return satisfeito;
        }
    }
}
