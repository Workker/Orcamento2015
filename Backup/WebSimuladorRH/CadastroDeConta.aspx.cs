using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Orcamento.Domain.Gerenciamento;
using Orcamento.Controller;
using Orcamento.Domain.DB.Repositorio;

namespace WebSimuladorRH
{
    public partial class CadastroDeConta : BasePage
    {
        private ContaController controller;
        private string funcaoDaPagina;

        public ContaController Controller
        {
            private get { return controller; }
            set { controller = value; }
        }

        public string FuncaoDaPagina
        {
            get { return funcaoDaPagina; }
            set { funcaoDaPagina = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.controller = new ContaController();

            FuncaoDaPagina = Request.QueryString["funcao"];

            if (FuncaoDaPagina == "a")
            {
                divAlteracaoDeContas.Visible = true;

                if (!Page.IsPostBack)
                {
                    PopularDropDownDeContas();
                }
            }
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtCodigoConta.Text))
                    throw new Exception("Insira o codigo da conta.");

                if (string.IsNullOrEmpty(txtDescricao.Text))
                    throw new Exception("Insira a descrição da conta.");

                if (FuncaoDaPagina == "a")
                {
                    var conta = Controller.BuscarContaPor(int.Parse(hdnIdConta.Value));

                    conta.Nome = txtDescricao.Text;
                    conta.CodigoDaConta = txtCodigoConta.Text;

                    Controller.Salvar(conta);
                }
                else
                {
                    var contaRecuperada = controller.BuscarContaPor(txtCodigoConta.Text);
                    if (contaRecuperada != null && contaRecuperada.Id > 0)
                        throw new Exception("Este codigo de conta já existe.");

                    Conta conta = new Conta();
                    var tiposDeConta = new TiposConta();

                    conta.Nome = txtDescricao.Text;
                    conta.CodigoDaConta = txtCodigoConta.Text;
                    conta.TipoConta = tiposDeConta.Obter<TipoConta>(1);

                    Controller.Salvar(conta);
                }


                Notificacao = "Conta salva com sucesso";
            }
            catch (Exception ex)
            {
                Notificacao = ex.Message;
            }

            EnviarMensagem();
            LimparCampos();
        }

        private void PopularDropDownDeContas()
        {
            ddlContas.DataSource = Controller.BuscarTodasAsContas();

            ddlContas.DataValueField = "Id";
            ddlContas.DataTextField = "Nome";
            ddlContas.DataBind();

            ddlContas.Items.Insert(0, new ListItem("Selecione", ""));
        }

        protected void ddlContas_SelectedIndexChanged(object sender, EventArgs e)
        {
            var conta = Controller.BuscarContaPor(int.Parse(ddlContas.SelectedValue));

            txtCodigoConta.Text = conta.CodigoDaConta;
            txtDescricao.Text = conta.Nome;
            hdnIdConta.Value = conta.Id.ToString();
        }

        private void LimparCampos()
        {
            txtCodigoConta.Text = string.Empty;
            txtDescricao.Text = string.Empty;
            hdnIdConta.Value = string.Empty;

            ddlContas.SelectedValue = "";
        }
    }
}