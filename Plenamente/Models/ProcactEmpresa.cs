using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Plenamente.Models
{
    public class ProcactEmpresa
    {

        [Key]
        public int Paem_Id { get; set; }
        //ForeignkEY Empresa
        [ForeignKey("Empresa")]
        public int Empr_Nit { get; set; }
        public Empresa Empresa { get; set; }
        //Foreign key de la tabla Procesactividad 
        [ForeignKey("ProcesActividad")]
        public int Pact_Id { get; set; }
        public ProcesActividad ProcesActividad { get; set; }
        //Fin Foreign Key 
        public DateTime Paem_Registro { get; set; }
    }
}