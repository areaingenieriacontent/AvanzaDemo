using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Plenamente.Models
{
    public class Eps
    {
        [Key]
        public int Eps_Id { get; set; }
        public string Eps_Nom { get; set; }
        public DateTime Eps_Registro { get; set; }


        public ICollection<ApplicationUser> ApplicationUsers { get; set; }
    }
}