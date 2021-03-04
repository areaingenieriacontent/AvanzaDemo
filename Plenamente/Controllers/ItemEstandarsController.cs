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
    public class ItemEstandarsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Administrador/ItemEstandars
        [Authorize(Roles = "Administrator")]
        public ActionResult Index()
        {
            var tb_ItemEstandar = db.Tb_ItemEstandar.Include(i => i.Estandar);
            return View(tb_ItemEstandar.ToList());
        }

        // GET: Administrador/ItemEstandars/Details/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemEstandar itemEstandar = db.Tb_ItemEstandar.Find(id);
            if (itemEstandar == null)
            {
                return HttpNotFound();
            }
            return View(itemEstandar);
        }

        // GET: Administrador/ItemEstandars/Create
        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            ViewBag.Esta_Id = new SelectList(db.Tb_Estandar, "Esta_Id", "Esta_Nom");
            return View();
        }

        // POST: Administrador/ItemEstandars/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Create([Bind(Include = "Iest_Id,Iest_Desc,Iest_Verificar,Iest_Porcentaje,Iest_Cumple,Iest_Nocumple,Iest_Justifica,Iest_Nojustifica,Esta_Id,Iest_Peri,Iest_Observa,Iest_Registro")] ItemEstandar itemEstandar)
        {
            if (ModelState.IsValid)
            {
                db.Tb_ItemEstandar.Add(itemEstandar);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Esta_Id = new SelectList(db.Tb_Estandar, "Esta_Id", "Esta_Nom", itemEstandar.Esta_Id);
            return View(itemEstandar);
        }

        // GET: Administrador/ItemEstandars/Edit/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemEstandar itemEstandar = db.Tb_ItemEstandar.Find(id);
            if (itemEstandar == null)
            {
                return HttpNotFound();
            }
            ViewBag.Esta_Id = new SelectList(db.Tb_Estandar, "Esta_Id", "Esta_Nom", itemEstandar.Esta_Id);
            return View(itemEstandar);
        }

        // POST: Administrador/ItemEstandars/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit([Bind(Include = "Iest_Id,Iest_Desc,Iest_Verificar,Iest_Porcentaje,Iest_Cumple,Iest_Nocumple,Iest_Justifica,Iest_Nojustifica,Esta_Id,Iest_Peri,Iest_Observa,Iest_Registro")] ItemEstandar itemEstandar)
        {
            if (ModelState.IsValid)
            {
                db.Entry(itemEstandar).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Esta_Id = new SelectList(db.Tb_Estandar, "Esta_Id", "Esta_Nom", itemEstandar.Esta_Id);
            return View(itemEstandar);
        }

        // GET: Administrador/ItemEstandars/Delete/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemEstandar itemEstandar = db.Tb_ItemEstandar.Find(id);
            if (itemEstandar == null)
            {
                return HttpNotFound();
            }
            return View(itemEstandar);
        }

        // POST: Administrador/ItemEstandars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult DeleteConfirmed(int id)
        {
            ItemEstandar itemEstandar = db.Tb_ItemEstandar.Find(id);
            db.Tb_ItemEstandar.Remove(itemEstandar);
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
