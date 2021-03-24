using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Plenamente.Models
{
    public class CumplimientoDecreto1072
    {
        [Key]
        public int CumpDecreto_Id { get; set; }
        public bool Cump_Cumple { get; set; }
        public bool Cump_Nocumple { get; set; }
        public bool Cump_Justifica { get; set; }
        public bool Cump_Nojustifica { get; set; }
        public string Cump_Observ { get; set; }

        // ForeignKey
        [ForeignKey("ItemEstandarDecreto1072")]
        public int? IeDecreto_Id { get; set; }
        public ItemEstandarDecreto1072 ItemEstandarDecreto1072 { get; set; }
        //ForeignKey
        [ForeignKey("Empresa")]
        public int? Empr_Nit { get; set; }
        public Empresa Empresa { get; set; }
        // Foreign Key
        [ForeignKey("AutoevaluacionDecreto1072")]
        public int AeDecreto_Id { get; set; }
        public AutoevaluacionDecreto1072 AutoevaluacionDecreto1072 { get; set; }

        public DateTime Cump_Registro { get; set; }

        // Permite que Acummes acceda a la data
        public ICollection<AcumMes> AcumMes { get; set; }

        // Permite que Evidencia Acceda a la Data
        public ICollection<EvidenciaDecreto1072> EvidenciasDecreto1072 { get; set; }
        public bool Cump_NoAplica { get; set; }
    }
    public class ViewCumplimientoDecreto1072
    {
        public HttpPostedFileBase Cump_Evidencia { get; set; }
    }
}