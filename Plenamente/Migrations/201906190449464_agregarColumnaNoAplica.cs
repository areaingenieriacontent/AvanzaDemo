namespace Plenamente.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class agregarColumnaNoAplica : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cumplimientoes", "Cump_NoAplica", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Cumplimientoes", "Cump_NoAplica");
        }
    }
}
