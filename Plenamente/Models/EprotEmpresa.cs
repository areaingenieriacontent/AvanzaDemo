using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Plenamente.Models
{
	public class EprotEmpresa
	{
        [Key]
        public int Epem_Id { get; set; }
        //Foreign Key Tabla EleProteccion
        public int Empr_Nit { get; set; }
        public Empresa Empresa { get; set; }
        // Otra Foreign Key
        public int Epro_Id { get; set; }
        public EleProteccion EleProteccion { get; set; }
    }
}