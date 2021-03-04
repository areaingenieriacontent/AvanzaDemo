using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using PagedList;
using Plenamente.Models;

namespace Plenamente.Areas.Administrador.Controllers
{
    public class AreaEmpresasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Administrador/AreaEmpresas
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
            var userId = User.Identity.GetUserId();
            var UserCurrent = db.Users.Find(userId);
            var Empr_Nit = UserCurrent.Empr_Nit;

            var Areas = from s in db.Tb_AreaEmpresa
                         where s.Empr_Nit == Empr_Nit
                         select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                Areas = Areas.Where(s => s.Aemp_Nom.Contains(searchString)
                                       || s.Aemp_Nom.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    Areas = Areas.OrderByDescending(s => s.Aemp_Nom);
                    break;
                default:  // Name ascending 
                    Areas = Areas.OrderBy(s => s.Aemp_Nom);
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(Areas.ToPagedList(pageNumber, pageSize));
        }

        // GET: Administrador/AreaEmpresas/Details/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AreaEmpresa areaEmpresa = db.Tb_AreaEmpresa.Find(id);
            if (areaEmpresa == null)
            {
                return HttpNotFound();
            }
            return View(areaEmpresa);
        }

        // GET: Administrador/AreaEmpresas/Create
        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            ViewBag.Empr_Nit = new SelectList(db.Tb_Empresa, "Empr_Nit", "Empr_Nom");
            return View();
        }

        // POST: Administrador/AreaEmpresas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Create([Bind(Include = "Aemp_Id,Aemp_Nom,Empr_Nit,Aemp_Registro")] AreaEmpresa areaEmpresa)
        {
            if (ModelState.IsValid)
            {
                db.Tb_AreaEmpresa.Add(areaEmpresa);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Empr_Nit = new SelectList(db.Tb_Empresa, "Empr_Nit", "Empr_Nom", areaEmpresa.Empr_Nit);
            return View(areaEmpresa);
        }

        // GET: Administrador/AreaEmpresas/Edit/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AreaEmpresa areaEmpresa = db.Tb_AreaEmpresa.Find(id);
            if (areaEmpresa == null)
            {
                return HttpNotFound();
            }
            ViewBag.Empr_Nit = new SelectList(db.Tb_Empresa, "Empr_Nit", "Empr_Nom", areaEmpresa.Empr_Nit);
            return View(areaEmpresa);
        }

        // POST: Administrador/AreaEmpresas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit([Bind(Include = "Aemp_Id,Aemp_Nom,Empr_Nit,Aemp_Registro")] AreaEmpresa areaEmpresa)
        {
            if (ModelState.IsValid)
            {
                db.Entry(areaEmpresa).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Empr_Nit = new SelectList(db.Tb_Empresa, "Empr_Nit", "Empr_Nom", areaEmpresa.Empr_Nit);
            return View(areaEmpresa);
        }

        // GET: Administrador/AreaEmpresas/Delete/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AreaEmpresa areaEmpresa = db.Tb_AreaEmpresa.Find(id);
            if (areaEmpresa == null)
            {
                return HttpNotFound();
            }
            return View(areaEmpresa);
        }

        // POST: Administrador/AreaEmpresas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult DeleteConfirmed(int id)
        {
            AreaEmpresa areaEmpresa = db.Tb_AreaEmpresa.Find(id);
            db.Tb_AreaEmpresa.Remove(areaEmpresa);
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
