using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Plenamente.Models
{
    // Para agregar datos de perfil del usuario, agregue más propiedades a su clase ApplicationUser. Visite https://go.microsoft.com/fwlink/?LinkID=317594 para obtener más información.
    public class ApplicationUser : IdentityUser
    {
        //Variables que se agregar en la tabla de AspNetUsers 
        [Index(IsUnique = true)]
        public int Pers_Doc { get; set; }
        public string Pers_Nom1 { get; set; }
        public string Pers_Nom2 { get; set; }
        public string Pers_Apel1 { get; set; }
        public string Pers_Apel2 { get; set; }
        public int? Pers_Licencia { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<DateTime> Pers_LicVence { get; set; }
        public byte[] Pers_Foto { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Pers_Ingreso { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Pers_Retiro { get; set; }
        public string Pers_Dir { get; set; }
        public string Pers_Cemeg { get; set; }
        public int Pers_Temeg { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Pers_Registro { get; set; }
        public string Pers_Cargo { get; set; }
        public bool Pers_terminos { get; set; }

        //Variables que "instancian" las llaves foraneas del sistema y se crean como campos en la tabla AspNetUsers
        public int? Tdoc_Id { get; set; }
        public TipoDocumento TipoDocumento { get; set; }
        public int? Sciu_Id { get; set; }
        public SedeCiudad SedeCiudad { get; set; }
        [NotMapped]
        public int? Ciud_Id { get; set; }
        [NotMapped]
        public Ciudad Ciudad { get; set; }
        public int? Cemp_Id { get; set; }
        public CargoEmpresa CargoEmpresa { get; set; }
        public int? Aemp_Id { get; set; }
        public AreaEmpresa AreaEmpresa { get; set; }

        internal void Add(ApplicationUser applicationUser)
        {
            throw new NotImplementedException();
        }

        public int? Cate_Id { get; set; }
        public CateLicencia CateLicencia { get; set; }
        public int? Gene_Id { get; set; }
        public Genero Genero { get; set; }
        public int? Jemp_Id { get; set; }
        public JornadaEmpresa JornadaEmpresa { get; set; }
        public int? Tvin_Id { get; set; }
        public TipoVinculacion TipoVinculacion { get; set; }
        public int? Eps_Id { get; set; }
        public Eps Eps { get; set; }
        public int? Afp_Id { get; set; }
        public Afp Afp { get; set; }
        public int? Arl_Id { get; set; }
        public Arl Arl { get; set; }
        public int? Empr_Nit { get; set; }
        public Empresa Empresa { get; set; }
        public int? Espe_Id { get; set; }
        public EstadoPersona EstadoPersona { get; set; }
        public string Jefe_Id { get; set; }
        public ApplicationUser Jefe { get; set; }

        // Permite que Resultado acceda a la data
        public ICollection<Resultado> Resultados { get; set; }
        // Permite que ActiCumplimiento acceda a la data
        public ICollection<ActiCumplimiento> ActiCumplimientos { get; set; }
        //Permite a Evidencia acceder a la Data
        public ICollection<Evidencia> Evidencias { get; set; }
        //Permite a Usersplandetrabajo acceder a la Data
        public ICollection<UsuariosPlandetrabajo> Usersplandetrabajos { get; set; }
        //Permite a Notificacion acceder a la Data
        public ICollection<Notificacion> notificaciones { get; set; }
        //Permite a EvidenciaAfp acceder a la Data
        public ICollection<EvidenciaAfp> EvidenciasAfp { get; set; }
        //Permite a EvidenciasDecreto1072 acceder a la Data
        public ICollection<EvidenciaDecreto1072> EvidenciasDecreto1072 { get; set; }


        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Tenga en cuenta que el valor de authenticationType debe coincidir con el definido en CookieAuthenticationOptions.AuthenticationType
            ClaimsIdentity userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Agregar aquí notificaciones personalizadas de usuario
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
        {

        }

        public ApplicationDbContext(string connectionString)
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        //Variables que establecen las tablas a crean en la base de datos
        public DbSet<ActiCumplimiento> Tb_ActiCumplimiento { get; set; }
        public DbSet<AcumMes> Tb_Acumes { get; set; }
        public DbSet<Afp> Tb_Afp { get; set; }
        public DbSet<AreaEmpresa> Tb_AreaEmpresa { get; set; }
        public DbSet<Arl> Tb_Arl { get; set; }
        public DbSet<CargoEmpresa> Tb_CargoEmpresa { get; set; }
        public DbSet<CateLicencia> Tb_CateLicencia { get; set; }
        public DbSet<Ciudad> Tb_Ciudad { get; set; }
        public DbSet<ClaseArl> Tb_ClaseArl { get; set; }
        public DbSet<Criterio> Tb_Criterio { get; set; }
        public DbSet<Cumplimiento> Tb_Cumplimiento { get; set; }
        public DbSet<EleProteccion> Tb_EleProteccion { get; set; }
        public DbSet<Empresa> Tb_Empresa { get; set; }
        public DbSet<Encuesta> Tb_Encuesta { get; set; }
        public DbSet<EprotEmpresa> Tb_EprotEmpresa { get; set; }
        public DbSet<Eps> Tb_Eps { get; set; }
        public DbSet<EstadoPersona> Tb_EstadoPersona { get; set; }
        public DbSet<Estandar> Tb_Estandar { get; set; }
        public DbSet<Frecuencia> Tb_Frecuencia { get; set; }
        public DbSet<Genero> Tb_Genero { get; set; }
        public DbSet<ItemEstandar> Tb_ItemEstandar { get; set; }
        public DbSet<JornadaEmpresa> Tb_JornadaEmpresa { get; set; }
        public DbSet<Mes> Tb_Mes { get; set; }
        public DbSet<ObjEmpresa> Tb_ObjEmpresa { get; set; }
        public DbSet<Periodo> Tb_Periodo { get; set; }
        public DbSet<Politica> Tb_politica { get; set; }
        public DbSet<ProcactEmpresa> Tb_ProcactEmpresa { get; set; }
        public DbSet<ProcesActividad> Tb_ProcesActividad { get; set; }
        public DbSet<ReglaHigiene> Tb_ReglaHigiene { get; set; }
        public DbSet<ReglaInterno> Tb_ReglaInterno { get; set; }
        public DbSet<Respuesta> Tb_Respuesta { get; set; }
        public DbSet<Resultado> Tb_Resultado { get; set; }
        public DbSet<SedeCiudad> Tb_SedeCiudad { get; set; }
        public DbSet<TipoDocCarga> Tb_TipoDocCarga { get; set; }
        public DbSet<TipoDocumento> Tb_TipoDocumento { get; set; }
        public DbSet<TipoVinculacion> Tb_TipoVinculacion { get; set; }
        public DbSet<ZonaEmpresa> Tb_ZonaEmpresa { get; set; }
        public DbSet<Pregunta> Tb_Pregunta { get; set; }
        public DbSet<AutoEvaluacion> Tb_AutoEvaluacion { get; set; }
        public DbSet<Evidencia> Tb_Evidencia { get; set; }
        public DbSet<PlandeTrabajo> Tb_PlandeTrabajo { get; set; }
        public DbSet<UsuariosPlandetrabajo> Tb_UsersPlandeTrabajo { get; set; }
        public DbSet<Notificacion> Tb_Notificacion { get; set; }
        public DbSet<TipoEmpresa> Tb_TipoEmpresa { get; set; }
        public DbSet<CicloPHVA> Tb_CicloPHVA { get; set; }
        public DbSet<ProgamacionTareas> Tb_ProgamacionTareas { get; set; }
        public DbSet<AutoEvaluacionAfp> Tb_AutoEvaluacionAfp { get; set; }
        public DbSet<CumplimientoAfp> Tb_cumplimientoAfp { get; set; }
        public DbSet<CicloPHVAAfp> Tb_cicloPHVAAfps { get; set; }
        public DbSet<CriterioAfp> Tb_CriterioAfp { get; set; }
        public DbSet<EstandarAfp> Tb_EstandarAfp { get; set; }
        public DbSet<ItemEstandarAfp> Tb_ItemEstandarAfp { get; set; }
        public DbSet<EvidenciaAfp> Tb_EvidenciaAfp { get; set; }
        public DbSet<DocsEvidencia> Tb_DocsEvidencia { get; set; }

        //DbSet para Decreto 1072
        public DbSet<CicloPHVADecreto1072> Tb_cicloPHVADecreto1072 { get; set; }
        public DbSet<CriterioDecreto1072> Tb_CriterioDecreto1072 { get; set; }
        public DbSet<AutoevaluacionDecreto1072> Tb_AutoEvaluacionDecreto1072 { get; set; }
        public DbSet<CumplimientoDecreto1072> Tb_cumplimientoDecreto1072 { get; set; }
        public DbSet<EstandarDecreto1072> Tb_EstandarDecreto1072 { get; set; }
        public DbSet<ItemEstandarDecreto1072> Tb_ItemEstandarDecreto1072 { get; set; }
        public DbSet<EvidenciaDecreto1072> Tb_EvidenciaDecreto1072 { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            //Llave reflexiva
            modelBuilder.Entity<ApplicationUser>().
                HasOptional(u => u.Jefe).WithMany().HasForeignKey(x => x.Jefe_Id);
            //Convierte los campos tipo datetime en datetime2 para generar la compatibilidad con SQL Server
            modelBuilder.Properties<DateTime>().Configure(c => c.HasColumnType("datetime2"));

            base.OnModelCreating(modelBuilder);
        }

    }
}