using Orcamento.Robo.Web.Models.Status;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Orcamento.Robo.Web.Controllers.Status
{
    public class StatusController : Controller
    {
        public ActionResult Index()
        {
           

            var status = new StatusModel
            {
                Id = new Guid(),
                NomeArquivo = "FuncionarioCorporativo",
                DataProcesso = DateTime.Now,
                Status = "erro na Validação",
            };

            var status1 = new StatusModel
            {
                Id = new Guid(),
                NomeArquivo = "FuncionarioCorporativo",
                DataProcesso = DateTime.Now,
                Status = "Validando",
            };


            var status2 = new StatusModel
            {
                Id = new Guid(),
                NomeArquivo = "FuncionarioCorporativo",
                DataProcesso = DateTime.Now,
                Status = "Concluido",
            };

            var status3 = new StatusModel
            {
                Id = new Guid(),
                NomeArquivo = "FuncionarioCorporativo",
                DataProcesso = DateTime.Now,
                Status = "Processando",
            };

            
            var statusList = new StatusListModel
                                 {StatusList = new List<StatusModel> {status, status1, status2, status3}};


            return View(statusList);
        }

    }
}
