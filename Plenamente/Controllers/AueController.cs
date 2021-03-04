using Plenamente.Models;
using Plenamente.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Plenamente.Controllers
{
    public class AueController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Aue
        [Authorize(Roles = "Administrator")]
        public ActionResult Index()
        {
            DinamicaAu Dau = new DinamicaAu();
            var query = (from cump in db.Tb_Cumplimiento
                         join iest in db.Tb_ItemEstandar on cump.Iest_Id equals iest.Iest_Id
                         join est in db.Tb_Estandar on iest.Esta_Id equals est.Esta_Id
                         join crit in db.Tb_Criterio on est.Crit_Id equals crit.Crit_Id
                         //where cump.Empr_Nit == '1' && crit.Crit_Id == '3'
                         select new DinamicaAu { Crit_Nom = crit.Crit_Nom, /*Cump_Aevidencia = cump.Cump_Aevidencia,*/ Esta_Nom = est.Esta_Nom, Iest_Desc=iest.Iest_Desc}).ToList();

            return View(query);
        }
        [Authorize(Roles = "Administrator")]
        public ActionResult Todos()
        {
            var quemado = (from iest in db.Tb_ItemEstandar
                           join est in db.Tb_Estandar on iest.Esta_Id equals est.Esta_Id
                           join crit in db.Tb_Criterio on est.Crit_Id equals crit.Crit_Id
                           select new DinamicaAu
                           {
                               Crit_Nom = crit.Crit_Nom,
                               Crit_Porcentaje = crit.Crit_Porcentaje,
                               Esta_Nom = est.Esta_Nom,
                               Iest_Desc = iest.Iest_Desc
                           }).ToList();
            return View(quemado);
        }
            
    }
}