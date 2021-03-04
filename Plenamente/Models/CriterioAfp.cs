using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Plenamente.Models
{
    public class CriterioAfp
    {
        [Key]
        public int Crit_Id { get; set; }
        public string Crit_Nom { get; set; }
        public float Crit_Porcentaje { get; set; }
        public DateTime Crit_Registro { get; set; }
        // Permite a Estandar acceder a la Data
        public ICollection<EstandarAfp> EstandarsAfp { get; set; }
        [ForeignKey("CicloPHVAAfp")]
        public int? CicloPHVA_Id { get; set; }
        public CicloPHVAAfp CicloPHVAAfp { get; set; }
        public short Categoria { get; set; }
        public short CategoriaExcepcion { get; set; }
    }
}