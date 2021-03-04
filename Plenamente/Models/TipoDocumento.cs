using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Plenamente.Models
{
    public class TipoDocumento
    {
        [Key]
        public int Tdoc_Id { get; set; }
        public string Tdoc_Nom { get; set; }
        public DateTime Tdoc_Registro { get; set; }
        //Nombre de la colección definido en el prural de TipoDocumento <TipoDocumentos>
        public ICollection<ApplicationUser> ApplicationUsers { get; set; }
    }
}