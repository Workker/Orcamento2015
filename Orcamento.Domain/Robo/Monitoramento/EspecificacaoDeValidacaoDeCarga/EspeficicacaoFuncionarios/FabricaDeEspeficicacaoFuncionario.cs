using Orcamento.Domain.Gerenciamento;
using Orcamento.Domain.Util.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Orcamento.Domain.Robo.Monitoramento.EspecificacaoDeValidacaoDeCarga.EspeficicacaoFuncionarios
{
    public class FabricaDeEspeficicacaoFuncionario
    {
        public static Especificacao ObterEspeficicacao(Domain.Entities.Monitoramento.Funcionarios.FuncionarioExcel funcionario, bool matriculaRepetida)
        {
            var validaAno =
                new EspecificacaoCargaValidaFuncionarioValidaAno(funcionario);

            var validaFuncao =
                new EspecificacaoCargaValidaFuncionarioValidaFuncao(funcionario);

            var validaMatricula =
                new EspecificacaoCargaValidaFuncionarioValidaMatricula(funcionario,matriculaRepetida);

            var validaMes =
                new EspecificacaoCargaValidaFuncionarioValidaMes(funcionario);

            var validaNome =
                new EspecificacaoCargaValidaFuncionarioValidaNome(funcionario);

           // var validaNumeroVaga =
           //new EspecificacaoCargaValidaFuncionarioValidaNumeroVaga(funcionario);

            var validaSalario =
           new EspecificacaoCargaValidaFuncionarioValidaSalario(funcionario);

            return
                validaAno.And(validaFuncao).And(validaMatricula).And(validaNome).And(validaSalario);
        }
    }

    public class FabricaDeEspeficicacaoDepartamento
    {
        public static Especificacao ObterEspeficicacao(Domain.Entities.Monitoramento.Funcionarios.FuncionarioExcel funcionario, Departamento departamento)
        {
            var validaDepartamento =
           new EspecificacaoCargaValidaFuncionarioValidaDepartamento(funcionario, departamento);

            return
                validaDepartamento;
        }
    }

    public class FabricaDeEspeficicacaoCentroDeCusto
    {
        public static Especificacao ObterEspeficicacao(Domain.Entities.Monitoramento.Funcionarios.FuncionarioExcel funcionario, CentroDeCusto centroDeCusto)
        {
            var validaCentroDeCusto =
           new EspecificacaoCargaValidaFuncionarioValidaCentroDeCusto(funcionario, centroDeCusto);

            return
                validaCentroDeCusto;
        }
    }
}
