using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Plenamente.App_Tool;

namespace Plenamente.Models
{
    public class ObjEmpresa
    {
        public ObjEmpresa()
        {
            // Llena automaticamente el campo tipo date.
            Oemp_Registro = DateTime.Now;
        }
        [Key]
        public int Oemp_Id { get; set; }
        public string Oemp_Nombre { get; set; }
        public string Oemp_Descrip { get; set; }
        public string Oemp_Meta { get; set; }
        public DateTime Oemp_Registro { get; set; }

        // Foreign Key Empresa
        public int Empr_Nit { get; set; }
        public Empresa Empresa { get; set; }
        
        // Permite a ActiCumplimientos acceder a la Data
       public ICollection<ActiCumplimiento> ActiCumplimientos { get; set; }
        

    }
}