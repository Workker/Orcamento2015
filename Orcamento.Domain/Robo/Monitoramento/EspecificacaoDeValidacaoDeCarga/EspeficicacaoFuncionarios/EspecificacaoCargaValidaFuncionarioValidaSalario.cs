using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Orcamento.Domain.Robo.Monitoramento.EspecificacaoDeValidacaoDeCarga
{
    public class EspecificacaoCargaValidaFuncionarioValidaSalario : EspecificacaoCargaValidaFuncionario
    {
        public EspecificacaoCargaValidaFuncionarioValidaSalario(Domain.Entities.Monitoramento.Funcionarios.FuncionarioExcel funcionario)
        {
            this.FuncionarioExcel = funcionario;
        }

        public override bool IsSatisfiedBy(Entities.Monitoramento.Carga candidate)
        {
            if (FuncionarioExcel.Salario == default(double))
                candidate.AdicionarDetalhe("Salário não preenchido", "Salário não preenchido", FuncionarioExcel.Linha,
                                       TipoDetalheEnum.erroDeProcesso);
            
            return true;
        }
    }
}
