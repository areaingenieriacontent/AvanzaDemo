using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Plenamente.Models
{
    public class CumplimientoAfp
    {
        [Key]
        public int Cumpafp_Id { get; set; }
        public bool Cump_Cumple { get; set; }
        public bool Cump_Nocumple { get; set; }
        public bool Cump_Justifica { get; set; }
        public bool Cump_Nojustifica { get; set; }
        public string Cump_Observ { get; set; }

        // ForeignKey
        [ForeignKey("ItemEstandarAfp")]
        public int? Iest_Id { get; set; }
        public ItemEstandarAfp ItemEstandarAfp { get; set; }
        //ForeignKey
        [ForeignKey("Empresa")]
        public int? Empr_Nit { get; set; }
        public Empresa Empresa { get; set; }
        // Foreign Key
        [ForeignKey("AutoEvaluacionAfp")]
        public int Auevafp_Id { get; set; }
        public AutoEvaluacionAfp AutoEvaluacionAfp { get; set; }

        public DateTime Cump_Registro { get; set; }

        // Permite que Acummes acceda a la data
        public ICollection<AcumMes> AcumMes { get; set; }
        // Permite que Evidencia Acceda a la Data
        public ICollection<EvidenciaAfp> EvidenciasAfp { get; set; }
        public bool Cump_NoAplica { get; set; }
    }
    public class ViewCumplimientoAfp
    {
        public HttpPostedFileBase Cump_Evidencia { get; set; }
    }
}
