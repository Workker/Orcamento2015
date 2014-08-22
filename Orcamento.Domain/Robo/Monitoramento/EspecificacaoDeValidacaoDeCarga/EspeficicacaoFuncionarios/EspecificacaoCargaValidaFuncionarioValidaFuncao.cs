using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Orcamento.Domain.Robo.Monitoramento.EspecificacaoDeValidacaoDeCarga
{
    public class EspecificacaoCargaValidaFuncionarioValidaFuncao : EspecificacaoCargaValidaFuncionario
    {
        public EspecificacaoCargaValidaFuncionarioValidaFuncao(Domain.Entities.Monitoramento.Funcionarios.FuncionarioExcel funcionario)
        {
            this.FuncionarioExcel = funcionario;
        }

        public override bool IsSatisfiedBy(Entities.Monitoramento.Carga candidate)
        {
            if (string.IsNullOrEmpty(FuncionarioExcel.Funcao))
                candidate.AdicionarDetalhe("Cargo não preenchido", "Cargo do funcionario não preenchido",
                                           FuncionarioExcel.Linha,
                                           TipoDetalheEnum.erroDeProcesso);

            return true;
        }
    }
}
