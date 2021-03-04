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
    public class EmpresasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Empresas
        public ActionResult Index()
        {
            var tb_Empresa = db.Tb_Empresa.Include(e => e.Arl).Include(e => e.ClaseArl);
            return View(tb_Empresa.ToList());
        }

        // GET: Empresas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empresa empresa = db.Tb_Empresa.Find(id);
            if (empresa == null)
            {
                return HttpNotFound();
            }
            return View(empresa);
        }

        // GET: Empresas/Create
        public ActionResult Create()
        {
            ViewBag.Arl_Id = new SelectList(db.Tb_Arl, "Arl_Id", "Arl_Nom");
            ViewBag.Carl_Id = new SelectList(db.Tb_ClaseArl, "Carl_Id", "Carl_Nom");
            return View();
        }

        // POST: Empresas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Empr_Nit,Empr_Nom,Empr_Dir,Arl_Id,Carl_Id,Empr_Afiarl,Empr_Ttrabaja,Empr_Itrabaja,Empr_telefono,Empr_Registro,Empr_NewNit,Empr_RepresentanteLegal,Empr_CargoRepresentante,Empre_RepresentanteDoc,Empr_ResponsableSST,Empre_ResponsableDoc")] Empresa empresa)
        {
            if (ModelState.IsValid)
            {
                db.Tb_Empresa.Add(empresa);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Arl_Id = new SelectList(db.Tb_Arl, "Arl_Id", "Arl_Nom", empresa.Arl_Id);
            ViewBag.Carl_Id = new SelectList(db.Tb_ClaseArl, "Carl_Id", "Carl_Nom", empresa.Carl_Id);
            return View(empresa);
        }

        // GET: Empresas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empresa empresa = db.Tb_Empresa.Find(id);
            if (empresa == null)
            {
                return HttpNotFound();
            }
            ViewBag.Arl_Id = new SelectList(db.Tb_Arl, "Arl_Id", "Arl_Nom", empresa.Arl_Id);
            ViewBag.Carl_Id = new SelectList(db.Tb_ClaseArl, "Carl_Id", "Carl_Nom", empresa.Carl_Id);
            return View(empresa);
        }

        // POST: Empresas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Empr_Nit,Empr_Nom,Empr_Dir,Arl_Id,Carl_Id,Empr_Afiarl,Empr_Ttrabaja,Empr_Itrabaja,Empr_telefono,Empr_Registro,Empr_NewNit,Empr_RepresentanteLegal,Empr_CargoRepresentante,Empre_RepresentanteDoc,Empr_ResponsableSST,Empre_ResponsableDoc")] Empresa empresa)
        {
            if (ModelState.IsValid)
            {
                db.Entry(empresa).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Arl_Id = new SelectList(db.Tb_Arl, "Arl_Id", "Arl_Nom", empresa.Arl_Id);
            ViewBag.Carl_Id = new SelectList(db.Tb_ClaseArl, "Carl_Id", "Carl_Nom", empresa.Carl_Id);
            return View(empresa);
        }

        // GET: Empresas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empresa empresa = db.Tb_Empresa.Find(id);
            if (empresa == null)
            {
                return HttpNotFound();
            }
            return View(empresa);
        }

        // POST: Empresas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Empresa empresa = db.Tb_Empresa.Find(id);
            db.Tb_Empresa.Remove(empresa);
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
