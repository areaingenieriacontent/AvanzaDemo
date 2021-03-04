namespace Plenamente.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class inges : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PlandeTrabajoes", "FechaInicio", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AddColumn("dbo.PlandeTrabajoes", "FechaFin", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PlandeTrabajoes", "FechaFin");
            DropColumn("dbo.PlandeTrabajoes", "FechaInicio");
        }
    }
}
