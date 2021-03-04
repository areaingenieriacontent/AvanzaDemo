using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Plenamente.Scheduler
{
    /// <summary>
    /// Clase base para programaciones que pueden generar múltiples citas
    /// </summary>
    public abstract class RepeatingSchedule : Schedule
    {
        /// <summary>
        /// Rango de programación de un periodo de fechas en el cual se puede generar una cita/reunión
        /// </summary>
        public Period SchedulingRange { get; set; }
        /// <summary>
        /// Metodo protegido que verifica que una fecha esté dentro del período de programación
        /// </summary>
        /// <param name="date">Fecha a validar si se encuentra en el periodo establecido</param>
        /// <returns>Devuelve verdadero si una fecha se encuentra dentro del periodo de programación
        /// de lo contrario devuelve falso</returns>
        protected bool DateIsInPeriod(DateTime date)
        {
            return date >= SchedulingRange.Start && date <= SchedulingRange.End;
        }
    }
}
