using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNet.Identity;
using Plenamente.Models;
using Plenamente.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Plenamente.Controllers
{
    public class EncuestaUsuariosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: EncuestaUsuarios
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var UserCurrent = db.Users.Find(userId);
            var Empr_Nit = UserCurrent.Empr_Nit;
            var preguntas = from e in db.Tb_Encuesta where e.Empr_Nit == Empr_Nit select e;
            return View(preguntas);
        }

        // GET: EncuestaUsuarios/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: EncuestaUsuarios/Create
        public ActionResult Encuesta(int id)
        {
            List<PreguntaViewModel> encuestaUsuarios = db.Tb_Pregunta.Where(m => m.Encu_Id == id).Select(m =>
                new PreguntaViewModel
                {
                    Preg_Id = m.Preg_Id,
                    Preg_Titulo = m.Preg_Titulo,
                    Preg_Registro = m.Preg_Registro,
                    Encu_Id = m.Encu_Id,
                    Respuesta = db.Tb_Respuesta.Select(c =>
                   new RespuestaViewModel
                   {
                       Resp_Id = c.Resp_Id,
                       Resp_Nom = c.Resp_Nom,
                       Resp_Registro = c.Resp_Registro,
                       Resp_Tipo = c.Resp_Tipo,
                       Preg_Id = c.Preg_Id,
                   }).ToList(),
                }).ToList();


            return View(encuestaUsuarios);
        }

        public ActionResult pdf()
        {

            Document doc = new Document(PageSize.A4);
            var output = new FileStream("c://pdf/reporte.pdf", FileMode.Create);
            var writer = PdfWriter.GetInstance(doc, output);


            doc.Open();


            var logo = iTextSharp.text.Image.GetInstance(Server.MapPath("~/imagenes/archivo.png"));
            logo.SetAbsolutePosition(430, 770);
            logo.ScaleAbsoluteHeight(30);
            logo.ScaleAbsoluteWidth(70);
            doc.Add(logo);

            PdfPTable table1 = new PdfPTable(2);
            table1.DefaultCell.Border = 0;
            table1.WidthPercentage = 80;

            var titleFont = new Font(Font.FontFamily.UNDEFINED, 24);
            var subTitleFont = new Font(Font.FontFamily.UNDEFINED, 16);

            PdfPCell cell11 = new PdfPCell();
            cell11.Colspan = 1;
            cell11.AddElement(new Paragraph("ABC Traders Receipt", titleFont));

            cell11.AddElement(new Paragraph("Thankyou for shoping at ABC traders,your order details are below", subTitleFont));


            cell11.VerticalAlignment = Element.ALIGN_LEFT;

            PdfPCell cell12 = new PdfPCell();


            cell12.VerticalAlignment = Element.ALIGN_CENTER;


            table1.AddCell(cell11);

            table1.AddCell(cell12);


            PdfPTable table2 = new PdfPTable(3);

            //One row added

            PdfPCell cell21 = new PdfPCell();

            cell21.AddElement(new Paragraph("Photo Type"));

            PdfPCell cell22 = new PdfPCell();

            cell22.AddElement(new Paragraph("No. of Copies"));

            PdfPCell cell23 = new PdfPCell();

            cell23.AddElement(new Paragraph("Amount"));


            table2.AddCell(cell21);

            table2.AddCell(cell22);

            table2.AddCell(cell23);


            //New Row Added

            PdfPCell cell31 = new PdfPCell();

            cell31.AddElement(new Paragraph("Safe"));

            cell31.FixedHeight = 300.0f;

            PdfPCell cell32 = new PdfPCell();

            cell32.AddElement(new Paragraph("2"));

            cell32.FixedHeight = 300.0f;

            PdfPCell cell33 = new PdfPCell();

            cell33.AddElement(new Paragraph("20.00 * " + "2" + " = " + (20 * Convert.ToInt32("2")) + ".00"));

            cell33.FixedHeight = 300.0f;



            table2.AddCell(cell31);

            table2.AddCell(cell32);

            table2.AddCell(cell33);


            PdfPCell cell2A = new PdfPCell(table2);

            cell2A.Colspan = 2;

            table1.AddCell(cell2A);

            PdfPCell cell41 = new PdfPCell();

            cell41.AddElement(new Paragraph("Name : " + "ABC"));

            cell41.AddElement(new Paragraph("Advance : " + "advance"));

            cell41.VerticalAlignment = Element.ALIGN_LEFT;

            PdfPCell cell42 = new PdfPCell();

            cell42.AddElement(new Paragraph("Customer ID : " + "011"));

            cell42.AddElement(new Paragraph("Balance : " + "3993"));

            cell42.VerticalAlignment = Element.ALIGN_RIGHT;


            table1.AddCell(cell41);

            table1.AddCell(cell42);


            doc.Add(table1);


            doc.Close();

            return null;
        }
    }
}



