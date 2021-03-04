using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Plenamente.Models
{
    public class EnvioCorreo
    {
        public string Destino { get; set; }
        public string Asunto { set; get; }
        public string Mensaje { get; set; }

    }
}