using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Plenamente.Models
{
    public class Archivo
    {
        public Archivo()
        {
            // Genera automaticamente el campo tipo date.
            Evid_Registro = DateTime.Now;
        }

        public string Id { get; set; }
        public string Evid_Nombre { get; set; } 
        public HttpPostedFileBase Evid_Archivo { get; set; }
        public DateTime Evid_Registro { get; set; }
        public int Cump_Id { get; set; }
        //public List<Cumplimiento> Cumplimientos { get; set; }
        public int Tdca_id { get; set; }
        //public List<TipoDocCarga> TipoDocCargas { get; set; }
    }
}