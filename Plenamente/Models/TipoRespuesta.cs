using System.ComponentModel.DataAnnotations;

namespace Plenamente.Models
{
    public class TipoRespuesta
    {
        [Key]
        public int Quem_Id { get; set; }
        public string Quem_Nom { get; set; }
    }
}