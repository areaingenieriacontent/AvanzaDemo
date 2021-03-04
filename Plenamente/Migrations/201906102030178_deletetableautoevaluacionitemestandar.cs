namespace Plenamente.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deletetableautoevaluacionitemestandar : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Autoevaluacion_itemEstandar", "Id_autoevaluacion", "dbo.AutoEvaluacions");
            DropForeignKey("dbo.Autoevaluacion_itemEstandar", "Id_itemestandar", "dbo.ItemEstandars");
            DropIndex("dbo.Autoevaluacion_itemEstandar", new[] { "Id_autoevaluacion" });
            DropIndex("dbo.Autoevaluacion_itemEstandar", new[] { "Id_itemestandar" });
            DropTable("dbo.Autoevaluacion_itemEstandar");
        }
        
        public override void Down()
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
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.Autoevaluacion_itemEstandar", "Id_itemestandar");
            CreateIndex("dbo.Autoevaluacion_itemEstandar", "Id_autoevaluacion");
            AddForeignKey("dbo.Autoevaluacion_itemEstandar", "Id_itemestandar", "dbo.ItemEstandars", "Iest_Id");
            AddForeignKey("dbo.Autoevaluacion_itemEstandar", "Id_autoevaluacion", "dbo.AutoEvaluacions", "Auev_Id");
        }
    }
}
