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
    public class CriteriosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Administrador/Criterios
        [Authorize(Roles = "Administrator")]
        public ActionResult Index()
        {
            return View(db.Tb_Criterio.ToList());
        }

        // GET: Administrador/Criterios/Details/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Criterio criterio = db.Tb_Criterio.Find(id);
            if (criterio == null)
            {
                return HttpNotFound();
            }
            return View(criterio);
        }

        // GET: Administrador/Criterios/Create
        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Administrador/Criterios/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Create([Bind(Include = "Crit_Id,Crit_Nom,Crit_Porcentaje,Crit_Registro")] Criterio criterio)
        {
            if (ModelState.IsValid)
            {
                db.Tb_Criterio.Add(criterio);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(criterio);
        }

        // GET: Administrador/Criterios/Edit/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Criterio criterio = db.Tb_Criterio.Find(id);
            if (criterio == null)
            {
                return HttpNotFound();
            }
            return View(criterio);
        }

        // POST: Administrador/Criterios/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit([Bind(Include = "Crit_Id,Crit_Nom,Crit_Porcentaje,Crit_Registro")] Criterio criterio)
        {
            if (ModelState.IsValid)
            {
                db.Entry(criterio).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(criterio);
        }

        // GET: Administrador/Criterios/Delete/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Criterio criterio = db.Tb_Criterio.Find(id);
            if (criterio == null)
            {
                return HttpNotFound();
            }
            return View(criterio);
        }

        // POST: Administrador/Criterios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult DeleteConfirmed(int id)
        {
            Criterio criterio = db.Tb_Criterio.Find(id);
            db.Tb_Criterio.Remove(criterio);
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
