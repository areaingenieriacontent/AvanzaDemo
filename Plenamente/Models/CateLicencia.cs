using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Plenamente.Models
{
    public class CateLicencia
    {
        [Key]
        public int Cate_Id { get; set; }
        public string Cate_Nom { get; set; }
        public DateTime Cate_Registro { get; set; }
        //Nombre de la colección definido en el prural de CateLicencia <CateLicencias>
        public ICollection<ApplicationUser> ApplicationUsers { get; set; }
    }
}