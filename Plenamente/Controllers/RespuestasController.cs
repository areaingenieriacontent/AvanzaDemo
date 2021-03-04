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
    
    public class RespuestasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        [Authorize(Roles = "Administrator")]
        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int idPregunta, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            ViewBag.idPregunta = idPregunta;


            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var respuestas = from s in db.Tb_Respuesta
                            where s.Preg_Id.Equals(idPregunta)
                            select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                respuestas = respuestas.Where(s => s.Resp_Nom.Contains(searchString) && s.Preg_Id.Equals(idPregunta)
                                       || s.Resp_Nom.Contains(searchString) && s.Preg_Id.Equals(idPregunta));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    respuestas = respuestas.OrderByDescending(s => s.Resp_Nom);
                    break;
                default:  // Name ascending 
                    respuestas = respuestas.OrderBy(s => s.Resp_Nom);
                    break;
            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(respuestas.ToPagedList(pageNumber, pageSize));
        }
        // GET: Administrador/Respuestas/Details/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Respuesta respuesta = db.Tb_Respuesta.Find(id);
            if (respuesta == null)
            {
                return HttpNotFound();
            }
            return View(respuesta);
        }

        // GET: Administrador/Respuestas/Create
        [Authorize(Roles = "Administrator")]
        public ActionResult Create(int? id, int idPregunta)
        {
            ViewBag.Resp_Id = new SelectList(db.Tb_Respuesta, "Resp_Id", "Resp_Tipo");
            ViewBag.Preg_Id = new SelectList(db.Tb_Pregunta, "Preg_Id", "Preg_Titulo");
            ViewBag.idPregunta = idPregunta;
            return View();
        }

        // POST: Administrador/Respuestas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Create([Bind(Include = "Resp_Id,Resp_Tipo,Resp_Nom,Resp_Registro,Preg_Id")] Respuesta respuesta, int? id, int idPregunta)
        {
            ViewBag.Resp_Id = new SelectList(db.Tb_Respuesta, "Resp_Id", "Resp_Tipo");
            if (ModelState.IsValid)
            {
                ViewBag.idPregunta = idPregunta;
                db.Tb_Respuesta.Add(respuesta);
                db.SaveChanges();
                return RedirectToAction("Index", "Respuestas", routeValues: new { ViewBag.idPregunta });

            }
            //ViewBag.Qure_Id = new SelectList(db.Tb_QuemRespuesta, "Qure_Id", "Qure_Nom");
            ViewBag.Preg_Id = new SelectList(db.Tb_Pregunta, "Preg_Id", "Preg_Titulo", respuesta.Preg_Id);
            return View(respuesta);
        }

        // GET: Administrador/Respuestas/Edit/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Respuesta respuesta = db.Tb_Respuesta.Find(id);
            if (respuesta == null)
            {
                return HttpNotFound();
            }
            ViewBag.Preg_Id = new SelectList(db.Tb_Pregunta, "Preg_Id", "Preg_Titulo", respuesta.Preg_Id);
            return View(respuesta);
        }

        // POST: Administrador/Respuestas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit([Bind(Include = "Resp_Id,Resp_Tipo,Resp_Nom,Resp_Registro,Preg_Id")] Respuesta respuesta)
        {
            if (ModelState.IsValid)
            {
                db.Entry(respuesta).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Preg_Id = new SelectList(db.Tb_Pregunta, "Preg_Id", "Preg_Titulo", respuesta.Preg_Id);
            return View(respuesta);
        }

        // GET: Administrador/Respuestas/Delete/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int? id, int idPregunta)
        {
            ViewBag.idPregunta = idPregunta;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Respuesta respuesta = db.Tb_Respuesta.Find(id);
            if (respuesta == null)
            {
                return HttpNotFound();
            }
            return View(respuesta);
        }

        // POST: Administrador/Respuestas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult DeleteConfirmed(int? id, int idPregunta)
        {
            ViewBag.idPregunta = idPregunta;
            Respuesta respuesta = db.Tb_Respuesta.Find(id);
            db.Tb_Respuesta.Remove(respuesta);
            db.SaveChanges();
            return RedirectToAction("Index", "Respuestas", routeValues: new { ViewBag.idPregunta });
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
