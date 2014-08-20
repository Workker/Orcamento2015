using Orcamento.Domain.Robo.Monitoramento.EstrategiasDeCargas;
using Orcamento.Robo.Web.Models.Importar;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Orcamento.Robo.Web.Controllers.Importar
{
    public class ImportarController : System.Web.Mvc.Controller
    {
        //
        // GET: /Importar/

        public ActionResult Index()
        {
            ImportarModel model = this.CriarModel();
            return View(model);
        }

        public ImportarModel CriarModel()
        {
            var model = new ImportarModel();
            model.TiposImportacao.Add
                (
                new TipoImportacaoModel()
                {
                    Id = TipoEstrategiaDeCargaEnum.Funcionarios,
                    Selecionado = true,
                    Tipo = "Funcionarios"
                }
                );
            model.TiposImportacao.Add
                (
             new TipoImportacaoModel()
             {
                 Id = TipoEstrategiaDeCargaEnum.TicketsDeProducao,
                 Selecionado = false,
                 Tipo = "Ticket de Producao"
             }
                );

            return model;

        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file,TipoEstrategiaDeCargaEnum IdTipo)
        {
            ImportarModel model = CriarModel();
            try
            {
                DetalhesDaImportacaoModel detalhes = new DetalhesDaImportacaoModel();
                if (file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/App_Data/uploads"), fileName);
                    file.SaveAs(path);

                    var controller = new Controller.Robo.ImportarController();
                    var carga = controller.ImportarCarga(IdTipo, path,fileName);

                    TransformarDetalhes(carga, detalhes);
                }
                //TODO: Nao consigo liberar um excel(arquivo do tipo file) depois de utiliza-lo
                //Ao tentar usar novamente, um erro de processo acontece pois ele diz que o arquivo esta sendo usado por outro processo.
                model.DetalheImportacao = detalhes;
              
                return View(model);
            }
            catch (Exception ex)
            {
                this.ShowMessage(MessageTypeEnum.danger, ex.Message);
                return View(model);
            }
        }

        //
        // GET: /Importar/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Importar/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Importar/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Importar/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Importar/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Importar/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Importar/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        //[HttpPost]
        //[OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        //public ActionResult Importar(TipoEstrategiaDeCargaEnum tipo)
        //{
        //    try
        //    {
        //        var controller = new Controller.Robo.ImportarController();
        //        var carga = controller.ImportarCarga(tipo,"");

        //        DetalhesDaImportacaoModel detalhes = new DetalhesDaImportacaoModel();

        //        TransformarDetalhes(carga, detalhes);
        //        return PartialView("_detalhesDaImportacao", detalhes);
        //    }
        //    catch (Exception ex)
        //    {
        //        this.ShowMessage(MessageTypeEnum.danger, ex.Message);
        //        return PartialView("_detalhesDaImportacao", null);
        //    }
        //}

        //[HttpPost]
        //[OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        //public ActionResult Importar(HttpPostedFileBase file, TipoEstrategiaDeCargaEnum IdTipo)
        //{



        //    try
        //    {
        //        DetalhesDaImportacaoModel detalhes = new DetalhesDaImportacaoModel();
        //        if (file.ContentLength > 0)
        //        {
        //            var fileName = Path.GetFileName(file.FileName);
        //            var path = Path.Combine(Server.MapPath("~/App_Data/uploads"), fileName);
        //            file.SaveAs(path);

        //            var controller = new Controller.Robo.ImportarController();
        //            var carga = controller.ImportarCarga(IdTipo, path);



        //            TransformarDetalhes(carga, detalhes);
        //        }
        //        return PartialView("_detalhesDaImportacao", detalhes);
        //    }
        //    catch (Exception ex)
        //    {
        //        this.ShowMessage(MessageTypeEnum.danger, ex.Message);
        //        return PartialView("_detalhesDaImportacao", null);
        //    }
        //}

        private void TransformarDetalhes(Domain.Entities.Monitoramento.Carga carga, DetalhesDaImportacaoModel detalhes)
        {
            foreach (var detalhe in carga.Detalhes)
            {
                detalhes.Detalhes.Add(new DetalheImportacaoModel() { Nome = detalhe.Nome, Descricao = detalhe.Descricao, Linha = detalhe.Linha, Tipo = detalhe.TipoDetalhe });
            }
        }
    }
}
