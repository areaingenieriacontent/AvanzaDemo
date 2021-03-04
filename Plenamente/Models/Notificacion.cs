using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Plenamente.Models
{
    public class Notificacion
    {
        [Key]
        public int Noti_Id { get; set; }
        public string Noti_Url { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime Noti_Inicio { get; set; }
        public bool Noti_Leido { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}