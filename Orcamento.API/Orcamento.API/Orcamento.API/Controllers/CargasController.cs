using Orcamento.Domain.DB.Repositorio.Robo;
using Orcamento.Domain.Entities.Monitoramento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;

namespace Orcamento.API.Controller
{
    public class CargasController : ApiController
    {

        public IEnumerable<Carga> Get()
        {
            return new Carga[]
            {
                new Carga(),
                new Carga(),
                new Carga()
            };
        }


        public Carga Get(Guid idCarga)
        {
            Cargas cargas = new Cargas();

            var carga = cargas.ObterPor(idCarga);

            carga.IniciarCarga();
            cargas.Salvar(carga);

            carga.Processa();
            cargas.Salvar(carga);

            if (carga.Ok())
            {
                carga.FinalizarCarga();
                cargas.Salvar(carga);
            }
            else
            {
                carga.InformarErroDeProcesso();
                cargas.Salvar(carga);
            }

            return carga;
        }


        public void Post([FromBody]Carga carga)
        {
        }


        public void Put(int id, [FromBody]Carga carga)
        {
        }


        public void Delete(int id)
        {
        }
    }
}