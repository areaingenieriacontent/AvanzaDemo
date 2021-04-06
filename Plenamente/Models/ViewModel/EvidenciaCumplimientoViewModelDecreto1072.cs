using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Plenamente.Models.ViewModel
{
	/// <summary>
	/// Modelo de vista que implementa las propiedades necesarias para entidad de Evidencias en el Decreto1072.
	/// </summary>
	public class EvidenciaCumplimientoViewModelDecreto1072
    {
		/// <summary>
		/// </summary>
		public EvidenciaDecreto1072 EvidenciasDecreto1072 { get; set; }
		/// <summary>
		/// </summary>
		public EvidenciaCumplimientoViewModelDecreto1072()
		{
			EvidenciasDecreto1072 = new EvidenciaDecreto1072();
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
		public int IdCumplimientoDecreto1072 { get; set; }
	}
}