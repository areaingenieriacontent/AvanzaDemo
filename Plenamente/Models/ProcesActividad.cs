using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Plenamente.Models
{
    public class ProcesActividad
    {
        public ProcesActividad()
        {
            // Llena automaticamente el campo tipo date.
            Pact_Registro = DateTime.Now;
        }
        [Key]
        public int Pact_Id { get; set; }
        public string Pact_Nombre { get; set; }
        public DateTime Pact_Registro { get; set; }
        // Permite a ProaActEmpresa acceder a la Data
        public ICollection<ProcactEmpresa> ProcactEmpresas { get; set; }
    }
}