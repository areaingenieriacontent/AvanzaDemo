using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Plenamente.Models
{
    public class ProgamacionTareas
    {
        public ProgamacionTareas()
        {
            Finalizada = true;
        }
        [Key]
        public int Id { get; set; }
        public string Descripcion { get; set; }
        [Display(Name = "Fecha Programación")]
        public DateTime FechaHora { get; set; }
        public DateTime FechaFinal => FechaHora.AddHours(1);
        public bool Estado { get; set; }
        [ForeignKey("ActiCumplimiento")]
        public int ActiCumplimiento_Id { get; set; }
        public bool Finalizada { get; set; }
        public ActiCumplimiento ActiCumplimiento { get; set; }
        [Display(Name = "Fecha Ejecución")]
        public DateTime Fechaeje { get; set; }
    }
}