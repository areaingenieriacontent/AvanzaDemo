using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Plenamente.Models
{
    public class Periodo
    {
        public Periodo()
        {
            // Llena automaticamente el campo tipo date.
            Peri_Registro = DateTime.Now;
        }
        [Key]
        public int Peri_Id { get; set; }
        [Display(Name = "Periodo - (Año)")]
        public string Peri_Nom { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Fecha de registro")]
        public DateTime Peri_Registro { get; set; }

        //Permite que Acticumplmientos acceda a la data
        public ICollection<ActiCumplimiento> ActiCumplimientos { get; set; }
    }
}