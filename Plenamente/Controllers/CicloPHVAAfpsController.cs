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
    public class CicloPHVAAfpsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: CicloPHVAAfps
        public ActionResult Index()
        {
            return View(db.Tb_cicloPHVAAfps.ToList());
        }

        // GET: CicloPHVAAfps/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CicloPHVAAfp cicloPHVAAfp = db.Tb_cicloPHVAAfps.Find(id);
            if (cicloPHVAAfp == null)
            {
                return HttpNotFound();
            }
            return View(cicloPHVAAfp);
        }

        // GET: CicloPHVAAfps/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CicloPHVAAfps/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nombre,Description,Categoria")] CicloPHVAAfp cicloPHVAAfp)
        {
            if (ModelState.IsValid)
            {
                db.Tb_cicloPHVAAfps.Add(cicloPHVAAfp);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cicloPHVAAfp);
        }

        // GET: CicloPHVAAfps/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CicloPHVAAfp cicloPHVAAfp = db.Tb_cicloPHVAAfps.Find(id);
            if (cicloPHVAAfp == null)
            {
                return HttpNotFound();
            }
            return View(cicloPHVAAfp);
        }

        // POST: CicloPHVAAfps/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nombre,Description,Categoria")] CicloPHVAAfp cicloPHVAAfp)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cicloPHVAAfp).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cicloPHVAAfp);
        }

        // GET: CicloPHVAAfps/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CicloPHVAAfp cicloPHVAAfp = db.Tb_cicloPHVAAfps.Find(id);
            if (cicloPHVAAfp == null)
            {
                return HttpNotFound();
            }
            return View(cicloPHVAAfp);
        }

        // POST: CicloPHVAAfps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CicloPHVAAfp cicloPHVAAfp = db.Tb_cicloPHVAAfps.Find(id);
            db.Tb_cicloPHVAAfps.Remove(cicloPHVAAfp);
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
