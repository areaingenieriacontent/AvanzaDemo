using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Plenamente.Models
{
    public class Respuesta
    {
        public Respuesta()
        {
            // Llena automaticamente el campo tipo date.
            Resp_Registro = DateTime.Now;
        }
        [Key]
        public int Resp_Id { get; set; }
        public string Resp_Nom { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Resp_Registro { get; set; }

        public string Resp_Tipo { get; set; }

        /*Llave Foranea a la tabla Pregunta*/
        [ForeignKey("Pregunta")]
        public int Preg_Id { get; set; }
        public Pregunta Pregunta { get; set; }
        public object Resp_Nomm { get; internal set; }
    }
}