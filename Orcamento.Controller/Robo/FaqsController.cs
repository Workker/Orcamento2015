using Orcamento.Domain.DB.Repositorio.Robo;
using Orcamento.Domain.Robo.Faq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Orcamento.Controller.Robo
{
    public class FaqsController
    {
        public List<Faq> Todos()
        {
            Faqs faqs = new Faqs();
            return faqs.Todos<Faq>().ToList();
        }
    }
}
