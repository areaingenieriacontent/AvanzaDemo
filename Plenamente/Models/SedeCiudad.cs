using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Plenamente.Models
{
    public class SedeCiudad
    {
        public SedeCiudad()
        {
            // Llena automaticamente el campo tipo date.
            Sciu_Registro = DateTime.Now;
        }
        [Key]
        public int Sciu_Id { get; set; }
        public string Sciu_Nom { get; set; }
        // Foreign Key Ciudades
        public int Ciud_Id { get; set; }
        public Ciudad Ciudad { get; set; }
        // Foreign Key Empresa
        public int Empr_Nit { get; set; }
        public Empresa Empresa { get; set; }
        public DateTime Sciu_Registro { get; set; }

        public ICollection<ApplicationUser> ApplicationUsers { get; set; }
    }
}