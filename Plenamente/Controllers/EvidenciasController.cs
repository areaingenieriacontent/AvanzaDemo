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
    public class EvidenciasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Evidencias
        public ActionResult Index()
        {
            var evidencias = db.Tb_Evidencia.Include(e => e.ApplicationUser).Include(e => e.Cumplimiento).Include(e => e.TipoDocCarga);
            return View(evidencias.ToList());
        }

        // GET: Evidencias/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Evidencia evidencia = db.Tb_Evidencia.Find(id);
            if (evidencia == null)
            {
                return HttpNotFound();
            }
            return View(evidencia);
        }

        // GET: Evidencias/Create
        public ActionResult Create()
        {
            ViewBag.Id = new SelectList(db.Users, "Id", "Pers_Nom1");
            ViewBag.Cump_Id = new SelectList(db.Tb_Cumplimiento, "Cump_Id", "Cump_Observ");
            ViewBag.Tdca_id = new SelectList(db.Tb_TipoDocCarga, "Tdca_id", "Tdca_Nom");
            return View();
        }

        // POST: Evidencias/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Evid_Id,Evid_Nombre,Evid_Archivo,Evid_Registro,Cump_Id,Tdca_id,Id")] Evidencia evidencia)
        {
            if (ModelState.IsValid)
            {
                db.Tb_Evidencia.Add(evidencia);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id = new SelectList(db.Users, "Id", "Pers_Nom1", evidencia.Id);
            ViewBag.Cump_Id = new SelectList(db.Tb_Cumplimiento, "Cump_Id", "Cump_Observ", evidencia.Cump_Id);
            ViewBag.Tdca_id = new SelectList(db.Tb_TipoDocCarga, "Tdca_id", "Tdca_Nom", evidencia.Tdca_id);
            return View(evidencia);
        }

        // GET: Evidencias/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Evidencia evidencia = db.Tb_Evidencia.Find(id);
            if (evidencia == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id = new SelectList(db.Users, "Id", "Pers_Nom1", evidencia.Id);
            ViewBag.Cump_Id = new SelectList(db.Tb_Cumplimiento, "Cump_Id", "Cump_Observ", evidencia.Cump_Id);
            ViewBag.Tdca_id = new SelectList(db.Tb_TipoDocCarga, "Tdca_id", "Tdca_Nom", evidencia.Tdca_id);
            return View(evidencia);
        }

        // POST: Evidencias/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Evid_Id,Evid_Nombre,Evid_Archivo,Evid_Registro,Cump_Id,Tdca_id,Id")] Evidencia evidencia)
        {
            if (ModelState.IsValid)
            {
                db.Entry(evidencia).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id = new SelectList(db.Users, "Id", "Pers_Nom1", evidencia.Id);
            ViewBag.Cump_Id = new SelectList(db.Tb_Cumplimiento, "Cump_Id", "Cump_Observ", evidencia.Cump_Id);
            ViewBag.Tdca_id = new SelectList(db.Tb_TipoDocCarga, "Tdca_id", "Tdca_Nom", evidencia.Tdca_id);
            return View(evidencia);
        }

        // GET: Evidencias/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Evidencia evidencia = db.Tb_Evidencia.Find(id);
            if (evidencia == null)
            {
                return HttpNotFound();
            }
            return View(evidencia);
        }

        // POST: Evidencias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Evidencia evidencia = db.Tb_Evidencia.Find(id);
            db.Tb_Evidencia.Remove(evidencia);
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
