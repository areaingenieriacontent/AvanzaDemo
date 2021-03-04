using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Plenamente.Models
{
    public class ItemEstandar
    {
        [Key]
        public int Iest_Id { get; set; }
        public string Iest_Desc { get; set; }
        public string Iest_Verificar { get; set; }
        public float Iest_Porcentaje { get; set; }

        [ForeignKey("Estandar")]
        public int Esta_Id { get; set; }
        public Estandar Estandar { get; set; }
        public short Categoria { get; set; }
        public short CategoriaExcepcion { get; set; }
        public DateTime Iest_Peri { get; set; }
        public string Iest_Observa { get; set; }
        public DateTime Iest_Registro { get; set; }
        public string Iest_Video { get; set; }
        public string Iest_Recurso { get; set; }
        public string Iest_Rescursob { get; set; }
        public string Iest_Rescursoc { get; set; }
        public string Iest_Rescursod { get; set; }
        public string Iest_Rescursoe { get; set; }
        public string Iest_Rescursof { get; set; }
        public string Iest_MasInfo { get; set; }


        //Permite a cumplimineto acceder a la Data
        public ICollection<Cumplimiento> Cumplimientos { get; set; }
    }
}