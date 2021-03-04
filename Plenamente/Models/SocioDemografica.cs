using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//Este modelo es  SOLO DE PRUEBA PARA AUTOEVALUACION
namespace Plenamente.Models
{
    public class SocioDemografica
    {
        public IEnumerable<Criterio> Criterios { get; set; }
        public IEnumerable<Estandar> Estandars { get; set; }
        public IEnumerable<ItemEstandar> ItemEstandars { get; set; }
        public IEnumerable<Cumplimiento> Cumplimientos { get; set; }

        public IEnumerable<Criterio> Criterios1 { get; set;  }
        public IEnumerable<Estandar> Estandars1 { get; set; }
        public IEnumerable<ItemEstandar> itemEstandars1 { get; set;  }
    }
}