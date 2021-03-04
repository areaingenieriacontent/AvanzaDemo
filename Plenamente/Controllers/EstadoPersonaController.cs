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
    public class EstadoPersonaController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: EstadoPersona
        [Authorize(Roles = "Administrator")]
        public ActionResult Index()
        {
            return View(db.Tb_EstadoPersona.ToList());
        }

        // GET: EstadoPersona/Details/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EstadoPersona estadoPersona = db.Tb_EstadoPersona.Find(id);
            if (estadoPersona == null)
            {
                return HttpNotFound();
            }
            return View(estadoPersona);
        }

        // GET: EstadoPersona/Create
        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: EstadoPersona/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Create([Bind(Include = "Espe_Id,Espe_Nom,Espe_Registro")] EstadoPersona estadoPersona)
        {
            if (ModelState.IsValid)
            {
                db.Tb_EstadoPersona.Add(estadoPersona);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(estadoPersona);
        }

        // GET: EstadoPersona/Edit/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EstadoPersona estadoPersona = db.Tb_EstadoPersona.Find(id);
            if (estadoPersona == null)
            {
                return HttpNotFound();
            }
            return View(estadoPersona);
        }

        // POST: EstadoPersona/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit([Bind(Include = "Espe_Id,Espe_Nom,Espe_Registro")] EstadoPersona estadoPersona)
        {
            if (ModelState.IsValid)
            {
                db.Entry(estadoPersona).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(estadoPersona);
        }

        // GET: EstadoPersona/Delete/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EstadoPersona estadoPersona = db.Tb_EstadoPersona.Find(id);
            if (estadoPersona == null)
            {
                return HttpNotFound();
            }
            return View(estadoPersona);
        }

        // POST: EstadoPersona/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult DeleteConfirmed(int id)
        {
            EstadoPersona estadoPersona = db.Tb_EstadoPersona.Find(id);
            db.Tb_EstadoPersona.Remove(estadoPersona);
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
