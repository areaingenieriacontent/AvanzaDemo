using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Plenamente.Models
{
    public class AutoEvaluacion
    {
        [Key]
        public int Auev_Id { get; set; }
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
        public ICollection<Cumplimiento> Cumplimientos { get; set; }
    }
}