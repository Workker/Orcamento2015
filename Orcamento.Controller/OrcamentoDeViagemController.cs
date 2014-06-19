using System;
using System.Collections.Generic;
using System.Linq;
using Orcamento.Domain.ComponentesDeOrcamento.OrcamentoDeViagem;
using Orcamento.Domain.DB.Repositorio;
using Orcamento.Domain;
using Orcamento.Domain.DTO.Viagem;
using Orcamento.Domain.Gerenciamento;
using Orcamento.Domain.Servico;
using Orcamento.Domain.DTO;
using Orcamento.InfraStructure;

namespace Orcamento.Controller
{
    public class OrcamentoDeViagemController
    {
        #region Atributos

        private ICidades cidades;
        private IOrcamentos orcamentos;

        #endregion

        #region Propriedades

        public ICidades Cidades
        {
            get { return cidades ?? (cidades = new Cidades()); }
            set { cidades = value; }
        }

        public IOrcamentos Orcamentos
        {
            get { return orcamentos ?? (orcamentos = new Orcamentos()); }
            set { orcamentos = value; }
        }

        public IViewOrcamentoDeViagem View
        {
            get;
            set;
        }

        public ServicoOrcamentoDeViagens ServicoOrcamentoDeViagens
        {
            get { return new ServicoOrcamentoDeViagens(); }
        }

        #endregion

        #region Construtores

        public OrcamentoDeViagemController(IViewOrcamentoDeViagem view)
        {
            this.View = view;
        }

        #endregion

        #region Métodos

        public void CarregarPaginaDeAcordoComOCentroDeCustoSelecionado()
        {
            CarregarInformacoesDosOrcamentos();
            this.View.ZerarViagens();
        }

        private void CarregarInformacoesDosOrcamentos()
        {
            var centroDeCusto = this.View.Departamento.ObterCentroDeCustoPor(this.View.CentroDeCustoId);

            var listaOrcamentos = this.Orcamentos.TodosOrcamentosDeViagemPor(centroDeCusto, this.View.Departamento);

            CarregarValidacao(listaOrcamentos);

            CarregarVersoesDeOrcamentos(listaOrcamentos);

            VerificarMensagemDeVersaoFinal(listaOrcamentos);
        }

        private void VerificarMensagemDeVersaoFinal(List<Domain.Orcamento> orcamentos)
        {
            if (orcamentos != null && orcamentos.Count() > 0 && !orcamentos.Any(o => o.VersaoFinal))
                this.View.InformarNaoExisteVersaoFinal();
        }

        public void CriarNovoOrcamentoOperacional(Departamento setor, int centroDeCustoId)
        {
            var orcamentosDoCentroDeCustoEDepartamentoLogado = ObterOrcamentosDoCentroDeCustoEDepartamentoLogado(setor, centroDeCustoId);

            bool podeCriarOrcamento = PodeCriarOrcamento(setor, centroDeCustoId, orcamentosDoCentroDeCustoEDepartamentoLogado);

            if (podeCriarOrcamento)
            {
                this.View.OrcamentoViagem = this.ServicoOrcamentoDeViagens.CriarOrcamentoDeViagem(orcamentosDoCentroDeCustoEDepartamentoLogado, setor, setor.ObterCentroDeCustoPor(centroDeCustoId), 2014);

                CarregarOrcamentoDesnormalizado();
            }

            CarregarInformacoesDosOrcamentos();
        }

        public void CarregarOrcamentosDeViagem(int orcamentoDeViagemId)
        {
            this.View.OrcamentoViagem = this.Orcamentos.Obter<OrcamentoDeViagem>(orcamentoDeViagemId);

            CarregarOrcamentoDesnormalizado();
        }

        public IOrderedEnumerable<DespesaDeViagemDTO> ObterDespesasTotais()
        {
            var cidades = this.Cidades.Todas();

            var servico = new ServicoDesnormalizarTotaisEmDespesasPorMeses(this.View.OrcamentoViagem, cidades);

            return servico.ObterDespesasDeViagemDesnormalizadas();
        }

        public void SalvarOrcamento(List<ContaDTO> contas)
        {
            try
            {
                this.ServicoOrcamentoDeViagens.SalvarOrcamento(this.View.OrcamentoViagem, contas);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AtribuirVersaoFinal()
        {
            try
            {
                this.ServicoOrcamentoDeViagens.AtribuirVersaoFinal(this.View.OrcamentoViagem);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ApagarVersao()
        {
            try
            {
                var orcamentosGerenciamento = Orcamentos.TodosOrcamentosDeViagemPor(this.View.OrcamentoViagem.CentroDeCusto, this.View.Departamento,
                    this.View.OrcamentoViagem.Id);
                this.ServicoOrcamentoDeViagens.DeletarOrcamento(this.View.OrcamentoViagem, orcamentosGerenciamento, this.View.OrcamentoViagem.Setor);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Remover(Domain.Orcamento orcamento)
        {
            Orcamentos.Deletar(orcamento);
        }

        public void DesnormalizarContasDeViagem()
        {
            var servicoDesnormalizarContas = new ServicoDesnormalizarContas(this.View.OrcamentoViagem);

            this.View.Contas = servicoDesnormalizarContas.ObterContasDesnormalizadas();
        }

        private void CarregarOrcamentoDesnormalizado()
        {
            DesnormalizarContasDeViagem();

            this.View.CarregarOrcamento();
        }

        private List<Domain.Orcamento> ObterOrcamentosDoCentroDeCustoEDepartamentoLogado(Departamento setor, int centroDeCustoId)
        {
            var centroDeCusto = setor.ObterCentroDeCustoPor(centroDeCustoId);

            return Orcamentos.TodosOrcamentosDeViagemPor(centroDeCusto, setor);
        }

        private void CarregarVersoesDeOrcamentos(List<Domain.Orcamento> listaOrcamentos)
        {
            var servico = new ServicoDesnormalizarVersoesDeOrcamentoDeViagens(listaOrcamentos);

            this.View.CarregarVersoesDeOrcamentos(servico.ObterOrcamentoDeVersoesDesnormalizados());
        }

        private void CarregarValidacao(List<Domain.Orcamento> listaOrcamentos)
        {
            if (!PodeCriarOrcamento(this.View.Departamento, this.View.CentroDeCustoId, listaOrcamentos))
                this.View.CarregarValidacaoDeDezVersoesDoBotaoIncluirNovaVersao();
            else
                this.View.RemoverValidacaoDeDezVersoesDoBotaoIncluirNovaVersao();
        }

        private bool PodeCriarOrcamento(Departamento setor, int centroDeCustoId, List<Domain.Orcamento> orcamentosDoCentroDeCustoEDepartamentoLogado)
        {
            return new GerenciadorDeOrcamentos().PodeCriarOrcamento(orcamentosDoCentroDeCustoEDepartamentoLogado, setor, setor.ObterCentroDeCustoPor(centroDeCustoId), TipoOrcamentoEnum.Viagem);
        }

        #endregion
    }
}
