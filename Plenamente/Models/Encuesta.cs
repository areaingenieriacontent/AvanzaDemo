using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Plenamente.Models
{
    public class Encuesta
    {
        public Encuesta()
        {
            // Llena automaticamente el campo tipo date.
            Encu_Creacion = DateTime.Now;
            Encu_Registro = DateTime.Now;
        }
        [Key]
        public int Encu_Id { get; set; }
        public string Encu_Nombre { get; set; }
        public DateTime Encu_Creacion { get; set; }
        [DataType(DataType.Date)]
        public DateTime Encu_Vence { get; set; } 
        public bool Encu_Estado { get; set; }
        public DateTime Encu_Registro { get; set; }



        /*Llave Foranea a la tabla Empresa*/
        [ForeignKey("Empresa")]
        public int Empr_Nit { get; set; }
        public Empresa Empresa { get; set; }

        // Permite que Pregunta acceda a la data
        public ICollection<Pregunta> Preguntas { get; set; }
    }
}