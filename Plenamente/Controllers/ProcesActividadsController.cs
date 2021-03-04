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
    public class ProcesActividadsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Administrador/ProcesActividads
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

            var actividades = from s in db.Tb_ProcesActividad
                         select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                actividades = actividades.Where(s => s.Pact_Nombre.Contains(searchString)
                                       || s.Pact_Nombre.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    actividades = actividades.OrderByDescending(s => s.Pact_Nombre);
                    break;
                default:  // Name ascending 
                    actividades = actividades.OrderBy(s => s.Pact_Nombre);
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(actividades.ToPagedList(pageNumber, pageSize));
        }

        // GET: Administrador/ProcesActividads/Details/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProcesActividad procesActividad = db.Tb_ProcesActividad.Find(id);
            if (procesActividad == null)
            {
                return HttpNotFound();
            }
            return View(procesActividad);
        }

        // GET: Administrador/ProcesActividads/Create
        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Administrador/ProcesActividads/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Create([Bind(Include = "Pact_Id,Pact_Nombre,Pact_Registro")] ProcesActividad procesActividad)
        {
            if (ModelState.IsValid)
            {
                db.Tb_ProcesActividad.Add(procesActividad);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(procesActividad);
        }

        // GET: Administrador/ProcesActividads/Edit/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProcesActividad procesActividad = db.Tb_ProcesActividad.Find(id);
            if (procesActividad == null)
            {
                return HttpNotFound();
            }
            return View(procesActividad);
        }

        // POST: Administrador/ProcesActividads/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit([Bind(Include = "Pact_Id,Pact_Nombre,Pact_Registro")] ProcesActividad procesActividad)
        {
            if (ModelState.IsValid)
            {
                db.Entry(procesActividad).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(procesActividad);
        }

        // GET: Administrador/ProcesActividads/Delete/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProcesActividad procesActividad = db.Tb_ProcesActividad.Find(id);
            if (procesActividad == null)
            {
                return HttpNotFound();
            }
            return View(procesActividad);
        }

        // POST: Administrador/ProcesActividads/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult DeleteConfirmed(int id)
        {
            ProcesActividad procesActividad = db.Tb_ProcesActividad.Find(id);
            db.Tb_ProcesActividad.Remove(procesActividad);
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
