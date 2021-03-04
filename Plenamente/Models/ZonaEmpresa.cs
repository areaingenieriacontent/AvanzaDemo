using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Plenamente.Models
{
    public class ZonaEmpresa
    {
        public ZonaEmpresa ()
        {
            Zemp_Registro = DateTime.Now;
        }
        [Key]
        public int Zemp_Id { get; set; }
        public string Zemp_Nom { get; set; }
        public DateTime Zemp_Registro { get; set; }

        [ForeignKey("Empresa")]
        public int Empr_Nit { get; set; }
        public Empresa Empresa { get; set; }
    }
}