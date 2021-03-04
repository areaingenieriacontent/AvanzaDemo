using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Plenamente.Scheduler
{
    /// <summary>
    /// Clases de programación recurrente.
    /// Crear citas cada 2 meses
    /// </summary>
    public class EveryXMonthsSchedule : RepeatingSchedule
    {
        /// <summary>
        /// Representa el número del día dentro del mes para las citas
        /// </summary>
        public int DayOfMonth { get; set; }
        /// <summary>
        /// Representa la frecuencia de meses en que se debe cumplir la reunion/cita
        /// Ejemplo cada 2 meses, cada 3  meses, cada seis meses
        /// </summary>
        public int MonthsBetween { get; set; }

        /// <summary>
        /// Verificar si una fecha debe tener una cita, el número del día de la fecha se compara con la propiedad DayOfMonth
        /// </summary>
        /// <param name="date">Fecha a validar</param>
        /// <returns>Devuelve verdadero si una cita/reunión ocurre en una fecha específica
        /// de lo contrario devuelve falso</returns>
        public override bool OccursOnDate(DateTime date)
        {
            return DateIsInPeriod(date) & IsOnCorrectDate(date);
        }
        /// <summary>
        /// Verifica si una fecha es correcta dentro de la fecha establecida
        /// </summary>
        /// <param name="date">Fecha a validar si es correcta</param>
        /// <returns>Devuelve verdadero si una cita/reunión ocurre en una fecha específica
        /// de lo contrario devuelve falso</returns>
        private bool IsOnCorrectDate(DateTime date)
        {
            if (date.Date == SchedulingRange.Start.Date)
                return true;

            int totalMonths = Math.Abs((date.Month - SchedulingRange.Start.Month) + 12 * (date.Year - SchedulingRange.Start.Year));
            if (totalMonths > 0 & totalMonths % MonthsBetween == 0)
            {
                if (date.Day == DayOfMonth)
                    return true;
                else if (date.Day == DateTime.DaysInMonth(date.Year, date.Month)
                                  && DayOfMonth > date.Day)
                    return true;
            }
            return false;           
        }
    }
}