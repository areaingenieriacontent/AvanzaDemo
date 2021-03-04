using Plenamente.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Plenamente.Models;


namespace Plenamente.Areas.Administrador.Controllers
{
    public class UploadCumpController : Controller
    {
        protected  ApplicationDbContext ApplicationDbContext { get; set; }
        // GET: Administrador/UploadCump
        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public ActionResult Upload()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult ResourceUpload(Cumplimiento model, HttpPostedFileBase upload)
        {
            if (upload != null && upload.ContentLength <= (5*1000000))
            {
                string[] allowedExtensions = new[] { ".pdf", ".txt" };
                var file = Path.GetExtension(DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + upload.FileName).ToLower();
                var ext = file;
                foreach (var Ext in allowedExtensions)
                {
                    if (Ext.Contains(file))
                    {
                        file = (DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + upload.FileName).ToLower();
                        upload.SaveAs(Server.MapPath("~/App_Data/" + file));
                        string ruta = file;
                        var cumpl = ApplicationDbContext.Tb_Cumplimiento.Find(model.Cump_Id);
                        //cumpl.Cump_Aevidencia = ruta;
                        //cumpl.Cump_Contenido = ext;
                        ApplicationDbContext.SaveChanges();
                        return RedirectToAction("Upload", new { id = model.Cump_Id });
                    }
                }

            }
            return RedirectToAction("Upload");
        }

    }
}