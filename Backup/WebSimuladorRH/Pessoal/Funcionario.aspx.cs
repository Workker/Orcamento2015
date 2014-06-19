using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Orcamento.Controller;
using Orcamento.Domain;
using Orcamento.Domain.Gerenciamento;
using System.Linq;
using System.Linq.Expressions;
using Orcamento.Domain.DB.Repositorio;

namespace WebSimuladorRH.Pessoal
{
    public partial class Funcionario : BasePage
    {
        #region Atributos

        private FuncionarioController _controller;

        #endregion

        #region  Propriedades

        public Departamento Departamento
        {
            get { return (Departamento)Session["DepartamentoFuncionario"]; }
            set { Session["DepartamentoFuncionario"] = value; }
        }

        public Orcamento.Domain.Gerenciamento.CentroDeCusto centroDeCusto
        {
            get { return (Orcamento.Domain.Gerenciamento.CentroDeCusto)Session["CentroCustoFuncionario"]; }
            set { Session["CentroCustoFuncionario"] = value; }
        }

        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            _controller = new FuncionarioController();

            if (IsPostBack) return;

            var departamentoId = int.Parse(Session["DepartamentoLogadoId"].ToString());

            Departamento = _controller.ObterDepartamentoPor(departamentoId);

            CarregarComboDeCentrosDeCusto(departamentoId);
        }

        private void CarregarComboDeCentrosDeCusto(int departamentoId)
        {
            ddlCentrosDeCusto.DataSource = _controller.ObterCentrosDeCustoPor(departamentoId).OrderBy(x => x.Nome);
            ddlCentrosDeCusto.DataTextField = "Nome";
            ddlCentrosDeCusto.DataValueField = "CodigoDoCentroDeCusto";
            ddlCentrosDeCusto.DataBind();

            ddlCentrosDeCusto.Items.Insert(0, new ListItem("Selecione", "0"));
        }

        protected void IncluindoNovoFuncionario(object sender, EventArgs e)
        {
            try
            {
                if (!NHibernateSessionPerRequest.GetCurrentSession().Contains(Departamento))
                    NHibernateSessionPerRequest.GetCurrentSession().Refresh(Departamento);

                if (ddlCentrosDeCusto.SelectedValue == "0")
                {
                    Notificacao = "É obrigatório selecionar um centro de custo para incluir um novo funcionário";
                    EnviarMensagem();
                    return;
                }
                if (this.Departamento.CentrosDeCusto.Where(c => c.CodigoDoCentroDeCusto == ddlCentrosDeCusto.SelectedValue).FirstOrDefault().Funcionarios != null &&
                    this.Departamento.CentrosDeCusto.Where(c => c.CodigoDoCentroDeCusto == ddlCentrosDeCusto.SelectedValue).FirstOrDefault().Funcionarios.Any(f => f.Matricula == txtMatricula.Text))
                {
                    Notificacao = "Não é possivel adicionar um funcionário com o mesmo numero de matricula.";
                    EnviarMensagem();
                    return;
                }
                if (string.IsNullOrEmpty(txCargo.Text))
                {
                    Notificacao = "Não é possivel adicionar um funcionário sem cargo.";
                    EnviarMensagem();
                    return;
                }

                double salario = 0;
                if (!string.IsNullOrEmpty(txtSalario.Text))
                    salario = double.Parse(txtSalario.Text.Replace(".", ""));
                else
                {
                    Notificacao = "Salário é obrigatório";
                    EnviarMensagem();
                    return;
                }

                int dataAdmissao = 0;
                if (!string.IsNullOrEmpty(ddlDataDeAdmissao.SelectedItem.Value))
                    dataAdmissao = int.Parse(ddlDataDeAdmissao.SelectedItem.Value);
                else
                {
                    Notificacao = "Data de admissão é obrigatória";
                    EnviarMensagem();
                    return;
                }

                var funcionario = new Orcamento.Domain.ComponentesDeOrcamento.OrcamentoPessoal.Funcionario(Departamento)
                {
                    Nome = txtNome.Text,
                    Matricula = txtMatricula.Text,
                    DataAdmissao = dataAdmissao,
                    Salario = salario,
                    Cargo = txCargo.Text,
                    AnoAdmissao = 2013
                };

                CentroDeCusto centroDeCusto = _controller.ObterCentroDeCustoPor(codigoDoCentroDeCusto: ddlCentrosDeCusto.SelectedValue);

                centroDeCusto.Adicionar(funcionario);

                _controller.SalvarCentroDeCusto(centroDeCusto);

            }
            catch (Exception ex)
            {
                Notificacao = ex.Message;
            }

            SelecionandoCentroDeCusto();

            if (string.IsNullOrEmpty(Notificacao))
                SalvarOrcamento();

            EnviarMensagem();
        }

        protected void SelecionandoCentroDeCusto(object sender, EventArgs e)
        {
            SelecionandoCentroDeCusto();
        }

        private void SelecionandoCentroDeCusto()
        {
            if (ddlCentrosDeCusto.SelectedValue == "0")
            {
                rptFuncionarios.DataSource = null;
                rptFuncionarios.DataBind();
                btnSalvar.Visible = false;
                return;
            }

            btnSalvar.Visible = true;
            divHeadCount.Visible = true;

            var departamentoId = int.Parse(Session["DepartamentoLogadoId"].ToString());

            centroDeCusto =
                 _controller.ObterCentroDeCustoPor(codigoDoCentroDeCusto: ddlCentrosDeCusto.SelectedValue);

            HeadCount.Text = centroDeCusto.Funcionarios.Where(d => d.Departamento.Id == Departamento.Id).Count().ToString();

            var funcionarios = _controller.ObterTodosFuncionariosPor(departamentoId,
                                                                               codigoDeCentroDeCusto:
                                                                                   ddlCentrosDeCusto.SelectedValue);
            rptFuncionarios.DataSource = funcionarios;
            rptFuncionarios.DataBind();

            var orcamentoPessoal = _controller.ObterOrcamento(centroDeCusto.Id,departamentoId);
            if (orcamentoPessoal != null)
            {
                txtJustificativa.Text = orcamentoPessoal.Justificativa;
            }
        }

        protected void Salvando(object sender, EventArgs e)
        {

            if (this.centroDeCusto.Funcionarios == null || this.centroDeCusto.Funcionarios != null && this.centroDeCusto.Funcionarios.Where(d => d.Departamento.Id == Departamento.Id).Count() == 0)
            {
                Notificacao = "Por favor adicione um funcionário.";
                EnviarMensagem();
                return;
            }
            try
            {
                SalvarOrcamento();
            }
            catch (Exception ex)
            {
                Notificacao = ex.Message;
            }

            if (string.IsNullOrEmpty(Notificacao))
                Notificacao = "Funcionários atualizados com sucesso!";

            EnviarMensagem();
        }

        private void SalvarOrcamento()
        {
            foreach (RepeaterItem item in rptFuncionarios.Items)
            {
                if (item.ItemType != ListItemType.Item && item.ItemType != ListItemType.AlternatingItem) continue;

                var funcionarioId = int.Parse(((HiddenField) item.FindControl("hdnId")).Value);

                var funcionario = _controller.ObterFuncionarioPor(funcionarioId);

                var chkDemitido = (CheckBox) item.FindControl("chkDemitido");
                var ddlMesDeDemissao = (DropDownList) item.FindControl("ddlMesDeDemissao");
                var chkAumentado = (CheckBox) item.FindControl("chkAumentado");
                var txtPercentualDeAumento = (TextBox) item.FindControl("txtPercentualDeAumento");
                var ddlMesesDeAumento = (DropDownList) item.FindControl("ddlMesesDeAumento");

                funcionario.Demitido = chkDemitido.Checked;
                funcionario.Aumentado = chkAumentado.Checked;

                if (funcionario.Demitido)
                {
                    if (ddlMesDeDemissao.SelectedValue != "0")
                        funcionario.MesDeDemissao = short.Parse(ddlMesDeDemissao.SelectedValue);
                }
                else
                    funcionario.MesDeDemissao = 0;

                if (funcionario.Aumentado)
                {
                    if (!string.IsNullOrEmpty(txtPercentualDeAumento.Text))
                        funcionario.Aumento = Convert.ToDouble(txtPercentualDeAumento.Text);

                    if (ddlMesesDeAumento.SelectedValue != "0")
                        funcionario.MesDeAumento = int.Parse(ddlMesesDeAumento.SelectedValue);
                }
                else
                {
                    funcionario.MesDeAumento = 0;
                    funcionario.Aumento = 0;
                }

                _controller.Departamento = Departamento;
                _controller.CodigoDoCentroDeCusto = ddlCentrosDeCusto.SelectedValue;

                _controller.Salvar(funcionario);
            }

            _controller.SalvarOrcamento(justificativa: txtJustificativa.Text);
        }


        protected void DeletarFuncionario(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "DeletarUsuario")
            {
                int usuarioID = int.Parse(e.CommandArgument.ToString());
                var funcionarios = new Orcamento.Domain.DB.Repositorio.Funcionarios();
                var funcionario = funcionarios.Obter<Orcamento.Domain.ComponentesDeOrcamento.OrcamentoPessoal.Funcionario>(usuarioID);

                centroDeCusto =
              _controller.ObterCentroDeCustoPor(codigoDoCentroDeCusto: ddlCentrosDeCusto.SelectedValue);
                centroDeCusto.Funcionarios.Remove(funcionario);
                funcionarios.Salvar(centroDeCusto);
                SelecionandoCentroDeCusto();
            }
        }

        #endregion
    }
}