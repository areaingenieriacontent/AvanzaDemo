using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Plenamente.Scheduler
{
    /// <summary>
    /// permitir que se repitan citas regularmente. 
    /// La primera cita que se puede generar cae en la fecha de inicio del período de programación. 
    /// Otras citas son a intervalos regulares
    /// </summary>
    public class SimpleRepeatingSchedule : RepeatingSchedule
    {
        int _daysBetween;
        /// <summary>
        /// Define un intervalo se mide en días 
        /// </summary>
        public int DaysBetween
        {
            get
            {
                return _daysBetween=1;
            }
            set
            {
                /*if (value <= 0) throw new ArgumentException(
                    "The days between appointments must be at least one.");*/

                //_daysBetween = value;
                _daysBetween = 1;
            }
        }
        /// <summary>
        /// realiza dos verificaciones. 
        /// 1. llama al método protegido, DateIsInPeriod. 
        /// Si la fecha que se está verificando no está dentro del período de programación, el método devuelve falso. 
        /// Si está dentro del período de programación, 
        /// 2. Se llama al método privado DateIsValidForSchedule . 
        /// Esto calcula la cantidad de días entre la fecha de inicio del período de programación y la fecha que se está verificando.
        /// </summary>
        /// <param name="date">Fecha a verificar dentro de la programación</param>
        /// <returns>Devuelve verdadero si una fecha se encuentra dentro del periodo de programación
        /// de lo contrario devuelve falso</returns>
        public override bool OccursOnDate(DateTime date)
        {
            /*if (DateIsInPeriod(date))
            {*/
                return DateIsValidForSchedule(date);
           // }
            //return false;
        }
        /// <summary>
        /// Esto calcula la cantidad de días entre la fecha de inicio del período de programación y la fecha que se está verificando. 
        /// Si se trata de un múltiplo del valor DaysBetween, se debe generar una cita/reunion para esta fecha verificada.
        /// </summary>
        /// <param name="date">Fecha a validar</param>
        /// <returns>Devuelve verdadero si una fecha se encuentra dentro del periodo de programación
        /// de lo contrario devuelve falso</returns>
        private bool DateIsValidForSchedule(DateTime date)
        {
            int daysBetweenFirstAndCheckDate
                = (int)date.Subtract(SchedulingRange.Start).TotalDays;
            return daysBetweenFirstAndCheckDate % DaysBetween == 0;
        }
    }
}
