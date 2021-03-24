using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Plenamente.Models
{
    public class AutoevaluacionDecreto1072
    {
        [Key]
        public int AeDecreto_Id { get; set; }
        public string Ae_Nom { get; set; }
        [DataType(DataType.Date)]
        public DateTime Ae_Inicio { get; set; }
        [DataType(DataType.Date)]
        public DateTime Ae_Fin { get; set; }
        [ForeignKey("Empresa")]
        public int Empr_Nit { get; set; }
        public Empresa Empresa { get; set; }
        public bool Finalizada { get; set; }
        //Permite que Cumplimiento acceda a la Data
        public ICollection<CumplimientoDecreto1072> CumplimientoDecreto1072 { get; set; }
    }
}