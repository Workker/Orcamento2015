using Orcamento.Domain.Entities.Monitoramento;
using Orcamento.Domain.Gerenciamento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using fun =Orcamento.Domain.Entities.Monitoramento.Funcionarios;

namespace Orcamento.Domain.Util.Specification
{
    public abstract class Especificacao : ISpecification<Carga>
    {
        public Carga Carga { get; set; }
        
        public abstract bool IsSatisfiedBy(Carga candidate);
       

        public Especificacao And(Especificacao other) 
        {
            return new AndSpecification(this, other);
        }
 
        public Especificacao Or(Especificacao other) 
        {
            return new OrSpecification(this, other);
        }
 
        public Especificacao Not() 
        {
           return new NotSpecification(this);
        }

        public void ProcessarEspecificacoes(Carga carga)
        {
            //var funcionariosNulos = new EspecificacaoCargaFuncionariosNulos();

            //funcionariosNulos.IsSatisfiedBy(carga);

            //foreach (var funcionarioExcel in Funcionarios)
            //{
            //    this.Funcionario = funcionarioExcel;

            //    var setorNulo = new EspecificacaoCargaFuncionarioSetorNaoEncontrado();
            //    var centroNulo = new EspecificacaoCargaFuncionarioCentroDeCustoNaoEncontrado();

            //    setorNulo.And(centroNulo).IsSatisfiedBy(carga);

            //}

        }
    }
}
