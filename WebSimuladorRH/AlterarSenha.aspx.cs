using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Orcamento.Domain.DB.Repositorio;
using Orcamento.Domain.Gerenciamento;

namespace WebSimuladorRH
{
    public partial class AlterarSenha : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Usuario usuario = ObterUsuarioLogado();

                if (usuario == null || usuario != null && usuario.Senha != "123456")
                    Response.Redirect("/Login.aspx");
            }
        }

        protected void IrParaLogin(object sender, EventArgs e)
        {
            if (txtNovaSenha1.Text.Equals("") || txtNovaSenha2.Text.Equals(""))
            {
                Notificacao = "Por favor preencha os campos.";
                EnviarMensagem();
            }
            else if (txtNovaSenha1.Text != txtNovaSenha2.Text)
            {
                Notificacao = "Os campos de senha devem ser iguais.";
                EnviarMensagem();
            }
            else
            {
                Usuarios usuarios = new Usuarios();
                Usuario usuario = ObterUsuarioLogado();


                if (usuario != null)
                {
                    AlterarSenhaUsuario(usuarios, usuario);

                    Session["UsuarioLogadoId"] = usuario.Id;
                    Session["TipoUsuarioId"] = usuario.TipoUsuario.Id;
                    Response.Redirect("/Login.aspx");
                }
                else
                {
                    Notificacao = "Não encontramos o seu usuário por favor fale com o administrador ou realize o login novamente.";
                    EnviarMensagem();
                    Response.Redirect("/Login.aspx");
                }
            }
        }

        private void AlterarSenhaUsuario(Usuarios usuarios, Usuario usuario)
        {
            usuario.Senha = txtNovaSenha1.Text;
            usuarios.Salvar(usuario);
        }
    }
}