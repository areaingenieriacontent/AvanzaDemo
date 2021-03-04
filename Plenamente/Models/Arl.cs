using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Plenamente.Models
{
    public class Arl
    {
        [Key]
        public int Arl_Id { get; set; }
        public string Arl_Nom { get; set; }
        public DateTime Arl_Registro { get; set; }
        //Permtite que Empresa acceda  a la Data
        public ICollection<Empresa> Empresas { get; set; }
        //Permite que persona Acceda a la Data
        public ICollection<ApplicationUser> ApplicationUsers { get; set; }
    }
}