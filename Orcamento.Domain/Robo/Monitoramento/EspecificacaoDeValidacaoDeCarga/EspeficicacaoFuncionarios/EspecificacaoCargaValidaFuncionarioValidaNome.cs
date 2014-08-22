using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Orcamento.Domain.Robo.Monitoramento.EspecificacaoDeValidacaoDeCarga
{
    public class EspecificacaoCargaValidaFuncionarioValidaNome : EspecificacaoCargaValidaFuncionario
    {
        public EspecificacaoCargaValidaFuncionarioValidaNome(Domain.Entities.Monitoramento.Funcionarios.FuncionarioExcel funcionario)
        {
            this.FuncionarioExcel = funcionario;
        }

        public override bool IsSatisfiedBy(Entities.Monitoramento.Carga candidate)
        {
            if (string.IsNullOrEmpty(FuncionarioExcel.Nome))
                candidate.AdicionarDetalhe("Nome não Preenchido", "Nome não Preenchido", FuncionarioExcel.Linha,
                                       TipoDetalheEnum.erroDeProcesso);
            return true;
        }
    }
}
