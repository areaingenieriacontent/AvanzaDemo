using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Plenamente.Models;

namespace Plenamente.Controllers
{
    public class AutoEvaluacionesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: AutoEvaluaciones
        public ActionResult Index()
        {
            var tb_AutoEvaluacion = db.Tb_AutoEvaluacion.Include(a => a.Empresa);
            return View(tb_AutoEvaluacion.ToList());
        }

        // GET: AutoEvaluaciones/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AutoEvaluacion autoEvaluacion = db.Tb_AutoEvaluacion.Find(id);
            if (autoEvaluacion == null)
            {
                return HttpNotFound();
            }
            return View(autoEvaluacion);
        }

        // GET: AutoEvaluaciones/Create
        public ActionResult Create()
        {
            ViewBag.Empr_Nit = new SelectList(db.Tb_Empresa, "Empr_Nit", "Empr_Nom");
            return View();
        }

        // POST: AutoEvaluaciones/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Auev_Id,Auev_Nom,Auev_Inicio,Auev_Fin,Empr_Nit")] AutoEvaluacion autoEvaluacion)
        {
            if (ModelState.IsValid)
            {
                db.Tb_AutoEvaluacion.Add(autoEvaluacion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Empr_Nit = new SelectList(db.Tb_Empresa, "Empr_Nit", "Empr_Nom", autoEvaluacion.Empr_Nit);
            return View(autoEvaluacion);
        }

        // GET: AutoEvaluaciones/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AutoEvaluacion autoEvaluacion = db.Tb_AutoEvaluacion.Find(id);
            if (autoEvaluacion == null)
            {
                return HttpNotFound();
            }
            ViewBag.Empr_Nit = new SelectList(db.Tb_Empresa, "Empr_Nit", "Empr_Nom", autoEvaluacion.Empr_Nit);
            return View(autoEvaluacion);
        }

        // POST: AutoEvaluaciones/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Auev_Id,Auev_Nom,Auev_Inicio,Auev_Fin,Empr_Nit")] AutoEvaluacion autoEvaluacion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(autoEvaluacion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Empr_Nit = new SelectList(db.Tb_Empresa, "Empr_Nit", "Empr_Nom", autoEvaluacion.Empr_Nit);
            return View(autoEvaluacion);
        }

        // GET: AutoEvaluaciones/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AutoEvaluacion autoEvaluacion = db.Tb_AutoEvaluacion.Find(id);
            if (autoEvaluacion == null)
            {
                return HttpNotFound();
            }
            return View(autoEvaluacion);
        }

        // POST: AutoEvaluaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AutoEvaluacion autoEvaluacion = db.Tb_AutoEvaluacion.Find(id);
            db.Tb_AutoEvaluacion.Remove(autoEvaluacion);
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
