using Orcamento.Domain.Gerenciamento;
using Orcamento.Domain.Robo.Monitoramento.EstrategiasDeCargas;
using Orcamento.Domain.Util.Specification;
using System;
using System.Collections.Generic;

namespace Orcamento.Domain.Robo.Monitoramento.EspecificacaoDeValidacaoDeCarga.EspecificacaoEstruturaOrcamentaria
{
    public class FabricaDeEspecificacaoEstruturaOrcamentariaDepartamento
    {
        public static Especificacao ObterEspeficiacao(List<EstruturaOrcamentariaExcel> estruturasOrcamentariasExcel, EstruturaOrcamentariaExcel estruturaOrcamentariaExcel, Departamento departamento)
        {
            return new EspecificacaoCargaValidaEstruturaOrcamentariaDepartamento(estruturasOrcamentariasExcel, estruturaOrcamentariaExcel, departamento);
        }
    }

    public class FabricaDeEspecificacaoCargaValidaEstruturaOrcamentariaConta
    {
        public static Especificacao ObterEspecificacao(List<EstruturaOrcamentariaExcel> estruturasOrcamentariasExcel, EstruturaOrcamentariaExcel estruturaOrcamentariaExcel, Conta conta)
        {
            return new EspecificacaoCargaValidaEstruturaOrcamentariaConta(estruturasOrcamentariasExcel, estruturaOrcamentariaExcel, conta);
        }
    }

    public class FabricaDeEspecificacaoCargaValidaEstruturaOrcamentariaGrupoDeConta
    {
        public static Especificacao ObterEspecificacao(List<EstruturaOrcamentariaExcel> estruturasOrcamentariasExcel, EstruturaOrcamentariaExcel estruturaOrcamentariaExcel, GrupoDeConta grupoDeConta)
        {
            return new EspecificacaoCargaValidaEstruturaOrcamentariaGrupoConta(estruturasOrcamentariasExcel, estruturaOrcamentariaExcel, grupoDeConta);
        }
    }

    public class FabricaDeEspecificacaoCargaValidaEstruturaOrcamentariaCentroDeUso
    {
        public static Especificacao ObterEspecificacao(List<EstruturaOrcamentariaExcel> estruturasOrcamentariasExcel, EstruturaOrcamentariaExcel estruturaOrcamentariaExcel, CentroDeCusto centroDeCusto)
        {
            return new EspecificacaoCargaValidaEstruturaOrcamentariaCentroDeCusto(estruturasOrcamentariasExcel, estruturaOrcamentariaExcel, centroDeCusto);
        }
    }
}