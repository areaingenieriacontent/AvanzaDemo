using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Plenamente.Models
{
    public class Mes
    {
        [Key]
        public int Mes_Id { get; set; }
        public string Mes_Nom { get; set; }
        public DateTime Mes_Registro { get; set; }

        // Permite que Acummes acceda a la data
        public ICollection<AcumMes> AcumMes { get; set; }
    }
}