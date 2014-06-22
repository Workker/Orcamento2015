using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;


namespace Orcamento.Robo.Web.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Models.Login.LoginModel user)
        {
            if (ModelState.IsValid)
            {
                if (user.IsValid(user.Login, user.Senha))
                {
                    FormsAuthentication.SetAuthCookie(user.Nome, true);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    this.ShowMessage(MessageTypeEnum.danger, "Usuário e Senha invalido");
                }
            }
            return View(user);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Login");
        }
    }
}
