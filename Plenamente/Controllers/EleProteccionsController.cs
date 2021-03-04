using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using PagedList;
using Plenamente.Models;

namespace Plenamente.Areas.Administrador.Controllers
{
    public class EleProteccionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Administrador/EleProteccions
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

            var proteccion = from s in db.Tb_EleProteccion
                         select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                proteccion = proteccion.Where(s => s.Epro_Nom.Contains(searchString)
                                       || s.Epro_Nom.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    proteccion = proteccion.OrderByDescending(s => s.Epro_Nom);
                    break;
                default:  // Name ascending 
                    proteccion = proteccion.OrderBy(s => s.Epro_Nom);
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(proteccion.ToPagedList(pageNumber, pageSize));
        }

        // GET: Administrador/EleProteccions/Details/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EleProteccion eleProteccion = db.Tb_EleProteccion.Find(id);
            if (eleProteccion == null)
            {
                return HttpNotFound();
            }
            return View(eleProteccion);
        }

        // GET: Administrador/EleProteccions/Create
        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Administrador/EleProteccions/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Create([Bind(Include = "Epro_Id,Epro_Nom,Epro_Registro")] EleProteccion eleProteccion)
        {
            if (ModelState.IsValid)
            {
                db.Tb_EleProteccion.Add(eleProteccion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(eleProteccion);
        }

        // GET: Administrador/EleProteccions/Edit/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EleProteccion eleProteccion = db.Tb_EleProteccion.Find(id);
            if (eleProteccion == null)
            {
                return HttpNotFound();
            }
            return View(eleProteccion);
        }

        // POST: Administrador/EleProteccions/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit([Bind(Include = "Epro_Id,Epro_Nom,Epro_Registro")] EleProteccion eleProteccion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(eleProteccion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(eleProteccion);
        }

        // GET: Administrador/EleProteccions/Delete/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EleProteccion eleProteccion = db.Tb_EleProteccion.Find(id);
            if (eleProteccion == null)
            {
                return HttpNotFound();
            }
            return View(eleProteccion);
        }

        // POST: Administrador/EleProteccions/Delete/5
        [Authorize(Roles = "Administrator")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EleProteccion eleProteccion = db.Tb_EleProteccion.Find(id);
            db.Tb_EleProteccion.Remove(eleProteccion);
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
