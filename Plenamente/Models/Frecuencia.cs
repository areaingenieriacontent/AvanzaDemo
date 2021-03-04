using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Plenamente.Models
{
    public class Frecuencia
    {
        public Frecuencia()
        {
            Frec_Registro = DateTime.Now;
        }
        [Key]
        public int Frec_Id { get; set; }

        [Display(Name ="Descripcion Frecuencia")]
        public string Frec_Descripcion { get; set; }
               
        [Display(Name = "Fecha de registro")]
        public DateTime Frec_Registro { get; set; }

        public bool Estado { get; set; }

        // Permite a acticumplimiento acceder a la Data
        public ICollection<ActiCumplimiento> ActiCumplimientos { get; set; }

        [ForeignKey("Empresa")]
        public int? Empr_Nit { get; set; }
        public Empresa Empresa { get; set; }
    }
}