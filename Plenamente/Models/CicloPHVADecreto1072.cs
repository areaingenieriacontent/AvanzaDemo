using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Plenamente.Models
{
    public class CicloPHVADecreto1072
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Description { get; set; }
        public short Categoria { get; set; }
        public ICollection<CriterioDecreto1072> CriteriosDecreto1072 { get; set; }
    }
}