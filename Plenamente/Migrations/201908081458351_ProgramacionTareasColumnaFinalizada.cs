namespace Plenamente.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProgramacionTareasColumnaFinalizada : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProgamacionTareas", "Finalizada", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProgamacionTareas", "Finalizada");
        }
    }
}
