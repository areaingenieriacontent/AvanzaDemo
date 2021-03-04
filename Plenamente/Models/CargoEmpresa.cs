using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Plenamente.Models
{
    public class CargoEmpresa
    {
       public CargoEmpresa ()
            {
            // Genera automaticamente el campo tipo date.
            Cemp_Registro = DateTime.Now;
        }
        [Key]
        public int Cemp_Id { get; set; }
        public string Cemp_Nom { get; set; }
        //Foreign key Para empresa
        [ForeignKey("Empresa")]
        public int Empr_Nit { get; set; }
        public Empresa Empresa { get; set; }
        public DateTime Cemp_Registro { get; set; }
        //Permite a personas acceder a la Data
        public ICollection<ApplicationUser> ApplicationUsers { get; set; }
    }
}