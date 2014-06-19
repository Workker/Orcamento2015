using System;
using System.Collections.Generic;
using System.Linq;
using Orcamento.Controller.Views;
using Orcamento.Domain.DB.Repositorio;
using Orcamento.Domain.Gerenciamento;
using Orcamento.Domain.Servico;
using Orcamento.Domain;
using Orcamento.Domain.DTO;
using Orcamento.InfraStructure;

namespace Orcamento.Controller
{
    public class DespesaOperacionalController
    {
        #region Atributos

        private IOrcamentos orcamentos;
        private IDepartamentos departamentos;

        #endregion

        #region Propriedades

        public IViewDespesaOperacional View { get; set; }

        public IOrcamentos Orcamentos
        {
            get { return orcamentos ?? (orcamentos = new Orcamentos()); }
            set { orcamentos = value; }
        }

        public IDepartamentos Departamentos
        {
            get { return departamentos ?? (departamentos = new Departamentos()); }
            set { departamentos = value; }
        }

        public ServicoOrcamentoOperacionalVersao ServicoOrcamento { get { return new ServicoOrcamentoOperacionalVersao(); } }

        #endregion

        #region Métodos

        public Departamento ObterDepartamento(int departamentoId)
        {
            return Departamentos.Obter(departamentoId);
        }

        public List<Domain.Orcamento> ObterOrcamentosOperacionais()
        {
            var centroDeCusto = this.View.SetorSelecionado.ObterCentroDeCustoPor(this.View.CentroDeCustoId);
            return Orcamentos.TodosOrcamentosOperacionaisPor(centroDeCusto, this.View.SetorSelecionado);
        }

        public void CriarNovoOrcamentoOperacional(Departamento setor, int centroDeCustoId)
        {
            try
            {
                var orcamentosGerenciamento = Orcamentos.TodosOrcamentosOperacionaisPor(setor.ObterCentroDeCustoPor(centroDeCustoId), setor);

                bool podeCriarMaisUmaVersaoDeOrcamento = PodeCriarMaisUmaVersaoDeOrcamento(setor, centroDeCustoId, orcamentosGerenciamento);

                if (podeCriarMaisUmaVersaoDeOrcamento)
                {
                    this.View.OrcamentoOperacional = this.ServicoOrcamento.CriarOrcamentoOperacional(orcamentosGerenciamento, setor, setor.ObterCentroDeCustoPor(centroDeCustoId), 2014);

                    this.View.InformarVersao();

                    InformarDespesas();
                }

                CarregarGrids();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool PodeCriarMaisUmaVersaoDeOrcamento(Departamento setor, int centroDeCustoId,List<Domain.Orcamento> orcamentosGerenciamento)
        {
            var gerenciamento = new GerenciadorDeOrcamentos();

            return gerenciamento.PodeCriarOrcamento(orcamentosGerenciamento, setor,
                                                    setor.ObterCentroDeCustoPor(
                                                        centroDeCustoId),
                                                    TipoOrcamentoEnum.
                                                        DespesaOperacional);
        }

        public void CarregarOrcamentoOperacional(int idOrcamentoId)
        {
            var orcamentoOperacional = Orcamentos.Obter<OrcamentoOperacionalVersao>(idOrcamentoId);
            this.View.OrcamentoOperacional = orcamentoOperacional;
            this.View.InformarVersao();

            InformarDespesas();
        }

        public DespesaOperacionalController(IViewDespesaOperacional view)
        {
            this.View = view;
        }

        public void Salvar()
        {
            var contas = this.View.ObterDespesasCorrentes();
            SalvarOrcamento(contas);
            CarregarGrids();
        }

        public void AtribuirVersaoFinal()
        {
            try
            {
                var contas = this.View.ObterDespesasCorrentes();
                SalvarOrcamento(contas);

                ServicoOrcamentoOperacionalVersao servico = new ServicoOrcamentoOperacionalVersao();
                servico.AtribuirVersaoFinal(this.View.OrcamentoOperacional);

                CarregarGrids();
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
                var orcamentosGerenciamento = Orcamentos.TodosOrcamentosOperacionaisPor(this.View.OrcamentoOperacional.CentroDeCusto, this.View.SetorSelecionado,this.View.OrcamentoOperacional.Id);
                ServicoOrcamento.DeletarOrcamento(this.View.OrcamentoOperacional, orcamentosGerenciamento, this.View.SetorSelecionado);

                ManipularVisualizacaoDoBotaoApagar();
                this.View.PreencherVersoes();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ManipularVisualizacaoDoBotaoApagar()
        {
            var orcamentos = Orcamentos.TodosOrcamentosOperacionaisPor(this.View.OrcamentoOperacional.CentroDeCusto,
                                                                       this.View.SetorSelecionado);

            if (!orcamentos.Any())
                this.View.EsconderOBotaoApagar();
        }

        public void CarregarValidacoesDeControleDeVersao(List<Domain.Orcamento> listaOrcamentos)
        {
            if (!PodeCriarMaisUmaVersaoDeOrcamento(this.View.SetorSelecionado, this.View.CentroDeCustoId, listaOrcamentos))
                this.View.CarregarValidacaoDeDezVersoesDoBotaoIncluirNovaVersao();
            else
                this.View.RemoverValidacaoDeDezVersoesDoBotaoIncluirNovaVersao();

            VerificarMensagemDeVersaoFinal(listaOrcamentos);
        }

        private void VerificarMensagemDeVersaoFinal(List<Domain.Orcamento> orcamentos)
        {
            if (orcamentos != null && orcamentos.Count() > 0 && !orcamentos.Any(o => o.VersaoFinal))
                this.View.InformarNaoExisteVersaoFinal();
        }

        private void CarregarGrids()
        {
            CarregarOrcamentoOperacional(this.View.OrcamentoOperacional.Id);
            this.View.PreencherVersoes();
            this.View.InformarVersao();
        }

        private void SalvarOrcamento(List<ContaDTO> contas)
        {
            try
            {
                ServicoOrcamento.SalvarOrcamento(View.OrcamentoOperacional, contas);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void InformarDespesas()
        {
            var despesas = this.View.OrcamentoOperacional.DespesasOperacionais;

            var contas = despesas.ToList<Despesa>().Select(x => x.Conta).Distinct();

            List<ContaDTO> contasDTO = new List<ContaDTO>();

            foreach (var conta in contas)
            {
                ContaDTO contaDTO = new ContaDTO() { DespesaId = despesas.FirstOrDefault(c => c.Conta.Id == conta.Id).Id, Conta = conta.Nome, ContaId = conta.Id, DespesaOperacionalId = this.View.OrcamentoOperacional.Id, ValorTotal = this.View.OrcamentoOperacional.DespesasOperacionais.Where(d => d.Conta.Id == conta.Id).Select(d => d.Valor).Sum() };

                contaDTO.Despesas = new List<DespesaDTO>();

                foreach (var despesa in despesas.Where(x => x.Conta.Id == conta.Id).OrderBy(x => (short)x.Mes))
                {
                    contaDTO.Despesas.Add(new DespesaDTO { Mes = (short)despesa.Mes, Valor = despesa.Valor, DespesaId = despesa.Id });
                }

                if (despesas != null && despesas.Where(x => x.Conta.Id == conta.Id).FirstOrDefault() != null)
                    contaDTO.MemoriaDeCalculo = despesas.Where(x => x.Conta.Id == conta.Id).FirstOrDefault().MemoriaDeCalculo;

                contasDTO.Add(contaDTO);
            }

            this.View.PreencherRepeaterDespesas(contasDTO);
        }

        #endregion
    }
}
