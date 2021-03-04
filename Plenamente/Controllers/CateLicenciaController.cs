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
    public class CateLicenciaController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: CateLicencia
        [Authorize(Roles = "Administrator")]
        public ActionResult Index()
        {
            return View(db.Tb_CateLicencia.ToList());
        }

        // GET: CateLicencia/Details/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CateLicencia cateLicencia = db.Tb_CateLicencia.Find(id);
            if (cateLicencia == null)
            {
                return HttpNotFound();
            }
            return View(cateLicencia);
        }

        // GET: CateLicencia/Create
        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: CateLicencia/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Create([Bind(Include = "Cate_Id,Cate_Nom,Cate_Registro")] CateLicencia cateLicencia)
        {
            if (ModelState.IsValid)
            {
                db.Tb_CateLicencia.Add(cateLicencia);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cateLicencia);
        }

        // GET: CateLicencia/Edit/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CateLicencia cateLicencia = db.Tb_CateLicencia.Find(id);
            if (cateLicencia == null)
            {
                return HttpNotFound();
            }
            return View(cateLicencia);
        }

        // POST: CateLicencia/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit([Bind(Include = "Cate_Id,Cate_Nom,Cate_Registro")] CateLicencia cateLicencia)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cateLicencia).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cateLicencia);
        }

        // GET: CateLicencia/Delete/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CateLicencia cateLicencia = db.Tb_CateLicencia.Find(id);
            if (cateLicencia == null)
            {
                return HttpNotFound();
            }
            return View(cateLicencia);
        }

        // POST: CateLicencia/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult DeleteConfirmed(int id)
        {
            CateLicencia cateLicencia = db.Tb_CateLicencia.Find(id);
            db.Tb_CateLicencia.Remove(cateLicencia);
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
