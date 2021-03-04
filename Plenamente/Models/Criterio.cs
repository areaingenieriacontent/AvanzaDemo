using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Plenamente.Models
{
    public class Criterio
    {
        [Key]
        public int Crit_Id { get; set; }
        public string Crit_Nom { get; set; }
        public float Crit_Porcentaje { get; set; }
        public DateTime Crit_Registro { get; set; }

        // Permite a Estandar acceder a la Data
        public ICollection<Estandar> Estandars { get; set; }
        [ForeignKey("CicloPHVA")]
        public int? CicloPHVA_Id { get; set; }
        public CicloPHVA CicloPHVA { get; set; }
        public short Categoria { get; set; }
        public short CategoriaExcepcion { get; set; }
    }
}