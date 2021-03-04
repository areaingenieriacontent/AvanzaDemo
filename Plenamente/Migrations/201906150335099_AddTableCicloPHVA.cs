namespace Plenamente.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTableCicloPHVA : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CicloPHVAs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Criterios", "CicloPHVA_Id", c => c.Int());
            CreateIndex("dbo.Criterios", "CicloPHVA_Id");
            AddForeignKey("dbo.Criterios", "CicloPHVA_Id", "dbo.CicloPHVAs", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Criterios", "CicloPHVA_Id", "dbo.CicloPHVAs");
            DropIndex("dbo.Criterios", new[] { "CicloPHVA_Id" });
            DropColumn("dbo.Criterios", "CicloPHVA_Id");
            DropTable("dbo.CicloPHVAs");
        }
    }
}
