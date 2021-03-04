using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Plenamente.Models.ViewModel
{
    public class PoliticasViewModel
    {
        public PoliticasViewModel()
        {
            // Llena automaticamente el campo tipo date.
            Poli_Registro = DateTime.Now;
        }
        public int Poli_Id { get; set; }
        public HttpPostedFileBase Poli_Archivo { get; set; }
        public string Poli_Nom { get; set; }
        public int Empr_Nit { get; set; }
        public DateTime Poli_Registro { get; set; }
    }
}