using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Plenamente.Scheduler
{
    /// <summary>
    /// clase base abstracta para todas las clases de reglas de programación de citas o tareas.
    /// Todas las nuevas reglas de programación de citos o tareas debe utilizar esta clase abstracta
    /// </summary>
    public abstract class Schedule
    {
        /// <summary>
        /// Hora del día
        /// </summary>
        public TimeSpan TimeOfDay { get; set; }
        /// <summary>
        /// Nombre de la cita o reunión
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Dia y hora en la que tiene ocurrencia la cita o reunión
        /// </summary>
        /// <param name="date"></param>
        /// <returns>Devuelve verdadero si una fecha se encuentra dentro del periodo de programación de la subclase
        /// de lo contrario devuelve falso</returns>
        public abstract bool OccursOnDate(DateTime date);
    }
}
