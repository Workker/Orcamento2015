using Orcamento.Domain.Entities.Monitoramento;
using Orcamento.Domain.Robo.Monitoramento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Orcamento.Domain.Util.Specification
{
    public class EspecificacaoCargaFuncionariosNulos : EspecificacaoCargaFuncionario
    {

        public override bool IsSatisfiedBy(Carga candidate)
        {
            var satisfeito = (this.Funcionarios != null && this.Funcionarios.Count > 0);

            if(!satisfeito)
                candidate.AdicionarDetalhe("Nenhum registro foi obtido", "Nenhum registro foi obtido por favor verifique o excel.", 0, TipoDetalheEnum.erroLeituraExcel);

            return satisfeito;
        }
    }
}
