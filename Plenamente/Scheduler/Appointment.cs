using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Plenamente.Scheduler
{
    /// <summary>
    /// Contiene información sobre citas/reuniones generadas 
    /// La información que contiene es sobre la fecha-hora y nombre de cita o reunión
    /// </summary>
    public class Appointment
    {
        //Fecha hora de cita/reunión
        public DateTime Time { get; set; }
        //Nombre de la cita/reunión
        public string Name { get; set; }
    }
}
