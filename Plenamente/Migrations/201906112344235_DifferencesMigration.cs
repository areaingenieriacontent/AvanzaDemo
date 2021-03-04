namespace Plenamente.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DifferencesMigration : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Evidencias", name: "Id", newName: "Responsable");
            RenameIndex(table: "dbo.Evidencias", name: "IX_Id", newName: "IX_Responsable");
            CreateTable(
                "dbo.TipoEmpresas",
                c => new
                    {
                        Id = c.Short(nullable: false, identity: true),
                        Decripcion = c.String(maxLength: 250),
                        RangoMinimoTrabajadores = c.Short(nullable: false),
                        RangoMaximoTrabajadores = c.Short(nullable: false),
                        NivelesRiesgo = c.String(maxLength: 250),
                        Categoria = c.Short(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.AutoEvaluacions", "Finalizada", c => c.Boolean(nullable: false));
            AddColumn("dbo.Empresas", "Empr_telefono", c => c.String());
            AddColumn("dbo.ItemEstandars", "Categoria", c => c.Short(nullable: false));
            DropColumn("dbo.Resultadoes", "Resu_Justificacion");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Resultadoes", "Resu_Justificacion", c => c.String());
            DropColumn("dbo.ItemEstandars", "Categoria");
            DropColumn("dbo.Empresas", "Empr_telefono");
            DropColumn("dbo.AutoEvaluacions", "Finalizada");
            DropTable("dbo.TipoEmpresas");
            RenameIndex(table: "dbo.Evidencias", name: "IX_Responsable", newName: "IX_Id");
            RenameColumn(table: "dbo.Evidencias", name: "Responsable", newName: "Id");
        }
    }
}
