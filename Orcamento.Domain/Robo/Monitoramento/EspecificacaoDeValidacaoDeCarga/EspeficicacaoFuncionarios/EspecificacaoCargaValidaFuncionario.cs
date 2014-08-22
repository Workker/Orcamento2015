using Orcamento.Domain.ComponentesDeOrcamento.OrcamentoPessoal;
using Orcamento.Domain.DB.Repositorio;
using Orcamento.Domain.Gerenciamento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Orcamento.Domain.Robo.Monitoramento.EspecificacaoDeValidacaoDeCarga
{
    public abstract class EspecificacaoCargaValidaFuncionario : EspecificacaoCarga
    {
        public virtual Domain.Entities.Monitoramento.Funcionarios.FuncionarioExcel FuncionarioExcel { get; set; }
        public virtual Funcionario Funcionario
        {
            get;
            set;
        }
        public virtual Departamento Departamento { get; set; }
        public virtual CentroDeCusto CentroDeCusto { get; set; }
        public bool MatriculaRepetida { get; set; }

        public EspecificacaoCargaValidaFuncionario()
        {
        }

        public EspecificacaoCargaValidaFuncionario(Domain.Entities.Monitoramento.Funcionarios.FuncionarioExcel funcionario, Departamento departamento, CentroDeCusto centroDeCusto)
       {
           this.CentroDeCusto = centroDeCusto;
           this.FuncionarioExcel = funcionario;
           this.Departamento = departamento;
       }
       public EspecificacaoCargaValidaFuncionario(Domain.Entities.Monitoramento.Funcionarios.FuncionarioExcel funcionario, Departamento departamento)
       {
           this.FuncionarioExcel = funcionario;
           this.Departamento = departamento;
       }

       public EspecificacaoCargaValidaFuncionario(Domain.Entities.Monitoramento.Funcionarios.FuncionarioExcel funcionario)
       {
           this.FuncionarioExcel = funcionario;
       }

    }
}
