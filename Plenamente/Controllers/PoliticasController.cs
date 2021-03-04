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

namespace Plenamente.Areas.Administrador.Controllers
{
    public class PoliticasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Administrador/Politicas
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
            var politicas = from s in db.Tb_politica
                            where s.Empr_Nit == Empr_Nit
                            select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                politicas = politicas.Where(s => s.Poli_Registro.ToString().Contains(searchString)
                                       || s.Poli_Registro.ToString().Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    politicas = politicas.OrderByDescending(s => s.Poli_Registro.ToString());
                    break;
                default:  // Name ascending 
                    politicas = politicas.OrderBy(s => s.Poli_Registro.ToString());
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(politicas.ToPagedList(pageNumber, pageSize));
        }

        // GET: Administrador/Politicas/Details/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Politica politica = db.Tb_politica.Find(id);
            if (politica == null)
            {
                return HttpNotFound();
            }
            return View(politica);
        }

        // GET: Administrador/Politicas/Create
        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            ApplicationDbContext entity = new ApplicationDbContext();

            List<Empresa> listE = entity.Tb_Empresa.ToList();
            ViewBag.EmpreList = new SelectList(listE, "Empr_Nit", "Empr_Nom");
            return View();
        }

        // POST: Administrador/Politicas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Create([Bind(Include = "Poli_Id,Poli_Archivo,Empr_Nit,Poli_Registro")] Politica politica)
        {
            if (ModelState.IsValid)
            {
                db.Tb_politica.Add(politica);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Empr_Nit = new SelectList(db.Tb_Empresa, "Empr_Nit", "Empr_Nom", politica.Empr_Nit);
            return View(politica);
        }

        // GET: Administrador/Politicas/Edit/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Politica politica = db.Tb_politica.Find(id);
            if (politica == null)
            {
                return HttpNotFound();
            }
            ViewBag.Empr_Nit = new SelectList(db.Tb_Empresa, "Empr_Nit", "Empr_Nom", politica.Empr_Nit);
            return View(politica);
        }

        // POST: Administrador/Politicas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit([Bind(Include = "Poli_Id,Poli_Archivo,Empr_Nit,Poli_Registro")] Politica politica)
        {
            if (ModelState.IsValid)
            {
                db.Entry(politica).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Empr_Nit = new SelectList(db.Tb_Empresa, "Empr_Nit", "Empr_Nom", politica.Empr_Nit);
            return View(politica);
        }

        // GET: Administrador/Politicas/Delete/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Politica politica = db.Tb_politica.Find(id);
            if (politica == null)
            {
                return HttpNotFound();
            }
            return View(politica);
        }

        // POST: Administrador/Politicas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult DeleteConfirmed(int id)
        {
            Politica politica = db.Tb_politica.Find(id);
            db.Tb_politica.Remove(politica);
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
        public ActionResult SaveRecord(PoliticasViewModel politicasViewModel)
        {
            try
            {
                ApplicationDbContext entity = new ApplicationDbContext();
                {
                    Politica pol = new Politica();
                    pol.Poli_Nom = politicasViewModel.Poli_Nom;
                    pol.Poli_Archivo = SaveToPhysicalLocation(politicasViewModel.Poli_Archivo);
                    pol.Poli_Registro = politicasViewModel.Poli_Registro;
                    pol.Empr_Nit = politicasViewModel.Empr_Nit;

                    entity.Tb_politica.Add(pol);
                    entity.SaveChanges();

                    int latest = pol.Poli_Id;
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
                var path = Path.Combine(Server.MapPath("~/Files"), fileName);

                file.SaveAs(path);
                return fileName;
            }
            return string.Empty;
        }


    }
}
