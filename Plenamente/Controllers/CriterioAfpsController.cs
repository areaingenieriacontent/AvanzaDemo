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
    public class CriterioAfpsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: CriterioAfps
        public ActionResult Index()
        {
            var tb_CriterioAfp = db.Tb_CriterioAfp.Include(c => c.CicloPHVAAfp);
            return View(tb_CriterioAfp.ToList());
        }

        // GET: CriterioAfps/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CriterioAfp criterioAfp = db.Tb_CriterioAfp.Find(id);
            if (criterioAfp == null)
            {
                return HttpNotFound();
            }
            return View(criterioAfp);
        }

        // GET: CriterioAfps/Create
        public ActionResult Create()
        {
            ViewBag.CicloPHVA_Id = new SelectList(db.Tb_cicloPHVAAfps, "Id", "Nombre");
            return View();
        }

        // POST: CriterioAfps/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Crit_Id,Crit_Nom,Crit_Porcentaje,Crit_Registro,CicloPHVA_Id,Categoria,CategoriaExcepcion")] CriterioAfp criterioAfp)
        {
            if (ModelState.IsValid)
            {
                db.Tb_CriterioAfp.Add(criterioAfp);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CicloPHVA_Id = new SelectList(db.Tb_cicloPHVAAfps, "Id", "Nombre", criterioAfp.CicloPHVA_Id);
            return View(criterioAfp);
        }

        // GET: CriterioAfps/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CriterioAfp criterioAfp = db.Tb_CriterioAfp.Find(id);
            if (criterioAfp == null)
            {
                return HttpNotFound();
            }
            ViewBag.CicloPHVA_Id = new SelectList(db.Tb_cicloPHVAAfps, "Id", "Nombre", criterioAfp.CicloPHVA_Id);
            return View(criterioAfp);
        }

        // POST: CriterioAfps/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Crit_Id,Crit_Nom,Crit_Porcentaje,Crit_Registro,CicloPHVA_Id,Categoria,CategoriaExcepcion")] CriterioAfp criterioAfp)
        {
            if (ModelState.IsValid)
            {
                db.Entry(criterioAfp).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CicloPHVA_Id = new SelectList(db.Tb_cicloPHVAAfps, "Id", "Nombre", criterioAfp.CicloPHVA_Id);
            return View(criterioAfp);
        }

        // GET: CriterioAfps/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CriterioAfp criterioAfp = db.Tb_CriterioAfp.Find(id);
            if (criterioAfp == null)
            {
                return HttpNotFound();
            }
            return View(criterioAfp);
        }

        // POST: CriterioAfps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CriterioAfp criterioAfp = db.Tb_CriterioAfp.Find(id);
            db.Tb_CriterioAfp.Remove(criterioAfp);
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
