using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Orcamento.Domain.Robo.Monitoramento.EspecificacaoDeValidacaoDeCarga
{
    public class EspecificacaoCargaValidaFuncionarioValidaMes : EspecificacaoCargaValidaFuncionario
    {
        public EspecificacaoCargaValidaFuncionarioValidaMes(Domain.Entities.Monitoramento.Funcionarios.FuncionarioExcel funcionario)
        {
            this.FuncionarioExcel = funcionario;
        }

        public override bool IsSatisfiedBy(Entities.Monitoramento.Carga candidate)
        {
            if (FuncionarioExcel.Mes == default(int))
                candidate.AdicionarDetalhe("Mês não preenchido", "Mês do funcionário não preenchido", FuncionarioExcel.Linha,
                                       TipoDetalheEnum.erroDeProcesso);

            return true;
        }
    }
}
