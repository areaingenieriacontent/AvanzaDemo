namespace Plenamente.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class itemestandar : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ItemEstandars", "Iest_Norma", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ItemEstandars", "Iest_Norma");
        }
    }
}
