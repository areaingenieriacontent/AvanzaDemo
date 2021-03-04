using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Plenamente.Models
{
    public class CicloPHVA
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Description { get; set; }
        public short Categoria { get; set; }
        public ICollection<Criterio> Criterios { get; set; }
    }
}