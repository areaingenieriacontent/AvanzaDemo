using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Plenamente.Models
{
    public class ClaseArl
    {
        [Key]
        public int Carl_Id { get; set; }
        public string Carl_Nom { get; set; }
        public DateTime Carl_Registro { get; set; }

        // Permite que empresa accesa a sus datos
        public ICollection<Empresa> Empresas { get; set; }
    }
}