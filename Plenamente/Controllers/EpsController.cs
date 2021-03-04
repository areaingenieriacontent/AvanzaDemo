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
    public class EpsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Eps
        [Authorize(Roles = "Administrator")]
        public ActionResult Index()
        {
            return View(db.Tb_Eps.ToList());
        }

        // GET: Eps/Details/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Eps eps = db.Tb_Eps.Find(id);
            if (eps == null)
            {
                return HttpNotFound();
            }
            return View(eps);
        }

        // GET: Eps/Create
        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Eps/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Create([Bind(Include = "Eps_Id,Eps_Nom,Eps_Registro")] Eps eps)
        {
            if (ModelState.IsValid)
            {
                db.Tb_Eps.Add(eps);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(eps);
        }

        // GET: Eps/Edit/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Eps eps = db.Tb_Eps.Find(id);
            if (eps == null)
            {
                return HttpNotFound();
            }
            return View(eps);
        }

        // POST: Eps/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit([Bind(Include = "Eps_Id,Eps_Nom,Eps_Registro")] Eps eps)
        {
            if (ModelState.IsValid)
            {
                db.Entry(eps).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(eps);
        }

        // GET: Eps/Delete/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Eps eps = db.Tb_Eps.Find(id);
            if (eps == null)
            {
                return HttpNotFound();
            }
            return View(eps);
        }

        // POST: Eps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult DeleteConfirmed(int id)
        {
            Eps eps = db.Tb_Eps.Find(id);
            db.Tb_Eps.Remove(eps);
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
