using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Plenamente.Models
{
    public class JornadaEmpresa
    {
        [Key]
        public int Jemp_Id { get; set; }
        public string Jemp_Nom { get; set; }
        //Foreign key Para empresa
        public int Empr_Nit { get; set; }
        public Empresa Empresa { get; set; }
        public DateTime Jemp_Registro { get; set; }

        //Permite a personas acceder a la Data
        public ICollection<ApplicationUser> ApplicationUsers { get; set; }
    }
}