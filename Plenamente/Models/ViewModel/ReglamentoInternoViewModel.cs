using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Plenamente.Models.ViewModel
{
    public class ReglamentoInternoViewModel
    {
        public ReglamentoInternoViewModel()
        {
            // Llena automaticamente el campo tipo date.
            Rint_Registro = DateTime.Now;
        }
        public int Rint_Id { get; set; }
        public HttpPostedFileBase Rint_Archivo { get; set; }
        public string Rint_Nom { get; set; }
        public int Empr_Nit { get; set; }
        public DateTime Rint_Registro { get; set; }
    }
}