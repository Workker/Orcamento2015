using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Orcamento.Domain.Robo.Monitoramento.EspecificacaoDeValidacaoDeCarga
{
    public class EspecificacaoCargaValidaFuncionarioValidaNumeroVaga : EspecificacaoCargaValidaFuncionario
    {
        public EspecificacaoCargaValidaFuncionarioValidaNumeroVaga(Domain.Entities.Monitoramento.Funcionarios.FuncionarioExcel funcionario)
        {
            this.FuncionarioExcel = funcionario;
        }

        public override bool IsSatisfiedBy(Entities.Monitoramento.Carga candidate)
        {
            if (FuncionarioExcel.NumeroVaga == default(int))
                candidate.AdicionarDetalhe("Número de vaga não preenchido", "Número de vaga não preenchido", FuncionarioExcel.Linha,
                                       TipoDetalheEnum.erroDeProcesso);

            return true;
        }
    }
}
