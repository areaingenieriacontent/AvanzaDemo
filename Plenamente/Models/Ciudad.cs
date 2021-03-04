using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Plenamente.Models
{
    public class Ciudad
    {
       [Key]
       public int Ciud_Id { get; set; }
        public string Ciud_Nom { get; set; }
        public DateTime Ciud_Registro { get; set; }
        //Obtencion de un coinjunto de objetos de SedeCiudad
        public ICollection<SedeCiudad> sedeCiudad { get; set; }
    }
}