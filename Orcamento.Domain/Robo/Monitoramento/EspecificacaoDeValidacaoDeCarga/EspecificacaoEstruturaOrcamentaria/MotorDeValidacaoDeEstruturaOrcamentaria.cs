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
        public virtual List<Departamento> Departamentos { get; set; }

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
                if (departamentos == null)
                    departamentos = new Departamentos();

                return departamentos;
            }
        }

        public void Validar()
        {
            foreach (EstruturaOrcamentariaExcel estruturaOrcamentariaExcel in estruturasOrcamentariasExcel)
            {
                // FabricaDeEspecificacaoEstruturaOrcamentaria.ObterEspeficicacao(estruturaOrcamentariaExcel).
                // IsSatisfiedBy(carga);

                // CONTA
                // GRUPO DE CONTA
                // CENTRO DE CUSTO

                // DEPARTAMENTOS
                Departamentos = DepartamentosRepositorio.Todos();
                Contract.Requires(Departamentos != null, "Departamentos não encontrados");

                ValidaDepartamentos();
            }
        }

        #region Departamentos

        public void ValidaDepartamentos()
        {
            foreach (EstruturaOrcamentariaExcel estruturaOrcamentariaExcel in estruturasOrcamentariasExcel)
            {
                if (estruturaOrcamentariaExcel.TipoAlteracaoDepartamento == TipoAlteracao.Inclusao)
                {
                    if (!Departamentos.Any(d => d.Nome == estruturaOrcamentariaExcel.Departamento))
                        AdicionarDepartamento(carga, estruturaOrcamentariaExcel);
                    else
                        carga.AdicionarDetalhe("Departamento já existe", string.Format("Não foi possível incluir o departamento {0} pois o mesmo já está cadastrado.", estruturaOrcamentariaExcel.Departamento), estruturaOrcamentariaExcel.Linha, TipoDetalheEnum.erroDeValidacao);
                }
                else
                {
                    Especificacao espeficicacaoDepartamento = FabricaDeEspecificacaoEstruturaOrcamentariaDepartamento.ObterEspeficiacao(estruturaOrcamentariaExcel, departamento);

                    if (espeficicacaoDepartamento.IsSatisfiedBy(carga))
                    {
                        estruturaOrcamentariaExcel.DepartamentoEntidade = Departamentos.FirstOrDefault(d => d.Nome == estruturaOrcamentariaExcel.Departamento);
                    }
                }
            }
        }

        private void AdicionarDepartamento(Carga carga, EstruturaOrcamentariaExcel estruturaOrcamentariaExcel)
        {

            var novoDepartamento = FabricaDepartamento.Construir(estruturaOrcamentariaExcel.TipoDepartamento,
                                                                 estruturaOrcamentariaExcel.Departamento);
            Departamentos.Add(novoDepartamento);
        }
    }

        #endregion

    public class FabricaDepartamento
    {
        public static Departamento Construir(TipoDepartamento tipoDepartamento, string nome)
        {
            switch (tipoDepartamento)
            {
                case TipoDepartamento.hospital:
                    return new Hospital(nome);
                case TipoDepartamento.setor:
                    return new Setor(nome);
            }

            throw new Exception("Erro ao criar um novo departamento.");
        }
    }
}