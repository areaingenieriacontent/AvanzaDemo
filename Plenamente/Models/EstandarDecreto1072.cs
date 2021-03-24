using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Plenamente.Models
{
    public class EstandarDecreto1072
    {
        [Key]
        public int Esta_Id { get; set; }
        public string Esta_Nom { get; set; }
        public float Esta_Porcentaje { get; set; }

        [ForeignKey("CriterioDecreto1072")]
        public int Crit_Id { get; set; }
        public CriterioDecreto1072 CriterioDecreto1072 { get; set; }

        public DateTime Esta_Registro { get; set; }

        // Permite a ItemStandar
        public ICollection<ItemEstandarDecreto1072> itemEstandarDecreto1072 { get; set; }
        public short Categoria { get; set; }
        public short CategoriaExcepcion { get; set; }
    }
}