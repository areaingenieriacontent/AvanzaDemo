using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Plenamente.Models
{
    public class Politica
    {
        public Politica()
        {
            // Llena automaticamente el campo tipo date.
            Poli_Registro = DateTime.Now;
        }
        [Key]
        public int Poli_Id { get; set; }
        public string Poli_Archivo { get; set; }
        public string Poli_Nom { get; set; }
        //Foreign Key Empresa
        [ForeignKey("Empresa")]
        public int Empr_Nit { get; set; }
        public Empresa Empresa { get; set; }
        public DateTime Poli_Registro { get; set; }
    }
}