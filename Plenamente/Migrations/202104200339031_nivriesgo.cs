namespace Plenamente.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nivriesgo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Empresas", "Empr_NivRies", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Empresas", "Empr_NivRies");
        }
    }
}
