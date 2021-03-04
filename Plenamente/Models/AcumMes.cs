using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Plenamente.Models
{
    public class AcumMes
    {
        [Key]
        public int Acme_Id { get; set; }
        // Foreign Key
        [ForeignKey("Cumplimiento")]
        public int Cump_Id { get; set; }
        public Cumplimiento Cumplimiento { get; set; }
        // Foreign Key 
        [ForeignKey("ActiCumplimiento")]
        public int Acum_Id { get; set; }
        public ActiCumplimiento ActiCumplimiento { get; set; }
        // Foreign Key
        [ForeignKey("Mes")]
        public int Mes_Id { get; set; }
        public Mes Mes { get; set; }
        [ForeignKey("CumplimientoAfp")]
        public int Cumpafp_Id { get; set; }
        public CumplimientoAfp CumplimientoAfp { get; set; }

        public DateTime Acme_Registro { get; set; }

    }
}