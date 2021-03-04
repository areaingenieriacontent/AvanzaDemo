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
    
    public class PreguntasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        [Authorize(Roles = "Administrator")]
        public ViewResult Index(string sortOrder, string currentFilter, int idEncuesta, int? page, string searchString)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            ViewBag.idEncuesta = idEncuesta;

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                ViewBag.idEncuesta = idEncuesta;
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            ViewBag.idEncuesta = idEncuesta;
            var preguntas = from s in db.Tb_Pregunta
                            where s.Encu_Id.Equals(idEncuesta)
                            select s;
            if (!string.IsNullOrEmpty(searchString))
            {
                preguntas = preguntas.Where(s => s.Preg_Titulo.Contains(searchString) && s.Encu_Id.Equals(idEncuesta)
                                       || s.Preg_Titulo.Contains(searchString) && s.Encu_Id.Equals(idEncuesta));
                ViewBag.idEncuesta = idEncuesta;
            }
            switch (sortOrder)
            {
                case "name_desc":
                    preguntas = preguntas.OrderByDescending(s => s.Preg_Id);
                    break;
                default:  // Name ascending 
                    preguntas = preguntas.OrderBy(s => s.Preg_Id);
                    break;
            }
           
            int pageSize = 20;
            int pageNumber = (page ?? 1);
            return View(preguntas.ToPagedList( pageNumber, pageSize));   
        }

        // GET: Administrador/Preguntas/Details/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Details(int? id, int? idEncuesta)
        {
            ViewBag.Encu_Id = idEncuesta;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pregunta pregunta = db.Tb_Pregunta.Find(id);
            if (pregunta == null)
            {
                return HttpNotFound();
            }

            return View(pregunta);
        }

        // GET: Administrador/Preguntas/Create
        [Authorize(Roles = "Administrator")]
        public ActionResult Create(int ?id, int idEncuesta)
        {
            ViewBag.Encu_Id = new SelectList(db.Tb_Encuesta, "Encu_Id", "Encu_Id");
            ViewBag.idEncuesta = idEncuesta;
            return View();
        }

        // POST: Administrador/Preguntas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Create([Bind(Include = "Preg_Id,Preg_Titulo,Preg_Registro,Encu_Id")] Pregunta pregunta, int? id, int idEncuesta)
        { 
            if (ModelState.IsValid)
            {
                ViewBag.idEncuesta = idEncuesta;
                db.Tb_Pregunta.Add(pregunta);
                db.SaveChanges();
                return RedirectToAction("Index", "Preguntas", routeValues: new { ViewBag.idEncuesta });
            }      
            ViewBag.Encu_Id = new SelectList(db.Tb_Encuesta, "Encu_Id", "Encu_Id", pregunta.Encu_Id);
            return View(pregunta);
        }

        // GET: Administrador/Preguntas/Edit/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int? id, int idEncuesta)
        {
            ViewBag.idEncuesta = idEncuesta;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pregunta pregunta = db.Tb_Pregunta.Find(id);
            if (pregunta == null)
            {
                return HttpNotFound();
            }
            ViewBag.Encu_Id = new SelectList(db.Tb_Encuesta, "Encu_Id", "Encu_Id", pregunta.Encu_Id);
            return View(pregunta);
        }

        // POST: Administrador/Preguntas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit([Bind(Include = "Preg_Id,Preg_Titulo,Preg_Registro,Encu_Id")] Pregunta pregunta, int ? id, int idEncuesta)
        {
            ViewBag.idEncuesta = idEncuesta;
            if (ModelState.IsValid)
            {
                ViewBag.idEncuesta = idEncuesta;
                db.Entry(pregunta).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Preguntas", routeValues: new { ViewBag.idEncuesta });
            }
            ViewBag.Encu_Id = new SelectList(db.Tb_Encuesta, "Encu_Id", "Encu_Id", pregunta.Encu_Id);
            return View(pregunta);
        }

        // GET: Administrador/Preguntas/Delete/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int? id, int idEncuesta)
        {
            ViewBag.idEncuesta = idEncuesta;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pregunta pregunta = db.Tb_Pregunta.Find(id);
            if (pregunta == null)
            {
                return HttpNotFound();
            }
            return View(pregunta);
        }

        // POST: Administrador/Preguntas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult DeleteConfirmed(int ?id, int idEncuesta)
        {
            ViewBag.idEncuesta = idEncuesta;
            Pregunta pregunta = db.Tb_Pregunta.Find(id);
            db.Tb_Pregunta.Remove(pregunta);  
            db.SaveChanges(); 
            return RedirectToAction("Index", "Preguntas", routeValues: new { ViewBag.idEncuesta });
           
        }
        [Authorize(Roles = "Administrator")]
        public ActionResult eliminarPreguntas(int ?id, int idEncuesta)
        {
            ViewBag.idEncuesta = idEncuesta;
            db.Database.ExecuteSqlCommand("DELETE FROM Preguntas");
            return RedirectToAction("Index", "Preguntas", routeValues: new { ViewBag.idEncuesta });
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
