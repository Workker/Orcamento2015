using Orcamento.Domain.DB.Repositorio;
using Orcamento.Domain.Entities.Monitoramento;
using Orcamento.Domain.Robo.Monitoramento;
using Orcamento.Domain.Util.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Orcamento.Domain.Util.Specification
{
    public class EspecificacaoCargaFuncionarioSetorNaoEncontrado : EspecificacaoCargaFuncionario
    {
        public override bool IsSatisfiedBy(Carga candidate)
        {
            Setores setores = new Setores();

            var setorCarga = setores.ObterPor(Funcionario.Departamento);

            var satisfeito = setorCarga != null;

            if (!satisfeito)
                candidate.AdicionarDetalhe("Hospital/Setor nao encontrado",
                                       "Hospital/Setor: " + Funcionario.Departamento + " inexistente.",
                                       Funcionario.Linha, TipoDetalheEnum.erroDeProcesso);

            return satisfeito;
        }
    }
}
