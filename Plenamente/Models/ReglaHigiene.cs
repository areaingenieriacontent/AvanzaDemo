using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Plenamente.Models
{
    public class ReglaHigiene
    {
        public ReglaHigiene()
        {
            // Llena automaticamente el campo tipo date.
            Rhig_Registro = DateTime.Now;
        }
        [Key]
        public int Rhig_Id { get; set; }
        public string Rhig_Archivo { get; set; }
        public string Rhig_Nom { get; set; }
        //Foreign Key Empresa
        [ForeignKey("Empresa")]
        public int Empr_Nit { get; set; }
        public Empresa Empresa { get; set; }
        //Fin Foreign Key
        public DateTime Rhig_Registro { get; set; }
    }
}