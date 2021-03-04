using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Plenamente.Models.ViewModel
{
    public class ActividadesAsignadasPlanDeTrabajoViewModel
    {
        public int IdUserPlanDeTrabajoActividad { get; set; }
        public int IdPlantTrabajo { get; set; }
        [Display(Name = "Nombre de plan de trabajo")]
        public string NombrePlanTrabajo { get; set; }
        [Display(Name = "Usuarios")]
        public string IdUser { get; set; }
        public string NombreUser { get; set; }
        [Display(Name = "Actividad")]
        public int IdActiCumplimiento { get; set; }
        [Display(Name = "Descripcion actividad")]
        public string DescripcionCumplimiento { get; set; }
    }
}