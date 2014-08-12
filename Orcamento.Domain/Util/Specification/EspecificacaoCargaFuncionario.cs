using Orcamento.Domain.Entities.Monitoramento;
using Orcamento.Domain.Gerenciamento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using fun =Orcamento.Domain.Entities.Monitoramento.Funcionarios;

namespace Orcamento.Domain.Util.Specification
{
    public abstract class EspecificacaoCargaFuncionario : ISpecification<Carga>
    {
        public Carga Carga { get; set; }
        public List<fun.FuncionarioExcel> Funcionarios { get; set; }
        public fun.FuncionarioExcel Funcionario { get; set; }
        public Setor Setor { get; set; }
        public CentroDeCusto CentroDeCusto { get; set; }

        public abstract bool IsSatisfiedBy(Carga candidate);
       

        public EspecificacaoCargaFuncionario And(EspecificacaoCargaFuncionario other) 
        {
            return new AndSpecification(this, other);
        }
 
        public EspecificacaoCargaFuncionario Or(EspecificacaoCargaFuncionario other) 
        {
            return new OrSpecification(this, other);
        }
 
        public EspecificacaoCargaFuncionario Not() 
        {
           return new NotSpecification(this);
        }

        public void ProcessarEspecificacoes(Carga carga)
        {
            var funcionariosNulos = new EspecificacaoCargaFuncionariosNulos();

            funcionariosNulos.IsSatisfiedBy(carga);

            foreach (var funcionarioExcel in Funcionarios)
            {
                this.Funcionario = funcionarioExcel;

                var setorNulo = new EspecificacaoCargaFuncionarioSetorNaoEncontrado();
                var centroNulo = new EspecificacaoCargaFuncionarioCentroDeCustoNaoEncontrado();

                setorNulo.And(centroNulo).IsSatisfiedBy(carga);

            }

        }
    }
}
