using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Plenamente.Models
{
    public class ActiCumplimiento
    {
        /// <summary>
        /// Clases de Actividades.
        /// Genera actividades pertenecientes a un plan de trabajo
        /// </summary>
        [Key]

        /// <summary>
        /// Obtiene o llena el identificador de la actividad.
        /// </summary>
        /// <value>
        /// identificador de la actividad 
        /// </value>
        public int Acum_Id { get; set; }
        /// <summary>
        /// Obtiene o llena el nombre de la actividad.
        /// </summary>
        /// <value>
        /// nombre de la actividad 
        /// </value>
        [Display(Name = "Descripción")]
        public string Acum_Desc { get; set; }
        /// <summary>
        /// Obtiene o llena el porcentaje de la meta de la actividad.
        /// </summary>
        /// <value>
        /// meta de la actividad 
        /// </value>
        [Display(Name = "Meta (%)")]
        public float Acum_Porcentest { get; set; }
        [Display(Name = "Cargue Evidencia")]
        public string Acum_Ejec { get; set; }
        /// <summary>
        /// Obtiene o llena las acciones realizada sobre el registro de la actividad.
        /// </summary>
        /// <value>
        /// fecha de generacion por el usuario 
        /// </value>
        [Display(Name = "Fecha Registro")]
        [DataType(DataType.Date)]
        public DateTime Acum_Registro { get; set; }
        /// <summary>
        /// Obtiene o llena la fecha de ejecucion de la actividad.
        /// </summary>
        /// <value>
        /// fecha inicial de la actividad 
        /// </value>
        [Display(Name = "Fecha Programada")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Acum_IniAct { get; set; }
        /// <summary>
        /// Obtiene o llena la fecha de finalizacion de la actividad.
        /// </summary>
        /// <value>
        /// fecha final de la actividad 
        /// </value>
        [Display(Name = "Finalización de Ejecución")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Acum_FinAct { get; set; }
        /// <summary>
        /// Obtiene o llena el objetivo de la empresa al cual va dirijida la actividad.
        /// </summary>
        /// <value>
        /// objetiva de la empresa amarrado a la actividad 
        /// </value>
        [ForeignKey("ObjEmpresa")]
        [Display(Name = "Objetivos empresa")]
        public int Oemp_Id { get; set; }
        public ObjEmpresa ObjEmpresa { get; set; }
        /// <summary>
        /// Obtiene o llena el identificador del los usuarios asignados a una empresa los cuales seran responsables de la actividad generada
        /// </summary>
        /// <value>
        /// identificador del usuario 
        /// </value>
        [ForeignKey("ApplicationUser")]
        [Display(Name = "Nombre del usuario")]
        public string Id { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        [ForeignKey("Periodo")]
        public int Peri_Id { get; set; }
        public Periodo Periodo { get; set; }
        /// <summary>
        /// Obtiene o llena el identificador de la empresa a la cual se le genera la actividad.
        /// </summary>
        /// <value>
        /// identificador de empresa(NIT) 
        /// </value>
        [ForeignKey("Empresa")]
        public int Empr_Nit { get; set; }
        public Empresa Empresa { get; set; }
        /// <summary>
        /// Obtiene o llena el numero de dia que se va a ejecutar la actividad.
        /// </summary>
        /// <value>
        /// nuemro de dias de la actividad
        /// </value>
        [ForeignKey("Frecuencia")]
        [Display(Name = "Periodicidad")]
        public int Frec_Id { get; set; }
        public Frecuencia Frecuencia { get; set; }

        /// <summary>
        /// Obtiene o llena los dia de la semana se va a ejecutar la actividad.
        /// </summary>
        /// <value>
        /// dias de la semana en cadena
        /// </value>
        [Display(Name = "Dias en que se ejecuta la actividad")]
        public string DiasSemana { get; set; }
        /// <summary>
        /// Obtiene o llena el numero de dia que se va a ejecutar la actividad.
        /// </summary>
        /// <value>
        /// numero de dias de la actividad
        /// </value>
        [Display(Name = "Cantidad de dias en que se ejecuta la actividad")]
        public int Repeticiones { get; set; }
        /// <summary>
        /// Obtiene o llena la hora del dia en que se va a ejecutar la actividad.
        /// </summary>
        /// <value>
        /// hora del del dia de la actividad 
        /// </value>
        public TimeSpan HoraAct { get; set; }
        /// <summary>
        /// Obtiene o llena si la actividad necesita de un recurso del aempresa apra ejecutarse.
        /// </summary>
        /// <value>
        /// usuarios, archivos, normas para ejecutar la actividad 
        /// </value>
        [Display(Name = "Asignación de recursos")]
        public string asigrecursos { get; set; }
        // Permite que Acummes acceda a la data
        public ICollection<AcumMes> AcumMes { get; set; }
        /// <summary>
        /// Obtiene o llena usuarios de la empresa para ejecutarse la actividad.
        /// </summary>
        /// <value>
        /// usuarios asignados a la empresa 
        /// </value>
        public ICollection<UsuariosPlandetrabajo> Usersplandetrabajo { get; set; }
        /// <summary>
        /// Obtiene o llena la periodicidad con la que se ejecutara la actividad.
        /// </summary>
        /// <value>
        /// diaria mensual semanal y dias a ejecutarse 
        /// </value>
        public ICollection<ProgamacionTareas> ProgamacionTareas { get; set; }
        /// <summary>
        ///  Obtiene o llena un boleano si la actividad se va a finalizar o no.
        /// </summary>
        /// <value>
        /// true o false
        /// </value>
        public bool Finalizada { get; set; }
    }
}