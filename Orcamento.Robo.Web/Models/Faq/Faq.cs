using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Orcamento.Robo.Web.Models.Faq
{
    [Serializable]
    public class Faqs
    {
        public List<Faq> faqs { get; set; }
    }

    [Serializable]
    public class Faq
    {
        public string Nome { get; set; }
        public int Id { get; set; }
        public List<Topico> Topicos { get; set; } 
    }

    [Serializable]
    public class Topico
    {
        public string Nome { get; set; }
        public string Resposta { get; set; }
    }
}