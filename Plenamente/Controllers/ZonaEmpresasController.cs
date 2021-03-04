using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using Plenamente.Models;

namespace Plenamente.Areas.Administrador.Controllers
{
    public class ZonaEmpresasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Administrador/ZonaEmpresas
        [Authorize(Roles = "Administrator")]
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var zona = from s in db.Tb_ZonaEmpresa
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                zona = zona.Where(s => s.Zemp_Nom.ToString().Contains(searchString)
                                       || s.Zemp_Nom.ToString().Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    zona = zona.OrderByDescending(s => s.Zemp_Nom.ToString());
                    break;
                default:  // Name ascending 
                    zona = zona.OrderBy(s => s.Zemp_Nom.ToString());
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(zona.ToPagedList(pageNumber, pageSize));
        }

        // GET: Administrador/ZonaEmpresas/Details/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ZonaEmpresa zonaEmpresa = db.Tb_ZonaEmpresa.Find(id);
            if (zonaEmpresa == null)
            {
                return HttpNotFound();
            }
            return View(zonaEmpresa);
        }

        // GET: Administrador/ZonaEmpresas/Create
        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            ViewBag.Empr_Nit = new SelectList(db.Tb_Empresa, "Empr_Nit", "Empr_Nom");
            return View();
        }

        // POST: Administrador/ZonaEmpresas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Create([Bind(Include = "Zemp_Id,Zemp_Nom,Zemp_Registro,Empr_Nit")] ZonaEmpresa zonaEmpresa)
        {
            if (ModelState.IsValid)
            {
                db.Tb_ZonaEmpresa.Add(zonaEmpresa);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Empr_Nit = new SelectList(db.Tb_Empresa, "Empr_Nit", "Empr_Nom", zonaEmpresa.Empr_Nit);
            return View(zonaEmpresa);
        }

        // GET: Administrador/ZonaEmpresas/Edit/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ZonaEmpresa zonaEmpresa = db.Tb_ZonaEmpresa.Find(id);
            if (zonaEmpresa == null)
            {
                return HttpNotFound();
            }
            ViewBag.Empr_Nit = new SelectList(db.Tb_Empresa, "Empr_Nit", "Empr_Nom", zonaEmpresa.Empr_Nit);
            return View(zonaEmpresa);
        }

        // POST: Administrador/ZonaEmpresas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit([Bind(Include = "Zemp_Id,Zemp_Nom,Zemp_Registro,Empr_Nit")] ZonaEmpresa zonaEmpresa)
        {
            if (ModelState.IsValid)
            {
                db.Entry(zonaEmpresa).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Empr_Nit = new SelectList(db.Tb_Empresa, "Empr_Nit", "Empr_Nom", zonaEmpresa.Empr_Nit);
            return View(zonaEmpresa);
        }

        // GET: Administrador/ZonaEmpresas/Delete/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ZonaEmpresa zonaEmpresa = db.Tb_ZonaEmpresa.Find(id);
            if (zonaEmpresa == null)
            {
                return HttpNotFound();
            }
            return View(zonaEmpresa);
        }

        // POST: Administrador/ZonaEmpresas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult DeleteConfirmed(int id)
        {
            ZonaEmpresa zonaEmpresa = db.Tb_ZonaEmpresa.Find(id);
            db.Tb_ZonaEmpresa.Remove(zonaEmpresa);
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
