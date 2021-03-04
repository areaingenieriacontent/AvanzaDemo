using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Plenamente.Models
{
    public class EstandarAfp
    {
        [Key]
        public int Esta_Id { get; set; }
        public string Esta_Nom { get; set; }
        public float Esta_Porcentaje { get; set; }

        [ForeignKey("CriterioAfp")]
        public int Crit_Id { get; set; }
        public CriterioAfp CriterioAfp { get; set; }

        public DateTime Esta_Registro { get; set; }

        // Permite a ItemStandar
        public ICollection<ItemEstandarAfp> itemEstandarsAfp { get; set; }
        public short Categoria { get; set; }
        public short CategoriaExcepcion { get; set; }
    }
}