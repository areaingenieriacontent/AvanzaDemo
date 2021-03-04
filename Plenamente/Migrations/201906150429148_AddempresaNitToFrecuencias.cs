namespace Plenamente.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddempresaNitToFrecuencias : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Frecuencias", "Empr_Nit", c => c.Int());
            CreateIndex("dbo.Frecuencias", "Empr_Nit");
            AddForeignKey("dbo.Frecuencias", "Empr_Nit", "dbo.Empresas", "Empr_Nit");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Frecuencias", "Empr_Nit", "dbo.Empresas");
            DropIndex("dbo.Frecuencias", new[] { "Empr_Nit" });
            DropColumn("dbo.Frecuencias", "Empr_Nit");
        }
    }
}
