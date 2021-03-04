using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Plenamente.Models.ViewModel
{
    public class EvidenciaDashViewModel
    {
        public DocsEvidencia DocsEvidencia { get; set; }
        public EvidenciaDashViewModel()
        {
            DocsEvidencia = new DocsEvidencia();
        }
        [Display(Name = "Nombre de documento")]
        public string NombreDocumento { get; set; }
        [Display(Name = "Tipo de documento")]
        public string TipoDocumento { get; set; }
        [Display(Name = "Descripción de documento")]
        public string DescDocumento { get; set; }
        [Display(Name = "Fecha")]
        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; }
        [Required]
        [Display(Name = "Archivo")]
        public HttpPostedFileBase Archivo { get; set; }
        [Display(Name = "Empresa")]
        public int Empresa { get; set; }
        public int IdDocsEvidencia { get; set; }

    }
}