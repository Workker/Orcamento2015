using System;
using System.Collections.Generic;
using Orcamento.Domain.Entities.Monitoramento;
using Orcamento.Domain.Robo.Monitoramento.EstrategiasDeCargas;
using Orcamento.Domain.DB.Repositorio;
using System.Diagnostics.Contracts;
using Orcamento.Domain.Gerenciamento;
using System.Linq;
using Orcamento.Domain.Util.Specification;

namespace Orcamento.Domain.Robo.Monitoramento.EspecificacaoDeValidacaoDeCarga.EspecificacaoEstruturaOrcamentaria
{
    public class MotorDeValidacaoDeEstruturaOrcamentaria
    {
        private readonly Carga carga;
        private readonly List<EstruturaOrcamentariaExcel> estruturasOrcamentariasExcel;
        private TipoConta tipoContaOutras;

        public MotorDeValidacaoDeEstruturaOrcamentaria(Carga carga,
                                                       List<EstruturaOrcamentariaExcel> estruturasOrcamentariasExcel)
        {
            this.carga = carga;
            this.estruturasOrcamentariasExcel = estruturasOrcamentariasExcel;
        }

        private Departamentos departamentos;
        public virtual Departamentos DepartamentosRepositorio
        {
            get
            {
                return departamentos ?? (departamentos = new Departamentos());
            }
        }
        public virtual List<Departamento> Departamentos { get; set; }

        private Contas contasRepositorio;
        public virtual Contas ContasRepositorio
        {
            get
            {
                return contasRepositorio ?? (contasRepositorio = new Contas());
            }
        }

        private List<Conta> contas;
        public virtual List<Conta> Contas
        {
            get
            {
                return contas ?? (contas = new List<Conta>());
            }
        }

        private GruposDeConta gruposDeContaRepositorio;
        public GruposDeConta GruposDeContaRepositorio { get { return gruposDeContaRepositorio ?? (gruposDeContaRepositorio = new GruposDeConta()); } }

        private List<GrupoDeConta> gruposDeConta;
        public virtual List<GrupoDeConta> GruposDeConta
        {
            get
            {
                return gruposDeConta ?? (gruposDeConta = new List<GrupoDeConta>());
            }
        }

        private TiposConta tiposContaRepositorio;
        public virtual TiposConta TiposContaRepositorio
        {
            get
            {
                return tiposContaRepositorio ?? (tiposContaRepositorio = new TiposConta());
            }
        }

        private CentrosDeCusto centrosDeCustoRepositorio;
        public virtual CentrosDeCusto CentrosDeCustoRepositorio { get { return centrosDeCustoRepositorio ?? (centrosDeCustoRepositorio = new CentrosDeCusto()); } }

        private List<CentroDeCusto> centrosDeCustos;
        public virtual List<CentroDeCusto> CentrosDeCustos { get { return centrosDeCustos ?? (centrosDeCustos = new List<CentroDeCusto>()); } }

        public void Validar()
        {
            Departamentos = DepartamentosRepositorio.Todos();
            Contract.Requires(Departamentos != null, "Departamentos não encontrados");

            foreach (EstruturaOrcamentariaExcel estruturaOrcamentariaExcel in estruturasOrcamentariasExcel)
            {
                ValidaContas(estruturaOrcamentariaExcel);

                if (!carga.Ok())
                    return;

                ValidaGrupoDeConta(estruturaOrcamentariaExcel);

                if (!carga.Ok())
                    return;

                ValidaCentroDeCusto(estruturaOrcamentariaExcel);

                if (!carga.Ok())
                    return;

                ValidaDepartamentos(estruturaOrcamentariaExcel);
            }
        }

        private void ValidaGrupoDeConta(EstruturaOrcamentariaExcel estruturaOrcamentariaExcel)
        {
            GrupoDeConta grupoDeConta = null;

            if (GruposDeConta.Any(c => c.Nome == estruturaOrcamentariaExcel.NomeDaConta))
                grupoDeConta = GruposDeConta.First(c => c.Nome == estruturaOrcamentariaExcel.NomeDoGrupoDeConta);
            else
                grupoDeConta = GruposDeContaRepositorio.ObterPor(estruturaOrcamentariaExcel.NomeDoGrupoDeConta);

            Especificacao especificacaoConta = FabricaDeEspecificacaoCargaValidaEstruturaOrcamentariaGrupoDeConta.ObterEspecificacao(estruturasOrcamentariasExcel,estruturaOrcamentariaExcel, grupoDeConta);

            especificacaoConta.IsSatisfiedBy(carga);
        }

        private void ValidaCentroDeCusto(EstruturaOrcamentariaExcel estruturaOrcamentariaExcel)
        {
            CentroDeCusto centroDeCusto = null;

            if (CentrosDeCustos.Any(c => c.CodigoDoCentroDeCusto == estruturaOrcamentariaExcel.CodigoCentroDeCusto))
                centroDeCusto = CentrosDeCustos.First(c => c.CodigoDoCentroDeCusto == estruturaOrcamentariaExcel.CodigoCentroDeCusto);
            else
                centroDeCusto = CentrosDeCustoRepositorio.ObterPor(estruturaOrcamentariaExcel.CodigoCentroDeCusto);

            Especificacao especificacaoConta = FabricaDeEspecificacaoCargaValidaEstruturaOrcamentariaCentroDeUso.ObterEspecificacao(estruturasOrcamentariasExcel, estruturaOrcamentariaExcel, centroDeCusto);

            especificacaoConta.IsSatisfiedBy(carga);
        }

        #region Contas

        private void ValidaContas(EstruturaOrcamentariaExcel estruturaOrcamentariaExcel)
        {
            tipoContaOutras = TiposContaRepositorio.ObterPor((int)TipoContaEnum.Outros);

            Conta conta = null;

            if (Contas.Any(c => c.CodigoDaConta == estruturaOrcamentariaExcel.CodigoDaConta))
                conta = Contas.First(c => c.CodigoDaConta == estruturaOrcamentariaExcel.CodigoDaConta);
            else
                conta = ContasRepositorio.ObterContaPor(estruturaOrcamentariaExcel.CodigoDaConta);

            Especificacao especificacaoConta = FabricaDeEspecificacaoCargaValidaEstruturaOrcamentariaConta.ObterEspecificacao(estruturasOrcamentariasExcel, estruturaOrcamentariaExcel, conta);
            especificacaoConta.IsSatisfiedBy(carga);

            estruturaOrcamentariaExcel.Conta = conta;
        }

        #endregion

        #region Departamentos

        public void ValidaDepartamentos(EstruturaOrcamentariaExcel estruturaOrcamentariaExcel)
        {
            var departamento = Departamentos.FirstOrDefault(d => d.Nome == estruturaOrcamentariaExcel.Departamento);

            Especificacao espeficicacaoDepartamento = FabricaDeEspecificacaoEstruturaOrcamentariaDepartamento.ObterEspeficiacao(estruturasOrcamentariasExcel, estruturaOrcamentariaExcel, departamento);

            espeficicacaoDepartamento.IsSatisfiedBy(carga);
        }

        private void AdicionarDepartamento(Carga carga, EstruturaOrcamentariaExcel estruturaOrcamentariaExcel)
        {
            var novoDepartamento = FabricaDeDepartamento.Construir(estruturaOrcamentariaExcel.TipoDepartamento,
                                                                 estruturaOrcamentariaExcel.Departamento);
            Departamentos.Add(novoDepartamento);
            estruturaOrcamentariaExcel.DepartamentoEntidade = novoDepartamento;
        }

        #endregion
    }
}