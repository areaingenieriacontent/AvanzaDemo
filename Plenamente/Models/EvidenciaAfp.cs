using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Plenamente.Models
{
    public class EvidenciaAfp
    {
        [Key]
        public int Evidafp_Id { get; set; }
        public string Evid_Nombre { get; set; }
        public string Evid_Archivo { get; set; }
        public DateTime Evid_Registro { get; set; }
        [ForeignKey("CumplimientoAfp")]
        public int Cumpafp_Id { get; set; }
        public CumplimientoAfp CumplimientoAfp { get; set; }
        [ForeignKey("TipoDocCarga")]
        public int Tdca_id { get; set; }
        public TipoDocCarga TipoDocCarga { get; set; }
        [ForeignKey("ApplicationUser")]
        public string Responsable { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}