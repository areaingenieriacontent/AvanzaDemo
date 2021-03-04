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
    public class TVinController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TVin
        [Authorize(Roles = "Administrator")]
        public ActionResult Index()
        {
            return View(db.Tb_TipoVinculacion.ToList());
        }

        // GET: TVin/Details/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoVinculacion tipoVinculacion = db.Tb_TipoVinculacion.Find(id);
            if (tipoVinculacion == null)
            {
                return HttpNotFound();
            }
            return View(tipoVinculacion);
        }

        // GET: TVin/Create
        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: TVin/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Create([Bind(Include = "Tvin_Id,Tvin_Nom,Tvin_Registro")] TipoVinculacion tipoVinculacion)
        {
            if (ModelState.IsValid)
            {
                db.Tb_TipoVinculacion.Add(tipoVinculacion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tipoVinculacion);
        }

        // GET: TVin/Edit/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoVinculacion tipoVinculacion = db.Tb_TipoVinculacion.Find(id);
            if (tipoVinculacion == null)
            {
                return HttpNotFound();
            }
            return View(tipoVinculacion);
        }

        // POST: TVin/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit([Bind(Include = "Tvin_Id,Tvin_Nom,Tvin_Registro")] TipoVinculacion tipoVinculacion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipoVinculacion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tipoVinculacion);
        }

        // GET: TVin/Delete/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoVinculacion tipoVinculacion = db.Tb_TipoVinculacion.Find(id);
            if (tipoVinculacion == null)
            {
                return HttpNotFound();
            }
            return View(tipoVinculacion);
        }

        // POST: TVin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult DeleteConfirmed(int id)
        {
            TipoVinculacion tipoVinculacion = db.Tb_TipoVinculacion.Find(id);
            db.Tb_TipoVinculacion.Remove(tipoVinculacion);
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
