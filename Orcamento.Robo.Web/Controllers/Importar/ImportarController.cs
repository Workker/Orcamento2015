using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Orcamento.Robo.Web.Controllers.Importar
{
    public class ImportarController : Controller
    {
        //
        // GET: /Importar/

        public ActionResult Index()
        {
            return View();
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
    }
}
