using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using PagedList;
using Plenamente.Models;
using Plenamente.App_Tool;

namespace Plenamente.Areas.Administrador.Controllers
{
    public class ObjEmpresasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Administrador/ObjEmpresas
        [Authorize(Roles = "Admin")]
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
            var actividades = from s in db.Tb_ObjEmpresa
                              where s.Empr_Nit == Empr_Nit
                              select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                actividades = actividades.Where(s => s.Oemp_Nombre.Contains(searchString)
                                       || s.Oemp_Nombre.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    actividades = actividades.OrderByDescending(s => s.Oemp_Nombre);
                    break;
                default:  // Name ascending 
                    actividades = actividades.OrderBy(s => s.Oemp_Nombre);
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(actividades.ToPagedList(pageNumber, pageSize));
        }

        // GET: Administrador/ProcesActividads/Details/

        // GET: Administrador/ObjEmpresas/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ObjEmpresa objEmpresa = db.Tb_ObjEmpresa.Find(id);
            if (objEmpresa == null)
            {
                return HttpNotFound();
            }
            return View(objEmpresa);
        }

        // GET: Administrador/ObjEmpresas/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.Empr_Nit = new SelectList(db.Tb_Empresa, "Empr_Nit", "Empr_Nom");
            return View();
        }

        // POST: Administrador/ObjEmpresas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "Oemp_Id,Oemp_Nombre,Oemp_Descrip,Oemp_Meta,Oemp_Registro,Empr_Nit")] ObjEmpresa objEmpresa)
        {
            if (ModelState.IsValid)
            {
                var userId = User.Identity.GetUserId();
                var UserCurrent = db.Users.Find(userId);
                var Empr_Nit = UserCurrent.Empr_Nit.ToString();
                int Empr_NitI = int.Parse(Empr_Nit);
                objEmpresa.Empr_Nit = Empr_NitI;
                db.Tb_ObjEmpresa.Add(objEmpresa);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Empr_Nit = new SelectList(db.Tb_Empresa, "Empr_Nit", "Empr_Nom", objEmpresa.Empr_Nit);
            return View(objEmpresa);
        }

        // GET: Administrador/ObjEmpresas/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ObjEmpresa objEmpresa = db.Tb_ObjEmpresa.Find(id);
            if (objEmpresa == null)
            {
                return HttpNotFound();
            }
            ViewBag.Empr_Nit = new SelectList(db.Tb_Empresa, "Empr_Nit", "Empr_Nom", objEmpresa.Empr_Nit);
            return View(objEmpresa);
        }

        // POST: Administrador/ObjEmpresas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "Oemp_Id,Oemp_Nombre,Oemp_Descrip,Oemp_Meta,Oemp_Registro,Empr_Nit")] ObjEmpresa objEmpresa)
        {
            if (ModelState.IsValid)
            {
                db.Entry(objEmpresa).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Empr_Nit = new SelectList(db.Tb_Empresa, "Empr_Nit", "Empr_Nom", objEmpresa.Empr_Nit);
            return View(objEmpresa);
        }

        // GET: Administrador/ObjEmpresas/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ObjEmpresa objEmpresa = db.Tb_ObjEmpresa.Find(id);
            if (objEmpresa == null)
            {
                return HttpNotFound();
            }
            return View(objEmpresa);
        }

        // POST: Administrador/ObjEmpresas/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ObjEmpresa objEmpresa = db.Tb_ObjEmpresa.Find(id);
            db.Tb_ObjEmpresa.Remove(objEmpresa);
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
