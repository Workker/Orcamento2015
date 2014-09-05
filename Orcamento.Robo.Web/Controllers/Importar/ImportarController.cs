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

            model.TiposImportacao.Add
                (
             new TipoImportacaoModel()
             {
                 Id = TipoEstrategiaDeCargaEnum.EstruturaOrcamentaria,
                 Selecionado = false,
                 Tipo = "Estrutura Orçamentária"
             }
                );

            model.TiposImportacao.Add
              (
           new TipoImportacaoModel()
           {
               Id = TipoEstrategiaDeCargaEnum.Usuarios,
               Selecionado = false,
               Tipo = "Usuários"
           }
              );

            return model;

        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file, TipoEstrategiaDeCargaEnum IdTipo, string atualiza)
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
                    var carga = controller.ImportarCarga(IdTipo, path, fileName, false);

                    TransformarDetalhes(carga, detalhes);

                    file.InputStream.Flush();
                    file.InputStream.Dispose();

                }

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

        private void TransformarDetalhes(Domain.Entities.Monitoramento.Carga carga, DetalhesDaImportacaoModel detalhes)
        {
            if (carga.Detalhes != null)
            {
                foreach (var detalhe in carga.Detalhes)
                {
                    detalhes.Detalhes.Add(new DetalheImportacaoModel() { Nome = detalhe.Nome, Descricao = detalhe.Descricao, Linha = detalhe.Linha, Tipo = detalhe.TipoDetalhe });
                }
            }
        }
    }
}
