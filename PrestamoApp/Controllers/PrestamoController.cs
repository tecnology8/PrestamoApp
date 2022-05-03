using PrestamoApp.Models;
using PrestamoApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PrestamoApp.Controllers
{
    public class PrestamoController : Controller
    {
        // GET: Prestamo

        PrestamoServices prestamos = new PrestamoServices();

        public ActionResult Index()
        {
            var lista = new List<PrestamoModel>();
            lista = prestamos.GetPrestamo().ToList();

            return View(lista);
        }

        public ActionResult List()
        {
            var lista = new List<PrestamoModel>();
            lista = prestamos.GetPrestamo().ToList();

            return View(lista); 
        }

        #region CRUD

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Create([Bind] PrestamoModel prestamo)
        {
            if (ModelState.IsValid)
            {
                prestamos.AddPrestamo(prestamo);
                return RedirectToAction("Index");
            }
            return View(prestamo);
        }



        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            var prestamo = prestamos.GetById(id);

            if (prestamo == null)
            {
                return RedirectToAction("Index");
            }
            return View(prestamo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind] PrestamoModel prestamo)
        {
            if (id != prestamo.Id)
            {
                return RedirectToAction("Index");
            }
            if (ModelState.IsValid)
            {
                prestamos.UpdatePrestamo(prestamo);
                return RedirectToAction("Index");
            }
            return View(prestamo);
        }


        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            var prestamo = prestamos.GetById(id);

            if (prestamo == null)
            {
                return RedirectToAction("Index");
            }
            return View(prestamo);
        }


        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            var prestamo = prestamos.GetById(id);

            if (prestamo == null)
            {
                return RedirectToAction("Index");
            }
            return View(prestamo);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            prestamos.DeletePrestamo(id);
            return RedirectToAction("Index");
        }

        #endregion
    }
}