namespace Plenamente.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tablaautoevalaucion_itemestandar : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Autoevaluacion_itemEstandar",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Id_autoevaluacion = c.Int(nullable: false),
                        Id_itemestandar = c.Int(nullable: false),
                        Cumplimiento = c.Boolean(nullable: false),
                        Estado = c.Boolean(nullable: false),
                        Value = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AutoEvaluacions", t => t.Id_autoevaluacion)
                .ForeignKey("dbo.ItemEstandars", t => t.Id_itemestandar)
                .Index(t => t.Id_autoevaluacion)
                .Index(t => t.Id_itemestandar);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Autoevaluacion_itemEstandar", "Id_itemestandar", "dbo.ItemEstandars");
            DropForeignKey("dbo.Autoevaluacion_itemEstandar", "Id_autoevaluacion", "dbo.AutoEvaluacions");
            DropIndex("dbo.Autoevaluacion_itemEstandar", new[] { "Id_itemestandar" });
            DropIndex("dbo.Autoevaluacion_itemEstandar", new[] { "Id_autoevaluacion" });
            DropTable("dbo.Autoevaluacion_itemEstandar");
        }
    }
}
