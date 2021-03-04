using System;

namespace Plenamente.Models.ViewModel
{
    /// <summary>
    /// Modelo que implementa las propiedades necesarias para la entidad evento.
    /// </summary>
    public class EventViewModel
    {
        /// <summary>
        /// Obtiene o llena el identificador.
        /// </summary>
        /// <value>
        /// The identificador del evento.
        /// </value>
        public int Id { get; set; }
        /// <summary>
        /// Obtiene o llena el titulo.
        /// </summary>
        /// <value>
        /// El titulo del evento.
        /// </value>
        public string Title { get; set; }
        /// <summary>
        /// Obtiene o llena la descripción.
        /// </summary>
        /// <value>
        /// La descripción del evento.
        /// </value>
        public string Description { get; set; }
        /// <summary>
        /// Obtiene o llena el color de fondo.
        /// </summary>
        /// <value>
        /// El color de fondo.
        /// </value>
        public string BackgroundColor { get; set; }
        /// <summary>
        /// Obtiene o llena el color del borde.
        /// </summary>
        /// <value>
        /// El color del borde.
        /// </value>
        public string BorderColor { get; set; }
        /// <summary>
        /// Obtiene o llena la ruta del evento.
        /// </summary>
        /// <value>
        /// La ruta del evento.
        /// </value>
        public string EventRoute { get; set; }
        /// <summary>
        /// Obtiene o llena la fecha de inicio.
        /// </summary>
        /// <value>
        /// La fecha de inicio.
        /// </value>
        public DateTime Start { get; set; }
        /// <summary>
        /// Obtiene o llena la fecha final.
        /// </summary>
        /// <value>
        /// La fecha final.
        /// </value>
        public DateTime End { get; set; }
        /// <summary>
        /// Obtiene la fecha inicial en texto.
        /// </summary>
        /// <value>
        /// La fecha inicial en texto.
        /// </value>
        public string StartDate => Start.ToString("yyyy-MM-dd");
        /// <summary>
        /// Obtiene la fecha final en texto.
        /// </summary>
        /// <value>
        /// La fecha final en texto.
        /// </value>
        public string EndDate => End.ToString("yyyy-MM-dd");
    }
}