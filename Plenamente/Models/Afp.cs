using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Plenamente.Models
{
    public class Afp
    {
        [Key]
        public int Afp_Id { get; set; }
        public string Afp_Nom { get; set; }
        public DateTime Afp_Registro { get; set; }

        public ICollection<ApplicationUser> ApplicationUsers { get; set; }
    }
}