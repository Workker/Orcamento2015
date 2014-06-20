using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Orcamento.Robo.Web.Controllers
{
    public class LoginController : Controller
    {

        //private IRepositorio<Usuario> _usuarios;

        public LoginController()
        {

        }

        //public LoginController(IRepositorio<Usuario> usuarios)
        //{
        //    _usuarios = usuarios;
        //}

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public RedirectResult Logar()
        {
            //Session["usuarioLogadoId"] = _usuarios.Todos().First().Id;

            return Redirect("/Home");
           // return Redirect("/Mapas");
        }
    }
}
