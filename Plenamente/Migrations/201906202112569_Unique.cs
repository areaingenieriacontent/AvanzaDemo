namespace Plenamente.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Unique : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.AspNetUsers", "Pers_Doc", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.AspNetUsers", new[] { "Pers_Doc" });
        }
    }
}
