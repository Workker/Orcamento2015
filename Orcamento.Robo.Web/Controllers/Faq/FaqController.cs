using Orcamento.Controller.Robo;
using Orcamento.Robo.Web.Models.Faq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Orcamento.Robo.Web.Controllers.Faq
{
    public class FaqController : ControllerBase
    {
        private FaqsController faqs = new FaqsController();
        //
        // GET: /Faq/

        public ActionResult Index()
        {

            var todos = faqs.Todos();

            var model = ObterFaqs(todos);

            return View(model);
        }

        private Faqs ObterFaqs(List<Domain.Robo.Faq.Faq> faqsDominio)
        {
            Faqs model = new Faqs();
            model.faqs = new List<Models.Faq.Faq>();

            if (faqsDominio != null)
            {
                foreach (var faq in faqsDominio)
                {
                    var faqCriado = new Models.Faq.Faq() { Nome = faq.Nome, Id = faq.Id };
                    faqCriado.Topicos = new List<Topico>();
                    foreach (var pergunta in faq.Perguntas)
                    {
                        faqCriado.Topicos.Add(new Topico() { Nome = pergunta.Nome, Resposta = pergunta.Resposta });
                    }

                    model.faqs.Add(faqCriado);
                }
            }

            return model;
        }

    }
}
