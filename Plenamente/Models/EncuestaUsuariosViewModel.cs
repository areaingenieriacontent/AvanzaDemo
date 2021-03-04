using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Plenamente.Models.ViewModel
{

        //Modelo para la obtención y envio de preguntas y respuestas 
        public class RespuestaViewModel
        {
        public int Resp_Id { get; set; }
        public string Resp_Nom { get; set; }
        public DateTime Resp_Registro { get; set; }
        public string Resp_Tipo { get; set; }
        public int Preg_Id { get; set; }
        }
        public class PreguntaViewModel
        {
            public int Preg_Id { get; set; }
            public string Preg_Titulo { get; set; }
            public DateTime Preg_Registro { get; set; }
            public int Encu_Id { get; set; }
        public List<RespuestaViewModel> Respuesta { get; set; }

    }
}