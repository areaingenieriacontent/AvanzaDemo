using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Plenamente.Models;

namespace Plenamente.Areas.Administrador.Controllers
{
    public class ActiCumplimientoesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Administrador/ActiCumplimientoes
        [Authorize(Roles = "Administrator")]
        public ActionResult Index()
        {


            var actiCumplimientoes = db.Tb_ActiCumplimiento.Include(a => a.ApplicationUser).Include(a => a.Empresa).Include(a => a.Frecuencia).Include(a => a.ObjEmpresa).Include(a => a.Periodo);

            
            return View(actiCumplimientoes.ToList());
        }

        // GET: Administrador/ActiCumplimientoes/Details/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActiCumplimiento actiCumplimiento = db.Tb_ActiCumplimiento.Find(id);
            if (actiCumplimiento == null)
            {
                return HttpNotFound();
            }
            return View(actiCumplimiento);
        }

        // GET: Administrador/ActiCumplimientoes/Create
        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            ViewBag.Id = new SelectList(db.Users, "Id", "Pers_Nom1");
            ViewBag.Empr_Nit = new SelectList(db.Tb_Empresa, "Empr_Nit", "Empr_Nom");
            ViewBag.Frec_Id = new SelectList(db.Tb_Frecuencia, "Frec_Id", "Frec_Nom");
            ViewBag.Oemp_Id = new SelectList(db.Tb_ObjEmpresa, "Oemp_Id", "Oemp_Nombre");
            ViewBag.Peri_Id = new SelectList(db.Tb_Periodo, "Peri_Id", "Peri_Nom");
            return View();
        }

        // POST: Administrador/ActiCumplimientoes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Create([Bind(Include = "Acum_Id,Acum_Desc,Acum_Porcentest,Acum_Ejec,Acum_Registro,Acum_IniAct,Acum_FinAct,Oemp_Id,Id,Peri_Id,Empr_Nit,Frec_Id")] ActiCumplimiento actiCumplimiento)
        {
            if (ModelState.IsValid)
            {
                db.Tb_ActiCumplimiento.Add(actiCumplimiento);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id = new SelectList(db.Users, "Id", "Pers_Nom1", actiCumplimiento.Id);
            ViewBag.Empr_Nit = new SelectList(db.Tb_Empresa, "Empr_Nit", "Empr_Nom", actiCumplimiento.Empr_Nit);
            ViewBag.Frec_Id = new SelectList(db.Tb_Frecuencia, "Frec_Id", "Frec_Nom", actiCumplimiento.Frec_Id);
            ViewBag.Oemp_Id = new SelectList(db.Tb_ObjEmpresa, "Oemp_Id", "Oemp_Nombre", actiCumplimiento.Oemp_Id);
            ViewBag.Peri_Id = new SelectList(db.Tb_Periodo, "Peri_Id", "Peri_Nom", actiCumplimiento.Peri_Id);
            return View(actiCumplimiento);
        }

        // GET: Administrador/ActiCumplimientoes/Edit/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActiCumplimiento actiCumplimiento = db.Tb_ActiCumplimiento.Find(id);
            if (actiCumplimiento == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id = new SelectList(db.Users, "Id", "Pers_Nom1", actiCumplimiento.Id);
            ViewBag.Empr_Nit = new SelectList(db.Tb_Empresa, "Empr_Nit", "Empr_Nom", actiCumplimiento.Empr_Nit);
            ViewBag.Frec_Id = new SelectList(db.Tb_Frecuencia, "Frec_Id", "Frec_Nom", actiCumplimiento.Frec_Id);
            ViewBag.Oemp_Id = new SelectList(db.Tb_ObjEmpresa, "Oemp_Id", "Oemp_Nombre", actiCumplimiento.Oemp_Id);
            ViewBag.Peri_Id = new SelectList(db.Tb_Periodo, "Peri_Id", "Peri_Nom", actiCumplimiento.Peri_Id);
            return View(actiCumplimiento);
        }

        // POST: Administrador/ActiCumplimientoes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit([Bind(Include = "Acum_Id,Acum_Desc,Acum_Porcentest,Acum_Ejec,Acum_Registro,Acum_IniAct,Acum_FinAct,Oemp_Id,Id,Peri_Id,Empr_Nit,Frec_Id")] ActiCumplimiento actiCumplimiento)
        {
            if (ModelState.IsValid)
            {
                db.Entry(actiCumplimiento).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id = new SelectList(db.Users, "Id", "Pers_Nom1", actiCumplimiento.Id);
            ViewBag.Empr_Nit = new SelectList(db.Tb_Empresa, "Empr_Nit", "Empr_Nom", actiCumplimiento.Empr_Nit);
            ViewBag.Frec_Id = new SelectList(db.Tb_Frecuencia, "Frec_Id", "Frec_Nom", actiCumplimiento.Frec_Id);
            ViewBag.Oemp_Id = new SelectList(db.Tb_ObjEmpresa, "Oemp_Id", "Oemp_Nombre", actiCumplimiento.Oemp_Id);
            ViewBag.Peri_Id = new SelectList(db.Tb_Periodo, "Peri_Id", "Peri_Nom", actiCumplimiento.Peri_Id);
            return View(actiCumplimiento);
        }

        // GET: Administrador/ActiCumplimientoes/Delete/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int? id)
        {
            ViewBag.Id = new SelectList(db.Users, "Id", "Pers_Nom1");
            ViewBag.Empr_Nit = new SelectList(db.Tb_Empresa, "Empr_Nit", "Empr_Nom");
            ViewBag.Frec_Id = new SelectList(db.Tb_Frecuencia, "Frec_Id", "Frec_Nom");
            ViewBag.Oemp_Id = new SelectList(db.Tb_ObjEmpresa, "Oemp_Id", "Oemp_Nombre");
            ViewBag.Peri_Id = new SelectList(db.Tb_Periodo, "Peri_Id", "Peri_Nom");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                ViewBag.Id = new SelectList(db.Users, "Id", "Pers_Nom1");
                ViewBag.Empr_Nit = new SelectList(db.Tb_Empresa, "Empr_Nit", "Empr_Nom");
                ViewBag.Frec_Id = new SelectList(db.Tb_Frecuencia, "Frec_Id", "Frec_Nom");
                ViewBag.Oemp_Id = new SelectList(db.Tb_ObjEmpresa, "Oemp_Id", "Oemp_Nombre");
                ViewBag.Peri_Id = new SelectList(db.Tb_Periodo, "Peri_Id", "Peri_Nom");
                ActiCumplimiento X = db.Tb_ActiCumplimiento.Find(id);
                if (X == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    return View(X);
                }
            }
        }

        // POST: Administrador/ActiCumplimientoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult DeleteConfirmed(int id, ActiCumplimiento X)
        {
            
            try
            {
                if (ModelState.IsValid)
                {
                    ViewBag.Id = new SelectList(db.Users, "Id", "Pers_Nom1");
                    ViewBag.Empr_Nit = new SelectList(db.Tb_Empresa, "Empr_Nit", "Empr_Nom");
                    ViewBag.Frec_Id = new SelectList(db.Tb_Frecuencia, "Frec_Id", "Frec_Nom");
                    ViewBag.Oemp_Id = new SelectList(db.Tb_ObjEmpresa, "Oemp_Id", "Oemp_Nombre");
                    ViewBag.Peri_Id = new SelectList(db.Tb_Periodo, "Peri_Id", "Peri_Nom");
                    X = db.Tb_ActiCumplimiento.Find(id);

                    if (X == null)
                    {
                        return HttpNotFound();
                    }
                    else
                    {
                        db.Tb_ActiCumplimiento.Remove(X);
                        db.SaveChanges();
                    }
                    return RedirectToAction("Index");
                }
                return View(X);
            }
            catch
            {
                return View(X);
            }
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
