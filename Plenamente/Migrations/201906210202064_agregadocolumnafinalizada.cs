namespace Plenamente.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class agregadocolumnafinalizada : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ActiCumplimientoes", "Finalizada", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ActiCumplimientoes", "Finalizada");
        }
    }
}
