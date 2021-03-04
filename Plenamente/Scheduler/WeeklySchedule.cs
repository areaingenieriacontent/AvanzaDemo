using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Plenamente.Scheduler
{
    /// <summary>
    /// Clases de programación recurrente.
    /// Permite seleccionar cualquier combinación de los siete días de la semana y crear citas solo en esos días
    /// </summary>
    public class WeeklySchedule : RepeatingSchedule
    {
        /// <summary>
        /// Listado de los dias de la semana
        /// </summary>
        List<DayOfWeek> _days;

        /// <summary>
        /// Establece el Listado de los dias de la semana, para el cual se desea realizar programacion de citas/reuniones
        /// </summary>
        /// <param name="days">Listado de dias de la semana a generar cita/reunión</param>
        public void SetDays(IEnumerable<DayOfWeek> days)
        {
            _days = days.Distinct().ToList();
        }
        /// <summary>
        /// Verifica que el día de la semana de la fecha suministrada esté presente en la lista 
        /// y que la fecha esté dentro del período de programación
        /// </summary>
        /// <param name="date">Fecha a validar</param>
        /// <returns>Devuelve verdadero si una cita/reunión ocurre en una fecha específica
        /// de lo contrario devuelve falso</returns>
        public override bool OccursOnDate(DateTime date)
        {
            return DateIsInPeriod(date) && _days.Contains(date.DayOfWeek);
        }
    }
}
