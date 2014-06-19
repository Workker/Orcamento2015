using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Orcamento.Domain.Gerenciamento;
using Orcamento.Domain.DB.Repositorio;

namespace WebSimuladorRH
{
    public partial class AlterarLocacaoFuncionario : BasePage
    {
        public Departamento Departamento
        {
            get { return (Departamento)Session["DepartamentoFuncionario"]; }
            set { Session["DepartamentoFuncionario"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;

            CarregarComboDeDepartamento();
        }

        private void CarregarComboDeDepartamento()
        {
            Departamentos departamentos = new Departamentos();
            var todosDepartamentos = departamentos.Todos();

            DdlDepartamento.DataSource = todosDepartamentos.OrderBy(x => x.Nome);
            DdlDepartamento.DataTextField = "Nome";
            DdlDepartamento.DataValueField = "Id";
            DdlDepartamento.DataBind();

            DdlDepartamento.Items.Insert(0, new ListItem("Selecione", "0"));

            ddlDepartamentoDestino.DataSource = todosDepartamentos.OrderBy(x => x.Nome);
            ddlDepartamentoDestino.DataTextField = "Nome";
            ddlDepartamentoDestino.DataValueField = "Id";
            ddlDepartamentoDestino.DataBind();

            ddlDepartamentoDestino.Items.Insert(0, new ListItem("Selecione", "0"));
        }

        protected void SelecionandoDepartamento(object sender, EventArgs e)
        {
            if (DdlDepartamento.SelectedValue == "0")
            {
                ddlCentrosDeCusto.DataSource = null;
                ddlCentrosDeCusto.DataBind();
                return;
            }

            Departamentos departamentos = new Departamentos();
            var departamento = departamentos.Obter(int.Parse(DdlDepartamento.SelectedValue));

            if (departamento == null)
            {
                Notificacao = "Departamento não encontrado por favor contacte o Administrador.";
                EnviarMensagem();
                return;
            }

            ddlCentrosDeCusto.DataSource = departamento.CentrosDeCusto.OrderBy(x => x.Nome);
            ddlCentrosDeCusto.DataTextField = "Nome";
            ddlCentrosDeCusto.DataValueField = "CodigoDoCentroDeCusto";
            ddlCentrosDeCusto.DataBind();

            ddlCentrosDeCusto.Items.Insert(0, new ListItem("Selecione", "0"));
        }

        protected void SelecionandoDepartamentoDestino(object sender, EventArgs e)
        {
            if (ddlDepartamentoDestino.SelectedValue == "0")
            {
                ddlCentroDeCustoDestino.DataSource = null;
                ddlCentroDeCustoDestino.DataBind();
                return;
            }

            Departamentos departamentos = new Departamentos();
            var departamento = departamentos.Obter(int.Parse(ddlDepartamentoDestino.SelectedValue));

            if (departamento == null)
            {
                Notificacao = "Departamento não encontrado por favor contacte o Administrador.";
                EnviarMensagem();
                return;
            }

            ddlCentroDeCustoDestino.DataSource = departamento.CentrosDeCusto.OrderBy(x => x.Nome);
            ddlCentroDeCustoDestino.DataTextField = "Nome";
            ddlCentroDeCustoDestino.DataValueField = "CodigoDoCentroDeCusto";
            ddlCentroDeCustoDestino.DataBind();

            ddlCentroDeCustoDestino.Items.Insert(0, new ListItem("Selecione", "0"));
        }

        protected void ProcurarFuncionario(object sender, EventArgs e) 
        {
            try
            {
                if (!ValidarCamposProcurar())
                    return;
                

                Departamentos departamentos = new Departamentos();
                CentrosDeCusto centros = new CentrosDeCusto();

                var departamentoAtual = departamentos.Obter(int.Parse(DdlDepartamento.SelectedValue));
                if (departamentoAtual == null)
                {
                    
                    Notificacao = "Departamento atual não econtrado, por favor selecione novamente.";
                    EnviarMensagem();
                    return;
                }
                var centroDeCustoAtual = centros.ObterPor(ddlCentrosDeCusto.SelectedValue);
                if (centroDeCustoAtual == null)
                {
                    
                    Notificacao = "Centro de custo atual não econtrado, por favor selecione novamente.";
                    EnviarMensagem();
                    return;
                }

                var funcionario = centroDeCustoAtual.Funcionarios.Where(f => f.Departamento.Id == departamentoAtual.Id &&
                   f.Matricula == txtMatricula.Text).FirstOrDefault();

                if (funcionario == null)
                {
                    
                    Notificacao = "Funcionário não existe para este departamento/Centro de custo com esta matricula.";
                    EnviarMensagem();
                    return;
                }

                labelNomeFuncionario.Text = funcionario.Nome;
                labelNomeFuncionario.Visible = true;
                lblNome.Visible = true;
            }
            catch (Exception ex)
            {
                Notificacao = ex.ToString();  
            }
            EnviarMensagem();
           
        }

        private bool ValidarCamposProcurar()
        {
            if (DdlDepartamento.SelectedValue == "0")
            {
                Notificacao = "É obrigatório selecionar o departamento atual para alterar um funcionário";
                EnviarMensagem();
                return false;
            }

            if (ddlCentrosDeCusto.SelectedValue == "0")
            {
                Notificacao = "É obrigatório selecionar o centro de custo atual para alterar um funcionário";
                EnviarMensagem();
                return false;
            }

            if (string.IsNullOrEmpty(txtMatricula.Text))
            {
                Notificacao = "por favor selecione a matricula.";
                EnviarMensagem();
                return false;
            }

            return true;

        }

        protected void AlterarFuncionario(object sender, EventArgs e)
        {
            try
            {
                if (!ValidarCampos())
                    return;

                Departamentos departamentos = new Departamentos();
                CentrosDeCusto centros = new CentrosDeCusto();

                var departamentoAtual = departamentos.Obter(int.Parse(DdlDepartamento.SelectedValue));
                if (departamentoAtual == null)
                {
                    Notificacao = "Departamento atual não econtrado, por favor selecione novamente.";
                    EnviarMensagem();
                    return;
                }
                var centroDeCustoAtual = centros.ObterPor(ddlCentrosDeCusto.SelectedValue);
                if (centroDeCustoAtual == null)
                {
                    Notificacao = "Centro de custo atual não econtrado, por favor selecione novamente.";
                    EnviarMensagem();
                    return;
                }
                var departamentoDestino = departamentos.Obter(int.Parse(ddlDepartamentoDestino.SelectedValue));
                if (departamentoDestino == null)
                {
                    Notificacao = "Departamento destino não econtrado, por favor selecione novamente.";
                    EnviarMensagem();
                    return;
                }
                var centroDeCustoDestino = centros.ObterPor(ddlCentroDeCustoDestino.SelectedValue);
                if (centroDeCustoDestino == null)
                {
                    Notificacao = "Centro de custo destino não econtrado, por favor selecione novamente.";
                    EnviarMensagem();
                    return;
                }

                var funcionario = centroDeCustoAtual.Funcionarios.Where(f => f.Departamento.Id == departamentoAtual.Id &&
                    f.Matricula == txtMatricula.Text).FirstOrDefault();

                if (funcionario == null)
                {
                    Notificacao = "Funcionário não existe para este departamento/Centro de custo com esta matricula.";
                    EnviarMensagem();
                    return;
                }

                centroDeCustoAtual.Funcionarios.Remove(funcionario);
                centroDeCustoDestino.Adicionar(funcionario);

                centroDeCustoDestino.Adicionar(funcionario);

                funcionario.Departamento = departamentoDestino;
                centros.Salvar(centroDeCustoAtual);             
                centros.Salvar(centroDeCustoDestino);

                Notificacao = "Funcionário foi atualizado.";
            }
            catch (Exception ex)
            {
                Notificacao = ex.Message;
            }

            EnviarMensagem();
        }

        public bool ValidarCampos() 
        {
            if (DdlDepartamento.SelectedValue == "0")
            {
                Notificacao = "É obrigatório selecionar o departamento atual para alterar um funcionário";
                EnviarMensagem();
                return false;
            }

            if (ddlCentrosDeCusto.SelectedValue == "0")
            {
                Notificacao = "É obrigatório selecionar o centro de custo atual para alterar um funcionário";
                EnviarMensagem();
                return false;
            }

            if (ddlDepartamentoDestino.SelectedValue == "0")
            {
                Notificacao = "É obrigatório selecionar o departamento destino para alterar um funcionário";
                EnviarMensagem();
                return false;
            }

            if (ddlCentroDeCustoDestino.SelectedValue == "0")
            {
                Notificacao = "É obrigatório selecionar o centro de custo destino para alterar um funcionário";
                EnviarMensagem();
                return false;
            }

            if (string.IsNullOrEmpty(txtMatricula.Text))
            {
                Notificacao = "por favor selecione a matricula.";
                EnviarMensagem();
                return false;
            }

            return true;
        }

    }
}