namespace Plenamente.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class recomendacion : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ItemEstandars", "Iest_Recomendacion", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ItemEstandars", "Iest_Recomendacion");
        }
    }
}
