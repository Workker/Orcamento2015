using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Orcamento.API.Controllers
{
    public class HomeController :  System.Web.Mvc.Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        protected override void ExecuteCore()
        {
            throw new NotImplementedException();
        }
    }
}
