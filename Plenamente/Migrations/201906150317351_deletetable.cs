namespace Plenamente.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deletetable : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.ProgramacionTareas");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ProgramacionTareas",
                c => new
                    {
                        ProgramacionTarea_Id = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(),
                        Fechahora = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Estado = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ProgramacionTarea_Id);
            
        }
    }
}
