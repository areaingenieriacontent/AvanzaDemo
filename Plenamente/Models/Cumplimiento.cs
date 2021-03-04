using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace Plenamente.Models
{
    public class Cumplimiento
    {
        [Key]
        public int Cump_Id { get; set; }
        public bool Cump_Cumple { get; set; }
        public bool Cump_Nocumple { get; set; }
        public bool Cump_Justifica { get; set; }
        public bool Cump_Nojustifica { get; set; }
        public string Cump_Observ { get; set; }

        // ForeignKey
        [ForeignKey("ItemEstandar")]
        public int? Iest_Id { get; set; }
        public ItemEstandar ItemEstandar { get; set; }
        //ForeignKey
        [ForeignKey("Empresa")]
        public int? Empr_Nit { get; set; }
        public Empresa Empresa { get; set; }
        // Foreign Key
        [ForeignKey("AutoEvaluacion")]
        public int Auev_Id { get; set; }
        public AutoEvaluacion AutoEvaluacion { get; set; }

        public DateTime Cump_Registro { get; set; }

        // Permite que Acummes acceda a la data
        public ICollection<AcumMes> AcumMes { get; set; }
        // Permite que Evidencia Acceda a la Data
        public ICollection<Evidencia> Evidencias { get; set; }
        public bool Cump_NoAplica { get; set; }
    }
    public class ViewCumplimiento
    {
        public HttpPostedFileBase Cump_Evidencia { get; set; }
    }
}