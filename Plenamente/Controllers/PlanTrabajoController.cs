using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Plenamente.Areas.Administrador.Controllers
{
    public class PlanTrabajoController : Controller
    {
        // GET: PlanTrabajo
        [HttpGet]
        public ActionResult Upload()
        {

            ViewBag.Cump_Id = new SelectList(db.Tb_Cumplimiento, "Cump_Id", "Cump_Observ");
            ViewBag.Tdca_id = new SelectList(db.Tb_TipoDocCarga, "Tdca_id", "Tdca_Nom");
            ViewBag.Id = new SelectList(db.Users, "Id", "Pers_Nom1");

            return View();
        }
    }
}