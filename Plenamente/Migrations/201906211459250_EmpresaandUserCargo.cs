namespace Plenamente.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EmpresaandUserCargo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Empresas", "Empr_NewNit", c => c.Int(nullable: false));
            AddColumn("dbo.Empresas", "Empr_RepresentanteLegal", c => c.String());
            AddColumn("dbo.Empresas", "Empr_CargoRepresentante", c => c.String());
            AddColumn("dbo.Empresas", "Empre_RepresentanteDoc", c => c.Int(nullable: false));
            AddColumn("dbo.Empresas", "Empr_ResponsableSST", c => c.String());
            AddColumn("dbo.Empresas", "Empre_ResponsableDoc", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "Pers_Cargo", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Pers_Cargo");
            DropColumn("dbo.Empresas", "Empre_ResponsableDoc");
            DropColumn("dbo.Empresas", "Empr_ResponsableSST");
            DropColumn("dbo.Empresas", "Empre_RepresentanteDoc");
            DropColumn("dbo.Empresas", "Empr_CargoRepresentante");
            DropColumn("dbo.Empresas", "Empr_RepresentanteLegal");
            DropColumn("dbo.Empresas", "Empr_NewNit");
        }
    }
}
