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
    public class AfpController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Afp
        [Authorize(Roles = "Administrator")]
        public ActionResult Index()
        {
            return View(db.Tb_Afp.ToList());
        }

        // GET: Afp/Details/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Afp afp = db.Tb_Afp.Find(id);
            if (afp == null)
            {
                return HttpNotFound();
            }
            return View(afp);
        }

        // GET: Afp/Create
        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Afp/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Create([Bind(Include = "Afp_Id,Afp_Nom,Afp_Registro")] Afp afp)
        {
            if (ModelState.IsValid)
            {
                db.Tb_Afp.Add(afp);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(afp);
        }

        // GET: Afp/Edit/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Afp afp = db.Tb_Afp.Find(id);
            if (afp == null)
            {
                return HttpNotFound();
            }
            return View(afp);
        }

        // POST: Afp/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit([Bind(Include = "Afp_Id,Afp_Nom,Afp_Registro")] Afp afp)
        {
            if (ModelState.IsValid)
            {
                db.Entry(afp).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(afp);
        }

        // GET: Afp/Delete/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Afp afp = db.Tb_Afp.Find(id);
            if (afp == null)
            {
                return HttpNotFound();
            }
            return View(afp);
        }

        // POST: Afp/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult DeleteConfirmed(int id)
        {
            Afp afp = db.Tb_Afp.Find(id);
            db.Tb_Afp.Remove(afp);
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
