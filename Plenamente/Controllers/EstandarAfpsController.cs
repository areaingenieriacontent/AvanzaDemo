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
    public class EstandarAfpsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: EstandarAfps
        public ActionResult Index()
        {
            var tb_EstandarAfp = db.Tb_EstandarAfp.Include(e => e.CriterioAfp);
            return View(tb_EstandarAfp.ToList());
        }

        // GET: EstandarAfps/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EstandarAfp estandarAfp = db.Tb_EstandarAfp.Find(id);
            if (estandarAfp == null)
            {
                return HttpNotFound();
            }
            return View(estandarAfp);
        }

        // GET: EstandarAfps/Create
        public ActionResult Create()
        {
            ViewBag.Crit_Id = new SelectList(db.Tb_CriterioAfp, "Crit_Id", "Crit_Nom");
            return View();
        }

        // POST: EstandarAfps/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Esta_Id,Esta_Nom,Esta_Porcentaje,Crit_Id,Esta_Registro,Categoria,CategoriaExcepcion")] EstandarAfp estandarAfp)
        {
            if (ModelState.IsValid)
            {
                db.Tb_EstandarAfp.Add(estandarAfp);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Crit_Id = new SelectList(db.Tb_CriterioAfp, "Crit_Id", "Crit_Nom", estandarAfp.Crit_Id);
            return View(estandarAfp);
        }

        // GET: EstandarAfps/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EstandarAfp estandarAfp = db.Tb_EstandarAfp.Find(id);
            if (estandarAfp == null)
            {
                return HttpNotFound();
            }
            ViewBag.Crit_Id = new SelectList(db.Tb_CriterioAfp, "Crit_Id", "Crit_Nom", estandarAfp.Crit_Id);
            return View(estandarAfp);
        }

        // POST: EstandarAfps/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Esta_Id,Esta_Nom,Esta_Porcentaje,Crit_Id,Esta_Registro,Categoria,CategoriaExcepcion")] EstandarAfp estandarAfp)
        {
            if (ModelState.IsValid)
            {
                db.Entry(estandarAfp).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Crit_Id = new SelectList(db.Tb_CriterioAfp, "Crit_Id", "Crit_Nom", estandarAfp.Crit_Id);
            return View(estandarAfp);
        }

        // GET: EstandarAfps/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EstandarAfp estandarAfp = db.Tb_EstandarAfp.Find(id);
            if (estandarAfp == null)
            {
                return HttpNotFound();
            }
            return View(estandarAfp);
        }

        // POST: EstandarAfps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EstandarAfp estandarAfp = db.Tb_EstandarAfp.Find(id);
            db.Tb_EstandarAfp.Remove(estandarAfp);
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
