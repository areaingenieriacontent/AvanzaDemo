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
    public class TipoDocCargasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Administrador/TipoDocCargas
        [Authorize(Roles = "Administrator")]
        public ActionResult Index()
        {
            return View(db.Tb_TipoDocCarga.ToList());
        }

        // GET: Administrador/TipoDocCargas/Details/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoDocCarga tipoDocCarga = db.Tb_TipoDocCarga.Find(id);
            if (tipoDocCarga == null)
            {
                return HttpNotFound();
            }
            return View(tipoDocCarga);
        }

        // GET: Administrador/TipoDocCargas/Create
        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Administrador/TipoDocCargas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Create([Bind(Include = "Tdca_id,Tdca_Nom,Tdca_Registro")] TipoDocCarga tipoDocCarga)
        {
            if (ModelState.IsValid)
            {
                db.Tb_TipoDocCarga.Add(tipoDocCarga);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tipoDocCarga);
        }

        // GET: Administrador/TipoDocCargas/Edit/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoDocCarga tipoDocCarga = db.Tb_TipoDocCarga.Find(id);
            if (tipoDocCarga == null)
            {
                return HttpNotFound();
            }
            return View(tipoDocCarga);
        }

        // POST: Administrador/TipoDocCargas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit([Bind(Include = "Tdca_id,Tdca_Nom,Tdca_Registro")] TipoDocCarga tipoDocCarga)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipoDocCarga).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tipoDocCarga);
        }

        // GET: Administrador/TipoDocCargas/Delete/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoDocCarga tipoDocCarga = db.Tb_TipoDocCarga.Find(id);
            if (tipoDocCarga == null)
            {
                return HttpNotFound();
            }
            return View(tipoDocCarga);
        }

        // POST: Administrador/TipoDocCargas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult DeleteConfirmed(int id)
        {
            TipoDocCarga tipoDocCarga = db.Tb_TipoDocCarga.Find(id);
            db.Tb_TipoDocCarga.Remove(tipoDocCarga);
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
