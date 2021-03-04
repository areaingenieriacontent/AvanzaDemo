namespace Plenamente.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class actusuarios : DbMigration
    {
        public override void Up()
        {            
            AddColumn("dbo.UsuariosPlandetrabajoes", "Emp_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.UsuariosPlandetrabajoes", "Emp_Id");
            AddForeignKey("dbo.UsuariosPlandetrabajoes", "Emp_Id", "dbo.Empresas", "Empr_Nit");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UsuariosPlandetrabajoes", "Emp_Id", "dbo.Empresas");
            DropIndex("dbo.UsuariosPlandetrabajoes", new[] { "Emp_Id" });
            DropColumn("dbo.UsuariosPlandetrabajoes", "Emp_Id");
        }
    }
}

