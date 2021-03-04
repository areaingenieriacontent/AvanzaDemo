using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Plenamente.Models;

namespace Plenamente.Areas.Administrador.Controllers
{
    public class FrecuenciasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Administrador/Frecuencias
        [Authorize(Roles = "Administrator")]
        public ActionResult Index()
        {
            return View(db.Tb_Frecuencia.ToList());
        }

        // GET: Administrador/Frecuencias/Details/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Frecuencia frecuencia = db.Tb_Frecuencia.Find(id);
            if (frecuencia == null)
            {
                return HttpNotFound();
            }
            return View(frecuencia);
        }

        // GET: Administrador/Frecuencias/Create
        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Administrador/Frecuencias/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Create([Bind(Include = "Frec_Id,Frec_Nom,Frec_Registro")] Frecuencia frecuencia)
        {
            if (ModelState.IsValid)
            {
                db.Tb_Frecuencia.Add(frecuencia);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(frecuencia);
        }

        // GET: Administrador/Frecuencias/Edit/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Frecuencia frecuencia = db.Tb_Frecuencia.Find(id);
            if (frecuencia == null)
            {
                return HttpNotFound();
            }
            return View(frecuencia);
        }

        // POST: Administrador/Frecuencias/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit([Bind(Include = "Frec_Id,Frec_Nom,Frec_Registro")] Frecuencia frecuencia)
        {
            if (ModelState.IsValid)
            {
                db.Entry(frecuencia).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(frecuencia);
        }

        // GET: Administrador/Frecuencias/Delete/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Frecuencia frecuencia = db.Tb_Frecuencia.Find(id);
            if (frecuencia == null)
            {
                return HttpNotFound();
            }
            return View(frecuencia);
        }

        // POST: Administrador/Frecuencias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult DeleteConfirmed(int id)
        {
            Frecuencia frecuencia = db.Tb_Frecuencia.Find(id);
            db.Tb_Frecuencia.Remove(frecuencia);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
