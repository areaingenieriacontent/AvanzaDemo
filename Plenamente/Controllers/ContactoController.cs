using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace Plenamente.Controllers
{
    public class ContactoController : Controller
    {
        // GET: Contacto/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: Contacto/Create
        [HttpPost]
        public ActionResult Create(string identificacion, string empresa, string nombres, string categoria, string descripcion)
        {
            MailMessage correo = new MailMessage();
            correo.From = new MailAddress("plataforma@plenamentetrabajando.com");
            correo.To.Add("plataforma@plenamentetrabajando.com");
            correo.Subject = categoria + identificacion;
            string caso = "El usuario " + nombres + " identicado con el numero " + identificacion + " genero un nuevo caso de soporte: ";
            correo.Body = caso + descripcion;
            correo.IsBodyHtml = false;
            correo.Priority = MailPriority.Normal;

            var smtp = new SmtpClient();

            smtp.Send(correo);


            return RedirectToAction("Index", "Home");

        }
    }
}