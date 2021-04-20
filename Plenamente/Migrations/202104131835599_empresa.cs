namespace Plenamente.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class empresa : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Empresas", "Sucursales", c => c.Int(nullable: false));
            AddColumn("dbo.Empresas", "Sector_Economico", c => c.String());
            AddColumn("dbo.Empresas", "Indice_Siniestralidad", c => c.Single(nullable: false));
            AddColumn("dbo.Empresas", "Prima_Cotizacion", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Empresas", "Prima_Cotizacion");
            DropColumn("dbo.Empresas", "Indice_Siniestralidad");
            DropColumn("dbo.Empresas", "Sector_Economico");
            DropColumn("dbo.Empresas", "Sucursales");
        }
    }
}
