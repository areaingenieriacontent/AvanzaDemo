using PagedList;
using Plenamente.Models;
using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace Plenamente.Areas.Administrador.Controllers
{
    public class EncuestasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Administrador/Encuestas
        //Se Agregan Los Parametros sortOrder, currentFilter, searchString, page, para la busqueda y paginacion de la encuesta.
        [Authorize(Roles = "Administrator")]
        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            //Se obtiene sortOrder para el orden de los datos
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            //De manera asendente o desendente
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            //Si el campo esta vacio, la pagina sera igual a 1
            if (searchString != null)
            {
                //La pagina empieza 1
                page = 1;
            }
            else
            {
                //Si el campo esta lleno la busqueda se hara el searchString 
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            //Se obtiene el parametro
            //Se realiza una consulta tipo Linq para obtener los datos de acuerdo a la empresa que este logeada 
            var userId = User.Identity.GetUserId();
            var UserCurrent = db.Users.Find(userId);
            var Empr_Nit = UserCurrent.Empr_Nit;
            var cargos = from s in db.Tb_Encuesta
                         where s.Empr_Nit == Empr_Nit
                         select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                //Se realiza la busqueda deacuerdo a la cadena de texto que se inserte en el input
                cargos = cargos.Where(s => s.Encu_Vence.ToString().Contains(searchString)
                                       || s.Encu_Vence.ToString().Contains(searchString));
            }
            switch (sortOrder)
            {
                //Organzia de manera decendente o asendente
                case "name_desc":
                    cargos = cargos.OrderByDescending(s => s.Encu_Vence.ToString());
                    break;
                default:  // Name ascending 
                    cargos = cargos.OrderBy(s => s.Encu_Vence.ToString());
                    break;
            }
            //Se muestran la cantidad de registros
            int pageSize = 20;
            int pageNumber = (page ?? 1);
            //Retorna la vista
            return View(cargos.ToPagedList(pageNumber, pageSize));
        }
        // GET: Administrador/Encuestas/Details/5
        //Metodo para la pagina de detalles
        [Authorize(Roles = "Administrator")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Encuesta encuesta = db.Tb_Encuesta.Find(id);
            if (encuesta == null)
            {
                return HttpNotFound();
            }
            return View(encuesta);
        }

        // GET: Administrador/Encuestas/Create
        //Metodo para invocarta la pagina de create
        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            ViewBag.Empr_Nit = new SelectList(db.Tb_Empresa, "Empr_Nit", "Empr_Nom");
            return View();
        }

        // POST: Administrador/Encuestas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        //Metodó guardar con parametro blindado
        public ActionResult Create([Bind(Include = "Encu_Id,Encu_Nombre,Encu_Creacion,Encu_Vence,Encu_Estado,Encu_Registro,Empr_Nit")] Encuesta encuesta)
        {
            //Si el modelo es valido guarda el registro
            if (ModelState.IsValid)
            {
                db.Tb_Encuesta.Add(encuesta);
                db.SaveChanges();
                GuardarPreguntas();
                return RedirectToAction("Index");
            }

            ViewBag.Empr_Nit = new SelectList(db.Tb_Empresa, "Empr_Nit", "Empr_Nom", encuesta.Empr_Nit);
            return View(encuesta);
        }

        // GET: Administrador/Encuestas/Edit/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Encuesta encuesta = db.Tb_Encuesta.Find(id);
            if (encuesta == null)
            {
                return HttpNotFound();
            }
            ViewBag.Empr_Nit = new SelectList(db.Tb_Empresa, "Empr_Nit", "Empr_Nom", encuesta.Empr_Nit);
            return View(encuesta);
        }

        // POST: Administrador/Encuestas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit([Bind(Include = "Encu_Id,Encu_Creacion,Encu_Vence,Encu_Estado,Encu_Registro,Empr_Nit")] Encuesta encuesta)
        {
            if (ModelState.IsValid)
            {
                db.Entry(encuesta).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Empr_Nit = new SelectList(db.Tb_Empresa, "Empr_Nit", "Empr_Nom", encuesta.Empr_Nit);
            return View(encuesta);
        }

        // GET: Administrador/Encuestas/Delete/5
        //Metodo para la pagina de eliminar
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Encuesta encuesta = db.Tb_Encuesta.Find(id);
            if (encuesta == null)
            {
                return HttpNotFound();
            }
            return View(encuesta);
        }

        // POST: Administrador/Encuestas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        //Metodo de elimincacion multi tabla en beta no funcional
        public ActionResult DeleteConfirmed(int id)
        {
            db.Database.ExecuteSqlCommand("DELETE ec FROM Encuestas ec LEFT JOIN  preguntas pt ON ec.Encu_Id = ec.Encu_Id LEFT JOIN respuestas rs ON pt.Preg_Id = pt.Preg_Id WHERE ec.Encu_Id = '"+id+"'");
            return RedirectToAction("Index");
        }
        //Query de insercion a las tabalas Preguntas y Respuestas de manera estatica.
        [Authorize(Roles = "Administrator")]
        public void GuardarPreguntas()
        {
            var maxEncuesta = db.Tb_Encuesta.Max(x => x.Encu_Id);

            var fechaActual = DateTime.Now;
            var fechaFinal = fechaActual.ToString("yyyy-MM-dd h:m:s");
            db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('Apellidos y Nombres Completos', '" + fechaFinal + "','" + maxEncuesta + "')");
            var maxPregunta8 = db.Tb_Pregunta.Max(x => x.Preg_Id);
            //Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Ingrese su Nombre y Apellidos','1','" + fechaFinal + "', '" + maxPregunta8 + "')");
            //Fin Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('Fecha de Diligenciamiento', '" + fechaFinal + "','" + maxEncuesta + "')");
            var maxPregunta9 = db.Tb_Pregunta.Max(x => x.Preg_Id);
            //Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Ingrese la Fecha','4','" + fechaFinal + "', '" + maxPregunta9 + "')");
            //Fin Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('Cargo u Ocupación', '" + fechaFinal + "','" + maxEncuesta + "')");
            var maxPregunta10 = db.Tb_Pregunta.Max(x => x.Preg_Id);
            //Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Ingre Su Fecha de Nacimiento','1','" + fechaFinal + "', '" + maxPregunta10 + "')");
            //Fin Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('Área de Trabajo', '" + fechaFinal + "','" + maxEncuesta + "')");
            var maxPregunta11 = db.Tb_Pregunta.Max(x => x.Preg_Id);
            //Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Ingre Su Área de trabajo','1','" + fechaFinal + "', '" + maxPregunta11 + "')");
            //Fin Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('Seleccione uno de los rangos a los que corresponde su edad', '" + fechaFinal + "','" + maxEncuesta + "')");
            var maxPregunta = db.Tb_Pregunta.Max(x => x.Preg_Id);
            ////Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('18-27','3','" + fechaFinal + "', '" + maxPregunta + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('28-37', '3','" + fechaFinal + "', '" + maxPregunta + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('38-47', '3' ,'" + fechaFinal + "', '" + maxPregunta + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('48 o Más', '3' ,'" + fechaFinal + "', '" + maxPregunta + "')");
            ////Fin Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('Seleccione su Estado Civil', '" + fechaFinal + "','" + maxEncuesta + "')");
            var maxPregunta1 = db.Tb_Pregunta.Max(x => x.Preg_Id);
            ////Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Soltero (A)', '3' , '" + fechaFinal + "', '" + maxPregunta1 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Casado (A)/ Unión Libre', '3' , '" + fechaFinal + "', '" + maxPregunta1 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Separado (A)/ Divorciado', '3' , '" + fechaFinal + "', '" + maxPregunta1 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Viudo (A)', '3' , '" + fechaFinal + "', '" + maxPregunta1 + "')");
            ////Fin Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('Genero', '" + fechaFinal + "','" + maxEncuesta + "')");
            var maxPregunta2 = db.Tb_Pregunta.Max(x => x.Preg_Id);
            ////Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Hombre', '3' , '" + fechaFinal + "', '" + maxPregunta2 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Mujer', '3' , '" + fechaFinal + "', '" + maxPregunta2 + "')");
            ////Fin Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('Fecha de Nacimiento', '" + fechaFinal + "','" + maxEncuesta + "')");
            var maxPregunta12 = db.Tb_Pregunta.Max(x => x.Preg_Id);
            //Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Ingre Su Fecha de Nacimiento','4','" + fechaFinal + "', '" + maxPregunta12 + "')");
            //Fin Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('Numero de Personas a Cargo', '" + fechaFinal + "','" + maxEncuesta + "')");
            var maxPregunta3 = db.Tb_Pregunta.Max(x => x.Preg_Id);
            ////Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Ninguna', '3' , '" + fechaFinal + "', '" + maxPregunta3 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('1 a 3 Personas', '3' , '" + fechaFinal + "', '" + maxPregunta3 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('4 a 6 Personas', '3' , '" + fechaFinal + "', '" + maxPregunta3 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Más de 6 Personas', '3' , '" + fechaFinal + "', '" + maxPregunta3 + "')");
            ////Fin Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('Numero de Hijos', '" + fechaFinal + "','" + maxEncuesta + "')");
            var maxPregunta4 = db.Tb_Pregunta.Max(x => x.Preg_Id);
            ////Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('No tiene Hijos', '3' , '" + fechaFinal + "', '" + maxPregunta4 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('1 a 3 Hijos', '3' , '" + fechaFinal + "', '" + maxPregunta4 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('4 a 6 Hijos', '3' , '" + fechaFinal + "', '" + maxPregunta4 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Más de 6 Hijos', '3' , '" + fechaFinal + "', '" + maxPregunta4 + "')");
            ////Fin Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('Seleccione su Nivel de Escolaridad', '" + fechaFinal + "','" + maxEncuesta + "')");
            var maxPregunta5 = db.Tb_Pregunta.Max(x => x.Preg_Id);
            ////Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Primaria', '3' , '" + fechaFinal + "', '" + maxPregunta5 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Secundaria', '3' , '" + fechaFinal + "', '" + maxPregunta5 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Técnico Tecnólogo', '3' , '" + fechaFinal + "', '" + maxPregunta5 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Estudiante Universitario', '3' , '" + fechaFinal + "', '" + maxPregunta5 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Profesional', '3' , '" + fechaFinal + "', '" + maxPregunta5 + "')");
            ////Fin Opcines
            db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('Profesión', '" + fechaFinal + "','" + maxEncuesta + "')");
            var maxPregunta13 = db.Tb_Pregunta.Max(x => x.Preg_Id);
            //Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Ingrese su Prefesion','1','" + fechaFinal + "', '" + maxPregunta13 + "')");
            //Fin Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('Tipo de Vivienda', '" + fechaFinal + "','" + maxEncuesta + "')");
            var maxPregunta6 = db.Tb_Pregunta.Max(x => x.Preg_Id);
            ////Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Propia', '3' , '" + fechaFinal + "', '" + maxPregunta6 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Arrendada', '3' , '" + fechaFinal + "', '" + maxPregunta6 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Familiar', '3' , '" + fechaFinal + "', '" + maxPregunta6 + "')");
            ////Fin Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('¿A qué estrato pertenece?', '" + fechaFinal + "','" + maxEncuesta + "')");
            var maxPregunta7 = db.Tb_Pregunta.Max(x => x.Preg_Id);
            ////Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('1', '3' , '" + fechaFinal + "', '" + maxPregunta7 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('2', '3' , '" + fechaFinal + "', '" + maxPregunta7 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('3', '3' , '" + fechaFinal + "', '" + maxPregunta7 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('4', '3' , '" + fechaFinal + "', '" + maxPregunta7 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('5', '3' ,'" + fechaFinal + "', '" + maxPregunta7 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('6', '3' ,'" + fechaFinal + "', '" + maxPregunta7 + "')");
            ////Fin Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('¿Con qué servicios cuenta su vivienda?', '" + fechaFinal + "','" + maxEncuesta + "')");
            var maxPregunta14 = db.Tb_Pregunta.Max(x => x.Preg_Id);
            //Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Agua', '2' , '" + fechaFinal + "', '" + maxPregunta14 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Luz', '2' , '" + fechaFinal + "', '" + maxPregunta14 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Gas Natural', '2' , '" + fechaFinal + "', '" + maxPregunta14 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Alcantarillado', '2' , '" + fechaFinal + "', '" + maxPregunta14 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Todas Las Anteriores', '2' ,'" + fechaFinal + "', '" + maxPregunta14 + "')");
            //Fin Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('¿En qué utiliza su tiempo libre?', '" + fechaFinal + "','" + maxEncuesta + "')");
            var maxPregunta15 = db.Tb_Pregunta.Max(x => x.Preg_Id);
            //Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Otro trabajo', '3' , '" + fechaFinal + "', '" + maxPregunta15 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Labores Domesticas', '3' , '" + fechaFinal + "', '" + maxPregunta15 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Recreación y Deporte', '3' , '" + fechaFinal + "', '" + maxPregunta15 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Estudio', '3' , '" + fechaFinal + "', '" + maxPregunta15 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Otros', '1' ,'" + fechaFinal + "', '" + maxPregunta15 + "')");
            //Fin Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('Promedio de Ingresos (S.M.L)', '" + fechaFinal + "','" + maxEncuesta + "')");
            var maxPregunta16 = db.Tb_Pregunta.Max(x => x.Preg_Id);
            //Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Menos de 1 Mínimo Legal (S.M.L.)', '3' , '" + fechaFinal + "', '" + maxPregunta16 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Entre 1 a 3 S.M.L.', '3' , '" + fechaFinal + "', '" + maxPregunta16 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Entre 3 a 6 S.M.L.', '3' , '" + fechaFinal + "', '" + maxPregunta16 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Más de 6 S.M.L', '3' , '" + fechaFinal + "', '" + maxPregunta16 + "')");
            //Fin Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('Cuántos Años Lleva Laborando en La Empresa ', '" + fechaFinal + "','" + maxEncuesta + "')");
            var maxPregunta17 = db.Tb_Pregunta.Max(x => x.Preg_Id);
            //Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Menos de 1 año', '3' , '" + fechaFinal + "', '" + maxPregunta17 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('De 1 a 5 años', '3' , '" + fechaFinal + "', '" + maxPregunta17 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('De 5 a 10 años', '3' , '" + fechaFinal + "', '" + maxPregunta17 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('De 11 a 15 años', '3' , '" + fechaFinal + "', '" + maxPregunta17 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Más de 15 años', '3' , '" + fechaFinal + "', '" + maxPregunta17 + "')");
            //Fin Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('Fecha de Ingreso a La Empresa', '" + fechaFinal + "','" + maxEncuesta + "')");
            var maxPregunta18 = db.Tb_Pregunta.Max(x => x.Preg_Id);
            //Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Ingre Fecha de Ingreso A La Empresa','4','" + fechaFinal + "', '" + maxPregunta18 + "')");
            //Fin Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('¿Tipo de Contratación?', '" + fechaFinal + "','" + maxEncuesta + "')");
            var maxPregunta19 = db.Tb_Pregunta.Max(x => x.Preg_Id);
            //Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Término Indefinido', '3' , '" + fechaFinal + "', '" + maxPregunta19 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Fijo', '3' , '" + fechaFinal + "', '" + maxPregunta19 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Por Termino de Labor (Obra o Labor)', '3' , '" + fechaFinal + "', '" + maxPregunta19 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Por Prestación de Servicios', '3' , '" + fechaFinal + "', '" + maxPregunta19 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Honorarios / Servicios Profesionales', '3' , '" + fechaFinal + "', '" + maxPregunta19 + "')");
            //Fin Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('Antigüedad en el Cargo Actual', '" + fechaFinal + "','" + maxEncuesta + "')");
            var maxPregunta20 = db.Tb_Pregunta.Max(x => x.Preg_Id);
            //Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Menos de 1 año', '3' , '" + fechaFinal + "', '" + maxPregunta20 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('De 1 a 5 años', '3' , '" + fechaFinal + "', '" + maxPregunta20 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('De 5 a 10 años', '3' , '" + fechaFinal + "', '" + maxPregunta20 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('De 11 a 15 años', '3' , '" + fechaFinal + "', '" + maxPregunta20 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Más de 15 años', '3' , '" + fechaFinal + "', '" + maxPregunta20 + "')");
            //Fin Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('¿En qué actividades de Salud ha Participado Usted en la Empresa?', '" + fechaFinal + "','" + maxEncuesta + "')");
            var maxPregunta21 = db.Tb_Pregunta.Max(x => x.Preg_Id);
            //Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Vacunación' , '3' , '" + fechaFinal + "', '" + maxPregunta21 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Exámenes de laboratorio y otros', '3' , '" + fechaFinal + "', '" + maxPregunta21 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Exámenes médicos anuales', '3' , '" + fechaFinal + "', '" + maxPregunta21 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Ninguna', '3' , '" + fechaFinal + "', '" + maxPregunta21 + "')");
            //Fin Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('¿Le han Diagnosticado Alguna Enfermedad?', '" + fechaFinal + "','" + maxEncuesta + "')");
            var maxPregunta22 = db.Tb_Pregunta.Max(x => x.Preg_Id);
            //Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Si' , '3' , '" + fechaFinal + "', '" + maxPregunta22 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('No', '3' , '" + fechaFinal + "', '" + maxPregunta22 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('¿Cual?', '1' , '" + fechaFinal + "', '" + maxPregunta22 + "')");
            //Fin Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('¿Consume Bebidas Alcohólicas?', '" + fechaFinal + "','" + maxEncuesta + "')");
            var maxPregunta23 = db.Tb_Pregunta.Max(x => x.Preg_Id);
            //Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Semanal' , '3' , '" + fechaFinal + "', '" + maxPregunta22 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Quincenal', '3' , '" + fechaFinal + "', '" + maxPregunta22 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Mensual', '3' , '" + fechaFinal + "', '" + maxPregunta22 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Ocasional', '3' , '" + fechaFinal + "', '" + maxPregunta22 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Nunca he consumido bebidas alcohólicas', '3' , '" + fechaFinal + "', '" + maxPregunta22 + "')");
            //Fin Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('¿Practica Algún Deporte?', '" + fechaFinal + "','" + maxEncuesta + "')");
            var maxPregunta24 = db.Tb_Pregunta.Max(x => x.Preg_Id);
            //Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Si' , '3' , '" + fechaFinal + "', '" + maxPregunta24 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('No', '3' , '" + fechaFinal + "', '" + maxPregunta24 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('¿Cual?', '1' , '" + fechaFinal + "', '" + maxPregunta24 + "')");
            //Fin Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('¿Fuma? Si su Respuesta es SI, Explique con qué Frecuencia', '" + fechaFinal + "','" + maxEncuesta + "')");
            var maxPregunta25 = db.Tb_Pregunta.Max(x => x.Preg_Id);
            //Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Si' , '3' , '" + fechaFinal + "', '" + maxPregunta25 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('No', '3' , '" + fechaFinal + "', '" + maxPregunta25 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('¿Cual?', '1' , '" + fechaFinal + "', '" + maxPregunta25 + "')");
            //Fin Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('¿Cuáles de las Siguientes Molestias ha Sentido con Frecuencia en los Últimos Seis (6) Meses?', '" + fechaFinal + "','" + maxEncuesta + "')");
            var maxPregunta26 = db.Tb_Pregunta.Max(x => x.Preg_Id);
            //Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Dolor de cabeza', '2' , '" + fechaFinal + "', '" + maxPregunta26 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Dolor de cuello, espalda y cintura' , '2' , '" + fechaFinal + "', '" + maxPregunta26 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Dolores Musculares', '2' , '" + fechaFinal + "', '" + maxPregunta26 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Dificultad para realizar algún movimiento', '2' , '" + fechaFinal + "', '" + maxPregunta26 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Tos frecuente', '2' , '" + fechaFinal + "', '" + maxPregunta26 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Dificultad Respiratoria', '2' , '" + fechaFinal + "', '" + maxPregunta26 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Gastritis, Ulcera', '2' , '" + fechaFinal + "', '" + maxPregunta26 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Otras alteraciones del funcionamiento digestivo', '2' , '" + fechaFinal + "', '" + maxPregunta26 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Alteraciones del sueño (insomnio, somnolencia)', '2' , '" + fechaFinal + "', '" + maxPregunta26 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Mal genio', '2' , '" + fechaFinal + "', '" + maxPregunta26 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Cansancio mental', '2' , '" + fechaFinal + "', '" + maxPregunta26 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Dolor en el pecho', '2' , '" + fechaFinal + "', '" + maxPregunta26 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Cambios visuales', '2' , '" + fechaFinal + "', '" + maxPregunta26 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Cansancio, fatiga, ardor o disconfort visual', '2' , '" + fechaFinal + "', '" + maxPregunta26 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Pitos o ruidos continuos o intermitentes en los oídos', '2' , '" + fechaFinal + "', '" + maxPregunta26 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Dificultad para oír', '2' , '" + fechaFinal + "', '" + maxPregunta26 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Otros', '2' , '" + fechaFinal + "', '" + maxPregunta26 + "')");
            //Fin Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('¿Conoce los riesgos a los que está expuesto en su lugar de trabajo?', '" + fechaFinal + "','" + maxEncuesta + "')");
            var maxPregunta27 = db.Tb_Pregunta.Max(x => x.Preg_Id);
            //Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Si' , '3' , '" + fechaFinal + "', '" + maxPregunta27 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('No', '3' , '" + fechaFinal + "', '" + maxPregunta27 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Otro', '1' , '" + fechaFinal + "', '" + maxPregunta27 + "')");
            //Fin Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('¿Ha recibido capacitación sobre el manejo de los riesgos a los que está expuesto?', '" + fechaFinal + "','" + maxEncuesta + "')");
            var maxPregunta28 = db.Tb_Pregunta.Max(x => x.Preg_Id);
            //Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Si' , '3' , '" + fechaFinal + "', '" + maxPregunta28 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('No', '3' , '" + fechaFinal + "', '" + maxPregunta28 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Otro', '1' , '" + fechaFinal + "', '" + maxPregunta28 + "')");
            //Fin Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('¿Considera que la iluminación de su puesto de trabajo es adecuada?', '" + fechaFinal + "','" + maxEncuesta + "')");
            var maxPregunta29 = db.Tb_Pregunta.Max(x => x.Preg_Id);
            //Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Si' , '3' , '" + fechaFinal + "', '" + maxPregunta29 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('No', '3' , '" + fechaFinal + "', '" + maxPregunta29 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Otro', '1' , '" + fechaFinal + "', '" + maxPregunta29 + "')");
            //Fin Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('¿La temperatura de su sitio de trabajo le ocasiona molestias?', '" + fechaFinal + "','" + maxEncuesta + "')");
            var maxPregunta30 = db.Tb_Pregunta.Max(x => x.Preg_Id);
            //Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Si' , '3' , '" + fechaFinal + "', '" + maxPregunta30 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('No', '3' , '" + fechaFinal + "', '" + maxPregunta30 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Otro', '1' , '" + fechaFinal + "', '" + maxPregunta30 + "')");
            //Fin Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('¿Considera que los pisos, techos, paredes, escaleras, presentan riesgo para su salud?', '" + fechaFinal + "','" + maxEncuesta + "')");
            var maxPregunta31 = db.Tb_Pregunta.Max(x => x.Preg_Id);
            //Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Si' , '3' , '" + fechaFinal + "', '" + maxPregunta31 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('No', '3' , '" + fechaFinal + "', '" + maxPregunta31 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Otro', '1' , '" + fechaFinal + "', '" + maxPregunta31 + "')");
            //Fin Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('¿Existen cables sin entubar, empalmes defectuosos, tomas eléctricas sobrecargadas?', '" + fechaFinal + "','" + maxEncuesta + "')");
            var maxPregunta32 = db.Tb_Pregunta.Max(x => x.Preg_Id);
            //Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Si' , '3' , '" + fechaFinal + "', '" + maxPregunta32 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('No', '3' , '" + fechaFinal + "', '" + maxPregunta32 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Otro', '1' , '" + fechaFinal + "', '" + maxPregunta32 + "')");
            //Fin Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('¿Los sitios destinados para el almacenamiento son suficientes? (archivo, materiales y herramientas)', '" + fechaFinal + "','" + maxEncuesta + "')");
            var maxPregunta33 = db.Tb_Pregunta.Max(x => x.Preg_Id);
            //Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Si' , '3' , '" + fechaFinal + "', '" + maxPregunta33 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('No', '3' , '" + fechaFinal + "', '" + maxPregunta33 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Otro', '1' , '" + fechaFinal + "', '" + maxPregunta33 + "')");
            //Fin Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('¿Las tareas que desarrolla le exigen realizar movimientos repetitivos?', '" + fechaFinal + "','" + maxEncuesta + "')");
            var maxPregunta34 = db.Tb_Pregunta.Max(x => x.Preg_Id);
            //Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Si' , '3' , '" + fechaFinal + "', '" + maxPregunta34 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('No', '3' , '" + fechaFinal + "', '" + maxPregunta34 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Otro', '1' , '" + fechaFinal + "', '" + maxPregunta34 + "')");
            //Fin Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('¿La altura de la superficie de trabajo es la adecuada a su estatura, la silla y la labor que realiza?', '" + fechaFinal + "','" + maxEncuesta + "')");
            var maxPregunta35 = db.Tb_Pregunta.Max(x => x.Preg_Id);
            //Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Si' , '3' , '" + fechaFinal + "', '" + maxPregunta35 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('No', '3' , '" + fechaFinal + "', '" + maxPregunta35 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Otro', '1' , '" + fechaFinal + "', '" + maxPregunta35 + "')");
            //Fin Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('¿Tiene espacio suficiente para variar la posición de las piernas y rodillas?', '" + fechaFinal + "','" + maxEncuesta + "')");
            var maxPregunta36 = db.Tb_Pregunta.Max(x => x.Preg_Id);
            //Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Si' , '3' , '" + fechaFinal + "', '" + maxPregunta36 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('No', '3' , '" + fechaFinal + "', '" + maxPregunta36 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Otro', '1' , '" + fechaFinal + "', '" + maxPregunta36 + "')");
            //Fin Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('¿Su trabajo le exige mantenerse frente a la pantalla del computador más del 50% de la jornada?', '" + fechaFinal + "','" + maxEncuesta + "')");
            var maxPregunta37 = db.Tb_Pregunta.Max(x => x.Preg_Id);
            //Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Si' , '3' , '" + fechaFinal + "', '" + maxPregunta37 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('No', '3' , '" + fechaFinal + "', '" + maxPregunta37 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Otro', '1' , '" + fechaFinal + "', '" + maxPregunta37 + "')");
            //Fin Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('¿Al finalizar la jornada laboral, el cansancio que se siente podría calificarse normal?', '" + fechaFinal + "','" + maxEncuesta + "')");
            var maxPregunta38 = db.Tb_Pregunta.Max(x => x.Preg_Id);
            //Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Si' , '3' , '" + fechaFinal + "', '" + maxPregunta38 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('No', '3' , '" + fechaFinal + "', '" + maxPregunta38 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Otro', '1' , '" + fechaFinal + "', '" + maxPregunta38 + "')");
            //Fin Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('¿Considera adecuada la distribución del horario de trabajo, de los turnos, de las horas de descanso, horas extras y pausas?', '" + fechaFinal + "','" + maxEncuesta + "')");
            var maxPregunta39 = db.Tb_Pregunta.Max(x => x.Preg_Id);
            //Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Si' , '3' , '" + fechaFinal + "', '" + maxPregunta39 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('No', '3' , '" + fechaFinal + "', '" + maxPregunta39 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Otro', '1' , '" + fechaFinal + "', '" + maxPregunta39 + "')");
            //Fin Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('¿El trabajo que desempeña le permite aplicar sus habilidades y conocimientos?', '" + fechaFinal + "','" + maxEncuesta + "')");
            var maxPregunta40 = db.Tb_Pregunta.Max(x => x.Preg_Id);
            //Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Si' , '3' , '" + fechaFinal + "', '" + maxPregunta40 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('No', '3' , '" + fechaFinal + "', '" + maxPregunta40 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Otro', '1' , '" + fechaFinal + "', '" + maxPregunta40 + "')");
            //Fin Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('¿La empresa cuenta con agua potable?', '" + fechaFinal + "','" + maxEncuesta + "')");
            var maxPregunta41 = db.Tb_Pregunta.Max(x => x.Preg_Id);
            //Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Si' , '3' , '" + fechaFinal + "', '" + maxPregunta41 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('No', '3' , '" + fechaFinal + "', '" + maxPregunta41 + "')");
            //Fin Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('¿Existe buen manejo de basuras y desechos?', '" + fechaFinal + "','" + maxEncuesta + "')");
            var maxPregunta42 = db.Tb_Pregunta.Max(x => x.Preg_Id);
            //Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Si' , '3' , '" + fechaFinal + "', '" + maxPregunta42 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('No', '3' , '" + fechaFinal + "', '" + maxPregunta42 + "')");
            //Fin Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('¿Las máquinas y herramientas que utiliza en el desempeño de su labor producen vibración?', '" + fechaFinal + "','" + maxEncuesta + "')");
            var maxPregunta43 = db.Tb_Pregunta.Max(x => x.Preg_Id);
            //Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Si' , '3' , '" + fechaFinal + "', '" + maxPregunta43 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('No', '3' , '" + fechaFinal + "', '" + maxPregunta43 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('No Aplica', '3' , '" + fechaFinal + "', '" + maxPregunta43 + "')");
            //Fin Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('¿Su trabajo lo realiza al aire libre o a la intemperie?', '" + fechaFinal + "','" + maxEncuesta + "')");
            var maxPregunta44 = db.Tb_Pregunta.Max(x => x.Preg_Id);
            //Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Siempre' , '3' , '" + fechaFinal + "', '" + maxPregunta44 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Nunca', '3' , '" + fechaFinal + "', '" + maxPregunta44 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('A Veces', '3' , '" + fechaFinal + "', '" + maxPregunta44 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Otro:', '1' , '" + fechaFinal + "', '" + maxPregunta44 + "')");
            //Fin Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('¿En el sitio de trabajo manipula o está en contacto con productos químicos? ­ En caso de responder SI indicar ¿cuáles?','" + fechaFinal + "','" + maxEncuesta + "')");
            var maxPregunta45 = db.Tb_Pregunta.Max(x => x.Preg_Id);
            //Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Si' , '3' , '" + fechaFinal + "', '" + maxPregunta45 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('No', '3' , '" + fechaFinal + "', '" + maxPregunta45 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Cuales', '1' , '" + fechaFinal + "', '" + maxPregunta45 + "')");
            //Fin Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('¿Permanece en una misma posición (sentado o de pie) durante más del 60% de la jornada de trabajo?', '" + fechaFinal + "','" + maxEncuesta + "')");
            var maxPregunta46 = db.Tb_Pregunta.Max(x => x.Preg_Id);
            //Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Si' , '3' , '" + fechaFinal + "', '" + maxPregunta46 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('No', '3' , '" + fechaFinal + "', '" + maxPregunta46 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Otro', '1' , '" + fechaFinal + "', '" + maxPregunta46 + "')");
            //Fin Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('¿Su labor le exige levantar y transportar cargas? ¿cuáles? ', '" + fechaFinal + "','" + maxEncuesta + "')");
            var maxPregunta47 = db.Tb_Pregunta.Max(x => x.Preg_Id);
            //Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Si' , '3' , '" + fechaFinal + "', '" + maxPregunta47 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('No', '3' , '" + fechaFinal + "', '" + maxPregunta47 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('¿Cuales?', '1' , '" + fechaFinal + "', '" + maxPregunta47 + "')");
            //Fin Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('¿En su puesto de trabajo necesita utilizar elementos de protección personal? ', '" + fechaFinal + "','" + maxEncuesta + "')");
            var maxPregunta48 = db.Tb_Pregunta.Max(x => x.Preg_Id);
            //Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Si' , '3' , '" + fechaFinal + "', '" + maxPregunta48 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('No', '3' , '" + fechaFinal + "', '" + maxPregunta48 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('¿Cuales?', '1' , '" + fechaFinal + "', '" + maxPregunta48 + "')");
            //Fin Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('¿Su labor le exige levantar y transportar cargas? ¿cuáles? ', '" + fechaFinal + "','" + maxEncuesta + "')");
            var maxPregunta49 = db.Tb_Pregunta.Max(x => x.Preg_Id);
            //Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Si' , '3' , '" + fechaFinal + "', '" + maxPregunta49 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('No', '3' , '" + fechaFinal + "', '" + maxPregunta49 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('¿Cuales?', '1' , '" + fechaFinal + "', '" + maxPregunta49 + "')");
            //Fin Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('¿En su puesto de trabajo necesita utilizar elementos de protección personal? ', '" + fechaFinal + "','" + maxEncuesta + "')");
            var maxPregunta50 = db.Tb_Pregunta.Max(x => x.Preg_Id);
            //Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Si' , '3' , '" + fechaFinal + "', '" + maxPregunta50 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('No', '3' , '" + fechaFinal + "', '" + maxPregunta50 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('¿Cuales?', '1' , '" + fechaFinal + "', '" + maxPregunta50 + "')");
            //Fin Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Preguntas (Preg_Titulo, Preg_Registro, Encu_Id) VALUES ('¿Consentimiento Informado/ Ley 1581 de 2012: de protección de datos personales ­ ¿Acepta poner a disposición de la Empresa la presente información suministrada? * ', '" + fechaFinal + "','" + maxEncuesta + "')");
            var maxPregunta51 = db.Tb_Pregunta.Max(x => x.Preg_Id);
            //Opciones
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('Si' , '3' , '" + fechaFinal + "', '" + maxPregunta51 + "')");
            db.Database.ExecuteSqlCommand("INSERT INTO Respuestas (Resp_Nom, Resp_Tipo, Resp_Registro, Preg_Id) VALUES ('No', '3' , '" + fechaFinal + "', '" + maxPregunta51 + "')");            
            //Fin Opciones


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
