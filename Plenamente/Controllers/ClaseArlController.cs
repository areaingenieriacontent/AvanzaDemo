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
    public class ClaseArlController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ClaseArl
        public ActionResult Index()
        {
            return View(db.Tb_ClaseArl.ToList());
        }

        // GET: ClaseArl/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClaseArl claseArl = db.Tb_ClaseArl.Find(id);
            if (claseArl == null)
            {
                return HttpNotFound();
            }
            return View(claseArl);
        }

        // GET: ClaseArl/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ClaseArl/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Carl_Id,Carl_Nom,Carl_Registro")] ClaseArl claseArl)
        {
            if (ModelState.IsValid)
            {
                db.Tb_ClaseArl.Add(claseArl);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(claseArl);
        }

        // GET: ClaseArl/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClaseArl claseArl = db.Tb_ClaseArl.Find(id);
            if (claseArl == null)
            {
                return HttpNotFound();
            }
            return View(claseArl);
        }

        // POST: ClaseArl/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Carl_Id,Carl_Nom,Carl_Registro")] ClaseArl claseArl)
        {
            if (ModelState.IsValid)
            {
                db.Entry(claseArl).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(claseArl);
        }

        // GET: ClaseArl/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClaseArl claseArl = db.Tb_ClaseArl.Find(id);
            if (claseArl == null)
            {
                return HttpNotFound();
            }
            return View(claseArl);
        }

        // POST: ClaseArl/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ClaseArl claseArl = db.Tb_ClaseArl.Find(id);
            db.Tb_ClaseArl.Remove(claseArl);
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
