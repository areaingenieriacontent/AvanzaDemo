namespace Plenamente.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCategoriasToCiclosCriteriosEstandares : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Estandars", "Categoria", c => c.Short(nullable: false));
            AddColumn("dbo.Criterios", "Categoria", c => c.Short(nullable: false));
            AddColumn("dbo.CicloPHVAs", "Categoria", c => c.Short(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CicloPHVAs", "Categoria");
            DropColumn("dbo.Criterios", "Categoria");
            DropColumn("dbo.Estandars", "Categoria");
        }
    }
}
