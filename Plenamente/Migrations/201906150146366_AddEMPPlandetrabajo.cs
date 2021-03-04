namespace Plenamente.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEMPPlandetrabajo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Usersplandetrabajoes", "Emp_Id", c => c.Int(nullable: false));
            AddColumn("dbo.PlandeTrabajoes", "Emp_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Usersplandetrabajoes", "Emp_Id");
            CreateIndex("dbo.PlandeTrabajoes", "Emp_Id");
            AddForeignKey("dbo.Usersplandetrabajoes", "Emp_Id", "dbo.Empresas", "Empr_Nit");
            AddForeignKey("dbo.PlandeTrabajoes", "Emp_Id", "dbo.Empresas", "Empr_Nit");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PlandeTrabajoes", "Emp_Id", "dbo.Empresas");
            DropForeignKey("dbo.Usersplandetrabajoes", "Emp_Id", "dbo.Empresas");
            DropIndex("dbo.PlandeTrabajoes", new[] { "Emp_Id" });
            DropIndex("dbo.Usersplandetrabajoes", new[] { "Emp_Id" });
            DropColumn("dbo.PlandeTrabajoes", "Emp_Id");
            DropColumn("dbo.Usersplandetrabajoes", "Emp_Id");
        }
    }
}
