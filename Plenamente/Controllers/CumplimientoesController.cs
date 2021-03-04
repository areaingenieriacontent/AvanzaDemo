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
    public class CumplimientoesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Cumplimientoes
        public ActionResult Index()
        {
            var tb_Cumplimiento = db.Tb_Cumplimiento.Include(c => c.AutoEvaluacion).Include(c => c.Empresa).Include(c => c.ItemEstandar);
            return View(tb_Cumplimiento.ToList());
        }

        // GET: Cumplimientoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cumplimiento cumplimiento = db.Tb_Cumplimiento.Find(id);
            if (cumplimiento == null)
            {
                return HttpNotFound();
            }
            return View(cumplimiento);
        }

        // GET: Cumplimientoes/Create
        public ActionResult Create()
        {
            ViewBag.Auev_Id = new SelectList(db.Tb_AutoEvaluacion, "Auev_Id", "Auev_Nom");
            ViewBag.Empr_Nit = new SelectList(db.Tb_Empresa, "Empr_Nit", "Empr_Nom");
            ViewBag.Iest_Id = new SelectList(db.Tb_ItemEstandar, "Iest_Id", "Iest_Desc");
            return View();
        }

        // POST: Cumplimientoes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Cump_Id,Cump_Cumple,Cump_Nocumple,Cump_Justifica,Cump_Nojustifica,Cump_Observ,Iest_Id,Empr_Nit,Auev_Id,Cump_Registro")] Cumplimiento cumplimiento)
        {
            if (ModelState.IsValid)
            {
                db.Tb_Cumplimiento.Add(cumplimiento);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Auev_Id = new SelectList(db.Tb_AutoEvaluacion, "Auev_Id", "Auev_Nom", cumplimiento.Auev_Id);
            ViewBag.Empr_Nit = new SelectList(db.Tb_Empresa, "Empr_Nit", "Empr_Nom", cumplimiento.Empr_Nit);
            ViewBag.Iest_Id = new SelectList(db.Tb_ItemEstandar, "Iest_Id", "Iest_Desc", cumplimiento.Iest_Id);
            return View(cumplimiento);
        }

        // GET: Cumplimientoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cumplimiento cumplimiento = db.Tb_Cumplimiento.Find(id);
            if (cumplimiento == null)
            {
                return HttpNotFound();
            }
            ViewBag.Auev_Id = new SelectList(db.Tb_AutoEvaluacion, "Auev_Id", "Auev_Nom", cumplimiento.Auev_Id);
            ViewBag.Empr_Nit = new SelectList(db.Tb_Empresa, "Empr_Nit", "Empr_Nom", cumplimiento.Empr_Nit);
            ViewBag.Iest_Id = new SelectList(db.Tb_ItemEstandar, "Iest_Id", "Iest_Desc", cumplimiento.Iest_Id);
            return View(cumplimiento);
        }

        // POST: Cumplimientoes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Cump_Id,Cump_Cumple,Cump_Nocumple,Cump_Justifica,Cump_Nojustifica,Cump_Observ,Iest_Id,Empr_Nit,Auev_Id,Cump_Registro")] Cumplimiento cumplimiento)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cumplimiento).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Auev_Id = new SelectList(db.Tb_AutoEvaluacion, "Auev_Id", "Auev_Nom", cumplimiento.Auev_Id);
            ViewBag.Empr_Nit = new SelectList(db.Tb_Empresa, "Empr_Nit", "Empr_Nom", cumplimiento.Empr_Nit);
            ViewBag.Iest_Id = new SelectList(db.Tb_ItemEstandar, "Iest_Id", "Iest_Desc", cumplimiento.Iest_Id);
            return View(cumplimiento);
        }

        // GET: Cumplimientoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cumplimiento cumplimiento = db.Tb_Cumplimiento.Find(id);
            if (cumplimiento == null)
            {
                return HttpNotFound();
            }
            return View(cumplimiento);
        }

        // POST: Cumplimientoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cumplimiento cumplimiento = db.Tb_Cumplimiento.Find(id);
            db.Tb_Cumplimiento.Remove(cumplimiento);
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
