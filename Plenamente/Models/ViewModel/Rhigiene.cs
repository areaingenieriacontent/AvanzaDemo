using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Plenamente.Models.ViewModel
{
    public class Rhigiene
    {
        public Rhigiene()
        {
            Rhig_Registro = DateTime.Now;
        }
        public int Rhig_Id { get; set; }
        public HttpPostedFileBase Rhig_Archivo { get; set; }
        public string Rhig_Nom { get; set; }
        //Foreign Key Empresa
        public int Empr_Nit { get; set; }
        public DateTime Rhig_Registro { get; set; }
    }
}