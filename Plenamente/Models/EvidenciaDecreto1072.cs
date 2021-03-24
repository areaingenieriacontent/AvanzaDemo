using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Plenamente.Models
{
    public class EvidenciaDecreto1072
    {
        [Key]
        public int EviDecreto_Id { get; set; }
        public string Evid_Nombre { get; set; }
        public string Evid_Archivo { get; set; }
        public DateTime Evid_Registro { get; set; }
        [ForeignKey("CumplimientoDecreto1072")]
        public int CumpDecreto_Id { get; set; }
        public CumplimientoDecreto1072 CumplimientoDecreto1072 { get; set; }
        [ForeignKey("TipoDocCarga")]
        public int Tdca_id { get; set; }
        public TipoDocCarga TipoDocCarga { get; set; }
        [ForeignKey("ApplicationUser")]
        public string Responsable { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}