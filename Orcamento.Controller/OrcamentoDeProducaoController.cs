using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Orcamento.Domain;
using Orcamento.Domain.Gerenciamento;
using Orcamento.Domain.Servico;
using Orcamento.Domain.DTO;
using Orcamento.InfraStructure;
using Orcamento.Domain.Servico.ServicoClonarObjetos;

namespace Orcamento.Controller
{
    public class OrcamentoDeProducaoController
    {
        private Orcamento.Domain.DB.Repositorio.Orcamentos orcamentos;

        public ServicoMapperOrcamentoView ServicoMapperOrcamentoView { get { return new ServicoMapperOrcamentoView(); } }
        public IViewOrcamentoDeProducao View { get; set; }
        public Orcamento.Domain.DB.Repositorio.Orcamentos Orcamentos
        {
            get
            {
                return orcamentos ?? (orcamentos = new Orcamento.Domain.DB.Repositorio.Orcamentos());
            }
            set
            {
                orcamentos = value;
            }
        }

        public OrcamentoDeProducaoController(IViewOrcamentoDeProducao view)
        {
            this.View = view;
        }

        public void PreencherOrcamentos()
        {
            this.View.PreencherOrcamentos();
            CarregarValidacao();
        }

        public void CriarNovoOrcamentoOperacional(Departamento departamento)
        {
            try
            {
                var servico = new ServicoCriarOrcamentoHospitalar();
                var orcamentosGerenciamento = Orcamentos.TodosOrcamentosHospitalares(departamento);

                GerenciadorDeOrcamentos gerenciamento = new GerenciadorDeOrcamentos();

                if (gerenciamento.PodeCriarOrcamento(orcamentosGerenciamento, departamento, TipoOrcamentoEnum.Hospitalar))
                {
                    this.View.Orcamento = servico.CriarOrcamentoHospitalar(orcamentosGerenciamento, departamento, 2014);
                    this.InformarDespesas();
                    PreencherOrcamentos();
                    CarregarValidacao();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CarregarOrcamento(int idOrcamentoId)
        {
            if (idOrcamentoId > 0)
            {
                var orcamentoDeProducao = Orcamentos.Obter<OrcamentoHospitalar>(idOrcamentoId);
                this.View.Orcamento = orcamentoDeProducao;
                this.InformarDespesas();
                CarregarValidacao();
            }
        }

        private void InformarDespesas()
        {
            this.View.Contas = ServicoMapperOrcamentoView.TransformarProducao(this.View.Orcamento);
        }

        public void SalvarOrcamento(List<ContaHospitalarDTO> contas, bool carregaOrcamentos)
        {
            ServicoMapperOrcamentoView.Salvar(this.View.Orcamento, contas);
            if (carregaOrcamentos)
                PreencherOrcamentos();
            this.InformarDespesas();
            this.View.PreencherReceitaBruta();
        }

        public void AtribuirVersaoFinal()
        {
            try
            {
                ServicoCriarOrcamentoHospitalar servico = new ServicoCriarOrcamentoHospitalar();
                servico.AtribuirVersaoFinal(this.View.Orcamento);
                PreencherOrcamentos();
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
                var servico = new ServicoCriarOrcamentoHospitalar();
                var orcamentosGerenciamento = Orcamentos.TodosOrcamentosHospitalaresMenos(this.View.Orcamento.Setor,this.View.Orcamento.Id);
                servico.DeletarOrcamento(this.View.Orcamento, orcamentosGerenciamento, this.View.Orcamento.Setor);
                PreencherOrcamentos();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<int> CalcularTotalMensal()
        {
            var listaResultados = new List<int>();

            for (int i = 1; i <= 12; i++)
            {
                var numeroAleatorio = new Random();
                listaResultados.Add(numeroAleatorio.Next());
            }

            return listaResultados;


        }

        private void CarregarValidacao()
        {
            var orcamentos = new Orcamento.Domain.DB.Repositorio.Orcamentos();
            var listaOrcamentos = orcamentos.TodosOrcamentosHospitalares(this.View.Departamento).ToList();

            if (!PodeCriarOrcamento(this.View.Departamento, listaOrcamentos))
                this.View.CarregarValidacaoDeDezVersoesDoBotaoIncluirNovaVersao();
            else
                this.View.RemoverValidacaoDeDezVersoesDoBotaoIncluirNovaVersao();
        }

        private bool PodeCriarOrcamento(Departamento setor, List<Domain.Orcamento> orcamentosDoCentroDeCustoEDepartamentoLogado)
        {
            return new GerenciadorDeOrcamentos().PodeCriarOrcamento(orcamentosDoCentroDeCustoEDepartamentoLogado, setor, TipoOrcamentoEnum.Hospitalar);
        }

        public void Clonar() 
        {

            try
            {
                var orcamentosGerenciamento = Orcamentos.TodosOrcamentosHospitalares(this.View.Orcamento.Setor);

                GerenciadorDeOrcamentos gerenciamento = new GerenciadorDeOrcamentos();
                var servicoCriar = new ServicoCriarOrcamentoHospitalar();

                if (gerenciamento.PodeCriarOrcamento(orcamentosGerenciamento, this.View.Orcamento.Setor, TipoOrcamentoEnum.Hospitalar))
                {
                    ServicoClonarOrcamentoHospitalar servico = new ServicoClonarOrcamentoHospitalar();
                    var orcamento = servico.Clonar(this.View.Orcamento);
                    this.View.Orcamento = servicoCriar.CriarOrcamentoHospitalar(orcamentosGerenciamento, orcamento.Setor, 2014, orcamento);
                    this.InformarDespesas();
                    PreencherOrcamentos();
                    CarregarValidacao();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            
            


        }
    }
}
