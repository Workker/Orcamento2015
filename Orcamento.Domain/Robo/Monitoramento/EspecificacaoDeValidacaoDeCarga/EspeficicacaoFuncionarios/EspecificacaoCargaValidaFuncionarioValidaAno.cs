using Orcamento.Domain.Gerenciamento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Orcamento.Domain.Robo.Monitoramento.EspecificacaoDeValidacaoDeCarga
{
    public class EspecificacaoCargaValidaFuncionarioValidaAno : EspecificacaoCargaValidaFuncionario
    {
        public EspecificacaoCargaValidaFuncionarioValidaAno(Domain.Entities.Monitoramento.Funcionarios.FuncionarioExcel funcionario, Departamento departamento, CentroDeCusto centroDeCusto)
        {
            this.CentroDeCusto = centroDeCusto;
            this.FuncionarioExcel = funcionario;
            this.Departamento = departamento;
        }
        public EspecificacaoCargaValidaFuncionarioValidaAno(Domain.Entities.Monitoramento.Funcionarios.FuncionarioExcel funcionario, Departamento departamento)
        {
            this.FuncionarioExcel = funcionario;
            this.Departamento = departamento;
        }

        public EspecificacaoCargaValidaFuncionarioValidaAno(Domain.Entities.Monitoramento.Funcionarios.FuncionarioExcel funcionario)
        {
            this.FuncionarioExcel = funcionario;
        }

        public override bool IsSatisfiedBy(Entities.Monitoramento.Carga candidate)
        {
            if (FuncionarioExcel.Ano == default(int))
                candidate.AdicionarDetalhe("Ano não preenchido", "Ano do funcionário não preenchido",
                                           FuncionarioExcel.Linha,
                                           TipoDetalheEnum.erroDeProcesso);
            return true;
        }
    }
}
