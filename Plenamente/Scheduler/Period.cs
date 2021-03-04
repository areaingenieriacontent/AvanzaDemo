using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Plenamente.Scheduler
{
    /// <summary>
    /// Representa un período de tiempo, que comienza en una fecha y termina en otra. Los periodos no incluyen elementos de tiempo
    /// </summary>
    public class Period
    {
        /// <summary>
        /// Fecha de inicio de programación
        /// </summary>
        public DateTime Start { get; private set; }

        /// <summary>
        /// Fecha fin de programación
        /// </summary>
        public DateTime End { get; private set; }

        /// <summary>
        /// Constructor con fecha de inicio y fecha final de programación
        /// </summary>
        /// <param name="start">Fecha de inicio de periodo</param>
        /// <param name="end">Fecha final de periodo</param>
        public Period(DateTime start, DateTime end)
        {
            Start = start.Date;
            End = end.Date;

            /*if (Start > End)
            {
                throw new ArgumentException("The start date may not be after the end date.");
            }*/
        }
    }
}
