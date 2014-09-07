using Orcamento.Robo.Web.Models.Configuracoes;
using Orcamento.Robo.Web.Models.ConfiguracoesPoDepartamento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Orcamento.Robo.Web.Controllers
{
    public abstract class ControllerBase : System.Web.Mvc.Controller
    {
        public virtual void CriarConfiguracao(int tipoProcesso, ConfiguracaoModel configuracao,string tipoMsg, string mensagem, string titulo)
        {
            configuracao.Tipo = tipoMsg;
            configuracao.Mensagem = mensagem;
            configuracao.Titulo = titulo;

            Session["Titulo"] = configuracao.Mensagem;
            Session["Mensagem"] = configuracao.Titulo;
            Session["Tipo"] = configuracao.Tipo;
        }

        public virtual void CriarConfiguracao(int tipoProcesso, ConfiguracaoPorDepartamentoModel configuracao, string tipoMsg, string mensagem, string titulo)
        {
            configuracao.Tipo = tipoMsg;
            configuracao.Mensagem = mensagem;
            configuracao.Titulo = titulo;

            Session["Titulo"] = configuracao.Mensagem;
            Session["Mensagem"] = configuracao.Titulo;
            Session["Tipo"] = configuracao.Tipo;
        }
    }
}