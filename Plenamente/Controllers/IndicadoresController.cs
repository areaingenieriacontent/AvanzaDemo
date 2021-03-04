using Plenamente.App_Tool;
using Plenamente.Models;
using Plenamente.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;

namespace Plenamente.Controllers
{
    /// <summary>
    /// Controlador destinado a la administración de los indicadores o reportes presentes en la página de inicio.
    /// </summary>
    /// <remarks>
    /// Utiliza la libreria de javascript chartjs toda la documentación en la url: https://www.chartjs.org
    /// </remarks>
    /// <include file='..\Scripts\chartjs\script-custom-chart.js' path='..[@name="script-custom-chart"]'/>
    /// <seealso cref="System.Web.Mvc.Controller" />
    [Authorize]
    public class IndicadoresController : Controller
    {
        /// <summary>
        /// Instancia de la base de datos.
        /// </summary>
        private readonly ApplicationDbContext db = new ApplicationDbContext();
        /// <summary>  
        /// Método GET: Indicadores/Index 
        /// Carga la vista de inicio.
        /// </summary>
        /// <returns>
        /// Retorna la vista de inicio.
        /// </returns>
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// Método POST: Indicadores/PromedioAutoevaluaciones
        /// Carga los datos necesarios para llenar el reporte de promedio de autoevaluaciones por ciclos.
        /// </summary>
        /// <returns>
        /// Retorna los datos necesarios para llenar el reporte de promedio de autoevaluaciones por ciclos.
        /// </returns>
        [HttpPost]
        [Authorize]
        public JsonResult PromedioAutoevaluaciones()
        {
            TipoEmpresa tipoEmpresa = new TipoEmpresa();
            if (AccountData.NitEmpresa != 0)
            {
                Empresa empresa = db.Tb_Empresa.Find(AccountData.NitEmpresa);
                tipoEmpresa = empresa.TipoEmpresa;
                if (empresa.Empr_Ttrabaja > 0 && (tipoEmpresa == null || tipoEmpresa.Categoria < 3))
                {
                    tipoEmpresa = db.Tb_TipoEmpresa.FirstOrDefault(t => t.RangoMinimoTrabajadores <= empresa.Empr_Ttrabaja && t.RangoMaximoTrabajadores >= empresa.Empr_Ttrabaja);
                }
            }
            AutoEvaluacion evaluacion =
                db.Tb_AutoEvaluacion
                    .Where(a => a.Empr_Nit == AccountData.NitEmpresa && a.Cumplimientos.Count > 0 && a.Finalizada)
                    .OrderByDescending(a => a.Auev_Inicio)
                    .FirstOrDefault();
            decimal[] lst = new decimal[0];
            string[] labels = new string[0];
            if (evaluacion != null)
            {
                var values =
                   db.Tb_ItemEstandar
                          .Where(ie => tipoEmpresa.Categoria == 0 || (ie.Categoria <= tipoEmpresa.Categoria && ie.CategoriaExcepcion != tipoEmpresa.Categoria && ie.CategoriaExcepcion != tipoEmpresa.Categoria))
                          .GroupBy(a => a.Estandar.Criterio.CicloPHVA).Select(a => new { key = a.Key.Id, value = (decimal)a.Count(), name = a.Key.Nombre })
                          .ToArray();
                labels = values.Select(v => v.name).ToArray();
                var temp =
                      db.Tb_Cumplimiento
                          .Where(a => a.Auev_Id == evaluacion.Auev_Id && (a.Cump_Cumple || a.Cump_Justifica))
                          .GroupBy(a => a.ItemEstandar.Estandar.Criterio.CicloPHVA).Select(a => new { key = a.Key.Id, value = (decimal)a.Where(e => e.Cump_Cumple || e.Cump_Justifica).Count() })
                          .ToArray();
                lst = new decimal[values.Length];
                for (int i = 0; i < values.Length; i++)
                {
                    var val = temp.FirstOrDefault(v => v.key == values[i].key);
                    if (val != null)
                    {
                        lst[i] = Decimal.Round((val.value * 100 / values[i].value),1);
                    }
                }
            }
            ChartDataViewModel datos =
               new ChartDataViewModel
               {
                   title = "Medición del ciclo PHVA",
                   labels = labels,
                   datasets =
                       new List<ChartDatasetsViewModel>{
                            new ChartDatasetsViewModel
                            {
                                label = "Avance porcentual %",
                                data = lst,
                                fill = false,
                                borderWidth = 1
                            }},
               };
            return Json(datos, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Método POST: Indicadores/AvanceAutoevaluaciones
        /// Carga los datos necesarios para llenar el reporte de avance planificación de tareas.
        /// </summary>
        /// <returns>
        /// Retorna los datos necesarios para llenar el reporte de avance planificación de tareas.
        /// </returns>
        [HttpPost]
        [Authorize]
        public JsonResult AvanceAutoevaluaciones()
        {
            List<ActiCumplimiento> lst = new List<ActiCumplimiento>();
            try
            {
                var cumplimientos =
                    db.Tb_ActiCumplimiento.Where(a => a.Empr_Nit == AccountData.NitEmpresa && a.Usersplandetrabajo.Any(u => u.PlandeTrabajo != null)).ToList();
                if (cumplimientos != null && cumplimientos.Count > 0)
                {
                    lst.AddRange(cumplimientos);
                }

                var ci = new CultureInfo("es-CO");
                ChartDataViewModel datos =
                  new ChartDataViewModel
                  {
                      title = "Alcance del plan de trabajo anual",
                      labels = lst.Select(a => a.Acum_IniAct.ToString("MMMM", ci)).Distinct().ToArray(),
                      datasets =
                          new List<ChartDatasetsViewModel>{
                            new ChartDatasetsViewModel
                            {
                                label = "Planeadas",
                                data = lst.GroupBy(a => a.Acum_IniAct.ToString("MMMM", ci)).Select(a => a.Count()).ToArray(),
                                fill = false,
                                borderWidth = 1
                            },
                            new ChartDatasetsViewModel
                            {
                                label = "En ejecución",
                                data = lst.Where(a => !a.Finalizada).GroupBy(a => a.Acum_IniAct.ToString("MMMM", ci)).Select(a => a.Count()).ToArray(),
                                fill = false,
                                borderWidth = 1
                            }},
                  };
                return Json(datos, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex, JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>
        /// Método POST: Indicadores/UltimaAutoevaluacion
        /// Carga los datos necesarios para llenar el reporte porcentaje de avance última autoevaluaciones.
        /// </summary>
        /// <returns>
        /// Retorna los datos necesarios para llenar el reporte porcentaje de avance última autoevaluaciones.
        /// </returns>
        [HttpPost]
        [Authorize]
        public JsonResult UltimaAutoevaluacion()
        {
            TipoEmpresa tipoEmpresa = new TipoEmpresa();
            if (AccountData.NitEmpresa != 0)
            {
                Empresa empresa = db.Tb_Empresa.Find(AccountData.NitEmpresa);
                tipoEmpresa = empresa.TipoEmpresa;
                if (empresa.Empr_Ttrabaja > 0 && (tipoEmpresa == null || tipoEmpresa.Categoria < 3))
                {
                    tipoEmpresa = db.Tb_TipoEmpresa.FirstOrDefault(t => t.RangoMinimoTrabajadores <= empresa.Empr_Ttrabaja && t.RangoMaximoTrabajadores >= empresa.Empr_Ttrabaja);
                }
            }
            decimal total =
                db.Tb_ItemEstandar
                    .Where(ie => tipoEmpresa.Categoria == 0 || (ie.Categoria <= tipoEmpresa.Categoria && ie.CategoriaExcepcion != tipoEmpresa.Categoria && ie.CategoriaExcepcion != tipoEmpresa.Categoria)).Count();
            decimal terminadas = 0;
            if (AccountData.NitEmpresa > 0)
            {
                AutoEvaluacion evaluacion =
                    db.Tb_AutoEvaluacion
                        .Where(a => a.Empr_Nit == AccountData.NitEmpresa && a.Cumplimientos.Count > 0 && a.Finalizada)
                        .OrderByDescending(a => a.Auev_Inicio)
                        .FirstOrDefault();

                if (evaluacion != null)
                {
                    terminadas =
                     db.Tb_Cumplimiento
                       .Count(a => a.Auev_Id == evaluacion.Auev_Id && (a.Cump_Cumple || a.Cump_Justifica));
                }
            }

            ChartDataViewModel datos =
              new ChartDataViewModel
              {
                  title = "Cumplimiento SG-SST",
                  labels = new string[2] { "Cumplido", "No cumplido" },
                  datasets =
                  new List<ChartDatasetsViewModel>{
                      new ChartDatasetsViewModel{
                          label = "Estado actividades %",
                          data = new string[2]{ String.Format("{0:0}", Convert.ToInt32((terminadas * 100) / total)) , String.Format("{0:0}", Convert.ToInt32(((total - terminadas) * 100) / total)) },
                          fill = true,
                          borderWidth = 1,
                          backgroundColor = new string[2] { "#6DB52D", "#AE2429" },
                          borderColor = new string[2] { "#6DB52D", "#AE2429" }
                      }}
              };
            return Json(datos, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Modelo que implementa las propiedades necesarias para cargar los datos de los reportes.
        /// </summary>
        private struct ChartDataViewModel
        {
            /// <summary>
            /// Obtiene o llena el título del reporte.
            /// </summary>
            /// <value>
            /// El título del reporte.
            /// </value>
            public string title { get; set; }
            /// <summary>
            /// Obtiene o llena la etiqueta.
            /// </summary>
            /// <value>
            /// La etiqueta del reporte.
            /// </value>
            public string[] labels { get; set; }
            /// <summary>
            /// Obtiene o llena los datos a mostrar en el reporte.
            /// </summary>
            /// <value>
            /// Los datos a mostrar en el reporte.
            /// </value>
            public List<ChartDatasetsViewModel> datasets { get; set; }
        };
        /// <summary>
        /// Modelo que implementa las propiedades necesarias para cargar los datasets de los reportes.
        /// </summary>
        private struct ChartDatasetsViewModel
        {
            /// <summary>
            /// Obtiene o llena la etiqueta.
            /// </summary>
            /// <value>
            /// La etiqueta del reporte.
            /// </value>
            public string label { get; set; }
            /// <summary>
            /// Obtiene o llena los datos a mostrar.
            /// </summary>
            /// <value>
            /// Los datos a mostrar en el reporte.
            /// </value>
            public object data { get; set; }
            /// <summary>
            /// Obtiene o llena el color de fondo.
            /// </summary>
            /// <value>
            /// El color de fondo del reporte.
            /// </value>
            public string[] backgroundColor { get; set; }
            /// <summary>
            /// Obtiene o llena el color del borde.
            /// </summary>
            /// <value>
            /// El color del borde del reporte.
            /// </value>
            public string[] borderColor { get; set; }
            /// <summary>
            /// Obtiene o llena el tamaño del borde.
            /// </summary>
            /// <value>
            /// El tamaño del borde del reporte.
            /// </value>
            public short borderWidth { get; set; }
            /// <summary>
            /// Obtiene o llena el valor indicando si <see cref="ChartDatasetsViewModel"/> es llenado.
            /// </summary>
            /// <value>
            ///   <c>Verdadero</c> es llenado; de otra forma, <c>Falso</c>.
            /// </value>
            public bool fill { get; set; }
        };
    }
}