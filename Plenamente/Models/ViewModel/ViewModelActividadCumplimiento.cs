using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Plenamente.Models.ViewModel
{
    public class ViewModelActividadCumplimiento
    {
        /// <summary>
        /// VIEWMODEL que implementa las propiedades necesarias para las actividades de un plan de trabajo.
        /// Contiene información sobre actividades generadas 
        /// </summary>

        /// <summary>
        /// Obtiene o llena el identificador de la actividad.
        /// </summary>
        /// <value>
        /// identificador de la actividad 
        /// </value>
        public int IdActiCumplimiento { get; set; }
        /// <summary>
        /// Obtiene o llena el identificador de la empresa a la cual se le genera la actividad.
        /// </summary>
        /// <value>
        /// identificador de empresa(NIT) 
        /// </value>
        public int IdEmpresa { get; set; }
        /// <summary>
        /// Obtiene o llena el identificador del los usuarios asignados a una empresa los cuales seran responsables de la actividad generada
        /// </summary>
        /// <value>
        /// identificador del usuario 
        /// </value>
        [Display(Name = "Usuarios")]
        public string IdUser { get; set; }
        /// <summary>
        /// Obtiene o llena el nombre de la actividad.
        /// </summary>
        /// <value>
        /// nombre de la actividad 
        /// </value>
        [Display(Name = "Actividad")]
        public string NombreActividad { get; set; }
        /// <summary>
        /// Obtiene o llena el porcentaje de la meta de la actividad.
        /// </summary>
        /// <value>
        /// meta de la actividad 
        /// </value>
        [Display(Name = "Meta (%)")]
        public float Meta { get; set; }
        /// <summary>
        /// Obtiene o llena la fecha de ejecucion de la actividad.
        /// </summary>
        /// <value>
        /// fecha inicial de la actividad 
        /// </value>
        [Display(Name = "Fecha Programada")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaInicial { get; set; }
        /// <summary>
        /// Obtiene o llena la fecha de finalizacion de la actividad.
        /// </summary>
        /// <value>
        /// fecha final de la actividad 
        /// </value>
        [Display(Name = "Fecha de finalización")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaFinal { get; set; }
        /// <summary>
        /// Obtiene o llena la hora del dia en que se va a ejecutar la actividad.
        /// </summary>
        /// <value>
        /// hora del del dia de la actividad 
        /// </value>
        [Display(Name = "Hora")]
        public TimeSpan hora { get; set; }
        /// <summary>
        /// Obtiene o llena el objetivo de la empresa al cual va dirijida la actividad.
        /// </summary>
        /// <value>
        /// objetiva de la empresa amarrado a la actividad 
        /// </value>
        [Display(Name = "Objetivo de la Empresa")]
        [Required]
        public int idObjetivo { get; set; }
        /// <summary>
        /// Obtiene o llena el numero de dia que se va a ejecutar la actividad.
        /// </summary>
        /// <value>
        /// nuemro de dias de la actividad
        /// </value>
        [Display(Name = "Periodicidad")]
        public string Frecuencia { get; set; }
        /// <summary>
        /// Obtiene o llena la url desde donde se ejecuto la accion para ver o crear una actividad.
        /// </summary>
        /// <value>
        /// URL de la pagina enterior desde donde se ejecuto la accion  
        /// </value>
        public string retornar { get; set; }
        /// <summary>
        /// Obtiene o llena la periodicidad de la actividad.
        /// </summary>
        /// <value>
        /// periodicidad de la actividad(semanal, diaria, mensual, nunca) 
        /// </value>
        public string Frecuencia_desc { get; set; }
        /// <summary>
        /// variable auxiliar que obtiene o llena la periodicidad de la actividad.
        /// </summary>
        /// <value>
        /// periodicidad de la actividad(semanal, diaria, mensual, nunca, bimestral, trimestral, semestral) 
        /// </value>
        public int period { get; set; }
        /// <summary>
        /// Obtiene o llena si es diaria la actividad con un dia de la semana.
        /// </summary>
        /// <value>
        /// dia lunes de la actividad 
        /// </value>
        public string weekly_0 { get; set; }
        /// <summary>
        /// Obtiene o llena si es diaria la actividad con un dia de la semana.
        /// </summary>
        /// <value>
        /// dia martes de la actividad 
        /// </value>
        public string weekly_1 { get; set; }
        /// <summary>
        /// Obtiene o llena si es diaria la actividad con un dia de la semana.
        /// </summary>
        /// <value>
        /// dia miercoles de la actividad 
        /// </value>
        public string weekly_2 { get; set; }
        /// <summary>
        /// Obtiene o llena si es diaria la actividad con un dia de la semana.
        /// </summary>
        /// <value>
        /// dia jueves de la actividad 
        /// </value>
        public string weekly_3 { get; set; }
        /// <summary>
        /// Obtiene o llena si es diaria la actividad con un dia de la semana.
        /// </summary>
        /// <value>
        /// dia viernes de la actividad 
        /// </value>
        public string weekly_4 { get; set; }
        /// <summary>
        /// Obtiene o llena si es diaria la actividad con un dia de la semana.
        /// </summary>
        /// <value>
        /// dia sabado de la actividad 
        /// </value>
        public string weekly_5 { get; set; }
        /// <summary>
        /// Obtiene o llena si es diaria la actividad con un dia de la semana.
        /// </summary>
        /// <value>
        /// dia domingo de la actividad 
        /// </value>
        public string weekly_6 { get; set; }
        /// <summary>
        /// Obtiene o llena si la actividad necesita de un recurso de la empresa para ejecutarse.
        /// </summary>
        /// <value>
        /// usuarios, archivos, normas para ejecutar la actividad 
        /// </value>
        [Display(Name = "Asignación de recursos")]
        public string asigrecursos { get; set; }
        /// <summary>
        ///  Obtiene o llena un boleano si la actividad se va a finalizar o no.
        /// </summary>
        /// <value>
        /// true o false
        /// </value>
        [Display(Name = "Finalizar actividad")]
        public bool Finalizada { get; set; }
        /// <summary>
        /// Obtiene o llena el id del plan de trabajo al cual pertenece una actividad.
        /// </summary>
        /// <value>
        /// identificador del plan de trabajo 
        /// </value>
        public int idPlanDeTrabajo { get; set; }
    }
}