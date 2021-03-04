using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Plenamente.Models
{
    public class AutoEvaluacionn
    {
        //Se le da un nombre a cada tipo de campo para poder evocarlo en la vista de autoevaluacion uno por uno. 
        //Criterio viene de la clase/Tabla criterio y se asigna segun su orden en el formulario. (Solo contiene información)
        //Estandar viene de la clase/Tabla Estandar y se asigna segun su orden en el formulario. (Solo contiene información)
        //ItemEstandar viene de la clase/Tabla ItemEstandar y se asigna segun su orden en el formulario. (Contiene información y validación de datos tipo Bool)
        //Cumplimiento viene de la clase/tabla Cumplimiento y se asigna segun su orden el el formulario. (Contiene la ruta del archivo subido al servidor correspondiente al ItemEstandar)
        public string Query { get; set; }
        public IEnumerable<Criterio> CriteriosQuery { get; set; }
        public IEnumerable<Estandar> EstandarsQuery { get; set; }
        public IEnumerable<ItemEstandar> ItemEstandarsQuery { get; set; }
        public IEnumerable<Criterio> Criterios1 { get; set; }
        public IEnumerable<Estandar> Estandars11 { get; set; }
        public IEnumerable<ItemEstandar> ItemEstandars111 { get; set; }
        public IEnumerable<Cumplimiento> Cumplimientos111 { get; set; }
        public IEnumerable<ItemEstandar> ItemEstandars112 { get; set; }
        public IEnumerable<Cumplimiento> Cumplimientos112 { get; set; }
        public IEnumerable<ItemEstandar> ItemEstandars113 { get; set; }
        public IEnumerable<Cumplimiento> Cumplimientos113 { get; set; }
        public IEnumerable<ItemEstandar> ItemEstandars114 { get; set; }
        public IEnumerable<Cumplimiento> Cumplimientos114 { get; set; }
        public IEnumerable<ItemEstandar> ItemEstandars115 { get; set; }
        public IEnumerable<Cumplimiento> Cumplimientos115 { get; set; }
        public IEnumerable<ItemEstandar> ItemEstandars116 { get; set; }
        public IEnumerable<Cumplimiento> Cumplimientos116 { get; set; }
        public IEnumerable<ItemEstandar> ItemEstandars117 { get; set; }
        public IEnumerable<Cumplimiento> Cumplimientos117 { get; set; }
        public IEnumerable<ItemEstandar> ItemEstandars118 { get; set; }
        public IEnumerable<Cumplimiento> Cumplimientos118 { get; set; }

        public IEnumerable<Estandar> Estandars12 { get; set; }
        public IEnumerable<ItemEstandar> ItemEstandars121 { get; set; }
        public IEnumerable<Cumplimiento> Cumplimientos121 { get; set; }
        public IEnumerable<ItemEstandar> ItemEstandars122 { get; set; }
        public IEnumerable<Cumplimiento> Cumplimientos122 { get; set; }
        public IEnumerable<ItemEstandar> ItemEstandars123 { get; set; }
        public IEnumerable<Cumplimiento> Cumplimientos123 { get; set; }


        public IEnumerable<Criterio> Criterios2 { get; set; }
        public IEnumerable<Estandar> Estandars21 { get; set; }
        public IEnumerable<ItemEstandar> ItemEstandars21 { get; set; }
        public IEnumerable<Cumplimiento> Cumplimientos21 { get; set; }
        public IEnumerable<Estandar> Estandars22 { get; set; }
        public IEnumerable<ItemEstandar> ItemEstandars22 { get; set; }
        public IEnumerable<Cumplimiento> Cumplimientos22 { get; set; }
        public IEnumerable<Estandar> Estandars23 { get; set; }
        public IEnumerable<ItemEstandar> ItemEstandars23 { get; set; }
        public IEnumerable<Cumplimiento> Cumplimientos23 { get; set; }
        public IEnumerable<Estandar> Estandars24 { get; set; }
        public IEnumerable<ItemEstandar> ItemEstandars24 { get; set; }
        public IEnumerable<Cumplimiento> Cumplimientos24 { get; set; }
        public IEnumerable<Estandar> Estandars25 { get; set; }
        public IEnumerable<ItemEstandar> ItemEstandars25 { get; set; }
        public IEnumerable<Cumplimiento> Cumplimientos25 { get; set; }
        public IEnumerable<Estandar> Estandars26 { get; set; }
        public IEnumerable<ItemEstandar> ItemEstandars26 { get; set; }
        public IEnumerable<Cumplimiento> Cumplimientos26 { get; set; }
        public IEnumerable<Estandar> Estandars27 { get; set; }
        public IEnumerable<ItemEstandar> ItemEstandars27 { get; set; }
        public IEnumerable<Cumplimiento> Cumplimientos27 { get; set; }
        public IEnumerable<Estandar> Estandars28 { get; set; }
        public IEnumerable<ItemEstandar> ItemEstandars28 { get; set; }
        public IEnumerable<Cumplimiento> Cumplimientos28 { get; set; }
        public IEnumerable<Estandar> Estandars29 { get; set; }
        public IEnumerable<ItemEstandar> ItemEstandars29 { get; set; }
        public IEnumerable<Cumplimiento> Cumplimientos29 { get; set; }
        public IEnumerable<Estandar> Estandars210 { get; set; }
        public IEnumerable<ItemEstandar> ItemEstandars210 { get; set; }
        public IEnumerable<Cumplimiento> Cumplimientos210 { get; set; }
        public IEnumerable<Estandar> Estandars211 { get; set; }
        public IEnumerable<ItemEstandar> ItemEstandars211 { get; set; }
        public IEnumerable<Cumplimiento> Cumplimientos211 { get; set; }


        public IEnumerable<Criterio> Criterios3 { get; set; }
        public IEnumerable<Estandar> Estandars31 { get; set; }
        public IEnumerable<ItemEstandar> ItemEstandars311 { get; set; }
        public IEnumerable<Cumplimiento> Cumplimientos311 { get; set; }
        public IEnumerable<ItemEstandar> ItemEstandars312 { get; set; }
        public IEnumerable<Cumplimiento> Cumplimientos312 { get; set; }
        public IEnumerable<ItemEstandar> ItemEstandars313 { get; set; }
        public IEnumerable<Cumplimiento> Cumplimientos313 { get; set; }
        public IEnumerable<ItemEstandar> ItemEstandars314 { get; set; }
        public IEnumerable<Cumplimiento> Cumplimientos314 { get; set; }
        public IEnumerable<ItemEstandar> ItemEstandars315 { get; set; }
        public IEnumerable<Cumplimiento> Cumplimientos315 { get; set; }
        public IEnumerable<ItemEstandar> ItemEstandars316 { get; set; }
        public IEnumerable<Cumplimiento> Cumplimientos316 { get; set; }
        public IEnumerable<ItemEstandar> ItemEstandars317 { get; set; }
        public IEnumerable<Cumplimiento> Cumplimientos317 { get; set; }
        public IEnumerable<ItemEstandar> ItemEstandars318 { get; set; }
        public IEnumerable<Cumplimiento> Cumplimientos318 { get; set; }
        public IEnumerable<ItemEstandar> ItemEstandars319 { get; set; }
        public IEnumerable<Cumplimiento> Cumplimientos319 { get; set; }
        public IEnumerable<Estandar> Estandars32 { get; set; }
        public IEnumerable<ItemEstandar> ItemEstandars321 { get; set; }
        public IEnumerable<Cumplimiento> Cumplimientos321 { get; set; }
        public IEnumerable<ItemEstandar> ItemEstandars322 { get; set; }
        public IEnumerable<Cumplimiento> Cumplimientos322 { get; set; }
        public IEnumerable<ItemEstandar> ItemEstandars323 { get; set; }
        public IEnumerable<Cumplimiento> Cumplimientos323 { get; set; }
        public IEnumerable<Estandar> Estandars33 { get; set; }
        public IEnumerable<ItemEstandar> ItemEstandars331 { get; set; }
        public IEnumerable<Cumplimiento> Cumplimientos331 { get; set; }
        public IEnumerable<ItemEstandar> ItemEstandars332 { get; set; }
        public IEnumerable<Cumplimiento> Cumplimientos332 { get; set; }
        public IEnumerable<ItemEstandar> ItemEstandars333 { get; set; }
        public IEnumerable<Cumplimiento> Cumplimientos333 { get; set; }
        public IEnumerable<ItemEstandar> ItemEstandars334 { get; set; }
        public IEnumerable<Cumplimiento> Cumplimientos334 { get; set; }
        public IEnumerable<ItemEstandar> ItemEstandars335 { get; set; }
        public IEnumerable<Cumplimiento> Cumplimientos335 { get; set; }
        public IEnumerable<ItemEstandar> ItemEstandars336 { get; set; }
        public IEnumerable<Cumplimiento> Cumplimientos336 { get; set; }

        public IEnumerable<Criterio> Criterios4 { get; set; }
        public IEnumerable<Estandar> Estandars41 { get; set; }
        public IEnumerable<ItemEstandar> ItemEstandars411 { get; set; }
        public IEnumerable<Cumplimiento> Cumplimientos411 { get; set; }
        public IEnumerable<ItemEstandar> ItemEstandars412 { get; set; }
        public IEnumerable<Cumplimiento> Cumplimientos412 { get; set; }
        public IEnumerable<ItemEstandar> ItemEstandars413 { get; set; }
        public IEnumerable<Cumplimiento> Cumplimientos413 { get; set; }
        public IEnumerable<ItemEstandar> ItemEstandars414 { get; set; }
        public IEnumerable<Cumplimiento> Cumplimientos414 { get; set; }
        public IEnumerable<Estandar> Estandars42 { get; set; }
        public IEnumerable<ItemEstandar> ItemEstandars421 { get; set; }
        public IEnumerable<Cumplimiento> Cumplimientos421 { get; set; }
        public IEnumerable<ItemEstandar> ItemEstandars422 { get; set; }
        public IEnumerable<Cumplimiento> Cumplimientos422 { get; set; }
        public IEnumerable<ItemEstandar> ItemEstandars423 { get; set; }
        public IEnumerable<Cumplimiento> Cumplimientos423 { get; set; }
        public IEnumerable<ItemEstandar> ItemEstandars424 { get; set; }
        public IEnumerable<Cumplimiento> Cumplimientos424 { get; set; }
        public IEnumerable<ItemEstandar> ItemEstandars425 { get; set; }
        public IEnumerable<Cumplimiento> Cumplimientos425 { get; set; }
        public IEnumerable<ItemEstandar> ItemEstandars426 { get; set; }
        public IEnumerable<Cumplimiento> Cumplimientos426 { get; set; }

        public IEnumerable<Criterio> Criterios5 { get; set; }
        public IEnumerable<Estandar> Estandars51 { get; set; }
        public IEnumerable<ItemEstandar> ItemEstandars511 { get; set; }
        public IEnumerable<Cumplimiento> Cumplimientos511 { get; set; }
        public IEnumerable<ItemEstandar> ItemEstandars512 { get; set; }
        public IEnumerable<Cumplimiento> Cumplimientos512 { get; set; }

        public IEnumerable<Criterio> criterios6 { get; set; }
        public IEnumerable<Estandar> Estandars6 { get; set; }
        public IEnumerable<ItemEstandar> ItemEstandars611 { get; set; }
        public IEnumerable<Cumplimiento> Cumplimientos611 { get; set; }
        public IEnumerable<ItemEstandar> ItemEstandars612 { get; set; }
        public IEnumerable<Cumplimiento> Cumplimientos612 { get; set; }
        public IEnumerable<ItemEstandar> ItemEstandars613 { get; set; }
        public IEnumerable<Cumplimiento> Cumplimientos613 { get; set; }
        public IEnumerable<ItemEstandar> ItemEstandars614 { get; set; }
        public IEnumerable<Cumplimiento> Cumplimientos614 { get; set; }

        public IEnumerable<Criterio> Criterios7 { get; set; }
        public IEnumerable<Estandar> Estandars7 { get; set; }
        public IEnumerable<ItemEstandar> ItemEstandars711 { get; set; }
        public IEnumerable<Cumplimiento> Cumplimientos711 { get; set; }
        public IEnumerable<ItemEstandar> ItemEstandars712 { get; set; }
        public IEnumerable<Cumplimiento> Cumplimientos712 { get; set; }
        public IEnumerable<ItemEstandar> ItemEstandars713 { get; set; }
        public IEnumerable<Cumplimiento> Cumplimientos713 { get; set; }
        public IEnumerable<ItemEstandar> ItemEstandars714 { get; set; }
        public IEnumerable<Cumplimiento> Cumplimientos714 { get; set; }


        public virtual ICollection<Cumplimiento> Cumplimientos { get; set; }
        public IEnumerable<Estandar> Estandars { get; set; }
        public IEnumerable<ItemEstandar> ItemEstandars { get; set; }
     
    }    
}