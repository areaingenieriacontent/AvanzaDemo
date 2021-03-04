using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Plenamente.Models.ViewModel
{
	public class EvidenciaCumplimientoViewModelafp
	{
		public EvidenciaAfp Evidencia { get; set; }
		public EvidenciaCumplimientoViewModelafp()
		{
			Evidencia = new EvidenciaAfp();
		}
		[Display(Name = "Nombre de documento")]
		public string NombreDocumento { get; set; }
		[Display(Name = "Tipo de documento")]
		public string TipoDocumento { get; set; }
		[Display(Name = "Responsable")]
		public string Responsable { get; set; }
		[Display(Name = "Fecha")]
		[DataType(DataType.Date)]
		public DateTime Fecha { get; set; }
		[Required]
		[Display(Name = "Archivo")]
		public HttpPostedFileBase Archivo { get; set; }
		public int IdCumplimiento { get; set; }
	}
}