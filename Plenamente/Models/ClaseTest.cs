using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Plenamente.Models
{
    public class ClaseTest
    {
        [Key]
        public int Ctes_Id { get; set; }
        public string Ctes_Nom { get; set; }

    }
}