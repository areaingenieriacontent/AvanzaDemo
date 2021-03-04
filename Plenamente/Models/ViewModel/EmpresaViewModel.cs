using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Plenamente.Models.ViewModel
{
	public class EmpresaViewModel
	{
		public int IdEmpresa { get; set; }

		[Display(Name = "Empresa")]
		public string NombreEmpresa { get; set; }

		[Display(Name = "Nro. Empleados")]
		[Required(ErrorMessage = "Debe de ingresar un valor para el número de empleados.")]
		[Range(1, Int32.MaxValue, ErrorMessage = "El valor ingresado para {0} no es válido")]
		public int NumeroEmpleados { get; set; }

	}
}