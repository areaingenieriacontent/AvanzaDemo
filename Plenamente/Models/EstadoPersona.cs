using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Plenamente.Models
{
    public class EstadoPersona
    {
        [Key]
        public int Espe_Id { get; set; }
        public string Espe_Nom { get; set; }
        public DateTime Espe_Registro { get; set; }
        //Permite a persona acceder a la Data
        public ICollection<ApplicationUser> ApplicationUsers { get; set; }
    }
}