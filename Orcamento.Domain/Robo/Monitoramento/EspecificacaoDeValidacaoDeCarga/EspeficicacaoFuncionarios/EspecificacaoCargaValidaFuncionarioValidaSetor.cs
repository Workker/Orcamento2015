using Orcamento.Domain.Gerenciamento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Orcamento.Domain.Robo.Monitoramento.EspecificacaoDeValidacaoDeCarga
{
    public class EspecificacaoCargaValidaFuncionarioValidaDepartamento : EspecificacaoCargaValidaFuncionario
    {
        public EspecificacaoCargaValidaFuncionarioValidaDepartamento(Domain.Entities.Monitoramento.Funcionarios.FuncionarioExcel funcionario, Departamento departamento)
        {
            this.FuncionarioExcel = funcionario;
            this.Departamento = departamento;
        }

        public override bool IsSatisfiedBy(Entities.Monitoramento.Carga candidate)
        {
            var satisfeito = Departamento != null;

            if (!satisfeito)
                candidate.AdicionarDetalhe("Hospital/Setor não encontrado",
                                       "Hospital/Setor: " + FuncionarioExcel.Departamento + " inexistente.",
                                       FuncionarioExcel.Linha, TipoDetalheEnum.erroDeValidacao);

            return satisfeito;
        }
    }
}
