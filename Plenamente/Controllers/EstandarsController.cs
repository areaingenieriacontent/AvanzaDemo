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
    public class EstandarsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Administrador/Estandars
        [Authorize(Roles = "Administrator")]
        public ActionResult Index()
        {
            var tb_Estandar = db.Tb_Estandar.Include(e => e.Criterio);
            return View(tb_Estandar.ToList());
        }

        // GET: Administrador/Estandars/Details/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estandar estandar = db.Tb_Estandar.Find(id);
            if (estandar == null)
            {
                return HttpNotFound();
            }
            return View(estandar);
        }

        // GET: Administrador/Estandars/Create
        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            ViewBag.Crit_Id = new SelectList(db.Tb_Criterio, "Crit_Id", "Crit_Nom");
            return View();
        }

        // POST: Administrador/Estandars/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Create([Bind(Include = "Esta_Id,Esta_Nom,Esta_Porcentaje,Crit_Id,Esta_Registro")] Estandar estandar)
        {
            if (ModelState.IsValid)
            {
                db.Tb_Estandar.Add(estandar);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Crit_Id = new SelectList(db.Tb_Criterio, "Crit_Id", "Crit_Nom", estandar.Crit_Id);
            return View(estandar);
        }

        // GET: Administrador/Estandars/Edit/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estandar estandar = db.Tb_Estandar.Find(id);
            if (estandar == null)
            {
                return HttpNotFound();
            }
            ViewBag.Crit_Id = new SelectList(db.Tb_Criterio, "Crit_Id", "Crit_Nom", estandar.Crit_Id);
            return View(estandar);
        }

        // POST: Administrador/Estandars/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit([Bind(Include = "Esta_Id,Esta_Nom,Esta_Porcentaje,Crit_Id,Esta_Registro")] Estandar estandar)
        {
            if (ModelState.IsValid)
            {
                db.Entry(estandar).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Crit_Id = new SelectList(db.Tb_Criterio, "Crit_Id", "Crit_Nom", estandar.Crit_Id);
            return View(estandar);
        }

        // GET: Administrador/Estandars/Delete/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estandar estandar = db.Tb_Estandar.Find(id);
            if (estandar == null)
            {
                return HttpNotFound();
            }
            return View(estandar);
        }

        // POST: Administrador/Estandars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult DeleteConfirmed(int id)
        {
            Estandar estandar = db.Tb_Estandar.Find(id);
            db.Tb_Estandar.Remove(estandar);
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
