using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Plenamente.Models
{
    //Esta clase adquiere los atributos del IdentityModel para poder referenciar los campos añadidos en 
    //La tabla AspNetUsers adicionando una IEnumerable de los Roles actuales del sistema
    public class ExpandedUserDTO
    {
        [Key]
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Documento { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public int? Pers_Licencia { get; set; }
        public string Pers_Cargo { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<DateTime> Pers_LicVence { get; set; }
        public string Pers_Direccion { get; set; }
        public string Pers_ContactoEmeg { get; set; }
        public int Pers_TelefonoEmeg { get; set; }
        [Display(Name = "Lockout End Date Utc")]
        public DateTime? LockoutEndDateUtc { get; set; }
        public int AccessFailedCount { get; set; }
        public string PhoneNumber { get; set; }
        public IEnumerable<UserRolesDTO> Roles { get; set; }

        public int? Tdoc_Id { get; set; }
        public List<TipoDocumento> tipoDocumento { get; set; }
        public int? Sciu_Id { get; set; }
        public List<SedeCiudad> sedeCiudad { get; set; }
        public int? Ciud_Id { get; set; }
        public List<Ciudad> ciudad { get; set; }
        public int? Cemp_Id { get; set; }
        public List<CargoEmpresa> cargoEmpresa { get; set; }
        public int? Aemp_Id { get; set; }
        public List<AreaEmpresa> areaEmpresa { get; set; }
        public int? Cate_Id { get; set; }
        public List<CateLicencia> cateLicencia { get; set; }
        public int? Gene_Id { get; set; }
        public List<Genero> genero { get; set; }
        public int? Jemp_Id { get; set; }
        public List<JornadaEmpresa> jornadaEmpresa { get; set; }
        public int? Tvin_Id { get; set; }
        public List<TipoVinculacion> tipoVinculacion { get; set; }
        public int? Eps_Id { get; set; }
        public List<Eps> eps { get; set; }
        public int? Afp_Id { get; set; }
        public List<Afp> afp { get; set; }
        public int? Arl_Id { get; set; }
        public List<Arl> arl { get; set; }
        public int? Empr_Nit { get; set; }
        public List<Empresa> Empresa { get; set; }
        public int? Espe_Id { get; set; }
        public List<EstadoPersona> estadoPersona { get; set; }
        public string Jefe_Id { get; set; }
        public List<ApplicationUser> Jefe { get; set; }
    }


    //Esta clase adquiere los roles actuales en el sistema
    public class UserRolesDTO
    {
        [Key]
        [Display(Name = "Role Name")]
        public string RoleName { get; set; }
    }

    //Esta clase asigna un Rol a un Usuario en especifico
    public class UserRoleDTO
    {
        [Key]
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        [Display(Name = "Role Name")]
        public string RoleName { get; set; }
    }

    //Esta clase permite añadir nuevos roles en el sistema identificandolos con su respectivo Id
    public class RoleDTO
    {
        [Key]
        public string Id { get; set; }
        [Display(Name = "Role Name")]
        public string RoleName { get; set; }
    }

    //Clase que trae el nombre del usuario y genera una lista con los roles actuales
    public class UserAndRolesDTO
    {
        [Key]
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        public List<UserRoleDTO> colUserRoleDTO { get; set; }
    }
}
