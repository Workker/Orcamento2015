using Orcamento.Domain.Gerenciamento;
using Orcamento.Domain.Robo.Monitoramento.EstrategiasDeCargas;
using Orcamento.Domain.Util.Specification;
using System;

namespace Orcamento.Domain.Robo.Monitoramento.EspecificacaoDeValidacaoDeCarga.EspecificacaoEstruturaOrcamentaria
{
    public class FabricaDeEspecificacaoEstruturaOrcamentaria
    {
        public static Especificacao ObterEspeficicacao(EstruturaOrcamentariaExcel estruturaOrcamentaria)
        {
            //var validaDepartamento =
            //    new EspecificacaoCargaValidaEstruturaOrcamentariaDepartamento(estruturaOrcamentaria);

            //var validaCentroDeCusto =
            //    new EspecificacaoCargaValidaEstruturaOrcamentariaCentroDeCusto(estruturaOrcamentaria);

            //var validaConta =
            //    new EspecificacaoCargaValidaEstruturaOrcamentariaConta(estruturaOrcamentaria);

            //var validaGrupoConta =
            //    new EspecificacaoCargaValidaEstruturaOrcamentariaGrupoConta(estruturaOrcamentaria);

            //return
            //    validaDepartamento.And(validaCentroDeCusto).And(validaConta).And(validaGrupoConta);
            throw new NotImplementedException();
        }
    }

    public class FabricaDeEspecificacaoEstruturaOrcamentariaDepartamento
    {
        public static Especificacao ObterEspeficiacao(EstruturaOrcamentariaExcel estruturaOrcamentariaExcel, Departamento departamento)
        {
            return new EspecificacaoCargaValidaEstruturaOrcamentariaDepartamento(estruturaOrcamentariaExcel, departamento);
        }
    }
}