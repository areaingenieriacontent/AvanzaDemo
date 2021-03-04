using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Plenamente.Models
{
    public class EncuestaPersonas
    {
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public int Documento { get; set; }
        public string Email { get; set; }
        public string Cargo { get; set; }
        public string Ciudad { get; set; }
        public string idPersona { get; set; }
    }
}