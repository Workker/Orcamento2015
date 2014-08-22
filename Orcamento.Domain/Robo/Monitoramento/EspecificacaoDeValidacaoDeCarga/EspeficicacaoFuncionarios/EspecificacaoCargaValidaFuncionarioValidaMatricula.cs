using Orcamento.Domain.Gerenciamento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Orcamento.Domain.Robo.Monitoramento.EspecificacaoDeValidacaoDeCarga
{
    public class EspecificacaoCargaValidaFuncionarioValidaMatricula : EspecificacaoCargaValidaFuncionario
    {
        public EspecificacaoCargaValidaFuncionarioValidaMatricula(Domain.Entities.Monitoramento.Funcionarios.FuncionarioExcel funcionario, bool matriculaRepetida)
        {
            this.MatriculaRepetida = matriculaRepetida;
            this.FuncionarioExcel = funcionario;
        }

        public override bool IsSatisfiedBy(Entities.Monitoramento.Carga candidate)
        {
            
            if (string.IsNullOrEmpty(FuncionarioExcel.NumeroMatricula))
                candidate.AdicionarDetalhe("Número de matrícula não preenchido", "Número de matrícula não preenchido",
                                           FuncionarioExcel.Linha,
                                           TipoDetalheEnum.erroDeProcesso);

            return true;
        }
    }
}
