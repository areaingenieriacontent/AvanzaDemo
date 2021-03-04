namespace Plenamente.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTableTipoEmpresaAndCategoriasColumns : DbMigration
    {
        public override void Up()
        {
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
            AddColumn("dbo.ItemEstandars", "Categoria", c => c.Short(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ItemEstandars", "Categoria");
            DropColumn("dbo.AutoEvaluacions", "Finalizada");
            DropTable("dbo.TipoEmpresas");
        }
    }
}
