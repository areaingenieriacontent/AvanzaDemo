using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Plenamente.Models;
using PagedList;
using Microsoft.AspNet.Identity;
using System.Web;
using Microsoft.AspNet.Identity.Owin;

namespace Plenamente.Areas.Administrador.Controllers
{
    public class CargoEmpresasController : Controller
    {

            private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Administrador/CargoEmpresas
        //Se Agregan Los Parametros sortOrder, currentFilter, searchString, page, para la busqueda y paginacion de la encuesta.
        [Authorize(Roles = "Administrator")]
        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            //Se obtiene sortOrder para el orden de los datos.
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            //De manera asendente o desendente
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            //Si el campo esta vacio, la pagina sera igual a 1
            if (searchString != null)
            {
                //La pagina empieza 1
                page = 1;
            }
            else
            {
                //Si el campo esta lleno la busqueda se hara el searchString 
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            //Se obtiene el parametro
            //Se realiza una consulta tipo Linq para obtener los datos de acuerdo a la empresa que este logeada
            var userId = User.Identity.GetUserId();
            var UserCurrent = db.Users.Find(userId);
            var Empr_Nit = UserCurrent.Empr_Nit;
            var cargos = from s in db.Tb_CargoEmpresa
                         where s.Empr_Nit == Empr_Nit
                         select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                //Se realiza la busqueda deacuerdo a la cadena de texto que se inserte en el input
                cargos = cargos.Where(s => s.Cemp_Nom.Contains(searchString)
                                       || s.Cemp_Nom.Contains(searchString));
            }
            switch (sortOrder)
            {
                //Organzia de manera decendente o asendente
                case "name_desc":
                    cargos = cargos.OrderByDescending(s => s.Cemp_Nom);
                    break;
                default:  // Name ascending 
                    cargos = cargos.OrderBy(s => s.Cemp_Nom);
                    break;
            }
            //Se muestran la cantidad de registros
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            //Retorna la vista      
            return View(cargos.ToPagedList(pageNumber, pageSize));
        }


        // GET: Administrador/CargoEmpresas/Details/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CargoEmpresa cargoEmpresa = db.Tb_CargoEmpresa.Find(id);
            if (cargoEmpresa == null)
            {
                return HttpNotFound();
            }
            return View(cargoEmpresa);
        }

        // GET: Administrador/CargoEmpresas/Create
        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            ViewBag.Empr_Nit = new SelectList(db.Tb_Empresa, "Empr_Nit", "Empr_Nom");
            return View();
        }

        // POST: Administrador/CargoEmpresas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Create([Bind(Include = "Cemp_Id,Cemp_Nom,Empr_Nit,Cemp_Registro")] CargoEmpresa cargoEmpresa)
        {
            if (ModelState.IsValid)
            {
                db.Tb_CargoEmpresa.Add(cargoEmpresa);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Empr_Nit = new SelectList(db.Tb_Empresa, "Empr_Nit", "Empr_Nom", cargoEmpresa.Empr_Nit);
            return View(cargoEmpresa);
        }

        // GET: Administrador/CargoEmpresas/Edit/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CargoEmpresa cargoEmpresa = db.Tb_CargoEmpresa.Find(id);
            if (cargoEmpresa == null)
            {
                return HttpNotFound();
            }
            ViewBag.Empr_Nit = new SelectList(db.Tb_Empresa, "Empr_Nit", "Empr_Nom", cargoEmpresa.Empr_Nit);
            return View(cargoEmpresa);
        }

        // POST: Administrador/CargoEmpresas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit([Bind(Include = "Cemp_Id,Cemp_Nom,Empr_Nit,Cemp_Registro")] CargoEmpresa cargoEmpresa)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cargoEmpresa).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Empr_Nit = new SelectList(db.Tb_Empresa, "Empr_Nit", "Empr_Nom", cargoEmpresa.Empr_Nit);
            return View(cargoEmpresa);
        }

        // GET: Administrador/CargoEmpresas/Delete/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CargoEmpresa cargoEmpresa = db.Tb_CargoEmpresa.Find(id);
            if (cargoEmpresa == null)
            {
                return HttpNotFound();
            }
            return View(cargoEmpresa);
        }

        // POST: Administrador/CargoEmpresas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult DeleteConfirmed(int id)
        {
            CargoEmpresa cargoEmpresa = db.Tb_CargoEmpresa.Find(id);
            db.Tb_CargoEmpresa.Remove(cargoEmpresa);
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
