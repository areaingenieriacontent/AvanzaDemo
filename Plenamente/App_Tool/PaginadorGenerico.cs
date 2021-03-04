using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Plenamente.Models.ViewModel;

namespace Plenamente.App_Tool
{
    /// <summary>
    /// clase encargada de la paginacion estandar ,
    /// PaginaActual=pagina en donde se encuentra actualemnte el usuario,
    /// RegistroPorPagina=cantidad de objetos a paginar por pagina,
    /// TotalRegistros=total de objetos listados,
    /// TotalPaginas=total paginas que se paginaron segun la lista de objetos obtenidos 
    /// Resutaldo = de clase T que puede ser cualquier objeto, este objeto es el que sera paginado
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PaginadorGenerico<T> where T : class
    {
        public int PaginaActual { get; set; }
        public int RegistrosPorPagina { get; set; }
        public int TotalRegistros { get; set; }
        public int TotalPaginas { get; set; }
        public IEnumerable<T> Resultado { get; set; }
		
	}
}