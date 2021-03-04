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
    public class PeriodoesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Administrador/Periodoes
        [Authorize(Roles = "Administrator")]
        public ActionResult Index()
        {
            return View(db.Tb_Periodo.ToList());
        }

        // GET: Administrador/Periodoes/Details/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Periodo periodo = db.Tb_Periodo.Find(id);
            if (periodo == null)
            {
                return HttpNotFound();
            }
            return View(periodo);
        }

        // GET: Administrador/Periodoes/Create
        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Administrador/Periodoes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Create([Bind(Include = "Peri_Id,Peri_Nom,Peri_Registro")] Periodo periodo)
        {
            if (ModelState.IsValid)
            {
                db.Tb_Periodo.Add(periodo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(periodo);
        }

        // GET: Administrador/Periodoes/Edit/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Periodo periodo = db.Tb_Periodo.Find(id);
            if (periodo == null)
            {
                return HttpNotFound();
            }
            return View(periodo);
        }

        // POST: Administrador/Periodoes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit([Bind(Include = "Peri_Id,Peri_Nom,Peri_Registro")] Periodo periodo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(periodo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(periodo);
        }

        // GET: Administrador/Periodoes/Delete/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Periodo periodo = db.Tb_Periodo.Find(id);
            if (periodo == null)
            {
                return HttpNotFound();
            }
            return View(periodo);
        }

        // POST: Administrador/Periodoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult DeleteConfirmed(int id)
        {
            Periodo periodo = db.Tb_Periodo.Find(id);
            db.Tb_Periodo.Remove(periodo);
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
