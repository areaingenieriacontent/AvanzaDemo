using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Plenamente.Models.ViewModel
{
    public class PlandetrabajoActividadesViewModel
    {
        public int IdPlantTrabajo { get; set; }
        [Display(Name = "Asignación de actividades")]
        public string NombrePlanTrabajo { get; set; }
		[Required]
        [Display(Name = "Usuarios")]
        public string IdUser { get; set; }       
        [Display(Name = "Actividad")]
		[Required]
		public int IdActiCumplimiento { get; set; }
        [Display(Name = "Descripcion actividad")]
        public string DescripcionCumplimiento { get; set; }
		[Display(Name = "Fecha de inicio")]
		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
		public DateTime FechaInicio { get; set; }
		[Display(Name = "Fecha de fin")]
		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
		public DateTime FechaFin { get; set; }

	}
}