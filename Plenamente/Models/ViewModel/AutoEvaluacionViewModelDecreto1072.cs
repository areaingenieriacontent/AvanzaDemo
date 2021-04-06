using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Plenamente.Models.ViewModel
{
    /// <summary>
    /// Modelo que implementa las propiedades necesarias para la autoevaluación.
    /// </summary>
    public class AutoEvaluacionViewModelDecreto1072
    {
            /// <summary>
            /// Obtiene o llena la autoevaluación.
            /// </summary>
            /// <value>
            /// La autoevaluacion de la autoevaluación.
            /// </value>
            public AutoevaluacionDecreto1072 AutoEvaluacionDecreto1072 { get; set; }
        /// <summary>
        /// Inicializa una nueva instancia de la <see cref="AutoEvaluacionViewModelDecreto1072"/> clase.
        /// </summary>
        public AutoEvaluacionViewModelDecreto1072()
            {
            AutoEvaluacionDecreto1072 = new AutoevaluacionDecreto1072();
            }
            /// <summary>
            /// Obtiene o llena el identificador.
            /// </summary>
            /// <value>
            /// The identificador de la autoevaluación.
            /// </value>
            public int Id { get => AutoEvaluacionDecreto1072.AeDecreto_Id; set => AutoEvaluacionDecreto1072.AeDecreto_Id = value; }
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
            public string NameAutoEvaluacion { get => AutoEvaluacionDecreto1072.Ae_Nom; set => AutoEvaluacionDecreto1072.Ae_Nom = value; }
            /// <summary>
            /// Obtiene o llena la fecha de inicio de la autoevaluación.
            /// </summary>
            /// <value>
            /// La fecha de inicio de la autoevaluación.
            /// </value>
            [Display(Name = "Fecha de inicio")]
            [DataType(DataType.Date)]
            public DateTime Auev_Inicio { get => AutoEvaluacionDecreto1072.Ae_Inicio; set => AutoEvaluacionDecreto1072.Ae_Inicio = value; }
            /// <summary>
            /// Obtiene o llena la fecha del fin de la autoevaluación.
            /// </summary>
            /// <value>
            /// La fecha del fin de la autoevaluación.
            /// </value>
            [Display(Name = "Fecha de finalizacion")]
            [DataType(DataType.Date)]
            public DateTime Auev_Fin { get => AutoEvaluacionDecreto1072.Ae_Fin; set => AutoEvaluacionDecreto1072.Ae_Fin = value; }
   
    }
}