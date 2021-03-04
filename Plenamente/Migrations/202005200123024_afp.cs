namespace Plenamente.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class afp : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EvidenciaAfps",
                c => new
                    {
                        Evidafp_Id = c.Int(nullable: false, identity: true),
                        Evid_Nombre = c.String(),
                        Evid_Archivo = c.String(),
                        Evid_Registro = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Cumpafp_Id = c.Int(nullable: false),
                        Tdca_id = c.Int(nullable: false),
                        Responsable = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Evidafp_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Responsable)
                .ForeignKey("dbo.CumplimientoAfps", t => t.Cumpafp_Id)
                .ForeignKey("dbo.TipoDocCargas", t => t.Tdca_id)
                .Index(t => t.Cumpafp_Id)
                .Index(t => t.Tdca_id)
                .Index(t => t.Responsable);
            
            CreateTable(
                "dbo.CumplimientoAfps",
                c => new
                    {
                        Cumpafp_Id = c.Int(nullable: false, identity: true),
                        Cump_Cumple = c.Boolean(nullable: false),
                        Cump_Nocumple = c.Boolean(nullable: false),
                        Cump_Justifica = c.Boolean(nullable: false),
                        Cump_Nojustifica = c.Boolean(nullable: false),
                        Cump_Observ = c.String(),
                        Iest_Id = c.Int(),
                        Empr_Nit = c.Int(),
                        Auevafp_Id = c.Int(nullable: false),
                        Cump_Registro = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Cump_NoAplica = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Cumpafp_Id)
                .ForeignKey("dbo.AutoEvaluacionAfps", t => t.Auevafp_Id)
                .ForeignKey("dbo.Empresas", t => t.Empr_Nit)
                .ForeignKey("dbo.ItemEstandarAfps", t => t.Iest_Id)
                .Index(t => t.Iest_Id)
                .Index(t => t.Empr_Nit)
                .Index(t => t.Auevafp_Id);
            
            CreateTable(
                "dbo.AutoEvaluacionAfps",
                c => new
                    {
                        Auevafp_Id = c.Int(nullable: false, identity: true),
                        Auev_Nom = c.String(),
                        Auev_Inicio = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Auev_Fin = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Empr_Nit = c.Int(nullable: false),
                        Finalizada = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Auevafp_Id)
                .ForeignKey("dbo.Empresas", t => t.Empr_Nit)
                .Index(t => t.Empr_Nit);
            
            CreateTable(
                "dbo.ItemEstandarAfps",
                c => new
                    {
                        Iest_Id = c.Int(nullable: false, identity: true),
                        Iest_Desc = c.String(),
                        Iest_Verificar = c.String(),
                        Iest_Porcentaje = c.Single(nullable: false),
                        Esta_Id = c.Int(nullable: false),
                        Categoria = c.Short(nullable: false),
                        CategoriaExcepcion = c.Short(nullable: false),
                        Iest_Peri = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Iest_Observa = c.String(),
                        Iest_Registro = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Iest_Video = c.String(),
                        Iest_Recurso = c.String(),
                        Iest_Rescursob = c.String(),
                        Iest_Rescursoc = c.String(),
                        Iest_Rescursod = c.String(),
                        Iest_Rescursoe = c.String(),
                        Iest_Rescursof = c.String(),
                        Iest_MasInfo = c.String(),
                    })
                .PrimaryKey(t => t.Iest_Id)
                .ForeignKey("dbo.EstandarAfps", t => t.Esta_Id)
                .Index(t => t.Esta_Id);
            
            CreateTable(
                "dbo.EstandarAfps",
                c => new
                    {
                        Esta_Id = c.Int(nullable: false, identity: true),
                        Esta_Nom = c.String(),
                        Esta_Porcentaje = c.Single(nullable: false),
                        Crit_Id = c.Int(nullable: false),
                        Esta_Registro = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Categoria = c.Short(nullable: false),
                        CategoriaExcepcion = c.Short(nullable: false),
                    })
                .PrimaryKey(t => t.Esta_Id)
                .ForeignKey("dbo.CriterioAfps", t => t.Crit_Id)
                .Index(t => t.Crit_Id);
            
            CreateTable(
                "dbo.CriterioAfps",
                c => new
                    {
                        Crit_Id = c.Int(nullable: false, identity: true),
                        Crit_Nom = c.String(),
                        Crit_Porcentaje = c.Single(nullable: false),
                        Crit_Registro = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CicloPHVA_Id = c.Int(),
                        Categoria = c.Short(nullable: false),
                        CategoriaExcepcion = c.Short(nullable: false),
                    })
                .PrimaryKey(t => t.Crit_Id)
                .ForeignKey("dbo.CicloPHVAAfps", t => t.CicloPHVA_Id)
                .Index(t => t.CicloPHVA_Id);
            
            CreateTable(
                "dbo.CicloPHVAAfps",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        Description = c.String(),
                        Categoria = c.Short(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            
            
            
           
        }
        
        public override void Down()
        {

            DropTable("dbo.CicloPHVAAfps");
            DropTable("dbo.CriterioAfps");
            DropTable("dbo.EstandarAfps");
            DropTable("dbo.ItemEstandarAfps");
            DropTable("dbo.AutoEvaluacionAfps");
            DropTable("dbo.CumplimientoAfps");
            DropTable("dbo.EvidenciaAfps");
        }
    }
}
