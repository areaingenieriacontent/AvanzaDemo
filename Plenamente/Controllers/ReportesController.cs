using Microsoft.Reporting.WebForms;
using Plenamente.App_Tool;
using Plenamente.Models;
using Plenamente.PlenamenteDataSetTableAdapters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace Plenamente.Controllers
{
    /// <summary>
    /// Controlador de reporte , encargado mediante de metodos actionresult de consultar procedimientos almacenados en la bd mediante el dataset establecido <Plenamentedataset>
    /// y mostrarlo en la vista mediante de RDLC
    /// </summary>
	public class ReportesController : Controller
	{
		private ApplicationDbContext db = new ApplicationDbContext();      
        /// <summary>
        /// Metodo encargado de buscar las autoevaluaciones mediante el id en la bd , consumiento los procedimiento almacenados en sus correspondientes adaptadores y mediante un viewbag 
        /// pintar el reporte.
        /// </summary>
        /// <param name="id">Recibe el id de la autoevaluacion</param>
        /// <returns>retorna a la vista normalmente</returns>
        public ActionResult VerReporte(int id)
		{
            try
            {
                ReportViewer reportViewer =
                    new ReportViewer()
                    {
                        ProcessingMode = ProcessingMode.Local,
                        SizeToReportContent = true,
                        Width = Unit.Percentage(100),
                        Height = Unit.Percentage(100),
                    };
                PlenamenteDataSet.ResumenCriteriosAutoEvaluacionDataTable data1 = new PlenamenteDataSet.ResumenCriteriosAutoEvaluacionDataTable();
                ResumenCriteriosAutoEvaluacionTableAdapter adapter1 = new ResumenCriteriosAutoEvaluacionTableAdapter();
                adapter1.Fill(data1, id, AccountData.NitEmpresa);
                if (data1 != null && data1.Rows.Count > 0)
                {
                    PlenamenteDataSet.ResumenAutoEvaluacionDataTable data = new PlenamenteDataSet.ResumenAutoEvaluacionDataTable();
                    ResumenAutoEvaluacionTableAdapter adapter = new ResumenAutoEvaluacionTableAdapter();
                    adapter.Fill(data, id);


                    if (data != null && data.Rows.Count > 0)
                    {
                        PlenamenteDataSet.ResumenEmpresaDataTable data2 = new PlenamenteDataSet.ResumenEmpresaDataTable();
                        ResumenEmpresaTableAdapter adapter2 = new ResumenEmpresaTableAdapter();
                        adapter2.Fill(data2, AccountData.NitEmpresa);

                        if (data2 != null && data2.Rows.Count > 0)
                        {
                            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DsDatosEmpresa", data2.CopyToDataTable()));
                        }
                        reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DsAutoEvaluacion", data.CopyToDataTable()));

                    }
                    reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DsResumenCriterios", data1.CopyToDataTable()));
                    reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"\Reportes\rptAutoEvaluacion.rdlc.";
                    ViewBag.ReportViewer = reportViewer;
                }
                else
                {
                    ViewBag.TextError = "No hay data valida para esta auto evaluacion";
                }            
            }
                
            catch (Exception ex)
            {
                ViewBag.TextError = ex.Message;
            }

            return View();
		}
        /// <summary>
        /// Metodo encargado de buscar las autoevaluaciones mediante el id en la bd , consumiento los procedimiento almacenados en sus correspondientes adaptadores y mediante un viewbag 
        /// pintar el reporte.
        /// </summary>
        /// <param name="id">Recibe el id del plan de trabajo</param>
        /// <returns>retorna a la vista normalmente</returns>
        public ActionResult VerReportePlandeTrabajo(int id)
        {
            try
            {
                ReportViewer reportViewer =
                    new ReportViewer()
                    {
                        ProcessingMode = ProcessingMode.Local,
                        SizeToReportContent = true,
                        Width = Unit.Percentage(100),
                        Height = Unit.Percentage(100),
                    };
                PlenamenteDataSet.ResumenPlanDeTrabajoDataTable data1 = new PlenamenteDataSet.ResumenPlanDeTrabajoDataTable();
                ResumenPlanDeTrabajoTableAdapter adapter1 = new ResumenPlanDeTrabajoTableAdapter();
                adapter1.Fill(data1, id, AccountData.NitEmpresa);
                if (data1 != null && data1.Rows.Count > 0)
                {                 
                   
                    reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DsPlanDeTrabajo", data1.CopyToDataTable()));
                    reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"\Reportes\rptPlanDeTrabajo.rdlc.";
                    ViewBag.ReportViewer = reportViewer;
                }
                else
                {
                    ViewBag.TextError = "No hay data valida para esta auto evaluacion";
                }
            }

            catch (Exception ex)
            {
                ViewBag.TextError = ex.Message;
            }

            return View();
        }

        /// <summary>
        /// Metodo encargado de buscar las autoevaluaciones mediante el id en la bd , consumiento los procedimiento almacenados en sus correspondientes adaptadores y mediante un viewbag 
        /// pintar el reporte.
        /// </summary>
        /// <param name="id">Recibe el id del plan de trabajo</param>
        /// <returns>retorna a la vista normalmente</returns>
        public ActionResult VerReportes(int id)
        {
            try
            {
                ReportViewer reportViewer =
                    new ReportViewer()
                    {
                        ProcessingMode = ProcessingMode.Local,
                        SizeToReportContent = true,
                        Width = Unit.Percentage(100),
                        Height = Unit.Percentage(100),
                    };
                PlenamenteDataSet.ResumenCriteriosAutoEvaluacionApbDataTable data1 = new PlenamenteDataSet.ResumenCriteriosAutoEvaluacionApbDataTable();
                ResumenCriteriosAutoEvaluacionApbTableAdapter adapter1 = new ResumenCriteriosAutoEvaluacionApbTableAdapter();
                adapter1.Fill(data1, id, AccountData.NitEmpresa);
                if (data1 != null && data1.Rows.Count > 0)
                {
                    PlenamenteDataSet.ResumenAutoEvaluacionApbDataTable data = new PlenamenteDataSet.ResumenAutoEvaluacionApbDataTable();
                    ResumenAutoEvaluacionApbTableAdapter adapter = new ResumenAutoEvaluacionApbTableAdapter();
                    adapter.Fill(data, id);


                    if (data != null && data.Rows.Count > 0)
                    {
                        PlenamenteDataSet.ResumenEmpresaDataTable data2 = new PlenamenteDataSet.ResumenEmpresaDataTable();
                        ResumenEmpresaTableAdapter adapter2 = new ResumenEmpresaTableAdapter();
                        adapter2.Fill(data2, AccountData.NitEmpresa);

                        if (data2 != null && data2.Rows.Count > 0)
                        {
                            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DsDatosEmpresa", data2.CopyToDataTable()));
                        }
                        reportViewer.LocalReport.DataSources.Add(new ReportDataSource("AutoevaluacionApb", data.CopyToDataTable()));

                    }
                    reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DsResumenCriteriosApb", data1.CopyToDataTable()));
                    reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"\Reportes\rptAutoEvaluacionApb.rdlc.";
                    ViewBag.ReportViewer = reportViewer;
                }
                else
                {
                    ViewBag.TextError = "No hay data valida para esta auto evaluacion";
                }
            }

            catch (Exception ex)
            {
                ViewBag.TextError = ex.Message;
            }

            return View();
        }
    }
}