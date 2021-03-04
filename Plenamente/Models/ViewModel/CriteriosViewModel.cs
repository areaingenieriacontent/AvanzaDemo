using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Plenamente.Models.ViewModel
{
    /// <summary>
    /// Modelo de vista que implementa las propiedades necesarias para entidad de ciclo PHVA (Planificar, Hacer, Verificar, Actuar).
    /// </summary>
    public class CicloPHVAViewModel
    {
        /// <summary>
        /// Obtiene o llena el identificador.
        /// </summary>
        /// <value>
        /// El identificador de los ciclos PHVA.
        /// </value>
        public int Id { get; set; }
        /// <summary>
        /// Obtiene o llena el nombre.
        /// </summary>
        /// <value>
        /// El nombre de los ciclos PHVA.
        /// </value>
        public string Nombre { get; set; }
        /// <summary>
        /// Obtiene o llena la description.
        /// </summary>
        /// <value>
        /// La description de los ciclos PHVA.
        /// </value>
        public string Description { get; set; }
        /// <summary>
        /// Obtiene o llena los criterios.
        /// </summary>
        /// <value>
        /// Los criterios asociados a los ciclos PHVA.
        /// </value>
        public List<CriteriosViewModel> Criterios { get; set; }
    }
    /// <summary>
    /// Modelo de vista que implementa las propiedades necesarias para entidad criterios de ciclo PHVA.
    /// </summary>
    public class CriteriosViewModel
    {
        /// <summary>
        /// Obtiene o llena el identificador.
        /// </summary>
        /// <value>
        /// El identificador de los criterios de ciclo PHVA.
        /// </value>
        public int Id { get; set; }
        /// <summary>
        /// Obtiene o llena el nombre.
        /// </summary>
        /// <value>
        /// El nombre de los criterios de ciclo PHVA.
        /// </value>
        public string Nombre { get; set; }
        /// <summary>
        /// Obtiene o llena el porcentaje.
        /// </summary>
        /// <value>
        /// El porcentaje de los criterios de ciclo PHVA.
        /// </value>
        public float Porcentaje { get; set; }
        /// <summary>
        /// Obtiene o llena el registro.
        /// </summary>
        /// <value>
        /// El registro de los criterios de ciclo PHVA.
        /// </value>
        public DateTime Registro { get; set; }
        /// <summary>
        /// Obtiene o llena el estandares.
        /// </summary>
        /// <value>
        /// El estandares de los criterios de ciclo PHVA.
        /// </value>
        public List<EstandaresViewModel> Estandares { get; set; }
    }
    /// <summary>
    /// Modelo de vista que implementa las propiedades necesarias para entidad estandares de los criterios.
    /// </summary>
    public class EstandaresViewModel
    {
        /// <summary>
        /// Obtiene o llena el identificador.
        /// </summary>
        /// <value>
        /// El identificador de los estandares de los criterios.
        /// </value>
        public int Id { get; set; }
        /// <summary>
        /// Obtiene o llena el nombre.
        /// </summary>
        /// <value>
        /// El nombre de los estandares de los criterios.
        /// </value>
        public string Nombre { get; set; }
        /// <summary>
        /// Obtiene o llena el porcentaje.
        /// </summary>
        /// <value>
        /// El porcentaje de los estandares de los criterios.
        /// </value>
        public float Porcentaje { get; set; }
        /// <summary>
        /// Obtiene o llena el registro.
        /// </summary>
        /// <value>
        /// El registro de los estandares de los criterios.
        /// </value>
        public DateTime Registro { get; set; }
        /// <summary>
        /// Obtiene o llena el elementos.
        /// </summary>
        /// <value>
        /// El elementos de los estandares de los criterios.
        /// </value>
        public List<ElementoViewModel> Elementos { get; set; }
    }
    /// <summary>
    /// Modelo de vista que implementa las propiedades necesarias para entidad elementos o items del estandar.
    /// </summary>
    public class ElementoViewModel
    {
        /// <summary>
        /// Obtiene o llena el identificador.
        /// </summary>
        /// <value>
        /// El identificador del elemento o item del estandar.
        /// </value>
        public int Id { get; set; }
        /// <summary>
        /// Obtiene o llena el descripcion.
        /// </summary>
        /// <value>
        /// La descripcion del elemento o item del estandar.
        /// </value>
        public string Descripcion { get; set; }
        /// <summary>
        /// Obtiene o llena el verificar.
        /// </summary>
        /// <value>
        /// El verificar.
        /// </value>
        public string Verificar { get; set; }
        /// <summary>
        /// Obtiene o llena el porcentaje.
        /// </summary>
        /// <value>
        /// El porcentaje.
        /// </value>
        public float Porcentaje { get; set; }
        /// <summary>
        /// Obtiene o llena el periodo.
        /// </summary>
        /// <value>
        /// El periodo del elemento o item del estandar.
        /// </value>
        public DateTime Periodo { get; set; }
        /// <summary>
        /// Obtiene o llena el observaciones.
        /// </summary>
        /// <value>
        /// Las observaciones del elemento o item del estandar.
        /// </value>
        public string Observaciones { get; set; }
        /// <summary>
        /// Obtiene o llena el registro.
        /// </summary>
        /// <value>
        /// El registro del elemento o item del estandar.
        /// </value>
        public DateTime Registro { get; set; }
        /// <summary>
        /// Obtiene o llena el video.
        /// </summary>
        /// <value>
        /// El video del elemento o item del estandar.
        /// </value>
        public string Video { get; set; }
        /// <summary>
        /// Obtiene o llena el recurso.
        /// </summary>
        /// <value>
        /// El recurso del elemento o item del estandar.
        /// </value>
        public string Recurso { get; set; }
        /// <summary>
        /// Obtiene o llena el reurso (b.).
        /// </summary>
        /// <value>
        /// El reurso (b.) del elemento o item del estandar.
        /// </value>
        public string Reursob { get; set; }
        /// <summary>
        /// Obtiene o llena el reurso (c.).
        /// </summary>
        /// <value>
        /// El reurso (c.) del elemento o item del estandar.
        /// </value>
        public string Reursoc { get; set; }
        /// <summary>
        /// Obtiene o llena el reurso (d.).
        /// </summary>
        /// <value>
        /// El reurso (d.) del elemento o item del estandar.
        /// </value>
        public string Reursod { get; set; }
        /// <summary>
        /// Obtiene o llena el reurso (e.).
        /// </summary>
        /// <value>
        /// El reurso (e.) del elemento o item del estandar.
        /// </value>
        public string Reursoe { get; set; }
        /// <summary>
        /// Obtiene o llena el reurso (f.).
        /// </summary>
        /// <value>
        /// El reurso (f.) del elemento o item del estandar.
        /// </value>
        public string Reursof { get; set; }
        /// <summary>
        /// Obtiene o llena el categoria.
        /// </summary>
        /// <value>
        /// El categoria del elemento o item del estandar.
        /// </value>
        public short Categoria { get; set; }
        /// <summary>
        /// Obtiene o llena el cumplimientos.
        /// </summary>
        /// <value>
        /// Los cumplimientos del elemento o item del estandar.
        /// </value>
        public List<Cumplimiento> Cumplimientos { get; set; }
        /// <summary>
        /// Obtiene el valor que indica si [existe un cumplimiento].
        /// </summary>
        /// <value>
        ///   <c>Verdadero</c> si [existe el cumplimiento]; Otra forma, <c>Falso</c> en el elemento o item del estandar.
        /// </value>
        public bool ExisteCumplimiento => Cumplimientos.Count() > 0;
        /// <summary>
        /// Obtiene o llena el mas informacion.
        /// </summary>
        /// <value>
        /// La informacion adicional del elemento o item del estandar.
        /// </value>
        public string MasInformacion { get; set; }
    }
    /// <summary>
    /// Modelo de vista que implementa las propiedades necesarias para entidad cumplimiento.
    /// </summary>
    public class CumplimientoViewModel
    {
        /// <summary>
        /// Initializa una instancia de la <see cref="CumplimientoViewModel"/> clase.
        /// </summary>
        public CumplimientoViewModel() { }
        /// <summary>
        /// Obtiene o llena el identificador.
        /// </summary>
        /// <value>
        /// El identificador del cumplimiento.
        /// </value>
        public int Id { get; set; }
        /// <summary>
        /// Obtiene el valor que indica si [cumple con el cumplimiento].
        /// </summary>
        /// <value>
        ///   <c>Verdadero</c> si [cumple con el cumplimiento]; Otra forma, <c>Falso</c>.
        /// </value>
        [Display(Name = "Cumple")]
        public bool Cumple { get; set; }
        /// <summary>
        /// Obtiene el valor que indica si [no cumple] el cumplimiento.
        /// </summary>
        /// <value>
        ///   <c>Verdadero</c> si [no cumple]; Otra forma, <c>Falso</c> el cumplimiento. 
        /// </value>
        [Display(Name = "No cumple")]
        public bool Nocumple { get; set; }
        /// <summary>
        /// Obtiene el valor que indica si [no aplica] para el cumplimiento.
        /// </summary>
        /// <value>
        ///   <c>Verdadero</c> si [no aplica]; Otra forma, <c>Falso</c> para el cumplimiento.
        /// </value>
        [Display(Name = "No aplica")]
        public bool NoAplica { get; set; }
        /// <summary>
        /// Obtiene el valor que indica si [justifica] el cumplimiento.
        /// </summary>
        /// <value>
        ///   <c>Verdadero</c> si [justifica]; Otra forma, <c>Falso</c> el cumplimiento.
        /// </value>
        [Display(Name = "Justifica")]
        public bool Justifica { get; set; }
        /// <summary>
        /// Obtiene el valor que indica si [no justifica] el cumplimiento.
        /// </summary>
        /// <value>
        ///   <c>Verdadero</c> si [no justifica]; Otra forma, <c>Falso</c> el cumplimiento.
        /// </value>
        [Display(Name = "No justifica")]
        public bool Nojustifica { get; set; }
        /// <summary>
        /// Obtiene o llena las observaciones.
        /// </summary>
        /// <value>
        /// Las observaciones.
        /// </value>
        [Required(ErrorMessage = "Las observaciones son requeridas.")]
        [StringLength(256)]
        [Display(Name = "Observaciones")]
        public string Observaciones { get; set; }
        /// <summary>
        /// Obtiene o llena el item estandar identificador.
        /// </summary>
        /// <value>
        /// El item estandar identificador.
        /// </value>
        public int? ItemEstandarId { get; set; }
        /// <summary>
        /// Obtiene o llena el item estandar.
        /// </summary>
        /// <value>
        /// El item estandar del cumplimiento.
        /// </value>
        public ElementoViewModel ItemEstandar { get; set; }
        /// <summary>
        /// Obtiene o llena el nit.
        /// </summary>
        /// <value>
        /// El nit de la empresa.
        /// </value>
        public int? Nit { get; set; }
        /// <summary>
        /// Obtiene o llena el identificador de la autoevaluacion.
        /// </summary>
        /// <value>
        /// El identificador de la autoevaluacion.
        /// </value>
        public int AutoEvaluacionId { get; set; }
        /// <summary>
        /// Obtiene o llena la fecha del registro.
        /// </summary>
        /// <value>
        /// La fecha del registro.
        /// </value>
        public DateTime Registro { get; set; }
        /// <summary>
        /// Obtiene o llena el acum mes.
        /// </summary>
        /// <value>
        /// El acum mes.
        /// </value>
        public List<AcumMes> AcumMes { get; set; }
        /// <summary>
        /// Obtiene o llena las evidencias del cumplimiento.
        /// </summary>
        /// <value>
        /// Las evidencias del cumplimiento.
        /// </value>
        public List<Evidencia> Evidencias { get; set; }
    }
}