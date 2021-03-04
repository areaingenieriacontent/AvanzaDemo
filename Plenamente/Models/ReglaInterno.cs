using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Plenamente.Models
{
    public class ReglaInterno
    {
        public ReglaInterno()
        {
            // Llena automaticamente el campo tipo date.
            Rint_Registro = DateTime.Now;
        }
        [Key]
        public int Rint_Id { get; set; }
        public string Rint_Archivo { get; set; }
        public string Rint_Nom { get; set; }
        //Foreign Key Empresa
        [ForeignKey("Empresa")]
        public int Empr_Nit { get; set; }
        public Empresa Empresa { get; set; }
        //Fin Foreign Key
        public DateTime Rint_Registro { get; set; }
    }
}