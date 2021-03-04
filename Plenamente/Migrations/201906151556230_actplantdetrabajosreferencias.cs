namespace Plenamente.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class actplantdetrabajosreferencias : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProgamacionTareas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(),
                        FechaHora = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Estado = c.Boolean(nullable: false),
                        ActiCumplimiento_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ActiCumplimientoes", t => t.ActiCumplimiento_Id)
                .Index(t => t.ActiCumplimiento_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProgamacionTareas", "ActiCumplimiento_Id", "dbo.ActiCumplimientoes");
            DropIndex("dbo.ProgamacionTareas", new[] { "ActiCumplimiento_Id" });
            DropTable("dbo.ProgamacionTareas");
        }
    }
}
