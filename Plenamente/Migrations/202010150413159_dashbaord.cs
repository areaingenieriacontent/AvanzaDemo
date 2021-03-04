namespace Plenamente.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dashbaord : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DocsEvidencias",
                c => new
                    {
                        Devide_Id = c.Int(nullable: false, identity: true),
                        Devide_Nombre = c.String(),
                        Devide_Archivo = c.String(),
                        File_Registro = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Devide_Descri = c.String(),
                        Empr_Nit = c.Int(nullable: false),
                        Tdca_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Devide_Id)
                .ForeignKey("dbo.Empresas", t => t.Empr_Nit)
                .ForeignKey("dbo.TipoDocCargas", t => t.Tdca_id)
                .Index(t => t.Empr_Nit)
                .Index(t => t.Tdca_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DocsEvidencias", "Tdca_id", "dbo.TipoDocCargas");
            DropForeignKey("dbo.DocsEvidencias", "Empr_Nit", "dbo.Empresas");
            DropIndex("dbo.DocsEvidencias", new[] { "Tdca_id" });
            DropIndex("dbo.DocsEvidencias", new[] { "Empr_Nit" });
            DropTable("dbo.DocsEvidencias");
        }
    }
}
