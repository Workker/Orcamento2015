using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace Orcamento.API.Controllers
{

    public class Totalizador
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Valor { get; set; }
    }


    public class TotalizadoresController : ApiController
    {
        //
        // GET: /Totalizadores/

        private static List<Totalizador> totalizadores;

        [System.Web.Http.HttpGet]
        public List<Totalizador> GET()
        {
            return totalizadores;
        }

        [System.Web.Http.HttpPost]
        public List<Totalizador> POST(string nome, string valor)
        {
            if (totalizadores == null)
                totalizadores = new List<Totalizador>();

            totalizadores.Add(new Totalizador() { Id = Guid.NewGuid(), Nome = nome, Valor = valor });

            return totalizadores;
        }
    }
}
