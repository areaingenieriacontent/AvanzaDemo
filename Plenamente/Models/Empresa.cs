using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Plenamente.Models
{
    public class Empresa
    {
        [Key]
        public int Empr_Nit { get; set; }
        [Display(Name = "Nombre Empresa")]
        public string Empr_Nom { get; set; }
        public string Empr_Dir { get; set; }
        // ForeignKey ARL
        public int Arl_Id { get; set; }
        public Arl Arl { get; set; }
        //ForeignKey ClaseArl
        public int Carl_Id { get; set; }
        public ClaseArl ClaseArl { get; set; }
        public int Empr_Afiarl { get; set; }
        public int Empr_Ttrabaja { get; set; }
        public int Empr_Itrabaja { get; set; }
        public string Empr_telefono { get; set; }
        public DateTime Empr_Registro { get; set; }
        //Index(IsUnique = true)]
        public int Empr_NewNit { get; set; }
        public int Empr_NivRies { get; set; }
        public int Sucursales { get; set; }
        public string Sector_Economico { get; set; }
        public float Indice_Siniestralidad { get; set; }
        public int Prima_Cotizacion { get; set; }
        public string Empr_RepresentanteLegal { get; set; }
        public string Empr_CargoRepresentante { get; set; }
        public int Empre_RepresentanteDoc { get; set; }
        public string Empr_ResponsableSST { get; set; }
        public int Empre_ResponsableDoc { get; set; }


        // Permite que Cargo Empresa acceda a la data
        public ICollection<CargoEmpresa> CargoEmpresas { get; set; }
        // Permite que Persona acceda a la Data
        public ICollection<ApplicationUser> ApplicationUsers { get; set; }
        // Permite que SedeCiudad Acceda a la Data
        public ICollection<SedeCiudad> SedeCiudades { get; set; }
        // Permite que Jornada Empresa Acceda a la Data
        public ICollection<JornadaEmpresa> JornadaEmpresas { get; set; }
        // Permite que Area Empresa Acceda a la Data
        public ICollection<AreaEmpresa> AreaEmpresas { get; set; }
        // Permite a Politica Acceder a la Data
        public ICollection<Politica> Politicas { get; set; }
        // Permite a proactempresa Acceder a la Data
        public ICollection<ProcactEmpresa> ProcactEmpresas { get; set; }
        // Permite a ReglaHigiene Acceder a la Data
        public ICollection<ReglaHigiene> ReglaHigienes { get; set; }
        // Permite a ReglaInterno Aceeder a la Data
        public ICollection<ReglaInterno> ReglaInternos { get; set; }
        // Permite a ObjEmpresa accede a la Data
        public ICollection<ObjEmpresa> ObjEmpresas { get; set; }
        // Permite a Encuesta Aceeder a la Data
        public ICollection<Encuesta> Encuestas { get; set; }
        // Permit a Acticumplimiento acceder a la Data 
        public ICollection<ActiCumplimiento> ActiCumplimientos { get; set; }
        //Permite a cumplimineto acceder a la Data
        public ICollection<Cumplimiento> Cumplimientos { get; set; }
        // Permite a Zonaempresa acceder a la Data
        public ICollection<ZonaEmpresa> ZonaEmpresas { get; set; }
        // Permite a EportEmpresa acceder a la Data
        public ICollection<EprotEmpresa> EprotEmpresas { get; set; }
        // Permite a AutoEvaluacion acceder a la Data
        public ICollection<AutoEvaluacion> AutoEvaluaciones { get; set; }
        // Permite a CumplimientoAfp
        public ICollection<CumplimientoAfp> CumplimientosAfp { get; set; }
        // Permite a AutoEvaluacion Afp acceder a la Data
        public ICollection<AutoEvaluacionAfp> AutoEvaluacionesAfp { get; set; }

        //[DefaultValue(1)]
        //public short? TipoEmpresa_Id { get; set; }
        public virtual TipoEmpresa TipoEmpresa { get; set; }
    }
}