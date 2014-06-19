using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Orcamento.Controller;
using Orcamento.Domain.Gerenciamento;
using System.Linq;

namespace WebSimuladorRH
{
    public partial class CadastroUsuario : BasePage
    {
        private CadastroDeUsuarioController _controller;
        private string funcaoDaPagina;

        public string FuncaoDaPagina
        {
            get { return funcaoDaPagina; }
            set { funcaoDaPagina = value; }
        }

        public Usuario Usuario
        {
            get { return (Usuario)Session["UsuarioRecuperado"]; }
            set { Session["UsuarioRecuperado"] = value; }
        }

        public CadastroUsuario()
        {
            _controller = new CadastroDeUsuarioController();
        }

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            FuncaoDaPagina = Request.QueryString["funcao"];

            divUsuarioTipo.Visible = string.IsNullOrEmpty(funcaoDaPagina);
            divUsuarioAlteracao.Visible = !string.IsNullOrEmpty(funcaoDaPagina);

            if (string.IsNullOrEmpty(funcaoDaPagina))
                Usuario = null;

            if (!IsPostBack)
            {
                VerificaSeExisteSessaoDeUsuario();
                CarregarDepartamentos();
                CarregarTipoUsuario();
                CarregarUsuarios();
            }
        }

        protected void ddlUsuario_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(ddlUsuarios.SelectedValue))
                    throw new Exception("Selecione um Usuário");

                Usuario = _controller.ObterUsuario(ddlUsuarios.SelectedValue);
                txtNome.Text = Usuario.Nome;
                txtLogin.Text = Usuario.Login;

                PopularRepeaterDeDepartamentos();
            }
            catch (Exception ex)
            {
                Notificacao = ex.Message;
            }
            EnviarMensagem();
        }

        public void PopularRepeaterDeDepartamentos()
        {

            rptDepartamento.DataSource = _controller.CarregarTodosDepartamentos();
            rptDepartamento.DataBind();
        }

        protected void rptDepartamento_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            foreach (RepeaterItem item in rptDepartamento.Items)
            {
                if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                {
                    var departamentoID = (HiddenField)item.FindControl("HiddenFieldidDepartamento");

                    if (Usuario != null)
                    {
                        foreach (var departamento in Usuario.Departamentos)
                        {
                            if (departamento.Id == int.Parse(departamentoID.Value))
                            {
                                ((CheckBox)item.FindControl("CheckBoxidDepartamento")).Checked = true;
                            }
                        }
                    }
                }
            }
        }

        private void CarregarUsuarios()
        {
            if (funcaoDaPagina == "a")
            {
                ddlUsuarios.DataSource = _controller.TodosUsuarios();
                ddlUsuarios.DataValueField = "Id";
                ddlUsuarios.DataTextField = "Nome";
                ddlUsuarios.DataBind();

                ddlUsuarios.Items.Insert(0, new ListItem("Selecione", ""));
            }
        }

        protected void BtnSalvarClick(object sender, EventArgs e)
        {
            try
            {


                Usuario usuario;
                if (string.IsNullOrEmpty(funcaoDaPagina))
                {
                    if (string.IsNullOrEmpty(txtLogin.Text))
                        throw new Exception("Preencha o login.");

                    if (string.IsNullOrEmpty(txtNome.Text))
                        throw new Exception("Preencha o nome.");

                    if (string.IsNullOrEmpty(ddlTipo.SelectedValue))
                        throw new Exception("Preencha o tipo do usuário.");

                    if (_controller.TodosUsuarios().Any(x => x.Login == txtLogin.Text))
                        throw new Exception("O Login escolhido já existe");

                    usuario = new Usuario
                    {
                        Nome = txtNome.Text,
                        Login = txtLogin.Text,
                        Senha = "123456",
                        TipoUsuario = _controller.ObterTipoUsuario(int.Parse(ddlTipo.SelectedValue))
                    };
                }
                else
                {
                    if (string.IsNullOrEmpty(ddlUsuarios.SelectedValue))
                        throw new Exception("Selecione um Usuário");

                    usuario = _controller.ObterUsuario(ddlUsuarios.SelectedValue);
                    txtLogin.Text = usuario.Login;
                    txtNome.Text = usuario.Nome;
                }

                var existeDepartamento = 0;
                foreach (Control control in rptDepartamento.Controls)
                {
                    if (control.GetType() == typeof(RepeaterItem))
                    {
                        foreach (Control repeaterItem in (control).Controls)
                        {
                            if (repeaterItem.GetType() == typeof(CheckBox))
                            {
                                var checkBox = (CheckBox)repeaterItem;
                                if (checkBox.Checked)
                                {
                                    existeDepartamento++;
                                    var item = control as RepeaterItem;
                                    if (item != null)
                                    {
                                        var idDepartamento =
                                            (HiddenField)
                                            (item.FindControl(checkBox.ID.Replace("CheckBox", "HiddenField")));
                                        var departamento = _controller.ObterDepartamento(int.Parse(idDepartamento.Value));
                                        if (usuario.Departamentos == null || !usuario.Departamentos.Any(d => d.Id == departamento.Id))
                                            usuario.ParticiparDe(departamento);
                                    }
                                }
                                else
                                {
                                    var item = control as RepeaterItem;
                                    if (item != null)
                                    {
                                        var idDepartamento =
                                            (HiddenField)
                                            (item.FindControl(checkBox.ID.Replace("CheckBox", "HiddenField")));
                                        var departamento = _controller.ObterDepartamento(int.Parse(idDepartamento.Value));
                                        if (usuario.Departamentos != null && usuario.Departamentos.Any(d => d.Id == departamento.Id))
                                            usuario.Departamentos.Remove(departamento);
                                    }
                                }
                            }
                        }
                    }
                }



                _controller.Salvar(usuario);


                Notificacao = "Usuário salvo com Sucesso";

            }
            catch (Exception ex)
            {

                Notificacao = ex.Message;
            }

            EnviarMensagem();
            LimparCampos();

        }

        protected void DdlTipoSelectedIndexChanged(object sender, EventArgs e)
        {
            string administrador = "1";

            foreach (Control control in rptDepartamento.Controls)
            {
                if (control.GetType() == typeof(RepeaterItem))
                {
                    foreach (Control repeaterItem in (control).Controls)
                    {
                        if (repeaterItem.GetType() == typeof(CheckBox))
                        {
                            var checkBox = (CheckBox)repeaterItem;
                            checkBox.Checked = ddlTipo.SelectedValue.Equals(administrador);
                        }
                    }
                }
            }

        }

        protected void DdlTipoOnDataBound(object sender, EventArgs e)
        {
            ddlTipo.Items.Insert(0, new ListItem("", ""));
        }

        #endregion

        #region Metodos Privados

        private void CarregarDepartamentos()
        {
            rptDepartamento.DataSource = _controller.CarregarTodosDepartamentos();
            rptDepartamento.DataBind();
        }

        private void CarregarTipoUsuario()
        {
            ddlTipo.DataSource = _controller.CarregarTodosTipoUsuario();
            ddlTipo.DataTextField = "Nome";
            ddlTipo.DataValueField = "Id";
            ddlTipo.DataBind();
        }

        private void LimparCampos()
        {
            txtNome.Text = string.Empty;
            txtLogin.Text = string.Empty;
            ddlTipo.SelectedIndex = 0;
            if (!string.IsNullOrEmpty(funcaoDaPagina))
                ddlUsuarios.SelectedIndex = 0;

            foreach (Control control in rptDepartamento.Controls)
            {
                if (control.GetType() == typeof(RepeaterItem))
                {
                    foreach (Control repeaterItem in (control).Controls)
                    {
                        if (repeaterItem.GetType() == typeof(CheckBox))
                        {
                            var checkBox = (CheckBox)repeaterItem;
                            if (checkBox.Checked)
                            {
                                checkBox.Checked = false;
                            }
                        }
                    }
                }
            }
        }

        #endregion
    }
}
