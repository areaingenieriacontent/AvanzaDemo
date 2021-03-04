namespace Plenamente.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumnMasInfoToItemEstandar : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ItemEstandars", "Iest_MasInfo", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ItemEstandars", "Iest_MasInfo");
        }
    }
}
