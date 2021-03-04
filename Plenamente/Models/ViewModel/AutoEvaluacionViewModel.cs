using System;
using System.ComponentModel.DataAnnotations;

namespace Plenamente.Models.ViewModel
{
    /// <summary>
    /// Modelo que implementa las propiedades necesarias para la autoevaluación.
    /// </summary>
    public class AutoEvaluacionViewModel
    {
        /// <summary>
        /// Obtiene o llena la autoevaluación.
        /// </summary>
        /// <value>
        /// La autoevaluacion de la autoevaluación.
        /// </value>
        public AutoEvaluacion AutoEvaluacion { get; set; }
        /// <summary>
        /// Inicializa una nueva instancia de la <see cref="AutoEvaluacionViewModel"/> clase.
        /// </summary>
        public AutoEvaluacionViewModel()
        {
            AutoEvaluacion = new AutoEvaluacion();
        }
        /// <summary>
        /// Obtiene o llena el identificador.
        /// </summary>
        /// <value>
        /// The identificador de la autoevaluación.
        /// </value>
        public int Id { get => AutoEvaluacion.Auev_Id; set => AutoEvaluacion.Auev_Id = value; }
        /// <summary>
        /// Obtiene o llena el identificador incremental.
        /// </summary>
        /// <value>
        /// El identificador incremental de la autoevaluación.
        /// </value>
        [Display(Name = "No")]
        public int IdentificadorIncremental { get; set; }
        /// <summary>
        /// Obtiene o llena el nombre de la autoevaluación.
        /// </summary>
        /// <value>
        /// El nombre de la autoevaluación.
        /// </value>
        [Display(Name = "Nombre")]
        public string NameAutoEvaluacion { get => AutoEvaluacion.Auev_Nom; set => AutoEvaluacion.Auev_Nom = value; }
        /// <summary>
        /// Obtiene o llena la fecha de inicio de la autoevaluación.
        /// </summary>
        /// <value>
        /// La fecha de inicio de la autoevaluación.
        /// </value>
        [Display(Name = "Fecha de inicio")]
        [DataType(DataType.Date)]
        public DateTime Auev_Inicio { get => AutoEvaluacion.Auev_Inicio; set => AutoEvaluacion.Auev_Inicio = value; }
        /// <summary>
        /// Obtiene o llena la fecha del fin de la autoevaluación.
        /// </summary>
        /// <value>
        /// La fecha del fin de la autoevaluación.
        /// </value>
        [Display(Name = "Fecha de finalizacion")]
        [DataType(DataType.Date)]
        public DateTime Auev_Fin { get => AutoEvaluacion.Auev_Fin; set => AutoEvaluacion.Auev_Fin = value; }
    }
}