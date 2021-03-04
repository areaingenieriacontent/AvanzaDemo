using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Plenamente.Models
{
    public class UsuariosPlandetrabajo
    {
        [Key]
        public int Uspl_Id { get; set; }

        //[ForeignKey("PlanTrabajo")]
        public int Plat_Id { get; set; }
        public PlandeTrabajo PlandeTrabajo { get; set; }

        //[ForeignKey("ApplicationUser")]
        public string Id { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public int Acum_Id { get; set; }
        public ActiCumplimiento actiCumplimiento { get; set; }
        [ForeignKey("empresa")]
        public int Emp_Id { get; set; }
        public Empresa empresa { get; set; }




    }
}