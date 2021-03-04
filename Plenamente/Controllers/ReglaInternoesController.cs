using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using Plenamente.Models;
using Plenamente.Models.ViewModel;

namespace Plenamente.Areas.Administrador.Controllers
{
    public class ReglaInternoesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Administrador/ReglaInternoes
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

            var rHigiene = from s in db.Tb_ReglaInterno
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                rHigiene = rHigiene.Where(s => s.Rint_Registro.ToString().Contains(searchString)
                                       || s.Rint_Registro.ToString().Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    rHigiene = rHigiene.OrderByDescending(s => s.Rint_Registro.ToString());
                    break;
                default:  // Name ascending 
                    rHigiene = rHigiene.OrderBy(s => s.Rint_Registro.ToString());
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(rHigiene.ToPagedList(pageNumber, pageSize));

        }

        // GET: Administrador/ReglaInternoes/Details/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReglaInterno reglaInterno = db.Tb_ReglaInterno.Find(id);
            if (reglaInterno == null)
            {
                return HttpNotFound();
            }
            return View(reglaInterno);
        }

        // GET: Administrador/ReglaInternoes/Create
        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            ApplicationDbContext entity = new ApplicationDbContext();

            List<Empresa> listE = entity.Tb_Empresa.ToList();
            ViewBag.EmpreList = new SelectList(listE, "Empr_Nit", "Empr_Nom");
            return View();
        }

        // POST: Administrador/ReglaInternoes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Create([Bind(Include = "Rint_Id,Rint_Archivo,Empr_Nit,Rint_Registro")] ReglaInterno reglaInterno)
        {
            if (ModelState.IsValid)
            {
                db.Tb_ReglaInterno.Add(reglaInterno);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //byte[] uploadedRint_Archivo = new byte[ReglaInterno.Rint_Archivo]
            ViewBag.Empr_Nit = new SelectList(db.Tb_Empresa, "Empr_Nit", "Empr_Nom", reglaInterno.Empr_Nit);
            return View(reglaInterno);
        }

        // GET: Administrador/ReglaInternoes/Edit/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReglaInterno reglaInterno = db.Tb_ReglaInterno.Find(id);
            if (reglaInterno == null)
            {
                return HttpNotFound();
            }
            ViewBag.Empr_Nit = new SelectList(db.Tb_Empresa, "Empr_Nit", "Empr_Nom", reglaInterno.Empr_Nit);
            return View(reglaInterno);
        }

        // POST: Administrador/ReglaInternoes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit([Bind(Include = "Rint_Id,Rint_Archivo,Empr_Nit,Rint_Registro")] ReglaInterno reglaInterno)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reglaInterno).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Empr_Nit = new SelectList(db.Tb_Empresa, "Empr_Nit", "Empr_Nom", reglaInterno.Empr_Nit);
            return View(reglaInterno);
        }

        // GET: Administrador/ReglaInternoes/Delete/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReglaInterno reglaInterno = db.Tb_ReglaInterno.Find(id);
            if (reglaInterno == null)
            {
                return HttpNotFound();
            }
            return View(reglaInterno);
        }

        // POST: Administrador/ReglaInternoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult DeleteConfirmed(int id)
        {
            ReglaInterno reglaInterno = db.Tb_ReglaInterno.Find(id);
            db.Tb_ReglaInterno.Remove(reglaInterno);
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
        public ActionResult SaveRecord(ReglamentoInternoViewModel reglamentointernoviewmodel)
        {
            try
            {
                ApplicationDbContext entity = new ApplicationDbContext();
                {
                    ReglaInterno Rint = new ReglaInterno();
                    Rint.Rint_Nom = reglamentointernoviewmodel.Rint_Nom;
                    Rint.Rint_Archivo = SaveToPhysicalLocation(reglamentointernoviewmodel.Rint_Archivo);
                    Rint.Rint_Registro = reglamentointernoviewmodel.Rint_Registro;
                    Rint.Empr_Nit = reglamentointernoviewmodel.Empr_Nit;

                    entity.Tb_ReglaInterno.Add(Rint);
                    entity.SaveChanges();

                    int latest = Rint.Rint_Id;
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
