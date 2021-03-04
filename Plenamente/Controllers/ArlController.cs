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
    public class ArlController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Arl
        [Authorize(Roles = "Administrator")]
        public ActionResult Index()
        {
            return View(db.Tb_Arl.ToList());
        }

        // GET: Arl/Details/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Arl arl = db.Tb_Arl.Find(id);
            if (arl == null)
            {
                return HttpNotFound();
            }
            return View(arl);
        }

        // GET: Arl/Create
        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Arl/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Create([Bind(Include = "Arl_Id,Arl_Nom,Arl_Registro")] Arl arl)
        {
            if (ModelState.IsValid)
            {
                db.Tb_Arl.Add(arl);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(arl);
        }

        // GET: Arl/Edit/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Arl arl = db.Tb_Arl.Find(id);
            if (arl == null)
            {
                return HttpNotFound();
            }
            return View(arl);
        }

        // POST: Arl/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit([Bind(Include = "Arl_Id,Arl_Nom,Arl_Registro")] Arl arl)
        {
            if (ModelState.IsValid)
            {
                db.Entry(arl).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(arl);
        }

        // GET: Arl/Delete/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Arl arl = db.Tb_Arl.Find(id);
            if (arl == null)
            {
                return HttpNotFound();
            }
            return View(arl);
        }

        // POST: Arl/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult DeleteConfirmed(int id)
        {
            Arl arl = db.Tb_Arl.Find(id);
            db.Tb_Arl.Remove(arl);
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
