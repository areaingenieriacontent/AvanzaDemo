namespace Plenamente.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tablaProgramacionTareas : DbMigration
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
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.ActiCumplimientoes", "ProgamacionTareas_Id", c => c.Int());
            CreateIndex("dbo.ActiCumplimientoes", "ProgamacionTareas_Id");
            AddForeignKey("dbo.ActiCumplimientoes", "ProgamacionTareas_Id", "dbo.ProgamacionTareas", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ActiCumplimientoes", "ProgamacionTareas_Id", "dbo.ProgamacionTareas");
            DropIndex("dbo.ActiCumplimientoes", new[] { "ProgamacionTareas_Id" });
            DropColumn("dbo.ActiCumplimientoes", "ProgamacionTareas_Id");
            DropTable("dbo.ProgamacionTareas");
        }
    }
}
