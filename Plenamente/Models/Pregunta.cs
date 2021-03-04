using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Plenamente.Models
{
    public class Pregunta
    {
        public Pregunta()
        {
            // Llena automaticamente el campo tipo date.
            Preg_Registro = DateTime.Now;
        }

        [Key]
        public int Preg_Id { get; set; }
        public string Preg_Titulo { get; set; }
        public DateTime Preg_Registro { get; set; }


        /*Llave Foranea a la tabla Encuesta*/
        [ForeignKey("Encuesta")]
        public int Encu_Id { get; set; }
        public Encuesta Encuesta { get; set; }

        // Permite que Respuesta acceda a la data
        public ICollection<Respuesta> Respuestas { get; set; }
        
    }
}