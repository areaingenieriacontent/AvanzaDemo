namespace Plenamente.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcoltelefonoempresamodidevidencia : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Evidencias", name: "Id", newName: "Responsable");
            RenameIndex(table: "dbo.Evidencias", name: "IX_Id", newName: "IX_Responsable");
            AddColumn("dbo.Empresas", "Empr_telefono", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Empresas", "Empr_telefono");
            RenameIndex(table: "dbo.Evidencias", name: "IX_Responsable", newName: "IX_Id");
            RenameColumn(table: "dbo.Evidencias", name: "Responsable", newName: "Id");
        }
    }
}
