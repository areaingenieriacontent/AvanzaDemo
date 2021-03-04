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
    public class SedeCiudadesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Administrador/SedeCiudades
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

            var sedes = from s in db.Tb_SedeCiudad
                         select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                sedes = sedes.Where(s => s.Sciu_Nom.Contains(searchString)
                                       || s.Sciu_Nom.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    sedes = sedes.OrderByDescending(s => s.Sciu_Nom);
                    break;
                default:  // Name ascending 
                    sedes = sedes.OrderBy(s => s.Sciu_Nom);
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(sedes.ToPagedList(pageNumber, pageSize));
        }

        // GET: Administrador/SedeCiudades/Details/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SedeCiudad sedeCiudad = db.Tb_SedeCiudad.Find(id);
            if (sedeCiudad == null)
            {
                return HttpNotFound();
            }
            return View(sedeCiudad);
        }

        // GET: Administrador/SedeCiudades/Create
        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            ViewBag.Ciud_Id = new SelectList(db.Tb_Ciudad, "Ciud_Id", "Ciud_Nom");
            ViewBag.Empr_Nit = new SelectList(db.Tb_Empresa, "Empr_Nit", "Empr_Nom");
            return View();
        }

        // POST: Administrador/SedeCiudades/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Create([Bind(Include = "Sciu_Id,Sciu_Nom,Ciud_Id,Empr_Nit,Sciu_Registro")] SedeCiudad sedeCiudad)
        {
            if (ModelState.IsValid)
            {
                db.Tb_SedeCiudad.Add(sedeCiudad);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Ciud_Id = new SelectList(db.Tb_Ciudad, "Ciud_Id", "Ciud_Nom", sedeCiudad.Ciud_Id);
            ViewBag.Empr_Nit = new SelectList(db.Tb_Empresa, "Empr_Nit", "Empr_Nom", sedeCiudad.Empr_Nit);
            return View(sedeCiudad);
        }

        // GET: Administrador/SedeCiudades/Edit/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SedeCiudad sedeCiudad = db.Tb_SedeCiudad.Find(id);
            if (sedeCiudad == null)
            {
                return HttpNotFound();
            }
            ViewBag.Ciud_Id = new SelectList(db.Tb_Ciudad, "Ciud_Id", "Ciud_Nom", sedeCiudad.Ciud_Id);
            ViewBag.Empr_Nit = new SelectList(db.Tb_Empresa, "Empr_Nit", "Empr_Nom", sedeCiudad.Empr_Nit);
            return View(sedeCiudad);
        }

        // POST: Administrador/SedeCiudades/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit([Bind(Include = "Sciu_Id,Sciu_Nom,Ciud_Id,Empr_Nit,Sciu_Registro")] SedeCiudad sedeCiudad)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sedeCiudad).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Ciud_Id = new SelectList(db.Tb_Ciudad, "Ciud_Id", "Ciud_Nom", sedeCiudad.Ciud_Id);
            ViewBag.Empr_Nit = new SelectList(db.Tb_Empresa, "Empr_Nit", "Empr_Nom", sedeCiudad.Empr_Nit);
            return View(sedeCiudad);
        }

        // GET: Administrador/SedeCiudades/Delete/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SedeCiudad sedeCiudad = db.Tb_SedeCiudad.Find(id);
            if (sedeCiudad == null)
            {
                return HttpNotFound();
            }
            return View(sedeCiudad);
        }

        // POST: Administrador/SedeCiudades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult DeleteConfirmed(int id)
        {
            SedeCiudad sedeCiudad = db.Tb_SedeCiudad.Find(id);
            db.Tb_SedeCiudad.Remove(sedeCiudad);
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
