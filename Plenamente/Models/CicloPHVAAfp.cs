using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Plenamente.Models
{
    public class CicloPHVAAfp
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Description { get; set; }
        public short Categoria { get; set; }

        public ICollection<CriterioAfp> CriteriosAfp { get; set; }
    }
}