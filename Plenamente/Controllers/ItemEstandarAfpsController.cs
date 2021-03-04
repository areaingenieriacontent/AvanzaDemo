using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Plenamente.Models;

namespace Plenamente.Controllers
{
    public class ItemEstandarAfpsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ItemEstandarAfps
        public ActionResult Index()
        {
            var tb_ItemEstandarAfp = db.Tb_ItemEstandarAfp.Include(i => i.EstandarAfp);
            return View(tb_ItemEstandarAfp.ToList());
        }

        // GET: ItemEstandarAfps/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemEstandarAfp itemEstandarAfp = db.Tb_ItemEstandarAfp.Find(id);
            if (itemEstandarAfp == null)
            {
                return HttpNotFound();
            }
            return View(itemEstandarAfp);
        }

        // GET: ItemEstandarAfps/Create
        public ActionResult Create()
        {
            ViewBag.Esta_Id = new SelectList(db.Tb_EstandarAfp, "Esta_Id", "Esta_Nom");
            return View();
        }

        // POST: ItemEstandarAfps/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Iest_Id,Iest_Desc,Iest_Verificar,Iest_Porcentaje,Esta_Id,Categoria,CategoriaExcepcion,Iest_Peri,Iest_Observa,Iest_Registro,Iest_Video,Iest_Recurso,Iest_Rescursob,Iest_Rescursoc,Iest_Rescursod,Iest_Rescursoe,Iest_Rescursof,Iest_MasInfo")] ItemEstandarAfp itemEstandarAfp)
        {
            if (ModelState.IsValid)
            {
                db.Tb_ItemEstandarAfp.Add(itemEstandarAfp);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Esta_Id = new SelectList(db.Tb_EstandarAfp, "Esta_Id", "Esta_Nom", itemEstandarAfp.Esta_Id);
            return View(itemEstandarAfp);
        }

        // GET: ItemEstandarAfps/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemEstandarAfp itemEstandarAfp = db.Tb_ItemEstandarAfp.Find(id);
            if (itemEstandarAfp == null)
            {
                return HttpNotFound();
            }
            ViewBag.Esta_Id = new SelectList(db.Tb_EstandarAfp, "Esta_Id", "Esta_Nom", itemEstandarAfp.Esta_Id);
            return View(itemEstandarAfp);
        }

        // POST: ItemEstandarAfps/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Iest_Id,Iest_Desc,Iest_Verificar,Iest_Porcentaje,Esta_Id,Categoria,CategoriaExcepcion,Iest_Peri,Iest_Observa,Iest_Registro,Iest_Video,Iest_Recurso,Iest_Rescursob,Iest_Rescursoc,Iest_Rescursod,Iest_Rescursoe,Iest_Rescursof,Iest_MasInfo")] ItemEstandarAfp itemEstandarAfp)
        {
            if (ModelState.IsValid)
            {
                db.Entry(itemEstandarAfp).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Esta_Id = new SelectList(db.Tb_EstandarAfp, "Esta_Id", "Esta_Nom", itemEstandarAfp.Esta_Id);
            return View(itemEstandarAfp);
        }

        // GET: ItemEstandarAfps/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemEstandarAfp itemEstandarAfp = db.Tb_ItemEstandarAfp.Find(id);
            if (itemEstandarAfp == null)
            {
                return HttpNotFound();
            }
            return View(itemEstandarAfp);
        }

        // POST: ItemEstandarAfps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ItemEstandarAfp itemEstandarAfp = db.Tb_ItemEstandarAfp.Find(id);
            db.Tb_ItemEstandarAfp.Remove(itemEstandarAfp);
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
