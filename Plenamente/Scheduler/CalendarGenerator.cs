using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Plenamente.Scheduler
{
    /// <summary>
    /// biblioteca de clase que tiene la capacidad de crear horarios y generar listas de citas que se encuentran entre dos fechas. 
    /// Clase responsable de crear una lista de citas por un período usando las reglas de uno o más programas establecidos en las subclases.
    /// </summary>
    public class CalendarGenerator
    {
        /// <summary>
        /// se utiliza para generar listas de citas usando reglas definidas en uno o más horarios
        /// </summary>
        /// <param name="period">Representa un período de tiempo-especifica el período para el cual queremos calcular las fechas de las citas</param>
        /// <param name="schedules">Información del evento a programar.
        /// Representa una secuencia de programaciones
        /// </param>
        /// <returns>Listado de cita/reuniones validas para el perido de tiempo establecido</returns>
        public IEnumerable<Appointment> GenerateCalendar(Period period, IEnumerable<Schedule> schedules)
        {
            var appointments = new List<Appointment>();
            for (DateTime checkDate = period.Start; checkDate <= period.End; checkDate = checkDate.AddDays(1))
            {
                AddAppointmentsForDate(checkDate, schedules, appointments);
            }
            return appointments.OrderBy(a => a.Time);
        }
        /// <summary>
        /// Acepta una fecha, el conjunto de horarios y la lista de citas. 
        /// Procesa cada una de las programaciones
        /// utiliza el método OccursOnDate de la programación para determinar si se requiere una cita para esa programación en la fecha proporcionada.
        /// </summary>
        /// <param name="checkDate">Fecha</param>
        /// <param name="schedules">Conjunto de horarios</param>
        /// <param name="appointments">Lista de citas o reuniones</param>
        private void AddAppointmentsForDate(DateTime checkDate, IEnumerable<Schedule> schedules, List<Appointment> appointments)
        {
            foreach (Schedule schedule in schedules)
            {
                if (schedule.OccursOnDate(checkDate))
                {
                    appointments.Add(GenerateAppointment(checkDate, schedule));
                }
            }
        }
        /// <summary>
        /// Devuelve un nuevo objeto de cita/reunión con el nombre del programa suministrado
        /// </summary>
        /// <param name="checkDate">Hora de cita o reunion</param>
        /// <param name="schedule">Hora de programacion</param>
        /// <returns>Listado de objetos de cita/reunión</returns>
        private Appointment GenerateAppointment(DateTime checkDate, Schedule schedule)
        {
            return new Appointment
            {
                Name = schedule.Name,
                Time = checkDate.Add(schedule.TimeOfDay)
            };
        }
    }
}
