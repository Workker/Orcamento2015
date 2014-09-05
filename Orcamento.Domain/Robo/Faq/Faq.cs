using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Orcamento.Domain.Robo.Faq
{
    public class Faq : IAggregateRoot<int>
    {
        public virtual int Id { get; set; }
        public virtual TipoFaq TipoFaq { get; set; }
        public virtual IList<Pergunta> Perguntas { get; set; }
        public virtual string Nome { get; set; }
    }
}
