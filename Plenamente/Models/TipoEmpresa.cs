using System.ComponentModel.DataAnnotations;

namespace Plenamente.Models
{
    public class TipoEmpresa
    {
        [Key]
        public short Id { get; set; }
        [StringLength(250)]
        public string Decripcion { get; set; }
        public short RangoMinimoTrabajadores { get; set; }
        public short RangoMaximoTrabajadores { get; set; }
        [StringLength(250)]
        public string NivelesRiesgo { get; set; }
        public short Categoria { get; set; }
    }
}