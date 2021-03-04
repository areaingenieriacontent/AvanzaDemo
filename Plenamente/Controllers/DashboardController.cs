using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using Plenamente.App_Tool;
using Plenamente.Models;
using Plenamente.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection.Emit;
using System.Web.Mvc;


namespace Plenamente.Controllers
{
    public class DashboardController : Controller
    {
        public int RecomendacionesHogar, DistanciaminetoFisico, Transporte, Guantes, Tabocas, LavadoManos, UsuariosMatriculados;
        public float PorcentajeRecomendacionesHogar, PorcentajeDistanciaminetoFisico, PorcentajeTransporte, PorcentajeGuantes, PorcentajeTapabocas, PorcentajeLavadoManos, PorcentajeTotal;
        /// <summary>
        /// Instancia de la base de datos.
        /// </summary>
        private readonly ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index(int Nitempresa, int location)
        {

            var ConsultaImagenLavadoManos = db.Tb_DocsEvidencia.Where(x => x.Empr_Nit == Nitempresa && x.Tdca_id == 3);
            var ImagenLavadoManos = "";
            if (ConsultaImagenLavadoManos.Count() == 0)
            {
                ImagenLavadoManos = "puestolavado.jpg";
            }
            else
            {
                ImagenLavadoManos = ConsultaImagenLavadoManos.ToList().LastOrDefault().Devide_Archivo;
            }
            var ConsultaImageProteccionPersonas = db.Tb_DocsEvidencia.Where(x => x.Empr_Nit == Nitempresa && x.Tdca_id == 4);
            var ImagenProteccionPersonal = "";
            if (ConsultaImageProteccionPersonas.Count() == 0)
            {
                ImagenProteccionPersonal = "elementos.jpg";
            }
            else
            {
                ImagenProteccionPersonal = ConsultaImageProteccionPersonas.ToList().LastOrDefault().Devide_Archivo;
            }

            var ConsultaDistanciamientoFisico = db.Tb_DocsEvidencia.Where(x => x.Empr_Nit == Nitempresa && x.Tdca_id == 5);
            var ImagenDistanciamientoFisico = "";
            if (ConsultaDistanciamientoFisico.Count() == 0)
            {
                ImagenDistanciamientoFisico = "Distancia.jpg";
            }
            else
            {
                ImagenDistanciamientoFisico = ConsultaDistanciamientoFisico.ToList().LastOrDefault().Devide_Archivo;
            }

            var ConsultaLavadoManos = db.Tb_DocsEvidencia.Where(x => x.Empr_Nit == Nitempresa && x.Tdca_id == 6);
            var ImagenLavadoManos2 = "";
            if (ConsultaLavadoManos.Count() == 0)
            {
                ImagenLavadoManos2 = "lavadomanos.png";
            }
            else
            {
                ImagenLavadoManos2 = ConsultaLavadoManos.ToList().LastOrDefault().Devide_Archivo;
            }

            var ConsultaPDFProtocolos = db.Tb_DocsEvidencia.Where(x => x.Empr_Nit == Nitempresa && x.Tdca_id == 8);
            var PDFProtocolos = "";
            if (ConsultaPDFProtocolos.Count() == 0)
            {
                PDFProtocolos = "";
            }
            else
            {
                PDFProtocolos = ConsultaPDFProtocolos.ToList().LastOrDefault().Devide_Archivo;
            }

            var ConsultaPDFCertificacion = db.Tb_DocsEvidencia.Where(x => x.Empr_Nit == Nitempresa && x.Tdca_id == 9);
            var PDFCertificacion = "";
            if (ConsultaPDFCertificacion.Count() == 0)
            {
                PDFCertificacion = "";
            }
            else
            {
                PDFCertificacion = ConsultaPDFCertificacion.ToList().LastOrDefault().Devide_Archivo;
            }
            var ConsultaImagenPLanMovilidad = db.Tb_DocsEvidencia.Where(x => x.Empr_Nit == Nitempresa && x.Tdca_id == 10);
            var ImagenPlanMovilidad = "";
            if (ConsultaImagenPLanMovilidad.Count() == 0)
            {
                ImagenPlanMovilidad = "planmovi.png";
            }
            else
            {
                ImagenPlanMovilidad = ConsultaImagenPLanMovilidad.ToList().LastOrDefault().Devide_Archivo;
            }
            var ConsultaImagenProtocoloAcceso = db.Tb_DocsEvidencia.Where(x => x.Empr_Nit == Nitempresa && x.Tdca_id == 11);
            var ImagenProtocoloAcceso = "";
            if (ConsultaPDFCertificacion.Count() == 0)
            {
                ImagenProtocoloAcceso = "control.jpg";
            }
            else
            {
                ImagenProtocoloAcceso = ConsultaImagenProtocoloAcceso.ToList().LastOrDefault().Devide_Archivo;
            }
            ViewData["ImagenLavadoManos"] = ImagenLavadoManos.ToString();
            ViewData["ImagenProteccionPersonal"] = ImagenProteccionPersonal.ToString();
            ViewData["ImagenDistanciamientoFisico"] = ImagenDistanciamientoFisico.ToString();
            ViewData["ImagenProtocoloLavadoManos"] = ImagenLavadoManos2.ToString();
            ViewData["PDFRadicacionProtocolosBioSeguridad"] = PDFProtocolos.ToString();
            ViewData["PDFCertificacionAcompañamientoArl"] = PDFCertificacion.ToString();
            ViewData["ImagenPlanMovilidad"] = ImagenPlanMovilidad.ToString();
            ViewData["ImagenProtocoloDeAcceso"] = ImagenProtocoloAcceso.ToString();

            ViewData["Nitempresa"] = Nitempresa;
            ViewData["location"] = location;

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
        public JsonResult PromedioPBA(int Nitempresa)
        {
            TipoEmpresa tipoEmpresa = new TipoEmpresa();
            if (Nitempresa != 0)
            {
                Empresa empresa = db.Tb_Empresa.Find(Nitempresa);
                tipoEmpresa = empresa.TipoEmpresa;
                if (empresa.Empr_Ttrabaja > 0 && (tipoEmpresa == null || tipoEmpresa.Categoria < 3))
                {
                    tipoEmpresa = db.Tb_TipoEmpresa.FirstOrDefault(t => t.RangoMinimoTrabajadores <= empresa.Empr_Ttrabaja && t.RangoMaximoTrabajadores >= empresa.Empr_Ttrabaja);
                }
            }
            AutoEvaluacionAfp evaluacion =
                db.Tb_AutoEvaluacionAfp
                    .Where(a => a.Empr_Nit == Nitempresa && a.CumplimientosAfp.Count > 0)
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
                      db.Tb_cumplimientoAfp
                          .Where(a => a.Auevafp_Id == evaluacion.Auevafp_Id && (a.Cump_Cumple || a.Cump_Justifica))
                          .GroupBy(a => a.ItemEstandarAfp.EstandarAfp.CriterioAfp.CicloPHVAAfp).Select(a => new { key = a.Key.Id, value = (decimal)a.Where(e => e.Cump_Cumple || e.Cump_Justifica).Count() })
                          .ToArray();
                lst = new decimal[values.Length];
                lst[0] = 0;
                for (int i = 0; i < values.Length; i++)
                {
                    var val = temp.FirstOrDefault(v => v.key == values[i].key);
                    if (val != null)
                    {
                        lst[0] = Decimal.Round((val.value * 100 / 25), 1) + lst[0];
                    }
                    if (lst[0] != null)
                    {
                        lst[1] = 100 - lst[0];

                    }
                }
            }
            ChartDataViewModel datos =
               new ChartDataViewModel
               {
                   title = "Promedio Protocoloclos de Bioseguridad",
                   labels = new string[2] { "Cumplido", "No cumplido" },
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
        public JsonResult PromedioSGSST(int Nitempresa)
        {
            TipoEmpresa tipoEmpresa = new TipoEmpresa();
            if (Nitempresa != 0)
            {
                Empresa empresa = db.Tb_Empresa.Find(Nitempresa);
                tipoEmpresa = empresa.TipoEmpresa;
                if (empresa.Empr_Ttrabaja > 0 && (tipoEmpresa == null || tipoEmpresa.Categoria < 3))
                {
                    tipoEmpresa = db.Tb_TipoEmpresa.FirstOrDefault(t => t.RangoMinimoTrabajadores <= empresa.Empr_Ttrabaja && t.RangoMaximoTrabajadores >= empresa.Empr_Ttrabaja);
                }
            }
            AutoEvaluacion evaluacion =
                db.Tb_AutoEvaluacion
                    .Where(a => a.Empr_Nit == Nitempresa && a.Cumplimientos.Count > 0)
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
                lst[0] = 0;
                for (int i = 0; i < values.Length; i++)
                {
                    var val = temp.FirstOrDefault(v => v.key == values[i].key);
                    if (val != null)
                    {
                        lst[0] = Decimal.Round((val.value * 100 / 25), 1) + lst[0];
                    }
                    if (lst[0] != null)
                    {
                        lst[1] = 100 - lst[0];

                    }
                }
            }

            ChartDataViewModel datos =
               new ChartDataViewModel
               {
                   title = "Promedio Protocoloclos SG-SST",
                   labels = new string[2] { "Cumplido", "No cumplido" },
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
        public JsonResult ImplementacionPHVAActuar(int Nitempresa)
        {
            TipoEmpresa tipoEmpresa = new TipoEmpresa();
            if (Nitempresa != 0)
            {
                Empresa empresa = db.Tb_Empresa.Find(Nitempresa);
                tipoEmpresa = empresa.TipoEmpresa;
                if (empresa.Empr_Ttrabaja > 0 && (tipoEmpresa == null || tipoEmpresa.Categoria < 3))
                {
                    tipoEmpresa = db.Tb_TipoEmpresa.FirstOrDefault(t => t.RangoMinimoTrabajadores <= empresa.Empr_Ttrabaja && t.RangoMaximoTrabajadores >= empresa.Empr_Ttrabaja);
                }
            }
            AutoEvaluacionAfp evaluacion =
                db.Tb_AutoEvaluacionAfp
                    .Where(a => a.Empr_Nit == Nitempresa && a.CumplimientosAfp.Count > 0)
                    .OrderByDescending(a => a.Auev_Inicio)
                    .FirstOrDefault();
            decimal[] lst = new decimal[0];
            string[] labels = new string[0];
            if (evaluacion != null)
            {
                var values =
                   db.Tb_ItemEstandarAfp
                          .Where(ie => tipoEmpresa.Categoria == 0 || (ie.Categoria <= tipoEmpresa.Categoria && ie.CategoriaExcepcion != tipoEmpresa.Categoria && ie.CategoriaExcepcion != tipoEmpresa.Categoria))
                          .GroupBy(a => a.EstandarAfp.CriterioAfp.CicloPHVAAfp).Select(a => new { key = a.Key.Id, value = (decimal)a.Count(), name = a.Key.Nombre })
                          .ToArray();
                labels = values.Where(v => v.name == "4. Actuar").Select(v => v.name).ToArray();
                var temp =
                      db.Tb_cumplimientoAfp
                          .Where(a => a.Auevafp_Id == evaluacion.Auevafp_Id && (a.Cump_Cumple || a.Cump_Justifica))
                          .GroupBy(a => a.ItemEstandarAfp.EstandarAfp.CriterioAfp.CicloPHVAAfp).Select(a => new { key = a.Key.Id, value = (decimal)a.Where(e => e.Cump_Cumple || e.Cump_Justifica).Count() })
                          .ToArray();
                lst = new decimal[values.Length];
                lst[0] = 0;
                var flag = 0;
                for (int i = 0; i < values.Length; i++)
                {
                    var val = temp.FirstOrDefault(v => v.key == values[i].key);
                    if (val != null)
                    {
                        if (values[i].name == "4. Actuar")
                        {
                            lst[0] = Decimal.Round((val.value * 100 / values[i].value), 1);
                            flag = 1;
                        }
                        if (flag == 1)
                        {
                            lst[0 + 1] = 100 - lst[0];
                            flag = 2;
                        }
                    }
                }
            }
            ChartDataViewModel datos =
               new ChartDataViewModel
               {
                   title = "4. Actuar PBA",
                   labels = labels,
                   datasets =
                       new List<ChartDatasetsViewModel>{
                            new ChartDatasetsViewModel
                            {
                                label = "Avance porcentual %",
                                data = lst,
                                fill = false,
                                borderWidth = 1
                            },
                       }

               };
            return Json(datos, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ImplementacionPHVAVerificar(int Nitempresa)
        {
            TipoEmpresa tipoEmpresa = new TipoEmpresa();
            if (Nitempresa != 0)
            {
                Empresa empresa = db.Tb_Empresa.Find(Nitempresa);
                tipoEmpresa = empresa.TipoEmpresa;
                if (empresa.Empr_Ttrabaja > 0 && (tipoEmpresa == null || tipoEmpresa.Categoria < 3))
                {
                    tipoEmpresa = db.Tb_TipoEmpresa.FirstOrDefault(t => t.RangoMinimoTrabajadores <= empresa.Empr_Ttrabaja && t.RangoMaximoTrabajadores >= empresa.Empr_Ttrabaja);
                }
            }
            AutoEvaluacionAfp evaluacion =
                db.Tb_AutoEvaluacionAfp
                    .Where(a => a.Empr_Nit == Nitempresa && a.CumplimientosAfp.Count > 0)
                    .OrderByDescending(a => a.Auev_Inicio)
                    .FirstOrDefault();
            decimal[] lst = new decimal[0];
            string[] labels = new string[0];
            if (evaluacion != null)
            {
                var values =
                   db.Tb_ItemEstandarAfp
                          .Where(ie => tipoEmpresa.Categoria == 0 || (ie.Categoria <= tipoEmpresa.Categoria && ie.CategoriaExcepcion != tipoEmpresa.Categoria && ie.CategoriaExcepcion != tipoEmpresa.Categoria))
                          .GroupBy(a => a.EstandarAfp.CriterioAfp.CicloPHVAAfp).Select(a => new { key = a.Key.Id, value = (decimal)a.Count(), name = a.Key.Nombre })
                          .ToArray();
                labels = values.Where(v => v.name == "3. Verificar").Select(v => v.name).ToArray();
                var temp =
                      db.Tb_cumplimientoAfp
                          .Where(a => a.Auevafp_Id == evaluacion.Auevafp_Id && (a.Cump_Cumple || a.Cump_Justifica))
                          .GroupBy(a => a.ItemEstandarAfp.EstandarAfp.CriterioAfp.CicloPHVAAfp).Select(a => new { key = a.Key.Id, value = (decimal)a.Where(e => e.Cump_Cumple || e.Cump_Justifica).Count() })
                          .ToArray();
                lst = new decimal[values.Length];
                lst[0] = 0;
                var flag = 0;
                for (int i = 0; i < values.Length; i++)
                {
                    var val = temp.FirstOrDefault(v => v.key == values[i].key);
                    if (val != null)
                    {
                        if (values[i].name == "3. Verificar")
                        {
                            lst[0] = Decimal.Round((val.value * 100 / values[i].value), 1);
                            flag = 1;
                        }
                        if (flag == 1)
                        {
                            lst[0 + 1] = 100 - lst[0];
                            flag = 2;
                        }
                    }
                }
            }
            ChartDataViewModel datos =
               new ChartDataViewModel
               {
                   title = "3. Verificar PBA",
                   labels = labels,
                   datasets =
                       new List<ChartDatasetsViewModel>{
                            new ChartDatasetsViewModel
                            {
                                label = "Avance porcentual %",
                                data = lst,
                                fill = false,
                                borderWidth = 1
                            },
                       }

               };
            return Json(datos, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ImplementacionPHVAHacer(int Nitempresa)
        {
            TipoEmpresa tipoEmpresa = new TipoEmpresa();
            if (Nitempresa != 0)
            {
                Empresa empresa = db.Tb_Empresa.Find(Nitempresa);
                tipoEmpresa = empresa.TipoEmpresa;
                if (empresa.Empr_Ttrabaja > 0 && (tipoEmpresa == null || tipoEmpresa.Categoria < 3))
                {
                    tipoEmpresa = db.Tb_TipoEmpresa.FirstOrDefault(t => t.RangoMinimoTrabajadores <= empresa.Empr_Ttrabaja && t.RangoMaximoTrabajadores >= empresa.Empr_Ttrabaja);
                }
            }
            AutoEvaluacionAfp evaluacion =
                db.Tb_AutoEvaluacionAfp
                    .Where(a => a.Empr_Nit == Nitempresa && a.CumplimientosAfp.Count > 0)
                    .OrderByDescending(a => a.Auev_Inicio)
                    .FirstOrDefault();
            decimal[] lst = new decimal[0];
            string[] labels = new string[0];
            if (evaluacion != null)
            {
                var values =
                   db.Tb_ItemEstandarAfp
                          .Where(ie => tipoEmpresa.Categoria == 0 || (ie.Categoria <= tipoEmpresa.Categoria && ie.CategoriaExcepcion != tipoEmpresa.Categoria && ie.CategoriaExcepcion != tipoEmpresa.Categoria))
                          .GroupBy(a => a.EstandarAfp.CriterioAfp.CicloPHVAAfp).Select(a => new { key = a.Key.Id, value = (decimal)a.Count(), name = a.Key.Nombre })
                          .ToArray();
                labels = values.Where(v => v.name == "2. Hacer").Select(v => v.name).ToArray();
                var temp =
                      db.Tb_cumplimientoAfp
                          .Where(a => a.Auevafp_Id == evaluacion.Auevafp_Id && (a.Cump_Cumple || a.Cump_Justifica))
                          .GroupBy(a => a.ItemEstandarAfp.EstandarAfp.CriterioAfp.CicloPHVAAfp).Select(a => new { key = a.Key.Id, value = (decimal)a.Where(e => e.Cump_Cumple || e.Cump_Justifica).Count() })
                          .ToArray();
                lst = new decimal[values.Length];
                lst[0] = 0;
                var flag = 0;
                for (int i = 0; i < values.Length; i++)
                {
                    var val = temp.FirstOrDefault(v => v.key == values[i].key);
                    if (val != null)
                    {
                        if (values[i].name == "2. Hacer")
                        {
                            lst[0] = Decimal.Round((val.value * 100 / values[i].value), 1);
                            flag = 1;
                        }
                        if (flag == 1)
                        {
                            lst[0 + 1] = 100 - lst[0];
                            flag = 2;
                        }
                    }
                }

            }
            ChartDataViewModel datos =
               new ChartDataViewModel
               {
                   title = "2. Hacer PBA",
                   labels = labels,
                   datasets =
                       new List<ChartDatasetsViewModel>{
                            new ChartDatasetsViewModel
                            {
                                label = "Avance porcentual %",
                                data = lst,
                                fill = false,
                                borderWidth = 1
                            },
                       }

               };
            return Json(datos, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ImplementacionPHVAPlanear(int Nitempresa)
        {
            TipoEmpresa tipoEmpresa = new TipoEmpresa();
            if (Nitempresa != 0)
            {
                Empresa empresa = db.Tb_Empresa.Find(Nitempresa);
                tipoEmpresa = empresa.TipoEmpresa;
                if (empresa.Empr_Ttrabaja > 0 && (tipoEmpresa == null || tipoEmpresa.Categoria < 3))
                {
                    tipoEmpresa = db.Tb_TipoEmpresa.FirstOrDefault(t => t.RangoMinimoTrabajadores <= empresa.Empr_Ttrabaja && t.RangoMaximoTrabajadores >= empresa.Empr_Ttrabaja);
                }
            }
            AutoEvaluacionAfp evaluacion =
                db.Tb_AutoEvaluacionAfp
                    .Where(a => a.Empr_Nit == Nitempresa && a.CumplimientosAfp.Count > 0)
                    .OrderByDescending(a => a.Auev_Inicio)
                    .FirstOrDefault();
            decimal[] lst = new decimal[0];
            string[] labels = new string[0];
            if (evaluacion != null)
            {
                var values =
                   db.Tb_ItemEstandarAfp
                          .Where(ie => tipoEmpresa.Categoria == 0 || (ie.Categoria <= tipoEmpresa.Categoria && ie.CategoriaExcepcion != tipoEmpresa.Categoria && ie.CategoriaExcepcion != tipoEmpresa.Categoria))
                          .GroupBy(a => a.EstandarAfp.CriterioAfp.CicloPHVAAfp).Select(a => new { key = a.Key.Id, value = (decimal)a.Count(), name = a.Key.Nombre })
                          .ToArray();
                labels = values.Where(v => v.name == "1. Planear").Select(v => v.name).ToArray();
                var temp =
                      db.Tb_cumplimientoAfp
                          .Where(a => a.Auevafp_Id == evaluacion.Auevafp_Id && (a.Cump_Cumple || a.Cump_Justifica))
                          .GroupBy(a => a.ItemEstandarAfp.EstandarAfp.CriterioAfp.CicloPHVAAfp).Select(a => new { key = a.Key.Id, value = (decimal)a.Where(e => e.Cump_Cumple || e.Cump_Justifica).Count() })
                          .ToArray();
                lst = new decimal[values.Length];
                lst[0] = 0;
                var flag = 0;
                for (int i = 0; i < values.Length; i++)
                {
                    var val = temp.FirstOrDefault(v => v.key == values[i].key);
                    if (val != null)
                    {
                        if (values[i].name == "1. Planear")
                        {
                            lst[0] = Decimal.Round((val.value * 100 / values[i].value), 1);
                            flag = 1;
                        }
                        if (flag == 1)
                        {
                            lst[0 + 1] = 100 - lst[0];
                            flag = 2;
                        }
                    }
                }
            }
            ChartDataViewModel datos =
               new ChartDataViewModel
               {
                   title = "1. Planear PBA",
                   labels = labels,
                   datasets =
                       new List<ChartDatasetsViewModel>{
                            new ChartDatasetsViewModel
                            {
                                label = "Avance porcentual %",
                                data = lst,
                                fill = false,
                                borderWidth = 1
                            },
                       }

               };
            return Json(datos, JsonRequestBehavior.AllowGet);
        }

        //SG-SST PROMEDIO INDIVIDUAL

        public JsonResult ImplementacionSGSSTActuar(int Nitempresa)
        {
            TipoEmpresa tipoEmpresa = new TipoEmpresa();
            if (Nitempresa != 0)
            {
                Empresa empresa = db.Tb_Empresa.Find(Nitempresa);
                tipoEmpresa = empresa.TipoEmpresa;
                if (empresa.Empr_Ttrabaja > 0 && (tipoEmpresa == null || tipoEmpresa.Categoria < 3))
                {
                    tipoEmpresa = db.Tb_TipoEmpresa.FirstOrDefault(t => t.RangoMinimoTrabajadores <= empresa.Empr_Ttrabaja && t.RangoMaximoTrabajadores >= empresa.Empr_Ttrabaja);
                }
            }
            AutoEvaluacion evaluacion =
                db.Tb_AutoEvaluacion
                    .Where(a => a.Empr_Nit == Nitempresa && a.Cumplimientos.Count > 0)
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
                labels = values.Where(v => v.name == "4. Actuar").Select(v => v.name).ToArray();
                var temp =
                      db.Tb_Cumplimiento
                          .Where(a => a.Auev_Id == evaluacion.Auev_Id && (a.Cump_Cumple || a.Cump_Justifica))
                          .GroupBy(a => a.ItemEstandar.Estandar.Criterio.CicloPHVA).Select(a => new { key = a.Key.Id, value = (decimal)a.Where(e => e.Cump_Cumple || e.Cump_Justifica).Count() })
                          .ToArray();
                lst = new decimal[values.Length];
                lst[0] = 0;
                var flag = 0;
                for (int i = 0; i < values.Length; i++)
                {
                    var val = temp.FirstOrDefault(v => v.key == values[i].key);
                    if (val != null)
                    {
                        if (values[i].name == "4. Actuar")
                        {
                            lst[0] = Decimal.Round((val.value * 100 / values[i].value), 1);
                            flag = 1;
                        }
                        if (flag == 1)
                        {
                            lst[0 + 1] = 100 - lst[0];
                            flag = 2;
                        }
                    }
                }
            }
            ChartDataViewModel datos =
               new ChartDataViewModel
               {
                   title = "4. Actuar SG-SST",
                   labels = labels,
                   datasets =
                       new List<ChartDatasetsViewModel>{
                            new ChartDatasetsViewModel
                            {
                                label = "Avance porcentual %",
                                data = lst,
                                fill = false,
                                borderWidth = 1
                            },
                       }

               };
            return Json(datos, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ImplementacionSGSSTVerificar(int Nitempresa)
        {
            TipoEmpresa tipoEmpresa = new TipoEmpresa();
            if (Nitempresa != 0)
            {
                Empresa empresa = db.Tb_Empresa.Find(Nitempresa);
                tipoEmpresa = empresa.TipoEmpresa;
                if (empresa.Empr_Ttrabaja > 0 && (tipoEmpresa == null || tipoEmpresa.Categoria < 3))
                {
                    tipoEmpresa = db.Tb_TipoEmpresa.FirstOrDefault(t => t.RangoMinimoTrabajadores <= empresa.Empr_Ttrabaja && t.RangoMaximoTrabajadores >= empresa.Empr_Ttrabaja);
                }
            }
            AutoEvaluacion evaluacion =
                db.Tb_AutoEvaluacion
                    .Where(a => a.Empr_Nit == Nitempresa && a.Cumplimientos.Count > 0)
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
                labels = values.Where(v => v.name == "3. Verificar").Select(v => v.name).ToArray();
                var temp =
                      db.Tb_Cumplimiento
                          .Where(a => a.Auev_Id == evaluacion.Auev_Id && (a.Cump_Cumple || a.Cump_Justifica))
                          .GroupBy(a => a.ItemEstandar.Estandar.Criterio.CicloPHVA).Select(a => new { key = a.Key.Id, value = (decimal)a.Where(e => e.Cump_Cumple || e.Cump_Justifica).Count() })
                          .ToArray();
                lst = new decimal[values.Length];
                lst[0] = 0;
                var flag = 0;
                for (int i = 0; i < values.Length; i++)
                {
                    var val = temp.FirstOrDefault(v => v.key == values[i].key);
                    if (val != null)
                    {
                        if (values[i].name == "3. Verificar")
                        {
                            lst[0] = Decimal.Round((val.value * 100 / values[i].value), 1);
                            flag = 1;
                        }
                        if (flag == 1)
                        {
                            lst[0 + 1] = 100 - lst[0];
                            flag = 2;
                        }
                    }
                }
            }
            ChartDataViewModel datos =
               new ChartDataViewModel
               {
                   title = "3. Verificar SG-SST",
                   labels = labels,
                   datasets =
                       new List<ChartDatasetsViewModel>{
                            new ChartDatasetsViewModel
                            {
                                label = "Avance porcentual %",
                                data = lst,
                                fill = false,
                                borderWidth = 1
                            },
                       }

               };
            return Json(datos, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ImplementacionSGSSTHacer(int Nitempresa)
        {
            TipoEmpresa tipoEmpresa = new TipoEmpresa();
            if (Nitempresa != 0)
            {
                Empresa empresa = db.Tb_Empresa.Find(Nitempresa);
                tipoEmpresa = empresa.TipoEmpresa;
                if (empresa.Empr_Ttrabaja > 0 && (tipoEmpresa == null || tipoEmpresa.Categoria < 3))
                {
                    tipoEmpresa = db.Tb_TipoEmpresa.FirstOrDefault(t => t.RangoMinimoTrabajadores <= empresa.Empr_Ttrabaja && t.RangoMaximoTrabajadores >= empresa.Empr_Ttrabaja);
                }
            }
            AutoEvaluacion evaluacion =
                db.Tb_AutoEvaluacion
                    .Where(a => a.Empr_Nit == Nitempresa && a.Cumplimientos.Count > 0)
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
                labels = values.Where(v => v.name == "2. Hacer").Select(v => v.name).ToArray();
                var temp =
                      db.Tb_Cumplimiento
                          .Where(a => a.Auev_Id == evaluacion.Auev_Id && (a.Cump_Cumple || a.Cump_Justifica))
                          .GroupBy(a => a.ItemEstandar.Estandar.Criterio.CicloPHVA).Select(a => new { key = a.Key.Id, value = (decimal)a.Where(e => e.Cump_Cumple || e.Cump_Justifica).Count() })
                          .ToArray();
                lst = new decimal[values.Length];
                lst[0] = 0;
                var flag = 0;
                for (int i = 0; i < values.Length; i++)
                {
                    var val = temp.FirstOrDefault(v => v.key == values[i].key);
                    if (val != null)
                    {
                        if (values[i].name == "2. Hacer")
                        {
                            lst[0] = Decimal.Round((val.value * 100 / values[i].value), 1);
                            flag = 1;
                        }
                        if (flag == 1)
                        {
                            lst[0 + 1] = 100 - lst[0];
                            flag = 2;
                        }
                    }
                }

            }
            ChartDataViewModel datos =
               new ChartDataViewModel
               {
                   title = "2. Hacer SG-SST",
                   labels = labels,
                   datasets =
                       new List<ChartDatasetsViewModel>{
                            new ChartDatasetsViewModel
                            {
                                label = "Avance porcentual %",
                                data = lst,
                                fill = false,
                                borderWidth = 1
                            },
                       }

               };
            return Json(datos, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ImplementacionSGSSTPlanear(int Nitempresa)
        {
            TipoEmpresa tipoEmpresa = new TipoEmpresa();
            if (Nitempresa != 0)
            {
                Empresa empresa = db.Tb_Empresa.Find(Nitempresa);
                tipoEmpresa = empresa.TipoEmpresa;
                if (empresa.Empr_Ttrabaja > 0 && (tipoEmpresa == null || tipoEmpresa.Categoria < 3))
                {
                    tipoEmpresa = db.Tb_TipoEmpresa.FirstOrDefault(t => t.RangoMinimoTrabajadores <= empresa.Empr_Ttrabaja && t.RangoMaximoTrabajadores >= empresa.Empr_Ttrabaja);
                }
            }
            AutoEvaluacion evaluacion =
                db.Tb_AutoEvaluacion
                    .Where(a => a.Empr_Nit == Nitempresa && a.Cumplimientos.Count > 0)
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
                labels = values.Where(v => v.name == "1. Planear").Select(v => v.name).ToArray();
                var temp =
                      db.Tb_Cumplimiento
                          .Where(a => a.Auev_Id == evaluacion.Auev_Id && (a.Cump_Cumple || a.Cump_Justifica))
                          .GroupBy(a => a.ItemEstandar.Estandar.Criterio.CicloPHVA).Select(a => new { key = a.Key.Id, value = (decimal)a.Where(e => e.Cump_Cumple || e.Cump_Justifica).Count() })
                          .ToArray();
                lst = new decimal[values.Length];
                lst[0] = 0;
                var flag = 0;
                for (int i = 0; i < values.Length; i++)
                {
                    var val = temp.FirstOrDefault(v => v.key == values[i].key);
                    if (val != null)
                    {
                        if (values[i].name == "1. Planear")
                        {
                            lst[0] = Decimal.Round((val.value * 100 / values[i].value), 1);
                            flag = 1;
                        }
                        if (flag == 1)
                        {
                            lst[0 + 1] = 100 - lst[0];
                            flag = 2;
                        }
                    }
                }
            }
            ChartDataViewModel datos =
               new ChartDataViewModel
               {
                   title = "1. Planear SG-SST",
                   labels = labels,
                   datasets =
                       new List<ChartDatasetsViewModel>{
                            new ChartDatasetsViewModel
                            {
                                label = "Avance porcentual %",
                                data = lst,
                                fill = false,
                                borderWidth = 1
                            },
                       }

               };
            return Json(datos, JsonRequestBehavior.AllowGet);
        }
        public JsonResult AvanceAutoevaluaciones(int Nitempresa)
        {
            List<ActiCumplimiento> lst = new List<ActiCumplimiento>();
            try
            {
                var cumplimientos =
                    db.Tb_ActiCumplimiento.Where(a => a.Empr_Nit == Nitempresa && a.Usersplandetrabajo.Any(u => u.PlandeTrabajo != null)).ToList();
                if (cumplimientos != null && cumplimientos.Count > 0)
                {
                    lst.AddRange(cumplimientos);
                }

                var ci = new CultureInfo("es-CO");
                var ejecucion = lst.Where(a => !a.Finalizada).GroupBy(a => a.Acum_IniAct.ToString("MMMM", ci)).Select(a => a.Count()).Count();
                var planeadas = lst.Count;
                var finalizadas = lst.Count - ejecucion;

                ChartDataViewModel datos =
                  new ChartDataViewModel
                  {
                      title = "Alcance del plan de trabajo anual",
                      labels = new string[3] { "Planeadas", "En Ejercucion", "Finalizadas" },
                      datasets =
                          new List<ChartDatasetsViewModel>{
                            new ChartDatasetsViewModel
                            {
                                label = "Porcentaje %",
                                data = new string[3]{planeadas.ToString(),ejecucion.ToString(),finalizadas.ToString()},
                                fill = false,
                                borderWidth = 1
                            },

                  }
                  };
                return Json(datos, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult ImplementacionApi(int location)
        {
            using (WebClient wc = new WebClient())
            {
                var json = wc.DownloadString("http://127.0.0.1/scoreapb/api/datosapi?location=" + location);
                dynamic jsonObj = JsonConvert.DeserializeObject(json);
                RecomendacionesHogar = Int32.Parse(jsonObj["RecomendacionesHogar"].ToString());
                DistanciaminetoFisico = Int32.Parse(jsonObj["DistanciaminetoFisico"].ToString());
                Transporte = Int32.Parse(jsonObj["Transporte"].ToString());
                Guantes = Int32.Parse(jsonObj["Guantes"].ToString());
                Tabocas = Int32.Parse(jsonObj["Tabocas"].ToString());
                LavadoManos = Int32.Parse(jsonObj["LavadoManos"].ToString());
                UsuariosMatriculados = Int32.Parse(jsonObj["UsuariosMatriculados"].ToString());

                PorcentajeRecomendacionesHogar = (RecomendacionesHogar * 100) / UsuariosMatriculados;
                PorcentajeDistanciaminetoFisico = (DistanciaminetoFisico * 100) / UsuariosMatriculados;
                PorcentajeTransporte = (Transporte * 100) / UsuariosMatriculados;
                PorcentajeGuantes = (Guantes * 100) / UsuariosMatriculados;
                PorcentajeTapabocas = (Tabocas * 100) / UsuariosMatriculados;
                PorcentajeLavadoManos = (LavadoManos * 100) / UsuariosMatriculados;
                PorcentajeTotal = ((RecomendacionesHogar + DistanciaminetoFisico + Transporte + Guantes + Tabocas + LavadoManos) * 100) / (UsuariosMatriculados * 6);
            }

            ChartDataViewModel datos =
               new ChartDataViewModel
               {
                   title = "INDICADOR TRABAJADORES CERTIFICADOS",
                   labels = new string[7] { "Recomendaciones Hogar", "Transporte", "Guantes", "Tapabocas", "Distanciamineto físico", "Lavado Manos", "Porcentaje Total" },
                   datasets =
                       new List<ChartDatasetsViewModel>{
                            new ChartDatasetsViewModel
                            {
                                label = "Avance porcentual %",
                                data = new string[7]{PorcentajeRecomendacionesHogar.ToString(), PorcentajeTransporte.ToString(), PorcentajeGuantes.ToString(),PorcentajeTapabocas.ToString(), PorcentajeDistanciaminetoFisico.ToString(),PorcentajeLavadoManos.ToString(), PorcentajeTotal.ToString() },
                                fill = false,
                                borderWidth = 1
                            },
                       }

               };
            return Json(datos, JsonRequestBehavior.AllowGet);
        }
        public ActionResult CargaEvidencia()
        {
            ViewBag.Tdca_id = new SelectList(db.Tb_TipoDocCarga, "Tdca_id", "Tdca_Nom");
            ApplicationUser usuario = db.Users.Find(AccountData.UsuarioId);
            ViewBag.Empr_Nit = new SelectList(db.Tb_Empresa, "Empr_Nit", "Empr_Nom");

            return View(
                new EvidenciaDashViewModel
                {
                    Empresa = AccountData.NitEmpresa
                }
                );
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CargaEvidencia([Bind(Include = "DocsEvidencia,Archivo,NombreDocumento,TipoDocumento,DescDocumento,Fecha,Empresa")] EvidenciaDashViewModel model)
        {

            ViewBag.Tdca_id = new SelectList(db.Tb_TipoDocCarga, "Tdca_id", "Tdca_Nom");
            string nombreArchivo = model.NombreDocumento;
            List<DocsEvidencia> docsEvidencias = db.Tb_DocsEvidencia.Where(f => f.Devide_Nombre == nombreArchivo).ToList();
            if (docsEvidencias.Count == 0)
            {
                if (model.Archivo == null)
                {
                    ViewBag.falla = "Seleccione un archivo";
                    return View(model);
                }
                string extensionArchivo = model.Archivo.FileName.Split('.').Last();
                var UserId = User.Identity.GetUserId();
                var UserCurrent = db.Users.Find(UserId);
                var Empr_Nit = UserCurrent.Empr_Nit.ToString();
                int Empr_NitI = int.Parse(Empr_Nit);
                DocsEvidencia docsEvidencia = new DocsEvidencia
                {
                    Devide_Nombre = nombreArchivo,
                    Tdca_id = Convert.ToInt32(model.TipoDocumento),
                    File_Registro = model.Fecha,
                    Devide_Archivo = nombreArchivo + "." + extensionArchivo,
                    Devide_Descri = model.DescDocumento,
                    Empr_Nit = model.Empresa,

                };
                docsEvidencia.Empr_Nit = AccountData.NitEmpresa;
                db.Tb_DocsEvidencia.Add(docsEvidencia);
                db.SaveChanges();

                if (model.Archivo.ContentLength > 0)
                {
                    string path = Path.Combine(Server.MapPath("~/Files"), nombreArchivo + "." + extensionArchivo);
                    model.Archivo.SaveAs(path);
                }
                ViewBag.exitoso = "Guardado con exito en la ruta";

            }
            else
            {
                ViewBag.falla = "Ya existe un documento con ese nombre";
                return View(model);
            }
            return View(new EvidenciaDashViewModel());


        }
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