using System;
using System.Web.UI;
using Orcamento.Domain.DB.Repositorio;
using Orcamento.Domain.Gerenciamento;
using System.Linq;

namespace WebSimuladorRH
{
    public partial class Principal : MasterPage
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            btnLogout.Visible = Session["UsuarioLogadoId"] != null;
            ddlDepartamentos.Visible = Session["NomeDepartamentoLogado"] != null;

            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["alert"]))
                    Request.QueryString["alert"].Remove(0);

                CarregarListadeDepartamentos();
            }

            if (Session["NomeDepartamentoLogado"] != null)
            {
                ltlDepartamento.Text = Session["NomeDepartamentoLogado"].ToString();
            }

            if (Session["UsuarioLogadoId"] != null)
            {
                divNomeDoUsuarioLogado.Visible = true;
                var usuarios = new Usuarios();
                Usuario usuario = usuarios.ObterUsuarioPorId(int.Parse(Session["UsuarioLogadoId"].ToString()));
                ltlNomeDoUsuario.Text = usuario.Nome;
            }
        }

        protected void Deslogando(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("/Login.aspx");
        }

        protected void ddlDepartamentos_SelectedIndexChanged(object sender, EventArgs e)
        {
            ltlDepartamento.Text = ddlDepartamentos.SelectedItem.Text;

            Session["DepartamentoLogadoId"] = int.Parse(ddlDepartamentos.SelectedValue);

            AlterarUnidadeSelecionadaNaDropList();

            Session["NomeDepartamentoLogado"] = ddlDepartamentos.SelectedItem.Text;
            Response.Redirect(Request.RawUrl);
        }

        protected void IndoParaOPainelOrcamento(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("/PainelOrcamento.aspx");
        }

        #endregion

        #region Metodos

        private void AlterarUnidadeSelecionadaNaDropList()
        {
            try
            {
                ddlDepartamentos.ClearSelection();

                int id = int.Parse(Session["DepartamentoLogadoId"].ToString());

                ddlDepartamentos.Items.FindByValue(id.ToString()).Selected = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CarregarListadeDepartamentos()
        {
            try
            {
                Usuario usuario = ObterUsuarioLogado();

                ddlDepartamentos.Items.Clear();

                CarregarDepartamentos(usuario);

                AlterarUnidadeSelecionadaNaDropList();
            }
            catch (Exception)
            {
            }
        }

        private Usuario ObterUsuarioLogado()
        {
            var usuarios = new Usuarios();
            Usuario usuario = usuarios.ObterUsuarioPorId(int.Parse(Session["UsuarioLogadoId"].ToString()));
            return usuario;
        }

        private void CarregarDepartamentos(Usuario usuario)
        {
            ddlDepartamentos.DataSource = usuario.Departamentos.OrderBy(d=> d.Tipo).ThenBy(d=> d.Nome);
            ddlDepartamentos.DataTextField = "Nome";
            ddlDepartamentos.DataValueField = "Id";
            ddlDepartamentos.DataBind();
        }
        
        #endregion
    }
}