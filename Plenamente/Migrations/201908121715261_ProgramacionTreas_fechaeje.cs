namespace Plenamente.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProgramacionTreas_fechaeje : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProgamacionTareas", "Fechaeje", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProgamacionTareas", "Fechaeje");
        }
    }
}
