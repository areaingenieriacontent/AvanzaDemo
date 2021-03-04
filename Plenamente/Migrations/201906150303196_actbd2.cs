namespace Plenamente.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class actbd2 : DbMigration
    {
        public override void Up()
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
        
        public override void Down()
        {
            DropTable("dbo.ProgramacionTareas");
        }
    }
}
