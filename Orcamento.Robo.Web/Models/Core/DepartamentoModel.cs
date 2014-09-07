using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Orcamento.Robo.Web.Models.Core
{
    public class DepartamentoModel
    {
        public string Nome { get; set; }
        public int Id { get; set; }
        public bool Selecionado { get; set; }
    }
}