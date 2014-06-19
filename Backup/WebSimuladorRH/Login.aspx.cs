using System;
using System.Web.UI.WebControls;
using Orcamento.Domain.DB.Repositorio;
using Orcamento.Domain.Gerenciamento;

namespace WebSimuladorRH
{
    public partial class Login : BasePage
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Session["DepartamentoLogadoId"] != null && Session["UsuarioLogadoId"] != null)
                {
                    Response.Redirect("/PainelOrcamento.aspx");
                }
                Session["NomeDepartamentoLogado"] = null;
                VerificaExistenciaDeUsuarioLogado();
            }
            if (Session["UsuarioLogadoId"] != null)
            {
                HabilitarSelecaoDeUnidade();
                PreencherDropdownComOsDepartamentosDoUsuarioLogado();
            }
        }

        protected void Logando(object sender, EventArgs e)
        {
            if (txtlogin.Text.Equals("") || txtSenha.Text.Equals(""))
            {
                EnviarMensagem();
            }
            else
            {
                var usuarios = new Usuarios();
                Usuario usuario = usuarios.ObterAcesso(txtlogin.Text, txtSenha.Text);

                if (usuario != null)
                {
                    Session["UsuarioLogadoId"] = usuario.Id;
                    Session["TipoUsuarioId"] = usuario.TipoUsuario.Id;

                    if (usuario.Senha == "123456")
                        Response.Redirect("/AlterarSenha.aspx");
                    else
                        Response.Redirect("/Login.aspx");
                }
                else
                {
                    Notificacao = "Usuário e/ou senha incorreta. Tente novamente.";
                    EnviarMensagem();
                }
            }
        }

        protected void ddlDepartamentos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlDepartamentos != null && Convert.ToInt16(value: ddlDepartamentos.SelectedValue) != 0)
            {
                Session["DepartamentoLogadoId"] = int.Parse(ddlDepartamentos.SelectedValue);
                Session["NomeDepartamentoLogado"] = ddlDepartamentos.SelectedItem.Text;

                Response.Redirect("/PainelOrcamento.aspx?alert=on");
            }
        }

        #endregion

        #region Metodos

        private void PreencherDropdownComOsDepartamentosDoUsuarioLogado()
        {
            Usuario usuarioLogado = ObterUsuarioLogado();

            DirecionaOUsuarioLogadoParaOPainelCasoPossuaApenasUmDepartamentoVinculado(usuarioLogado);

            foreach (Departamento d in usuarioLogado.Departamentos)
            {
                var item = new ListItem { Text = d.Nome, Value = d.Id.ToString() };
                ddlDepartamentos.Items.Add(item);
            }
            ddlDepartamentos.Items.Insert(0, new ListItem("", ""));
        }

        private void VerificaExistenciaDeUsuarioLogado()
        {
            try
            {
                Session["DepartamentoLogadoId"].ToString();
                Response.Redirect("/PainelOrcamento.aspx");
            }
            catch (Exception)
            {
                Session["TentativaDeVoltarAPaginaDeLogin"] = false;
            }
        }

        private void HabilitarSelecaoDeUnidade()
        {
            ltlLoginFormTitulo.Text = "Selecione a Unidade / Diretoria";
            divLogin.Attributes.Remove("class");
            divLogin.Attributes["class"] = "selecao-de-unidades";
            divComboBoxDeSelecaoDeUnidade.Visible = true;

            divTextFieldUsuario.Visible = false;
            divTextFieldSenha.Visible = false;
            divBotaoLogar.Visible = false;
        }



        private void DirecionaOUsuarioLogadoParaOPainelCasoPossuaApenasUmDepartamentoVinculado(Usuario usuarioLogado)
        {
            if (usuarioLogado.Departamentos.Count == 1)
            {
                Departamento departamento = usuarioLogado.Departamentos[0];
                Session["DepartamentoLogadoId"] = int.Parse(departamento.Id.ToString());
                Session["NomeDepartamentoLogado"] = departamento.Nome;
                Response.Redirect("/PainelOrcamento.aspx?alert=on");
            }
        }

        #endregion
    }
}