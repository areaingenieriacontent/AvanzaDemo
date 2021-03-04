using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Plenamente.Scheduler
{
    /// <summary>
    /// Permite generar una unica cita en una fecha y hora determinadas
    /// </summary>
    public class SingleSchedule : Schedule
    {
        /// <summary>
        /// Fecha de programación de la cita/reunión a generar
        /// </summary>
        public DateTime Date { get; set; }
        /// <summary>
        /// Solo cuando la fecha  (Date) pasada al parámetro coincide con la de la propiedad Fecha, el método devuelve verdadero.
        /// </summary>
        /// <param name="date">Fecha a validar la ocurrencia</param>
        /// <returns>Devuelve verdadero si la fecha es valida para la cita/reunión de lo contrario devuelve falso</returns>
        public override bool OccursOnDate(DateTime date)
        {
            return Date.Date == date;
        }
    }
}
