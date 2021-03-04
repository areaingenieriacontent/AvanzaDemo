using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Plenamente.Models
{
    public class PreguntaRespuesta
    {
        public Respuesta respuesta { get; set; }
        public Pregunta pregunta { get; set; }
    }
}