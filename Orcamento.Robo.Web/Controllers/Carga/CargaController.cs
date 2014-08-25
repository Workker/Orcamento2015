using DotNetOpenAuth.OpenId.Extensions.AttributeExchange;
using Orcamento.Robo.Web.Models.Carga;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Orcamento.Robo.Web.Controllers.Carga
{
    public class CargaController : System.Web.Mvc.Controller
    {
        readonly Controller.Robo.CargaController _controller = new Controller.Robo.CargaController();

        public ActionResult Index()
        {
            var controllerCargas = _controller.Todos();

            var cargasModel = new CargasModel();

            foreach (var carga in controllerCargas)
            {
                cargasModel.Cargas.Add(new CargaModel() { DataCriacao = carga.DataCriacao, DataFim = carga.DataFim, DataInicio = carga.DataInicio, Diretorio = carga.Diretorio, Id = carga.Id, NomeArquivo = carga.NomeArquivo, Status = carga.Status, Usuario = carga.Usuario, Detalhes = null,TipoDaCarga = carga.Tipo.ToString() });
            }

            cargasModel.CarregarPaginas();

            return View(cargasModel);
        }

        public ActionResult Details(Guid id)
        {
            var controllerCarga = _controller.ObterPor(id);

            var detalhes = controllerCarga.Detalhes.Select(controleDetalhe => new DetalheModel()
                                                                                  {
                                                                                      Id = controleDetalhe.Id, Linha = controleDetalhe.Linha, Nome = controleDetalhe.Nome, Descricao = controleDetalhe.Descricao
                                                                                  }).ToList();


            var cargaModel = new CargaModel { DataCriacao = controllerCarga.DataCriacao, DataFim = controllerCarga.DataFim, DataInicio = controllerCarga.DataInicio, Diretorio = controllerCarga.Diretorio, Id = controllerCarga.Id, NomeArquivo = controllerCarga.NomeArquivo, Status = controllerCarga.Status, Usuario = controllerCarga.Usuario, Detalhes = detalhes };

            return PartialView("_DetalheCarga", cargaModel);
        }

    }
}
