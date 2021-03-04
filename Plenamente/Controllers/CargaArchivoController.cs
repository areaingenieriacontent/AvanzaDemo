using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Plenamente.Models;
using System.IO;
using System.Web.Hosting;

namespace Plenamente.Areas.Administrador.Controllers
{
    public class CargaArchivoController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Administrador/CargaArchivo
        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public ActionResult Upload()
        {
            
            ViewBag.Cump_Id = new SelectList(db.Tb_Cumplimiento, "Cump_Id", "Cump_Observ");
            ViewBag.Tdca_id = new SelectList(db.Tb_TipoDocCarga, "Tdca_id", "Tdca_Nom");
            ViewBag.Id = new SelectList(db.Users, "Id", "Pers_Nom1");

            return View();
        }
       [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult Upload([Bind(Include = "Evid_Id,Evid_Nombre,Evid_Archivo,Evid_Registro,Cump_Id,Tdca_id,Id")]Archivo archivo)
        {
            using (ApplicationDbContext entity = new ApplicationDbContext())
            {
                //var cumplimiento = new Cumplimiento() 
                var evidencia = new Evidencia()
                {
                    Evid_Nombre = archivo.Evid_Nombre,
                    Evid_Archivo = SaveToPhysicalLocation(archivo.Evid_Archivo),
                    Evid_Registro = archivo.Evid_Registro,
                    
            };
                ViewBag.Cump_Id = new SelectList(entity.Tb_Cumplimiento, "Cump_Id", "Cump_Observ", archivo.Cump_Id);
                ViewBag.Tdca_id = new SelectList(entity.Tb_TipoDocCarga, "Tdca_id", "Tdca_Nom", archivo.Tdca_id);

                entity.Tb_Evidencia.Add(evidencia);
                //entity.Tb_Cumplimiento.Add(cumplimiento);
                entity.SaveChanges();
           }
           return View(archivo);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Upload([Bind(Include = "Evid_Id,Evid_Nombre,Evid_Archivo,Evid_Registro,Cump_Id,Tdca_id,Id")] Evidencia evidencia)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Tb_Evidencia.Add(evidencia);
        //        db.SaveChanges();
        //    }

        //    ViewBag.Id = new SelectList(db.Users, "Id", "Pers_Nom1", evidencia.Id);
        //    ViewBag.Cump_Id = new SelectList(db.Tb_Cumplimiento, "Cump_Id", "Cump_Observ", evidencia.Cump_Id);
        //    ViewBag.Tdca_id = new SelectList(db.Tb_TipoDocCarga, "Tdca_id", "Tdca_Nom", evidencia.Tdca_id);
        //    return View(evidencia);
        //}

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public ActionResult Evidencia()
        {
            ApplicationDbContext entity = new ApplicationDbContext();

            List<Cumplimiento> list = entity.Tb_Cumplimiento.ToList();
            ViewBag.CumplimientoList = new SelectList(list, "Cump_Id", "Cump_Observ");
            List<TipoDocCarga> listT = entity.Tb_TipoDocCarga.ToList();
            ViewBag.TipoDocCargaList = new SelectList(listT, "Tdca_id", "Tdca_Nom");
            List<ApplicationUser> listU =  entity.Users.ToList();
            ViewBag.UsersList = new SelectList(listU, "Id", "Pers_Nom1");

            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult SaveRecord(Archivo archivo)
        {
            try
            {
                ApplicationDbContext entity = new ApplicationDbContext();
                { 
                    Evidencia evid = new Evidencia();
                    evid.Evid_Nombre = archivo.Evid_Nombre;
                    evid.Evid_Archivo = SaveToPhysicalLocation(archivo.Evid_Archivo);
                    evid.Evid_Registro = archivo.Evid_Registro;
                    evid.Tdca_id = archivo.Tdca_id;
                    evid.Cump_Id = archivo.Cump_Id;
                    evid.Responsable = archivo.Id;

                    entity.Tb_Evidencia.Add(evid);

                    entity.SaveChanges();

                    int latest = evid.Evid_Id;
                }
            } catch (Exception ex)
            {
                throw ex;
            }
            return RedirectToAction("Evidencia");
        }

        /// <summary>  
        /// Save Posted File in Physical path and return saved path to store in a database  
        /// </summary>  
        /// <param name="file"></param>  
        /// <returns></returns>  
        private string SaveToPhysicalLocation(HttpPostedFileBase file)
        {
            if (file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/Files"), fileName);
               
                file.SaveAs(path);
                return fileName;
            }
            return string.Empty;
        }
        //public ActionResult DownloadFile(string file = "")
        //{

        //    file = HostingEnvironment.MapPath("~" + file);

        //    string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        //    var fileName = Path.GetFileName(file);
        //    return File(file, contentType, fileName);

        //}
       
    }
}