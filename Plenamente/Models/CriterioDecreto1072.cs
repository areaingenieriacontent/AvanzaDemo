using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Plenamente.Models
{
    public class CriterioDecreto1072
    {
        [Key]
        public int Crit_Id { get; set; }
        public string Crit_Nom { get; set; }
        public float Crit_Porcentaje { get; set; }
        public DateTime Crit_Registro { get; set; }

        // Permite a Estandar acceder a la Data
        public ICollection<EstandarDecreto1072> EstandarDecreto1072 { get; set; }
        [ForeignKey("CicloPHVADecreto1072")]
        public int? CicloDecreto1072_Id { get; set; }
        public CicloPHVADecreto1072 CicloPHVADecreto1072 { get; set; }
        public short Categoria { get; set; }
        public short CategoriaExcepcion { get; set; }
    }
}