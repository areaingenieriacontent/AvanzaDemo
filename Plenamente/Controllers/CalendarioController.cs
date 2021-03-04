using Plenamente.App_Tool;
using Plenamente.Models;
using Plenamente.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Plenamente.Controllers
{
    /// <summary>
    /// Controlador destinado a la administración del calendario.
    /// </summary>
    /// <remarks>
    /// Utiliza el paquete NUGET FullCalendar.MVC5 que implementa la libreria de javascript fullcalendar toda la documentación en la url: https://fullcalendar.io/ 
    /// </remarks>
    /// <include file='\Plenamente\Scripts\script-custom-calendar.js' path='[@name="script-custom-calendar"]'/>
    /// <seealso cref="System.Web.Mvc.Controller" />
    public class CalendarioController : Controller
    {
        /// <summary>
        /// Instancia del modelo de base de datos.
        /// </summary>
        private ApplicationDbContext db = new ApplicationDbContext();
        #region Index method
        /// <summary>  
        /// Método GET: Home/Index 
        /// Carga la vista de inicio que contiene el calendario.
        /// </summary>
        /// <returns>
        /// Retorna la lista de empresas cargadas en la vista.
        /// </returns>
        public ActionResult Index()
        {
            return View();
        }
        #endregion

        #region Get Calendar data method
        /// <summary>  
        /// Método GET: /Home/GetCalendarData  
        /// Obtiene la lista de eventos y la carga en la vista a traves de un método ajax.
        /// </summary>  
        /// <returns>
        /// Retorna la lista de eventos del calendario cargado en un JSON
        /// </returns>  
        public ActionResult GetCalendarData()
        {
            JsonResult result = new JsonResult();

            try
            {
                List<EventViewModel> data = LoadData();
                result = Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
            return result;
        }
        #endregion
        #region Helpers  
        #region Load Data  
        /// <summary>  
        /// Obtiene la lista de eventos que se quieren mostrar en el calendario
        /// </summary>  
        /// <returns>
        /// Un listado de eventos
        /// </returns>  
        private List<EventViewModel> LoadData()
        {
            List<EventViewModel> lst = new List<EventViewModel>();
            try
            {
                /*
                List<EventViewModel> cumplimientos =
                 db.Tb_ActiCumplimiento
                    .Where(a => a.Empr_Nit == AccountData.NitEmpresa && a.Usersplandetrabajo.Any(u => u.PlandeTrabajo != null))
                     .Select(a =>
                         new EventViewModel
                         {
                             Id = a.Acum_Id,
                             Description = "Cumplimiento",
                             Title = a.Acum_Desc,
                             Start = a.Acum_IniAct,
                             End = a.Acum_FinAct,
                             BackgroundColor = a.Finalizada ? "#FF1F17" : "#6CB52D",
                             BorderColor = a.Finalizada ? "#FF6963" : "#65ac1e",
                             EventRoute = "../ActividadCumplimiento/Details/" + a.Acum_Id
                         }).ToList();

                if (cumplimientos != null && cumplimientos.Count > 0)
                {
                    lst.AddRange(cumplimientos);
                }
                */
                var now = DateTime.Now;
                List<EventViewModel> planes =
                    db.Tb_ProgamacionTareas
                        .Where(a => a.ActiCumplimiento.Empr_Nit == AccountData.NitEmpresa
                                && a.Estado
                                && a.ActiCumplimiento.Usersplandetrabajo.Count > 0)
                        .Select(a =>
                            new EventViewModel
                            {
                                Id = a.Id,
                                Description = "Tarea programada",
                                Title = a.Descripcion,
                                Start = a.FechaHora,
                                // BackgroundColor = a.ActiCumplimiento.Acum_FinAct > now || a.Finalizada ? "#FF1F17" /*Verde*/: "#6CB52D" /*Rojo*/ ,
                                BackgroundColor =  a.Finalizada ? "#6CB52D" /*Rojo*/: "#FF1F17" /*Verde*/ ,
                               // BorderColor = a.ActiCumplimiento.Acum_FinAct > now || a.Finalizada ? "#FF6963" /*Verde*/: "#65ac1e" /*Rojo*/,
                                BorderColor =  a.Finalizada ? "#65ac1e" /*Rojo*/: "#FF6963" /*Verde*/,
                                
                                EventRoute = "../ActividadCumplimiento/Details?id=" + a.ActiCumplimiento_Id + "&idpt=" + a.Id
                            }).ToList();

                if (planes != null && planes.Count > 0)
                {
                    lst.AddRange(planes);
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
            return lst;
        }
        #endregion
        #endregion
    }
}
