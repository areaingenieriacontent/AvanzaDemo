namespace Plenamente.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class excepcionescategoria : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ItemEstandars", "CategoriaExcepcion", c => c.Short(nullable: false));
            AddColumn("dbo.Estandars", "CategoriaExcepcion", c => c.Short(nullable: false));
            AddColumn("dbo.Criterios", "CategoriaExcepcion", c => c.Short(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Criterios", "CategoriaExcepcion");
            DropColumn("dbo.Estandars", "CategoriaExcepcion");
            DropColumn("dbo.ItemEstandars", "CategoriaExcepcion");
        }
    }
}
