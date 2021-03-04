using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Plenamente.Models
{
    public class DocsEvidencia
    {
        public DocsEvidencia()
        {
            File_Registro = DateTime.Now;
        }

        [Key]
        public int Devide_Id { get; set; }
        public string Devide_Nombre { get; set; }
        public string Devide_Archivo { get; set; }
        public DateTime File_Registro { get; set; }
        public string Devide_Descri { get; set; }
        // Foreign Key Empresa
        [ForeignKey("Empresa")]
        public int Empr_Nit { get; set; }
        public Empresa Empresa { get; set; }
        [ForeignKey("TipoDocCarga")]
        public int Tdca_id { get; set; }
        public TipoDocCarga TipoDocCarga { get; set; }
    }
}