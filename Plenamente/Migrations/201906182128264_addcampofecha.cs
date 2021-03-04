namespace Plenamente.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcampofecha : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PlandeTrabajoes", "FechaCreacion", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AddColumn("dbo.PlandeTrabajoes", "FechaActualizacion", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PlandeTrabajoes", "FechaActualizacion");
            DropColumn("dbo.PlandeTrabajoes", "FechaCreacion");
        }
    }
}
