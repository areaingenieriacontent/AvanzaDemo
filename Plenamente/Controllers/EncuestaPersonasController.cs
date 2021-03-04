using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Plenamente.Models;
using System.Net;
using Microsoft.AspNet.Identity;
using System.Net.Mail;
using System.Net.Mime;


namespace Plenamente.Controllers
{
    public class EncuestaPersonasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: EntrevistPersonas
        [Authorize(Roles = "Administrator")]
        public ActionResult Index()
        {

            List<EncuestaPersonas> encuPerso = new List<EncuestaPersonas>();
            var userId = User.Identity.GetUserId();
            var UserCurrent = db.Users.Find(userId);
            var Empr_Nit = UserCurrent.Empr_Nit;
            var Usuarios = from e in db.Users where e.Empr_Nit == Empr_Nit select e;
            /*var tb_AutoEvaluacion = db.Tb_AutoEvaluacion.Include(a => a.Empresa);
            return View(tb_AutoEvaluacion.ToList());*/

            ViewBag.Prueba = Usuarios.FirstOrDefault().Pers_Nom1;

            var Usuario = Usuarios
            .OrderBy(x => x.UserName).ToList();

            foreach (var item in Usuarios)
            {
                EncuestaPersonas model = new EncuestaPersonas();

                model.Nombres = item.Pers_Nom1;
                model.Email = item.Email;
                model.Documento = item.Pers_Doc;
                model.Apellidos = item.Pers_Apel1;
                model.idPersona = item.Id;



                encuPerso.Add(model);
            }

            return View(encuPerso);
        }
        [Authorize(Roles = "Administrator")]
        public ActionResult EnviarCorreo()
        {
            return View();
        }
        //[HttpPost, ValidateInput(false)]
        //[Authorize(Roles = "Administrator")]
        //public ActionResult EnviarCorreo(string id, EnvioCorreo model)
        //{


        //    string[] separadas;
        //    separadas = id.Split('|');
        //    ViewBag.id = separadas[0];
        //    var mensaje = new MailMessage();


        //    for (int i = 0; i < separadas.Length; i++)
        //    {
        //        var id1 = separadas[i];
        //        var Correo = db.Users.FirstOrDefault(p => p.Id == id1);
        //        if (Correo != null)
        //        {
        //            mensaje.To.Add(Correo.Email);
        //        }
        //    }
        //    mensaje.Subject = model.Asunto;

        //    mensaje.Body = model.Mensaje;

        //    mensaje.IsBodyHtml = true;

        //    var smtp = new SmtpClient();

        //    smtp.Send(mensaje);

        //    return RedirectToAction("Index");
        //}


    }
    //    public ActionResult Prueba (string id)
    //    {
    //        string[] separadas;
    //        EnvioCorreo model = new EnvioCorreo();
    //        separadas = id.Split('|');
    //        ViewBag.id = separadas[0];

    //        for(int i=0; i < separadas.Length; i++)
    //        {
    //            var mensaje = new MailMessage();
    //            mensaje.Subject = "pruebasBien2";
    //            mensaje.Body = model.Mensaje;
    //            var id1 = separadas[i];
    //            var Correo = db.Users.FirstOrDefault(p => p.Id == id1);


    //            if (Correo != null)
    //            {
    //                ViewBag.salida = Correo.Email;

    //                mensaje.To.Add(Correo.Email);

    //                mensaje.IsBodyHtml = true;

    //                var smtp = new SmtpClient();

    //                smtp.Send(mensaje);

    //            }



    //        }


    //        return View();
    //    }

    //}
}
