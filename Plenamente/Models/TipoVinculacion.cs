using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Plenamente.Models
{
    public class TipoVinculacion
    {
        [Key]
        public int Tvin_Id { get; set; }
        public string Tvin_Nom { get; set; }
        public DateTime Tvin_Registro { get; set; }
        //Permite a persona acceder a la Data
        public ICollection<ApplicationUser> ApplicationUsers { get; set; }
    }
}