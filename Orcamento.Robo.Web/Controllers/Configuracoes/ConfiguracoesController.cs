using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Orcamento.Robo.Web.Controllers.Configuracoes
{
    public class ConfiguracoesController : System.Web.Mvc.Controller
    {
        private  Orcamento.Controller.Robo.ConfiguracoesController controller = new Controller.Robo.ConfiguracoesController();

        //
        // GET: /Configuracoes/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Deletar()
        {
            try
            {
                controller.Deletar();
                this.ShowMessage(MessageTypeEnum.success, "Estrutura deletada com sucesso!",true);
                return null;
            }
            catch (Exception)
            {
                
                throw;
            }
            
        }

    }
}
