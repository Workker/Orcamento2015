using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Orcamento.Robo.Web.Controllers.Exportar
{
    public class ExportarController : Controller
    {
        //
        // GET: /Exportar/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Exportar/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Exportar/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Exportar/Create

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
        // GET: /Exportar/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Exportar/Edit/5

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
        // GET: /Exportar/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Exportar/Delete/5

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
