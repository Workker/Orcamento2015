using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Orcamento.Robo.Web.Controllers.Fila
{
    public class FilaController : Controller
    {
        //
        // GET: /Fila/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Fila/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Fila/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Fila/Create

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
        // GET: /Fila/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Fila/Edit/5

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
        // GET: /Fila/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Fila/Delete/5

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
