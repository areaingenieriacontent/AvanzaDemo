using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using PagedList;
using Plenamente.Models;
using Plenamente.Models.ViewModel;

namespace Plenamente.Controllers
{
    public class ReglaHigienesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ReglaHigienes
        [Authorize(Roles = "Administrator")]
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            //var tb_ReglaHigiene = db.Tb_ReglaHigiene.Include(r => r.Empresa);
            //return View(tb_ReglaHigiene.ToList());
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
            var rHigiene = from s in db.Tb_ReglaHigiene
                           where s.Empr_Nit == Empr_Nit
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                rHigiene = rHigiene.Where(s => s.Rhig_Registro.ToString().Contains(searchString)
                                       || s.Rhig_Registro.ToString().Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    rHigiene = rHigiene.OrderByDescending(s => s.Rhig_Registro.ToString());
                    break;
                default:  // Name ascending 
                    rHigiene = rHigiene.OrderBy(s => s.Rhig_Registro.ToString());
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(rHigiene.ToPagedList(pageNumber, pageSize));

        }

        // GET: Administrador/ReglaHigienes/Details/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReglaHigiene reglaHigiene = db.Tb_ReglaHigiene.Find(id);
            if (reglaHigiene == null)
            {
                return HttpNotFound();
            }
            return View(reglaHigiene);
        }

        // GET: ReglaHigienes/Create
        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            ApplicationDbContext entity = new ApplicationDbContext();

            List<Empresa> listE = entity.Tb_Empresa.ToList();
            ViewBag.EmpreList = new SelectList(listE, "Empr_Nit", "Empr_Nom");
            return View();
        }

        // POST: ReglaHigienes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Create([Bind(Include = "Rhig_Id,Rhig_Archivo,Rhig_Nom,Empr_Nit,Rhig_Registro")] ReglaHigiene reglaHigiene)
        {
            if (ModelState.IsValid)
            {
                db.Tb_ReglaHigiene.Add(reglaHigiene);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Empr_Nit = new SelectList(db.Tb_Empresa, "Empr_Nit", "Empr_Nom", reglaHigiene.Empr_Nit);
            return View(reglaHigiene);
        }

        // GET: ReglaHigienes/Edit/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReglaHigiene reglaHigiene = db.Tb_ReglaHigiene.Find(id);
            if (reglaHigiene == null)
            {
                return HttpNotFound();
            }
            ViewBag.Empr_Nit = new SelectList(db.Tb_Empresa, "Empr_Nit", "Empr_Nom", reglaHigiene.Empr_Nit);
            return View(reglaHigiene);
        }

        // POST: ReglaHigienes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit([Bind(Include = "Rhig_Id,Rhig_Archivo,Rhig_Nom,Empr_Nit,Rhig_Registro")] ReglaHigiene reglaHigiene)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reglaHigiene).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Empr_Nit = new SelectList(db.Tb_Empresa, "Empr_Nit", "Empr_Nom", reglaHigiene.Empr_Nit);
            return View(reglaHigiene);
        }

        // GET: ReglaHigienes/Delete/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReglaHigiene reglaHigiene = db.Tb_ReglaHigiene.Find(id);
            if (reglaHigiene == null)
            {
                return HttpNotFound();
            }
            return View(reglaHigiene);
        }

        // POST: ReglaHigienes/Delete/5
        [Authorize(Roles = "Administrator")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ReglaHigiene reglaHigiene = db.Tb_ReglaHigiene.Find(id);
            db.Tb_ReglaHigiene.Remove(reglaHigiene);
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

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult SaveRecord(Rhigiene rhigiene)
        {
            try
            {
                ApplicationDbContext entity = new ApplicationDbContext();
                {
                    ReglaHigiene rh = new ReglaHigiene();
                    rh.Rhig_Nom = rhigiene.Rhig_Nom;
                    rh.Rhig_Archivo = SaveToPhysicalLocation(rhigiene.Rhig_Archivo);
                    rh.Rhig_Registro = rhigiene.Rhig_Registro;
                    rh.Empr_Nit = rhigiene.Empr_Nit;

                    entity.Tb_ReglaHigiene.Add(rh);
                    entity.SaveChanges();

                    int latest = rh.Rhig_Id;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RedirectToAction("Create");
        }
        private string SaveToPhysicalLocation(HttpPostedFileBase file)
        {
            if (file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(DateTime.Now.ToString("yyyyMMddHHmmss") + file.FileName);
                var path = Path.Combine(Server.MapPath("~/Files"),fileName);
                
                file.SaveAs(path);
                return fileName;
            }
            return string.Empty;
        }
    }
}
