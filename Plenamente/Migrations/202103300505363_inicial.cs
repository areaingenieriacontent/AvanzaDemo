namespace Plenamente.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class inicial : DbMigration
    {
        public override void Up()
        {

            CreateTable(
                "dbo.EvidenciaDecreto1072",
                c => new
                {
                    EviDecreto_Id = c.Int(nullable: false, identity: true),
                    Evid_Nombre = c.String(),
                    Evid_Archivo = c.String(),
                    Evid_Registro = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    CumpDecreto_Id = c.Int(nullable: false),
                    Tdca_id = c.Int(nullable: false),
                    Responsable = c.String(maxLength: 128),
                })
                .PrimaryKey(t => t.EviDecreto_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Responsable)
                .ForeignKey("dbo.CumplimientoDecreto1072", t => t.CumpDecreto_Id)
                .ForeignKey("dbo.TipoDocCargas", t => t.Tdca_id)
                .Index(t => t.CumpDecreto_Id)
                .Index(t => t.Tdca_id)
                .Index(t => t.Responsable);

            CreateTable(
                "dbo.CumplimientoDecreto1072",
                c => new
                {
                    CumpDecreto_Id = c.Int(nullable: false, identity: true),
                    Cump_Cumple = c.Boolean(nullable: false),
                    Cump_Nocumple = c.Boolean(nullable: false),
                    Cump_Justifica = c.Boolean(nullable: false),
                    Cump_Nojustifica = c.Boolean(nullable: false),
                    Cump_Observ = c.String(),
                    IeDecreto_Id = c.Int(),
                    Empr_Nit = c.Int(),
                    AeDecreto_Id = c.Int(nullable: false),
                    Cump_Registro = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    Cump_NoAplica = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.CumpDecreto_Id)
                .ForeignKey("dbo.AutoevaluacionDecreto1072", t => t.AeDecreto_Id)
                .ForeignKey("dbo.Empresas", t => t.Empr_Nit)
                .ForeignKey("dbo.ItemEstandarDecreto1072", t => t.IeDecreto_Id)
                .Index(t => t.IeDecreto_Id)
                .Index(t => t.Empr_Nit)
                .Index(t => t.AeDecreto_Id);

            CreateTable(
                "dbo.AutoevaluacionDecreto1072",
                c => new
                {
                    AeDecreto_Id = c.Int(nullable: false, identity: true),
                    Ae_Nom = c.String(),
                    Ae_Inicio = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    Ae_Fin = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    Empr_Nit = c.Int(nullable: false),
                    Finalizada = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.AeDecreto_Id)
                .ForeignKey("dbo.Empresas", t => t.Empr_Nit)
                .Index(t => t.Empr_Nit);

            CreateTable(
                "dbo.ItemEstandarDecreto1072",
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
                .ForeignKey("dbo.EstandarDecreto1072", t => t.Esta_Id)
                .Index(t => t.Esta_Id);

            CreateTable(
                "dbo.EstandarDecreto1072",
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
                .ForeignKey("dbo.CriterioDecreto1072", t => t.Crit_Id)
                .Index(t => t.Crit_Id);

            CreateTable(
                "dbo.CriterioDecreto1072",
                c => new
                {
                    Crit_Id = c.Int(nullable: false, identity: true),
                    Crit_Nom = c.String(),
                    Crit_Porcentaje = c.Single(nullable: false),
                    Crit_Registro = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    CicloDecreto1072_Id = c.Int(),
                    Categoria = c.Short(nullable: false),
                    CategoriaExcepcion = c.Short(nullable: false),
                })
                .PrimaryKey(t => t.Crit_Id)
                .ForeignKey("dbo.CicloPHVADecreto1072", t => t.CicloDecreto1072_Id)
                .Index(t => t.CicloDecreto1072_Id);

            CreateTable(
                "dbo.CicloPHVADecreto1072",
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
            DropForeignKey("dbo.EvidenciaDecreto1072", "Tdca_id", "dbo.TipoDocCargas");
            DropForeignKey("dbo.EvidenciaDecreto1072", "CumpDecreto_Id", "dbo.CumplimientoDecreto1072");
            DropForeignKey("dbo.CumplimientoDecreto1072", "IeDecreto_Id", "dbo.ItemEstandarDecreto1072");
            DropForeignKey("dbo.ItemEstandarDecreto1072", "Esta_Id", "dbo.EstandarDecreto1072");
            DropForeignKey("dbo.EstandarDecreto1072", "Crit_Id", "dbo.CriterioDecreto1072");
            DropForeignKey("dbo.CriterioDecreto1072", "CicloDecreto1072_Id", "dbo.CicloPHVADecreto1072");
            DropForeignKey("dbo.CumplimientoDecreto1072", "Empr_Nit", "dbo.Empresas");
            DropForeignKey("dbo.CumplimientoDecreto1072", "AeDecreto_Id", "dbo.AutoevaluacionDecreto1072");
            DropForeignKey("dbo.AutoevaluacionDecreto1072", "Empr_Nit", "dbo.Empresas");
            DropForeignKey("dbo.AcumMes", "CumplimientoDecreto1072_CumpDecreto_Id", "dbo.CumplimientoDecreto1072");
            DropForeignKey("dbo.EvidenciaDecreto1072", "Responsable", "dbo.AspNetUsers");

            DropIndex("dbo.CriterioDecreto1072", new[] { "CicloDecreto1072_Id" });
            DropIndex("dbo.EstandarDecreto1072", new[] { "Crit_Id" });
            DropIndex("dbo.ItemEstandarDecreto1072", new[] { "Esta_Id" });
            DropIndex("dbo.AutoevaluacionDecreto1072", new[] { "Empr_Nit" });
            DropIndex("dbo.CumplimientoDecreto1072", new[] { "AeDecreto_Id" });
            DropIndex("dbo.CumplimientoDecreto1072", new[] { "Empr_Nit" });
            DropIndex("dbo.CumplimientoDecreto1072", new[] { "IeDecreto_Id" });
            DropIndex("dbo.EvidenciaDecreto1072", new[] { "Responsable" });
            DropIndex("dbo.EvidenciaDecreto1072", new[] { "Tdca_id" });
            DropIndex("dbo.EvidenciaDecreto1072", new[] { "CumpDecreto_Id" });

            DropTable("dbo.CicloPHVADecreto1072");
            DropTable("dbo.CriterioDecreto1072");
            DropTable("dbo.EstandarDecreto1072");
            DropTable("dbo.ItemEstandarDecreto1072");
            DropTable("dbo.AutoevaluacionDecreto1072");
            DropTable("dbo.CumplimientoDecreto1072");
            DropTable("dbo.EvidenciaDecreto1072");

        }
    }
}
