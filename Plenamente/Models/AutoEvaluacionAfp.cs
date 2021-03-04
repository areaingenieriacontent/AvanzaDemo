using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Plenamente.Models
{
    public class AutoEvaluacionAfp
    {
        [Key]
        public int Auevafp_Id { get; set; }
        public string Auev_Nom { get; set; }
        [DataType(DataType.Date)]
        public DateTime Auev_Inicio { get; set; }
        [DataType(DataType.Date)]
        public DateTime Auev_Fin { get; set; }
        [ForeignKey("Empresa")]
        public int Empr_Nit { get; set; }
        public Empresa Empresa { get; set; }
        public bool Finalizada { get; set; }
        //Permite que Cumplimiento acceda a la Data
        public ICollection<CumplimientoAfp> CumplimientosAfp { get; set; }
    }
}