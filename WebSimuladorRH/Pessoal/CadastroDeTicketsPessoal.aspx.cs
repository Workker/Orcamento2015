using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Orcamento.Controller;
using Orcamento.Controller.Views;
using Orcamento.Domain;
using System.Linq;
using Orcamento.Domain.DB.Repositorio;
using Orcamento.Domain.Gerenciamento;
using Orcamento.Domain.Servico.Pessoal;

namespace WebSimuladorRH.Pessoal
{
    public partial class CadastroDeTicketsPessoal : BasePage, IViewTicketDeOrcamento
    {
        

        public IList<TicketDeOrcamentoPessoal> TodosOsTicketsCadastrados
        {
            get { return (IList<TicketDeOrcamentoPessoal>)Session["todosOsTicketsCadastrados"]; }
            set { Session["todosOsTicketsCadastrados"] = value; }
        }

        private ServicoGerarOrcamentoPessoalPorCentroDeCusto _servicoGerarOrcamentoPessoalPor;
        public ServicoGerarOrcamentoPessoalPorCentroDeCusto ServicoGerarOrcamentoPessoalPor
        {
            get { return _servicoGerarOrcamentoPessoalPor ?? (_servicoGerarOrcamentoPessoalPor = new ServicoGerarOrcamentoPessoalPorCentroDeCusto()); }
            set { _servicoGerarOrcamentoPessoalPor = value; }
        }

        public Departamento Departamento
        {
            get
            {
                return (Departamento)Session["DepartamentoLogado"];
            }
            set
            {
                Session["DepartamentoLogado"] = value;
            }
        }

        private TicketsController _controller;

        protected void Page_Load(object sender, EventArgs e)
        {
            _controller = new TicketsController { View = this };
            var departamentos = new Departamentos();
            Departamento = departamentos.Obter((int)Session["DepartamentoLogadoId"]);

            if (!IsPostBack)
            {
                _controller.CarregarPagina(Departamento);
            }
        }

        public void Carregar(IList<TicketDeOrcamentoPessoal> ticketsDeOrcamentoPessoal)
        {

            TodosOsTicketsCadastrados = ticketsDeOrcamentoPessoal;

            rptTickets.DataSource = ticketsDeOrcamentoPessoal.OrderBy(x => x.Descricao);
            rptTickets.DataBind();

            CarregarAcordoConvencao();
        }

        private void CarregarAcordoConvencao()
        {
            AcordosDeConvencao acordos = new AcordosDeConvencao();
            var acordoConvencao = acordos.ObterAcordoDeConvencao(Departamento);

            txtAcordoConvencaoMes.Text = acordoConvencao.MesAumento.ToString();
            txtAcordoConvencaoValor.Text = acordoConvencao.Porcentagem.ToString();
        }

        private void Salvar()
        {
            foreach (RepeaterItem item in rptTickets.Items)
            {
                if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                {
                    var ticketId = int.Parse(((HiddenField)item.FindControl("hdnIdTicket")).Value);

                    var ticket = TodosOsTicketsCadastrados.SingleOrDefault(x => x.Id == ticketId);

                    if (ticket != null)
                    {
                        ticket.Valor = int.Parse(((TextBox)item.FindControl("txtValor")).Text.Replace("(", "").Replace(")", "").Replace(".", ""));

                        _controller.Salvar(ticket);
                    }
                }
            }

            SalvarAcordoConvencao();
            //ControlesCentroDeCusto controles = new ControlesCentroDeCusto();
            //var todosControles = controles.ObterTodos(Departamento);
            //foreach (var controle in todosControles)
            //{
            //    controle.Salvo = false;
            //}

            //controles.SalvarLista(todosControles);
        }

        private void SalvarAcordoConvencao()
        {
            AcordosDeConvencao acordos = new AcordosDeConvencao();
            var acordoConvencao = acordos.ObterAcordoDeConvencao(Departamento);

            int mesAumento = 0;
            int.TryParse(txtAcordoConvencaoMes.Text.Replace("(", "").Replace(")", ""), out mesAumento);
            acordoConvencao.MesAumento = mesAumento;
            double porcentagem = 0;
            double.TryParse(txtAcordoConvencaoValor.Text.Replace("(", "").Replace(")", ""), out porcentagem);
            acordoConvencao.Porcentagem = porcentagem;

            acordos.Salvar(acordoConvencao);
        }


        protected void Salvando(object sender, EventArgs e)
        {
            Salvar();
            var departamentos = new Departamentos();

            var departamento = departamentos.Obter((int)Session["DepartamentoLogadoId"]);

            _controller.CarregarPagina(departamento);
        }
    }
}