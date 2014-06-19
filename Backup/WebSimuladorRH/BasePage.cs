using System;
using System.Web.UI;
using Orcamento.Domain.DB.Repositorio;
using Orcamento.Domain;
using Orcamento.Domain.Gerenciamento;

namespace WebSimuladorRH
{
    public class BasePage : Page
    {
        public string Notificacao { get; set; }

        public void EnviarMensagem()
        {
            if (!string.IsNullOrEmpty(Notificacao))
                ClientScript.RegisterClientScriptBlock(GetType(), Guid.NewGuid().ToString(),
                                                   string.Format("<script type=\"text/javascript\">alert('{0}');</script>", Notificacao));

                
        }

        public Usuario ObterUsuarioLogado()
        {
            var usuarios = new Usuarios();
            Usuario usuario = usuarios.ObterUsuarioPorId(int.Parse(Session["UsuarioLogadoId"].ToString()));
            return usuario;
        }

        public void VerificaSeExisteSessaoDeUsuario()
        {
            try
            {
                Session["UsuarioLogadoId"].ToString();
            }
            catch (Exception)
            {
                Response.Redirect("/Login.aspx");
            }
        }

        public void VerificarSeUsuarioCoorporativo() 
        {
            var departamentos = new Departamentos();

            var departamento = departamentos.Obter((int)Session["DepartamentoLogadoId"]);

            if (departamento.GetType() != typeof(Setor))
                Response.Redirect("/PainelOrcamento.aspx");
        }

        public void VerificarSeUsuarioHospitalar()
        {
            var departamentos = new Departamentos();

            var departamento = departamentos.Obter((int)Session["DepartamentoLogadoId"]);

            if (departamento.GetType() != typeof(Hospital))
                Response.Redirect("/PainelOrcamento.aspx");
        }
    }
}